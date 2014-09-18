using System;
using System.Text;
using System.Security.Cryptography;

namespace project.Utils {

    /// Classe di utilità per il calcolo dell'hash md5
    // fonte: http://blogs.msdn.com/b/csharpfaq/archive/2006/10/09/how-do-i-calculate-a-md5-hash-from-a-string_3f00_.aspx
	public class Hashing {

        /// Calcola l'hash md5 di una stringa
        public static String CalculateMD5Hash(String input) {

			// step 1, calculate MD5 hash from input

			MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < hash.Length; i++)
				sb.Append(hash[i].ToString("X2"));

			return sb.ToString();
		}
	}
}
