using System.Security.Cryptography;
using System.Text;

namespace StarWarsApi.Database
{
    
    public class PasswordHashing
    {
        private const string salt = "78378265240709988066"; 
        //Salting password to enhance security by adding data to the password before the SHA256 conversion.
        //This makes it more difficult(impossible) to datamine.
        public static string HashPassword(string input)
        {
            var hashedData = ComputeSha256Hash(input + salt);
            return hashedData;
        }
        private static string ComputeSha256Hash(string rawData)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData + salt));
            var builder = new StringBuilder();  
            foreach (var t in bytes)
                builder.Append(t.ToString("x2"));
            return builder.ToString();
        } 
    }
    
}