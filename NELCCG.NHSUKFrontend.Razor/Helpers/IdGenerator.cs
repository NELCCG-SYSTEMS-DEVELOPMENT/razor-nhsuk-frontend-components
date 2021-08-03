namespace NELCCG.NHSUKFrontend.Razor.Helpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.WebUtilities;
    using System.Security.Cryptography;

    public class IdGenerator
    {
        private static readonly RNGCryptoServiceProvider rngCsp = new();

        public static string GenerateId()
        {
            return GenerateId(string.Empty);
        }

        public static string GenerateId(string prefix, string suffix = "")
        {
            byte[] key = new byte[4];
            rngCsp.GetBytes(key);
            return TagBuilder.CreateSanitizedId($"{prefix}{WebEncoders.Base64UrlEncode(key)}{suffix}", "");
        }
    }
}
