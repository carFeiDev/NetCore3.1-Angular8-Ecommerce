using Project.TakuGames.Model.Domain;
using System.Collections.Generic;
namespace Project.TakuGames.Model.Business
{
    public interface IFavoritelistBusiness
    {
        void AddOrDeleteFavoriteListItem(int userId, int gameId);
        int ClearFavoritelist(int userId);
        string GetFavoritelistId(int userId);
        List<Game> GetUserFavoritelist(int userId);
    }
}