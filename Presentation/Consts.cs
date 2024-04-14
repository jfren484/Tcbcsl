using System;
using System.Collections.Generic;

namespace Tcbcsl.Presentation
{
    public static class Consts
    {
        public const int FirstYear = 2001;
        public const int CurrentYear = 2024;

        public const int TeamTBDTeamId = 44;
        public const int PlayerPoolTeamId = 60;
        public const int NonLeagueDivisionId = 4;
        public static string PlayerPoolTeamName = ""; // Set in Global.asax

        public const string AllTime = "All-Time";
        public const string LeagueNameForList = "*League*";

        public const string WholeNumberFormat = "N0";
        public const string Decimal3PlacesFormat = "N3";
        public const string DateFormat = "yyyy-MM-dd";
        public const string DateFormatDisplay = "MMMM d, yyyy";
        public const string DateTimeFormatDisplay = "MMMM d, yyyy, h:mm tt";
        public const string GameDateTimeFormat = "MMM d, h:mm tt";
        public const string GamesBackFormat = "0.0;0.0;'---'";

        public static DateTime[] TournamentDates; // Set in Global.asax

        public static readonly Dictionary<int, string> GameOutcomeVerbs = new Dictionary<int, string>
                                                                          {
                                                                              [-1] = "Lost",
                                                                              [0] = "Tied",
                                                                              [1] = "Won"
                                                                          };

        public static readonly int[] InvalidPlayerTeamIds = {44, 50, 51, 52};

        public static readonly Dictionary<string, string> ClinchDescriptions = new Dictionary<string, string>
                                                                               {
                                                                                   ["w"] = "Wild Card",
                                                                                   ["x"] = "Division",
                                                                                   ["y"] = "Division and 1st-Round Home-field Advantage",
                                                                                   ["z"] = "Division and 1st-Round Bye"
                                                                               };
    }
}