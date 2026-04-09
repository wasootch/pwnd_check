using System.Security.Cryptography;
using System.Text;

namespace pwnd_check.common
{
    public static class HibpService
    {
        private static readonly HttpClient _client = new HttpClient();
        private const string BaseUrl = "https://api.pwnedpasswords.com/range/";

        public static string ComputeSha1(string input)
        {
            byte[] hash = SHA1.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(hash);
        }

        public static async Task<(bool Found, int Count)> CheckPasswordAsync(string password)
        {
            string hashed = ComputeSha1(password);
            string prefix = hashed[..5];
            string suffix = hashed[5..];

            string response = await _client.GetStringAsync(BaseUrl + prefix);

            foreach (string line in response.Split('\n', StringSplitOptions.RemoveEmptyEntries))
            {
                int colon = line.IndexOf(':');
                if (colon < 0) continue;

                if (line[..colon].TrimEnd() == suffix)
                    return (true, int.Parse(line[(colon + 1)..].Trim()));
            }

            return (false, 0);
        }
    }
}
