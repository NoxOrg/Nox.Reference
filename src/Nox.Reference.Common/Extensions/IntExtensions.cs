namespace Nox.Reference.Common;

// TODO: make internal
public static class IntExtensions
{
    public static int Mod(this int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }
}