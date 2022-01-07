using Project.TakuGames.Model.Domain;


namespace Project.TakuGames.Model.Business
{
    public interface IUserBusiness
    {
        UserMaster AuthenticateUser(UserMaster loginCredentials);
        int? RegisterUser(UserMaster userData);
        bool isUserExists(string userName);
        string GenerateJSONWebToken(UserMaster userMaster);
        bool CheckUserAwaillabity(int userId);
    }
}
