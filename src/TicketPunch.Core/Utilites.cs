namespace TicketPunch.Core
{
    public static class Utilites
    {
        public static string Base64EncodeString(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64DecodeString(string base64) {
            var plainTextBytes = System.Convert.FromBase64String(base64);
            return System.Text.Encoding.UTF8.GetString(plainTextBytes);
        }
    }
}