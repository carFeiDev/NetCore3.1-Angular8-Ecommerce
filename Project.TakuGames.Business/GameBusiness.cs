using AutoMapper;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Dal;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Dto;
using Project.TakuGames.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Project.TakuGames.Business
{
    public class GameBusiness :BaseBusiness, IGameBusiness
    {
        public GameBusiness(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            ILogger<GameBusiness> logger)
                            : base(unitOfWork, mapper, logger) { }
        public List<Game> GetAllGames()
        {
            return ListAllFromDatabase();
        }
        public Game GetGame(int GameId)
        {
            return GameSearch(GameId);
        }

        public Game CreateGame(Game game)
        {
            UnitOfWork.GameRepository.Insert(game);
            UnitOfWork.Save();
            return game;
        }
        public Game UpdateGame(Game game)
        {
            ValidateUpdateGame(game);
            Game newGameData = GameSearch(game.GameId);
            newGameData.Title = game.Title.Trim();
            newGameData.Description = game.Description;
            newGameData.Developer = game.Developer;
            newGameData.Publisher = game.Publisher;
            newGameData.Platform = game.Platform;
            newGameData.Category = game.Category;
            newGameData.Price = game.Price;
            newGameData.CoverFileName = game.CoverFileName;

            UnitOfWork.GameRepository.Update(newGameData);
            UnitOfWork.Save();
            return game;
        }
    
        public string DeleteGame(int id)
        {
            var gam = GameSearch(id);
            UnitOfWork.GameRepository.Delete(gam);
            UnitOfWork.Save();
            return (gam.CoverFileName);
        }

        public List<CartItemDto> GetGameAvailableInCart(string cartid)
        {
            try
            {
                List<CartItemDto> cartItemList = new List<CartItemDto>();
                List<CartItems> cartItems = UnitOfWork.CartItemsRepository.Get().Where(x => x.CartId == cartid).ToList();
                foreach (CartItems item in cartItems)
                {
                    Game game = GameSearch(item.ProductId);
                    CartItemDto objCartItem = new CartItemDto
                    {
                        Game = game,
                        Quantity = item.Quantity
                    };
                    cartItemList.Add(objCartItem);
                }
                return cartItemList;
            }
            catch
            {
                throw;
            }
        }

        public List<Categories> GetCategories()
        {
            List<Categories> listCategories = new List<Categories>();
            listCategories = UnitOfWork.CategoriesRepository.Get().ToList();
            return listCategories;
        }
        public List<Game> GetSimilarGames(int gameid)
        {
            List<Game> lstGame = new List<Game>();
            Game game = GameSearch(gameid);           
            lstGame = UnitOfWork.GameRepository.Get().Where(x => x.Category == game.Category && x.GameId != game.GameId)
                                               .OrderBy(u => Guid.NewGuid())
                                               .Take(9)
                                               .ToList();
            return lstGame;
        }

        public List<Game> GetGamesAvailableInFavoritelist(string favoritelistId)
        {
            try
            {
                List<Game> favoritelist = new List<Game>();
                List<FavoritelistItems> cartItems = GetAllFavoritelistItems().Where(x => x.FavoritelistId == favoritelistId).ToList();
                foreach (FavoritelistItems item in cartItems)
                {
                    Game game = GameSearch(item.ProductId);
                    favoritelist.Add(game);
                }
                return favoritelist;
            }
            catch
            {
                throw;
            }
        }


        #region Validaciones
        private void ValidateUpdateGame(Game game)
        {
            ValidateExistId(game);
            if (!ComponentError.IsValid)
            {
                throw new BadRequestException(ComponentError);
            }
        }
        private void ValidateExistId(Game game)
        {
            var gam = GameSearch(game.GameId);
            if (gam == null)
            {
                throw new NotFoundException();
            }
        }

        #endregion
        
        #region Helper
        private List<Game> ListAllFromDatabase()
        {
            var resp = UnitOfWork.GameRepository.Get().ToList();
            return resp;
        }
        private Game GameSearch(int gameId)
        {
            return UnitOfWork.GameRepository.Get().Where(x => x.GameId == gameId ).FirstOrDefault();
        }
        private List<FavoritelistItems> GetAllFavoritelistItems(){
            return  UnitOfWork.FavoritelistItemsRepository.Get().ToList();
        }
      
        #endregion
    }
}
