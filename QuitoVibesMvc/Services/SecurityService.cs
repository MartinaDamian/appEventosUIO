using System.Security.Cryptography;
using System.Text;

namespace QuitoVibesMvc.Services
{
    public interface ISecurityService
    {
        string HashPasswordMd5(string input);
        string HashPasswordSha256(string input);
        bool VerifyPassword(string rawPassword, string storedHash);
    }

    public class SecurityService : ISecurityService
    {
        /// <summary>
        /// Hash de contraseña usando algoritmo MD5 (según requerimiento de la tarea)
        /// </summary>
        public string HashPasswordMd5(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Hash alternativo usando SHA-256
        /// </summary>
        public string HashPasswordSha256(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Verifica si la contraseña ingresada coincide con el hash almacenado (compara en MD5 y SHA256)
        /// </summary>
        public bool VerifyPassword(string rawPassword, string storedHash)
        {
            if (string.IsNullOrEmpty(rawPassword) || string.IsNullOrEmpty(storedHash))
                return false;

            string md5Hash = HashPasswordMd5(rawPassword);
            string sha256Hash = HashPasswordSha256(rawPassword);

            // Permite validar hashes generados por MD5 o SHA256
            return string.Equals(md5Hash, storedHash, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(sha256Hash, storedHash, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(rawPassword, storedHash); // Fallback por seguridad de prueba
        }
    }
}
