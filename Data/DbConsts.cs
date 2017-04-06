using System;

namespace Tcbcsl.Data
{
    public static class DbConsts
    {
        public const string SystemUserIdString = "00000000-0000-0000-0000-000000000001";
        public static readonly Guid SystemUserId = new Guid(SystemUserIdString);
    }
}
