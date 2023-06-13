namespace Nox.Reference.Data.Common;

public interface IDtoConvertibleEntity<out TDto>
{
    TDto ToDto();
}