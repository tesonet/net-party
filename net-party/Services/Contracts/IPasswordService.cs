namespace net_party.Services.Contracts
{
    public interface IPasswordService
    {
        string HashPassword(string password);

        bool ValidatePassword(string password, string correctHash);
    }
}