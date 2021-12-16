using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Dto;
using System.Collections.Generic;


namespace Project.TakuGames.Model.Business
{
    public interface IGameBusiness
    {
        List<Game> GetAllGames();
        Game CreateGame(Game game);
        Game UpdateGame(Game game);
        Game GetGame(int gameId);
        string DeleteGame(int gameId);
        List<Categories> GetCategories();
        List<Game> GetSimilarGames(int gameId);
        List<CartItemDto> GetGameAvailableInCart(string cartId);
        List<Game> GetGamesAvailableInFavoritelist(string favoriteistId);
     
    }
}
