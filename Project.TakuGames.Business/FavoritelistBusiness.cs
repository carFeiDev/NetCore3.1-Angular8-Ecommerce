using AutoMapper;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Dal;
using Project.TakuGames.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.TakuGames.Business
{
    public class FavoritelistBusiness : BaseBusiness,IFavoritelistBusiness
    {
        private readonly IGameBusiness gameBusiness;
        private readonly IUserBusiness userBusiness;     
        public FavoritelistBusiness(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IGameBusiness gameBusiness,
            IUserBusiness userBusiness,
            ILogger<FavoritelistBusiness> logger):
            base(unitOfWork, mapper, logger)      
        {
            this.gameBusiness = gameBusiness;
            this.userBusiness = userBusiness;
        }
        public void ToggleFavoritelistItem(int userId, int gameId)
        {
            string favoritelistId = GetFavoritelistId(userId);
            FavoritelistItems existingFavoritelistItem = GetFavoritelistWithUserIdAndFavoritelistId(gameId,favoritelistId); 
            if (existingFavoritelistItem != null)
            {
                UnitOfWork.FavoritelistItemsRepository.Delete(existingFavoritelistItem);
                UnitOfWork.Save();
            }
            else
            {
                FavoritelistItems favoritelistItem = new FavoritelistItems
                {
                    FavoritelistId = favoritelistId,
                    ProductId = gameId,
                };
                UnitOfWork.FavoritelistItemsRepository.Insert(favoritelistItem);
                UnitOfWork.Save();
            }
        }
        public int ClearFavoritelist(int userId)
        {
            try
            {
                string favoritelistId = GetFavoritelistId(userId);
                List<FavoritelistItems> favoritelistItem = GetListFavoritelistItemsWithId(favoritelistId);
                
                if (!string.IsNullOrEmpty(favoritelistId))
                {
                    foreach (FavoritelistItems item in favoritelistItem)
                    {
                        UnitOfWork.FavoritelistItemsRepository.Delete(item);
                        UnitOfWork.Save();
                    }
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }
         
        public List<Game> GetUserFavoritelist(int userId)
        {
            bool user = userBusiness.CheckUserAwaillabity(userId);
            if (user)
            {
                string favoritelistid = GetFavoritelistId(userId);
                return  gameBusiness.GetGamesAvailableInFavoritelist(favoritelistid);
            }
            else
            {
                return new List<Game>();
            }

        }

        #region Helper    
        public string GetFavoritelistId(int userId)
        {
            try
            {
                Favoritelist favoritelist = GetFavoritelistWithUserId(userId);

                if (favoritelist != null)
                {
                    return favoritelist.FavoritelistId;
                }
                else
                {
                    return CreateFavoritelist(userId);
                }

            }
            catch
            {
                throw;
            }
        }

        private string CreateFavoritelist(int userId)
        {
            try
            {
                Favoritelist favoriteList = new Favoritelist
                {
                    FavoritelistId = Guid.NewGuid().ToString(),
                    UserId = userId,
                    DateCreated = DateTime.Now.Date
                };
                UnitOfWork.FavoritelistRepository.Insert(favoriteList);
                UnitOfWork.Save();

                return favoriteList.FavoritelistId;
            }
            catch
            {
                throw;
            }
        }
        private Favoritelist GetFavoritelistWithUserId(int userId)
        {
            return GetListFavoritelist().Where(x => x.UserId == userId ).FirstOrDefault(); 
        }
         private FavoritelistItems GetFavoritelistWithUserIdAndFavoritelistId(int gameId, string favoritelistId)
        {
            return GetListFavoritelistItems().Where(x => x.ProductId == gameId && x.FavoritelistId == favoritelistId).FirstOrDefault();
        }
        
        private List<FavoritelistItems> GetListFavoritelistItemsWithId(string favoritelistId)
        {
            return GetListFavoritelistItems().Where(x => x.FavoritelistId == favoritelistId).ToList();
        }
        private List<Favoritelist> GetListFavoritelist() 
        {
            return UnitOfWork.FavoritelistRepository.Get().ToList();   
        } 
        
        private List<FavoritelistItems> GetListFavoritelistItems()
        {
            return UnitOfWork.FavoritelistItemsRepository.Get().ToList();
        }
         #endregion       
    }
}
