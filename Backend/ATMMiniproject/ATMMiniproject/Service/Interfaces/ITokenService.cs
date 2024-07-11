namespace ATMMiniproject.Service.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(int id);
        public bool VerifyPassword(byte[] encrypterPass, byte[] password);
        public int GetUidFromToken();
    }
}
