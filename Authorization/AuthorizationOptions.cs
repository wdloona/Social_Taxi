using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CSharp_React.Authorization
{
    public class AuthorizationOptions
    {
        public const string ISSUER = "CSharp_React"; // издатель токена
        public const string AUDIENCE = "CSharp_React_Site"; // потребитель токена
        const string SOLT = "bb87e4cdd332493968df6661b15dc17acaf2d373baf2b89dd8d47b3cae700b59";   // соль для хэширования пароля
        const string KEY = "873ec2881b724fc43afe5941b93a6a68ccee3dbaf72b3e325015eadf56d36eb3";   // ключ для формирования токена

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        public static string ComputePassSha256Hash(string password)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(SOLT + "-" + password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
