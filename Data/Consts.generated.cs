namespace Tcbcsl.Data.Entities
{
    public partial class PhoneNumberType
    {
        public const int Main = 1;
        public const int Mobile = 2;
        public const int Home = 3;
        public const int Work = 4;
    }

    public partial class GameStatus
    {
        public const int Scheduled = 1;
        public const int Postponed = 2;
        public const int RainedOut = 3;
        public const int Forfeited = 4;
        public const int Final = 5;
    }

    public partial class GameType
    {
        public const int RegularSeason = 1;
        public const int PostSeason = 2;
        public const int Exhibition = 3;
        public const int GamePlaceholder = 4;
    }

    public class Roles
    {
        public const string LeagueCommissioner = "League Commissioner";
        public const string TeamCoach = "Team Coach";
    }

}
