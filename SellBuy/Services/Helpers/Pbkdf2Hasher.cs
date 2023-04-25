using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;


namespace SellBuy.Services.Helpers
{
    public class Pbkdf2Hasher
    {
        private int _saltLengthBytes = 16;
        private int _subkeyLengthBytes = 32;
        private int _iterationCount = 100000;
        private KeyDerivationPrf _prf = KeyDerivationPrf.HMACSHA256;

        public string Generate(string msg)
        {
            if (string.IsNullOrEmpty(msg))
                throw new ArgumentNullException();

            return Hash(msg);
        }

        public bool Verify(string plaintext, string existingSaltedHash)
        {
            var saltedHashBytes = Convert.FromBase64String(existingSaltedHash);

            var salt = new byte[_saltLengthBytes];
            Buffer.BlockCopy(saltedHashBytes, 0, salt, 0, _saltLengthBytes);

            var existingHash = new byte[_subkeyLengthBytes];
            Buffer.BlockCopy(saltedHashBytes, _saltLengthBytes, existingHash, 0, _subkeyLengthBytes);

            var newHash = KeyDerivation.Pbkdf2(
                 password: plaintext,
                 salt: salt,
                 prf: _prf,
                 iterationCount: _iterationCount,
                 numBytesRequested: _subkeyLengthBytes
            );

            return existingHash.SequenceEqual(newHash);
        }

        private string Hash(string plaintext)
        {
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[_saltLengthBytes];
            rng.GetBytes(salt);

            var hashed = KeyDerivation.Pbkdf2(
                password: plaintext,
                salt: salt,
                prf: _prf,
                iterationCount: _iterationCount,
                numBytesRequested: _subkeyLengthBytes
            );

            var saltedHash = new byte[_saltLengthBytes + _subkeyLengthBytes];
            Buffer.BlockCopy(salt, 0, saltedHash, 0, _saltLengthBytes);
            Buffer.BlockCopy(hashed, 0, saltedHash, _saltLengthBytes, _subkeyLengthBytes);

            return Convert.ToBase64String(saltedHash);
        }
    }
}
