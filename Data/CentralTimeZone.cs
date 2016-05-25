using System;

namespace Tcbcsl.Data
{
    public static class CentralTimeZone
    {
        private static readonly TimeZoneInfo CentralTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

        public static readonly TimeSpan DaylightSavingsOffset = CentralTimeZoneInfo.GetUtcOffset(new DateTime(2001, 7, 4));

        public static DateTime Now => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, CentralTimeZoneInfo);
        public static DateTime Today => Now.Date;
    }
}
