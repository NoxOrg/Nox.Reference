﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Nox.Reference.Repository.Migrations
{
    [DbContext(typeof(NoxReferencesContext))]
    [Migration("20230406161251_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Continent", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlaceType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Continent", (string)null);
                });

            modelBuilder.Entity("Country", b =>
                {
                    b.Property<short>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AlphaCode2")
                        .IsRequired()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("TEXT")
                        .IsFixedLength();

                    b.Property<string>("AlphaCode3")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("TEXT")
                        .IsFixedLength();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<string>("CodeAssignedStatus")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<string>("CommonName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("EmojiFlag")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("FifaCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("TEXT")
                        .IsFixedLength();

                    b.Property<string>("FipsCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("TEXT")
                        .IsFixedLength();

                    b.Property<bool>("IsIndependent")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLandLockec")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsUnitedNationsMember")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LandAreaInSquareKilometers")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<short>("NumericCode")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OfficialName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("OlympicCommitteeCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("TEXT")
                        .IsFixedLength();

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("SubRegion")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("CountryAlternateSpelling", b =>
                {
                    b.Property<short>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<short>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Spelling")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("CountryAlternateSpellings");
                });

            modelBuilder.Entity("CountryBorderingCountry", b =>
                {
                    b.Property<short>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("BorderingCountryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CountryId", "BorderingCountryId");

                    b.HasIndex("BorderingCountryId");

                    b.ToTable("CountryBorderingCountries", (string)null);
                });

            modelBuilder.Entity("CountryCapital", b =>
                {
                    b.Property<short>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GeoPlaceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CountryId", "GeoPlaceId");

                    b.HasIndex("GeoPlaceId");

                    b.ToTable("CountryCapitals", (string)null);
                });

            modelBuilder.Entity("CountryContinent", b =>
                {
                    b.Property<short>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("ContinentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CountryId", "ContinentId");

                    b.HasIndex("ContinentId");

                    b.ToTable("CountryContinents", (string)null);
                });

            modelBuilder.Entity("CountryCurrency", b =>
                {
                    b.Property<short>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("CurrencyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CountryId", "CurrencyId");

                    b.HasIndex("CurrencyId");

                    b.ToTable("CountryCurrencies", (string)null);
                });

            modelBuilder.Entity("CountryDemonym", b =>
                {
                    b.Property<short>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("DemonymId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CountryId", "DemonymId");

                    b.HasIndex("DemonymId");

                    b.ToTable("CountryDemonyms", (string)null);
                });

            modelBuilder.Entity("CountryDomainNameExtension", b =>
                {
                    b.Property<short>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DomainNameExtensionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CountryId", "DomainNameExtensionId");

                    b.HasIndex("DomainNameExtensionId");

                    b.ToTable("CountryDomainNameExtensions", (string)null);
                });

            modelBuilder.Entity("CountryLanguage", b =>
                {
                    b.Property<short>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("LanguageId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CountryId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("CountryLanguages", (string)null);
                });

            modelBuilder.Entity("CountryNameTranslation", b =>
                {
                    b.Property<short>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("LanguageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CommonName")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("OfficialName")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("CountryId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("CountryNameTranslations");
                });

            modelBuilder.Entity("CountryNativeName", b =>
                {
                    b.Property<short>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("LanguageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CommonName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("OfficialName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("CountryId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("CountryNativeNames");
                });

            modelBuilder.Entity("Currency", b =>
                {
                    b.Property<short>("Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Currency", (string)null);
                });

            modelBuilder.Entity("Demonym", b =>
                {
                    b.Property<short>("Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Demonym", (string)null);
                });

            modelBuilder.Entity("DemonymTranslation", b =>
                {
                    b.Property<short>("DemonymId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("LanguageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Feminine")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Masculine")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("DemonymId", "LanguageId")
                        .HasName("PK_DemonymTranslation");

                    b.HasIndex("LanguageId");

                    b.ToTable("DemonymTranslations");
                });

            modelBuilder.Entity("DialingInfo", b =>
                {
                    b.Property<short>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Prefix")
                        .HasMaxLength(7)
                        .IsUnicode(false)
                        .HasColumnType("TEXT")
                        .IsFixedLength();

                    b.Property<short>("Suffix")
                        .HasColumnType("INTEGER");

                    b.HasKey("CountryId", "Prefix", "Suffix");

                    b.ToTable("DialingInfo", (string)null);
                });

            modelBuilder.Entity("DomainNameExtension", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DomainNameExtension", (string)null);
                });

            modelBuilder.Entity("GeoPlace", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("decimal(8, 6)");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("PlaceTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlaceTypeId");

                    b.ToTable("GeoPlace", (string)null);
                });

            modelBuilder.Entity("GeoPlaceType", b =>
                {
                    b.Property<short>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlaceType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GeoPlaceType", (string)null);
                });

            modelBuilder.Entity("Language", b =>
                {
                    b.Property<short>("Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Language", (string)null);
                });

            modelBuilder.Entity("CountryAlternateSpelling", b =>
                {
                    b.HasOne("Country", "Country")
                        .WithMany("CountryAlternateSpellings")
                        .HasForeignKey("CountryId")
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("CountryBorderingCountry", b =>
                {
                    b.HasOne("Country", null)
                        .WithMany()
                        .HasForeignKey("BorderingCountryId")
                        .IsRequired()
                        .HasConstraintName("FK_CountryBorderingCountries_Country_CountryId2");

                    b.HasOne("Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .IsRequired();
                });

            modelBuilder.Entity("CountryCapital", b =>
                {
                    b.HasOne("Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .IsRequired();

                    b.HasOne("GeoPlace", null)
                        .WithMany()
                        .HasForeignKey("GeoPlaceId")
                        .IsRequired();
                });

            modelBuilder.Entity("CountryContinent", b =>
                {
                    b.HasOne("Continent", null)
                        .WithMany()
                        .HasForeignKey("ContinentId")
                        .IsRequired();

                    b.HasOne("Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .IsRequired();
                });

            modelBuilder.Entity("CountryCurrency", b =>
                {
                    b.HasOne("Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .IsRequired();

                    b.HasOne("Currency", null)
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .IsRequired();
                });

            modelBuilder.Entity("CountryDemonym", b =>
                {
                    b.HasOne("Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .IsRequired();

                    b.HasOne("Demonym", null)
                        .WithMany()
                        .HasForeignKey("DemonymId")
                        .IsRequired();
                });

            modelBuilder.Entity("CountryDomainNameExtension", b =>
                {
                    b.HasOne("Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .IsRequired();

                    b.HasOne("DomainNameExtension", null)
                        .WithMany()
                        .HasForeignKey("DomainNameExtensionId")
                        .IsRequired();
                });

            modelBuilder.Entity("CountryLanguage", b =>
                {
                    b.HasOne("Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .IsRequired();

                    b.HasOne("Language", null)
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .IsRequired();
                });

            modelBuilder.Entity("CountryNameTranslation", b =>
                {
                    b.HasOne("Country", "Country")
                        .WithMany("CountryNameTranslations")
                        .HasForeignKey("CountryId")
                        .IsRequired();

                    b.HasOne("Language", "Language")
                        .WithMany("CountryNameTranslations")
                        .HasForeignKey("LanguageId")
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("CountryNativeName", b =>
                {
                    b.HasOne("Country", "Country")
                        .WithMany("CountryNativeNames")
                        .HasForeignKey("CountryId")
                        .IsRequired();

                    b.HasOne("Language", "Language")
                        .WithMany("CountryNativeNames")
                        .HasForeignKey("LanguageId")
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("DemonymTranslation", b =>
                {
                    b.HasOne("Demonym", "Demonym")
                        .WithMany("DemonymTranslations")
                        .HasForeignKey("DemonymId")
                        .IsRequired();

                    b.HasOne("Language", "Language")
                        .WithMany("DemonymTranslations")
                        .HasForeignKey("LanguageId")
                        .IsRequired();

                    b.Navigation("Demonym");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("DialingInfo", b =>
                {
                    b.HasOne("Country", "Country")
                        .WithMany("DialingInfos")
                        .HasForeignKey("CountryId")
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("GeoPlace", b =>
                {
                    b.HasOne("GeoPlaceType", "PlaceType")
                        .WithMany("GeoPlaces")
                        .HasForeignKey("PlaceTypeId")
                        .IsRequired();

                    b.Navigation("PlaceType");
                });

            modelBuilder.Entity("Country", b =>
                {
                    b.Navigation("CountryAlternateSpellings");

                    b.Navigation("CountryNameTranslations");

                    b.Navigation("CountryNativeNames");

                    b.Navigation("DialingInfos");
                });

            modelBuilder.Entity("Demonym", b =>
                {
                    b.Navigation("DemonymTranslations");
                });

            modelBuilder.Entity("GeoPlaceType", b =>
                {
                    b.Navigation("GeoPlaces");
                });

            modelBuilder.Entity("Language", b =>
                {
                    b.Navigation("CountryNameTranslations");

                    b.Navigation("CountryNativeNames");

                    b.Navigation("DemonymTranslations");
                });
#pragma warning restore 612, 618
        }
    }
}
