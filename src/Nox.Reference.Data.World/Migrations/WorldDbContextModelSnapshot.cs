﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nox.Reference.Data.World;

#nullable disable

namespace Nox.Reference.Data.World.Migrations
{
    [DbContext(typeof(WorldDbContext))]
    partial class WorldDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Nox.Reference.Data.World.CountryHoliday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("CountryName")
                        .HasColumnType("TEXT");

                    b.Property<string>("DayOff")
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CountryHoliday");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BanknotesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CoinsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DecimalDigits")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DecimalSeparator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IsoCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IsoNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MajorUnitId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MinorUnitId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("SpaceBetweenAmountAndSymbol")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("SymbolOnLeft")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ThousandsSeparator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BanknotesId");

                    b.HasIndex("CoinsId");

                    b.HasIndex("MajorUnitId");

                    b.HasIndex("MinorUnitId");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.CurrencyFrequentUsage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CurrencyUsageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyUsageId");

                    b.ToTable("CurrencyFrequentUsage");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.CurrencyRareUsage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CurrencyUsageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyUsageId");

                    b.ToTable("CurrencyRareUsage");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.CurrencyUsage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CurrencyUsage");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.Entities.Cultures.Culture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CharacterOrientation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CommonName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayNameWithDialect")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FormalName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LanguageIso_639_2t")
                        .HasColumnType("TEXT");

                    b.Property<string>("LineOrientation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NativeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Culture");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.Entities.Cultures.DateFormat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AmPmStrings")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("CultureId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date_0")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date_1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date_2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date_3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EraNames")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Eras")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Months")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortMonths")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortWeekdays")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Weekdays")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CultureId")
                        .IsUnique();

                    b.ToTable("DateFormat");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.Entities.Cultures.NumberFormat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CultureId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CurrencySymbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DecimalSeparator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Digit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ExponentSeparator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GroupingSeparator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Infinity")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("InternationalCurrencySymbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MinusSign")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MonetaryDecimalSeparator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NotANumberSymbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PadEscape")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PatternSeparator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PerMill")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Percent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PlusSign")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SignificantDigit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZeroDigit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CultureId")
                        .IsUnique();

                    b.ToTable("NumberFormat");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.HolidayData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CountryHolidayId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RegionHolidayId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StateHolidayId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CountryHolidayId");

                    b.HasIndex("RegionHolidayId");

                    b.HasIndex("StateHolidayId");

                    b.ToTable("HolidayData");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Common")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Iso_639_1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Iso_639_2b")
                        .HasColumnType("TEXT");

                    b.Property<string>("Iso_639_2t")
                        .HasColumnType("TEXT");

                    b.Property<string>("Iso_639_3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Scope")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("WikiUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.LanguageTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("LanguageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Translation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("LanguageTranslation");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.LocalHolidayName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("HolidayDataId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HolidayDataId");

                    b.ToTable("LocalHolidayName");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.MajorCurrencyUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MajorCurrencyUnit");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.MinorCurrencyUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("MajorValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MinorCurrencyUnit");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.RegionHoliday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("StateHolidayId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StateHolidayId");

                    b.ToTable("RegionHoliday");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.StateHoliday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CountryHolidayId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CountryHolidayId");

                    b.ToTable("StateHoliday");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.VatNumberDefinition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LocalName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("VerificationApi")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("VatNumberDefinition");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.VatNumberValidationRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("InputMask")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MaximumLength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MinimumLength")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Regex")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TranslationId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ValidationFormatDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("VatNumberDefinitionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VatNumberDefinitionId");

                    b.ToTable("VatNumberValidationRule");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.Currency", b =>
                {
                    b.HasOne("Nox.Reference.Data.World.CurrencyUsage", "Banknotes")
                        .WithMany()
                        .HasForeignKey("BanknotesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nox.Reference.Data.World.CurrencyUsage", "Coins")
                        .WithMany()
                        .HasForeignKey("CoinsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nox.Reference.Data.World.MajorCurrencyUnit", "MajorUnit")
                        .WithMany()
                        .HasForeignKey("MajorUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nox.Reference.Data.World.MinorCurrencyUnit", "MinorUnit")
                        .WithMany()
                        .HasForeignKey("MinorUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Banknotes");

                    b.Navigation("Coins");

                    b.Navigation("MajorUnit");

                    b.Navigation("MinorUnit");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.CurrencyFrequentUsage", b =>
                {
                    b.HasOne("Nox.Reference.Data.World.CurrencyUsage", null)
                        .WithMany("Frequent")
                        .HasForeignKey("CurrencyUsageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nox.Reference.Data.World.CurrencyRareUsage", b =>
                {
                    b.HasOne("Nox.Reference.Data.World.CurrencyUsage", null)
                        .WithMany("Rare")
                        .HasForeignKey("CurrencyUsageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Nox.Reference.Data.World.Entities.Cultures.DateFormat", b =>
                {
                    b.HasOne("Nox.Reference.Data.World.Entities.Cultures.Culture", "Culture")
                        .WithOne("DateFormat")
                        .HasForeignKey("Nox.Reference.Data.World.Entities.Cultures.DateFormat", "CultureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Culture");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.Entities.Cultures.NumberFormat", b =>
                {
                    b.HasOne("Nox.Reference.Data.World.Entities.Cultures.Culture", "Culture")
                        .WithOne("NumberFormat")
                        .HasForeignKey("Nox.Reference.Data.World.Entities.Cultures.NumberFormat", "CultureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Culture");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.HolidayData", b =>
                {
                    b.HasOne("Nox.Reference.Data.World.CountryHoliday", null)
                        .WithMany("Holidays")
                        .HasForeignKey("CountryHolidayId");

                    b.HasOne("Nox.Reference.Data.World.RegionHoliday", null)
                        .WithMany("Holidays")
                        .HasForeignKey("RegionHolidayId");

                    b.HasOne("Nox.Reference.Data.World.StateHoliday", null)
                        .WithMany("Holidays")
                        .HasForeignKey("StateHolidayId");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.LanguageTranslation", b =>
                {
                    b.HasOne("Nox.Reference.Data.World.Language", null)
                        .WithMany("NameTranslations")
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.LocalHolidayName", b =>
                {
                    b.HasOne("Nox.Reference.Data.World.HolidayData", null)
                        .WithMany("LocalNames")
                        .HasForeignKey("HolidayDataId");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.RegionHoliday", b =>
                {
                    b.HasOne("Nox.Reference.Data.World.StateHoliday", null)
                        .WithMany("Regions")
                        .HasForeignKey("StateHolidayId");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.StateHoliday", b =>
                {
                    b.HasOne("Nox.Reference.Data.World.CountryHoliday", null)
                        .WithMany("States")
                        .HasForeignKey("CountryHolidayId");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.VatNumberValidationRule", b =>
                {
                    b.HasOne("Nox.Reference.Data.World.VatNumberDefinition", null)
                        .WithMany("ValidationRules")
                        .HasForeignKey("VatNumberDefinitionId");

                    b.OwnsOne("Nox.Reference.Data.World.Checksum", "Checksum", b1 =>
                        {
                            b1.Property<int>("VatNumberValidationRuleId")
                                .HasColumnType("INTEGER");

                            b1.Property<int?>("Algorithm")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("ChecksumDigit")
                                .HasColumnType("TEXT");

                            b1.Property<int?>("Modulus")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Weights")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("VatNumberValidationRuleId");

                            b1.ToTable("VatNumberValidationRule");

                            b1.WithOwner()
                                .HasForeignKey("VatNumberValidationRuleId");
                        });

                    b.Navigation("Checksum");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.CountryHoliday", b =>
                {
                    b.Navigation("Holidays");

                    b.Navigation("States");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.CurrencyUsage", b =>
                {
                    b.Navigation("Frequent");

                    b.Navigation("Rare");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.Entities.Cultures.Culture", b =>
                {
                    b.Navigation("DateFormat")
                        .IsRequired();

                    b.Navigation("NumberFormat")
                        .IsRequired();
                });

            modelBuilder.Entity("Nox.Reference.Data.World.HolidayData", b =>
                {
                    b.Navigation("LocalNames");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.Language", b =>
                {
                    b.Navigation("NameTranslations");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.RegionHoliday", b =>
                {
                    b.Navigation("Holidays");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.StateHoliday", b =>
                {
                    b.Navigation("Holidays");

                    b.Navigation("Regions");
                });

            modelBuilder.Entity("Nox.Reference.Data.World.VatNumberDefinition", b =>
                {
                    b.Navigation("ValidationRules");
                });
#pragma warning restore 612, 618
        }
    }
}
