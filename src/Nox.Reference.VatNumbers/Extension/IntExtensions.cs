namespace Nox.Reference.VatNumbers.Extension
{
    internal static class IntExtensions
    {
        public static int Mod(this int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }
    }
}
