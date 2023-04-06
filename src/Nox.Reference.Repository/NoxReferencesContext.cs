using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


public partial class NoxReferencesContext : DbContext
{
    public string DbPath { get; }

    public NoxReferencesContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "noxreferences.db");
    
    }

    // public NoxReferencesContext(DbContextOptions<NoxReferencesContext> options)
    //     : base(options)
    // {
    // }

    public virtual DbSet<Continent> Continents { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CountryAlternateSpelling> CountryAlternateSpellings { get; set; }

    public virtual DbSet<CountryNameTranslation> CountryNameTranslations { get; set; }

    public virtual DbSet<CountryNativeName> CountryNativeNames { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Demonym> Demonyms { get; set; }

    public virtual DbSet<DemonymTranslation> DemonymTranslations { get; set; }

    public virtual DbSet<DialingInfo> DialingInfos { get; set; }

    public virtual DbSet<DomainNameExtension> DomainNameExtensions { get; set; }

    public virtual DbSet<GeoPlace> GeoPlaces { get; set; }

    public virtual DbSet<GeoPlaceType> GeoPlaceTypes { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Continent>(entity =>
        {
            entity.ToTable("Continent");

            entity.Property(e => e.PlaceType)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AlphaCode2)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AlphaCode3)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CodeAssignedStatus)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CommonName).HasMaxLength(255);
            entity.Property(e => e.EmojiFlag).HasMaxLength(255);
            entity.Property(e => e.FifaCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FipsCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.OfficialName).HasMaxLength(255);
            entity.Property(e => e.OlympicCommitteeCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Region).HasMaxLength(255);
            entity.Property(e => e.SubRegion).HasMaxLength(255);

            entity.HasMany(d => d.BorderingCountries).WithMany(p => p.Countries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryBorderingCountry",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("BorderingCountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CountryBorderingCountries_Country_CountryId2"),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CountryId", "BorderingCountryId");
                        j.ToTable("CountryBorderingCountries");
                    });

            entity.HasMany(d => d.Continents).WithMany(p => p.Countries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryContinent",
                    r => r.HasOne<Continent>().WithMany()
                        .HasForeignKey("ContinentId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CountryId", "ContinentId");
                        j.ToTable("CountryContinents");
                    });

            entity.HasMany(d => d.Countries).WithMany(p => p.BorderingCountries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryBorderingCountry",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("BorderingCountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CountryBorderingCountries_Country_CountryId2"),
                    j =>
                    {
                        j.HasKey("CountryId", "BorderingCountryId");
                        j.ToTable("CountryBorderingCountries");
                    });

            entity.HasMany(d => d.Currencies).WithMany(p => p.Countries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryCurrency",
                    r => r.HasOne<Currency>().WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CountryId", "CurrencyId");
                        j.ToTable("CountryCurrencies");
                    });

            entity.HasMany(d => d.Demonyms).WithMany(p => p.Countries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryDemonym",
                    r => r.HasOne<Demonym>().WithMany()
                        .HasForeignKey("DemonymId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CountryId", "DemonymId");
                        j.ToTable("CountryDemonyms");
                    });

            entity.HasMany(d => d.DomainNameExtensions).WithMany(p => p.Countries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryDomainNameExtension",
                    r => r.HasOne<DomainNameExtension>().WithMany()
                        .HasForeignKey("DomainNameExtensionId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CountryId", "DomainNameExtensionId");
                        j.ToTable("CountryDomainNameExtensions");
                    });

            entity.HasMany(d => d.GeoPlaces).WithMany(p => p.Countries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryCapital",
                    r => r.HasOne<GeoPlace>().WithMany()
                        .HasForeignKey("GeoPlaceId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CountryId", "GeoPlaceId");
                        j.ToTable("CountryCapitals");
                    });

            entity.HasMany(d => d.Languages).WithMany(p => p.Countries)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryLanguage",
                    r => r.HasOne<Language>().WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CountryId", "LanguageId");
                        j.ToTable("CountryLanguages");
                    });
        });

        modelBuilder.Entity<CountryAlternateSpelling>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Spelling).HasMaxLength(255);

            entity.HasOne(d => d.Country).WithMany(p => p.CountryAlternateSpellings)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CountryNameTranslation>(entity =>
        {
            entity.HasKey(e => new { e.CountryId, e.LanguageId });

            entity.Property(e => e.CommonName).HasMaxLength(255);
            entity.Property(e => e.OfficialName).HasMaxLength(255);

            entity.HasOne(d => d.Country).WithMany(p => p.CountryNameTranslations)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Language).WithMany(p => p.CountryNameTranslations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CountryNativeName>(entity =>
        {
            entity.HasKey(e => new { e.CountryId, e.LanguageId });

            entity.Property(e => e.CommonName).HasMaxLength(255);
            entity.Property(e => e.OfficialName).HasMaxLength(255);

            entity.HasOne(d => d.Country).WithMany(p => p.CountryNativeNames)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Language).WithMany(p => p.CountryNativeNames)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("Currency");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Demonym>(entity =>
        {
            entity.ToTable("Demonym");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<DemonymTranslation>(entity =>
        {
            entity.HasKey(e => new { e.DemonymId, e.LanguageId }).HasName("PK_DemonymTranslation");

            entity.Property(e => e.Feminine).HasMaxLength(255);
            entity.Property(e => e.Masculine).HasMaxLength(255);

            entity.HasOne(d => d.Demonym).WithMany(p => p.DemonymTranslations)
                .HasForeignKey(d => d.DemonymId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Language).WithMany(p => p.DemonymTranslations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DialingInfo>(entity =>
        {
            entity.HasKey(e => new { e.CountryId, e.Prefix, e.Suffix });

            entity.ToTable("DialingInfo");

            entity.Property(e => e.Prefix)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Country).WithMany(p => p.DialingInfos)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DomainNameExtension>(entity =>
        {
            entity.ToTable("DomainNameExtension");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Extension)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GeoPlace>(entity =>
        {
            entity.ToTable("GeoPlace");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Latitude).HasColumnType("decimal(8, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Name).HasMaxLength(500);

            entity.HasOne(d => d.PlaceType).WithMany(p => p.GeoPlaces)
                .HasForeignKey(d => d.PlaceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<GeoPlaceType>(entity =>
        {
            entity.ToTable("GeoPlaceType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PlaceType)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Language");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
