using Project.TakuGames.Model.Domain;


namespace Project.TakuGames.Model.Business
{
    public interface IUserBusiness
    {
        UserMaster AuthenticateUser(UserMaster loginCredentials);
        int? RegisterUser(UserMaster userData);
        bool CheckUserAwaillabity(string userName);
        string GenerateJSONWebToken(UserMaster userMaster);
        bool isUserExists(int userId);
    }
}
