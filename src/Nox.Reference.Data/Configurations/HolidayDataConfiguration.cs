using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext.Configurations;

internal class HolidayDataConfiguration : NoxReferenceEntityConfigurationBase<HolidayData>
{
    protected override void DoConfigure(EntityTypeBuilder<HolidayData> builder)
    {
    }
}