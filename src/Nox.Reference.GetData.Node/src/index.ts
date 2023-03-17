import { Command } from "commander";
import Holidays, { HolidaysTypes } from 'date-holidays';
import { DateTime } from "luxon";
import ct from 'countries-and-timezones';
import fs from 'fs';

const program = new Command();

program
    .version("1.0.0")
    .description("Calculates and exports global holidays and to a json file")
    .option("-y, --year <value>","The year for which you want holidays for")
    .option("-f, --file <value>","The jJSON file to output the holidays to")
    .parse(process.argv);

const options = program.opts();

const year = options.year ? parseInt(options.year) : 2023;
const file = options.file ?? "holidays.json";

console.log(`Calculating public holidays for ${year} to ${file}`);

declare global {
    interface String {
        toTitleCase() : string
    }
}

String.prototype.toTitleCase = function (this: string) {
    return this.charAt(0).toUpperCase() + this.substring(1).toLowerCase();
}


class YearInfo {
    public year: number;
    public countries: CountryHolidayInfo[] = [];
    constructor(year:number){
        this.year = year;
    }
}

class CountryHolidayInfo {
    public country: string; 
    public countryName: string;
    public dayOff: string; 
    public timeZones: string[]; 
    public holidays: HolidayInfo[];
    public states: StateHolidayInfo[];
    constructor(countryCode:string, countryName:string){
        this.country = countryCode;
        this.countryName = countryName;
        this.dayOff = "unknown";
        this.timeZones = ct.getCountry(countryCode)?.timezones ?? ["Europe/London"]
        this.holidays = [];
        this.states = [];
    }
}

class StateHolidayInfo {
    public state: string; 
    public stateName: string;
    public holidays: HolidayInfo[];
    public regions: RegionHolidayInfo[];
    constructor(stateCode:string, stateName:string){
        this.state = stateCode;
        this.stateName = stateName;
        this.holidays = [];
        this.regions = [];
    }
}

class RegionHolidayInfo {
    public region: string; 
    public regionName: string;
    public holidays: HolidayInfo[];
    constructor(regionCode:string, regionName:string){
        this.region = regionCode;
        this.regionName = regionName;
        this.holidays = [];
    }
}

class LocalHolidayName{
    public language: string;
    public name: string;
    constructor(languageCode:string, name:string){
        this.language = languageCode;
        this.name = name;
    }
}

class HolidayInfo {
    public name: string; 
    public localNames: LocalHolidayName[];
    public type: string; 
    public date: string;
    constructor(holiday:HolidaysTypes.Holiday, localNames: LocalHolidayName[]){
        const date = DateTime.fromFormat(holiday.date,'yyyy-MM-dd hh:mm:ss'); 
        this.name = holiday.name;
        this.type = holiday.type;
        this.date = date.toISODate();
        this.localNames = localNames; 
    }
}

function getNamesArray(holidays:HolidaysTypes.Holiday[], languages:string[]) : LocalHolidayName[][]{
    const names = Array.from(Array(holidays.length), () => new Array<LocalHolidayName>() );
    languages.forEach(lang => {
        const holidaysLang = hd.getHolidays(year,lang);
        holidaysLang.forEach((h,i) => {
            if(i < names.length){
                names[i].push(new LocalHolidayName(lang, h.name));           
            }
        });
    });
    return names;
}


const hd = new Holidays();

const getCountries = hd.getCountries('en');

const yearInfo = new YearInfo(year);

Object.entries(getCountries).map(([countryCode,countryName]) => {
    
    const countryHolidayInfo = new CountryHolidayInfo(countryCode,countryName);
    
    yearInfo.countries.push(countryHolidayInfo);

    hd.init(countryCode);

    const holidays = hd.getHolidays(year,'en');
    const languages = hd.getLanguages().filter( l => !l.startsWith('en') );
    countryHolidayInfo.dayOff = (hd.getDayOff() ?? "<unknown>").trimStart().toTitleCase();

    if(holidays) {
        const names = getNamesArray(holidays, languages);
        holidays.forEach( (h,i) => {
            countryHolidayInfo.holidays.push(new HolidayInfo(h,names[i]));
        });
    }

    const states = hd.getStates(countryCode, 'en');
    if(states) {
        Object.entries(states).map(([stateCode,stateName]) => {

            const stateHolidayInfo = new StateHolidayInfo(stateCode,stateName);
            countryHolidayInfo.states.push(stateHolidayInfo);

            hd.init(countryCode, stateCode);

            const holidays = hd.getHolidays(year,'en');

            if(holidays) {
                const names = getNamesArray(holidays, languages);
                holidays.forEach( (h,i) => {
                    stateHolidayInfo.holidays.push(new HolidayInfo(h,names[i]));
                });
            }

            const regions = hd.getRegions(countryCode,stateCode,'en');
    
            if(regions) {
                Object.entries(states).map(([regionCode,regionName]) => {
    
                    const regionHolidayInfo = new RegionHolidayInfo(regionCode,regionName);
                    stateHolidayInfo.regions.push(regionHolidayInfo);

                    hd.init(countryCode, stateCode, regionCode);
                    const holidays = hd.getHolidays(year,'en');

                    if(holidays) {

                        const names = getNamesArray(holidays, languages);

                        holidays.forEach( (h,i) => {
                            regionHolidayInfo.holidays.push(new HolidayInfo(h,names[i]));
                        });
                    }
                    });
            }
        });
    };
});

let data = JSON.stringify(yearInfo,undefined,3);

fs.writeFile(file, data, err => {
    if (err) {
      console.error(err);
      return;
    }
});

console.log("Done.");