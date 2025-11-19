namespace Moondesk.Domain.Interfaces;

public interface IEncryptionService
{
    (string encryptedData, string iv) Encrypt(string plainText);
    string Decrypt(string encryptedData, string iv);
}
