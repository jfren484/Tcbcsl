using System.Collections.Generic;

namespace Tcbcsl.Presentation
{
    public static class Consts
    {
        public const int FirstYear = 2000;
        public const int CurrentYear = 2015;

        public const string WholeNumberFormat = "N0";
        public const string Decimal3PlacesFormat = "N3";
        public const string DateFormat = "MMMM d, yyyy";
        public const string TiesFormat = "'- '#;;''";
        public const string GamesBackFormat = "0.0;0.0;'---'";

        public static readonly Dictionary<int, string> GameOutcomeVerbs = new Dictionary<int, string>
        {
            [-1] = "Lost",
            [0] = "Tied",
            [1] = "Won"
        };
    }
}