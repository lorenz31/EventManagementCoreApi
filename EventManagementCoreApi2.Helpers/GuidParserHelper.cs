using System;

namespace EventManagementCoreApi2.Helpers
{
    public class GuidParserHelper
    {
        public static bool ParseStringToGuid(string input)
        {
            Guid userid;

            var isValid = Guid.TryParse(input, out userid);

            if (isValid)
                return true;
            else
                return false;
        }

        public static Guid StringToGuid(string input)
        {
            return Guid.Parse(input);
        }
    }
}