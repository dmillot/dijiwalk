using DijiWalk.Common.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

namespace DijiWalk.Common.Encryption
{
    public class Cryption : ICryption
    {

        private readonly IConfiguration _configurationRoot;

        public Cryption(IConfiguration configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        /// <summary>
        /// Permet d'encrypter le password rentrer par le participant / organisateur
        /// </summary>
        /// <param name="password">Password de l'utilisateur</param>
        /// <returns>Retourne le password encrypté</returns>
        public string Encrypt(string password)

        {
            byte[] passwordBytes = UTF8Encoding.UTF8.GetBytes(password);

            TripleDESCryptoServiceProvider tripleEncrypt = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5Encrypt = new MD5CryptoServiceProvider();

            tripleEncrypt.Key = md5Encrypt.ComputeHash(UTF8Encoding.UTF8.GetBytes(_configurationRoot["JWTSettings:SecretKey"]));
            tripleEncrypt.Mode = CipherMode.ECB;
            tripleEncrypt.Padding = PaddingMode.PKCS7;

            byte[] resArray = tripleEncrypt.CreateEncryptor().TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);

            md5Encrypt.Clear();
            tripleEncrypt.Clear();

            return Convert.ToBase64String(resArray, 0, resArray.Length);

        }

        /// <summary>
        /// Permet de décrypter le password passer en paramètre
        /// </summary>
        /// <param name="password">Password crypté</param>
        /// <returns>Retourne le password décrypté</returns>
        public string Decrypt(string decryptText)

        {

            byte[] cryptBytes = Convert.FromBase64String(decryptText);

            TripleDESCryptoServiceProvider tripleEncrypt = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5Encrypt = new MD5CryptoServiceProvider();

            tripleEncrypt.Key = md5Encrypt.ComputeHash(UTF8Encoding.UTF8.GetBytes(_configurationRoot["JWTSettings:SecretKey"]));
            tripleEncrypt.Mode = CipherMode.ECB;
            tripleEncrypt.Padding = PaddingMode.PKCS7;

            byte[] password = tripleEncrypt.CreateDecryptor().TransformFinalBlock(cryptBytes, 0, cryptBytes.Length);

            md5Encrypt.Clear();
            tripleEncrypt.Clear();

            return UTF8Encoding.UTF8.GetString(password);

        }
    }
}
