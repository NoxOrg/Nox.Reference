namespace Nox.Reference.Common;

// TODO: make internal
public static class IntExtensions
{
    public static int Mod(this int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }

    public static IEnumerable<int> ToDigitEnumerable(this int number)
    {
        IList<int> digits = new List<int>();

        while (number > 0)
        {
            digits.Add(number % 10);
            number = number / 10;
        }

        //digits are currently backwards, reverse the order
        return digits.Reverse();
    }
}