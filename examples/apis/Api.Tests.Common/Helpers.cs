using System;

namespace Api.Tests.Common
{
    public static class Helpers
    {
        public static string GetDateTime()
        {
            return TimeZoneInfo
                .ConvertTimeFromUtc(
                    DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time")
                )
                .ToString("yyyy-MM-dd HH-mm-ss");
        }
    }
}
