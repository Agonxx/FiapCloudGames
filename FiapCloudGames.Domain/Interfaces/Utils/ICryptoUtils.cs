namespace FiapCloudGames.Domain.Interfaces.Utils
{
    public interface ICryptoUtils
    {
        string EncryptString(string plainText);
        string DecryptString(string cipherText);
    }
}
