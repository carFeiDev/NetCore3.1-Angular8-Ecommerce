using Project.TakuGames.Model.Domain;


namespace Project.TakuGames.Model.Business
{
    public interface IUserBusiness
    {
        UserMaster AuthenticateUser(UserMaster loginCredentials);
        UserMaster RegisterUser(UserMaster userData);
        string GenerateJSONWebToken(UserMaster userMaster);
        
    }
}
