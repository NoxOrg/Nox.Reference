namespace Nox.Reference;

public interface IDtoConvertibleEntity<out TDto>
{
    TDto ToDto();
}