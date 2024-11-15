using System;
using System.Collections.Generic;
using System.Text;
using Hiper.Security.Encryption;

namespace LAP.TUUA.UTIL
{
    public class Cryptografia
    {
        Crypto.Algorithm algoritmo = Crypto.Algorithm.TripleDES;
        Crypto.EncodingType encodTipe = Crypto.EncodingType.HEX;

        public string Encriptar(string plainText,string key)
        {
            Crypto.Clear();
            Crypto.EncryptionAlgorithm = algoritmo;
            Crypto.Encoding = encodTipe;
            Crypto.Key = key;
            try
            {
                if (Crypto.EncryptString(plainText))
                {
                    return Crypto.Content;
                }
            }
            catch (Exception)
            {
                return Crypto.CryptoException.Message;
                throw;
            }
            return "";
        }

        public string Desencriptar(string plainText, string key)
        {
            Crypto.Clear();
            Crypto.EncryptionAlgorithm = algoritmo;
            Crypto.Encoding = encodTipe;
            Crypto.Key = key;
            Crypto.Content = plainText;
            try
            {
                if (Crypto.DecryptString())
                {
                    return Crypto.Content;
                }
            }
            catch (Exception)
            {
                return Crypto.CryptoException.Message;
                throw;
            }
            return "";
        }


    }
}
