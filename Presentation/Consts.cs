using System;
using System.Collections.Generic;

namespace Tcbcsl.Presentation
{
    public static class Consts
    {
        public const int FirstYear = 2001;
        public const int CurrentYear = 2017;

        public const int PlayerPoolTeamId = 60;
        public static string PlayerPoolTeamName = ""; // Set in Global.asax
        public const int TeamTBDTeamId = 44;

        public const string AllTime = "All-Time";
        public const string LeagueNameForList = "*League*";

        public const string WholeNumberFormat = "N0";
        public const string Decimal3PlacesFormat = "N3";
        public const string DateFormat = "yyyy-MM-dd";
        public const string DateFormatDisplay = "MMMM d, yyyy";
        public const string DateTimeFormatDisplay = "MMMM d, yyyy, h:mm tt";
        public const string GameDateTimeFormat = "MMM d, h:mm tt";
        public const string GamesBackFormat = "0.0;0.0;'---'";

        public static readonly DateTime[] TournamentDates =
        {
            DateTime.Parse("2001-08-18"),
            DateTime.Parse("2002-08-17"),
            DateTime.Parse("2003-08-16"),
            DateTime.Parse("2004-08-21"),
            DateTime.Parse("2005-08-20"),
            DateTime.Parse("2006-08-19"),
            DateTime.Parse("2007-08-18"),
            DateTime.Parse("2008-08-16"),
            DateTime.Parse("2009-08-15"),
            DateTime.Parse("2010-08-21"),
            DateTime.Parse("2011-08-20"),
            DateTime.Parse("2012-08-18"),
            DateTime.Parse("2013-08-17"),
            DateTime.Parse("2014-08-16"),
            DateTime.Parse("2015-08-15"),
            DateTime.Parse("2016-08-20"),
            DateTime.Parse("2017-08-19")
        };

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

        /*
        $g_lCOACH_ID_DUMMY	= 31;
        $g_sPH_DIV_NAME		= 'Post-Season';
        */
    }
}