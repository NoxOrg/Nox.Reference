namespace Nox.Reference.Data.World.Helpers;

internal static class ChecksumConstants
{
    public static readonly string[] CardHolderTypes = new string[]{
                "A",// 'Association of Persons (AOP)'
                "B",//Body of Individuals (BOI)'
                "C",//Company'
                "F",//Firm'
                "G",//Government'
                "H",//HUF (Hindu Undivided Family)'
                "L",//Local Authority'
                "J",//Artificial Juridical Person'
                "P",//Individual
                "T",//Trust (AOP)
                "K",//Krish (Trust Krish)
            };

    public static readonly int[] ControlCodeArray = new[]
    {
        1, 0, 5, 7, 9, 13, 15, 17, 19, 21, 2, 4, 18,
        20, 11, 3, 6, 8, 12, 14, 16, 10, 22, 25, 24, 23
    };
}
