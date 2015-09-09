using System;
using System.Collections.Generic;

namespace Tcbcsl.Presentation
{
    public static class Consts
    {
        public const int FirstYear = 2001;
        public const int CurrentYear = 2015;

        public const string AllTime = "All-Time";
        public const string LeagueNameForList = "*League*";

        public const string WholeNumberFormat = "N0";
        public const string Decimal3PlacesFormat = "N3";
        public const string DateFormat = "yyyy-MM-dd";
        public const string DateFormatDisplay = "MMMM d, yyyy";
        public const string DateTimeFormatDisplay = "MMMM d, yyyy h:mm tt";
        public const string GamesBackFormat = "0.0;0.0;'---'";

        public static readonly List<DateTime> TournamentDates = new List<DateTime>
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
            DateTime.Parse("2015-08-15")
        };

        public static readonly Dictionary<int, string> GameOutcomeVerbs = new Dictionary<int, string>
        {
            [-1] = "Lost",
            [0] = "Tied",
            [1] = "Won"
        };

        /*
        $g_lTEAM_ID_TBD		= 44;
        $g_lTEAM_ID_PP		= 60;
        $g_lCOACH_ID_DUMMY	= 31;
        $g_sPH_DIV_NAME		= 'Post-Season';
        */
    }
}