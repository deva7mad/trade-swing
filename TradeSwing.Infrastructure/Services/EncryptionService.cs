using System.Security.Cryptography;

namespace TradeSwing.Infrastructure.Services;

public class EncryptionService
{
    private readonly byte[] _key = new byte[] {};
    private readonly byte[] _iv = new byte[] {};

    public string Encrypt(string data)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = _key;
        aesAlg.IV = _iv;

        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msEncrypt = new MemoryStream();
        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(data);
            }
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public string Decrypt(string encryptedData)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = _key;
        aesAlg.IV = _iv;

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        byte[] cipherText = Convert.FromBase64String(encryptedData);

        using MemoryStream msDecrypt = new MemoryStream(cipherText);
        using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using StreamReader srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}