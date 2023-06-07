using Nox.Reference.Common;

namespace Nox.Reference.Data.Common;

public static class MapperExtensions
{
    public static TDto ToDto<TDto>(this NoxReferenceEntityBase entity)
    {
        return MapperHolder.Mapper.Map<TDto>(entity);
    }
}