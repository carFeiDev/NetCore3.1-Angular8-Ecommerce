using System;
using System.Collections.Generic;
using System.Linq;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Dal;
using Project.TakuGames.Model.Domain;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Project.TakuGames.Business
{
    public class FavoritelistBusiness : BaseBusiness, IFavoritelistBusiness
    {
        private readonly IGameBusiness gameBusiness;
        private readonly IUserBusiness userBusiness;
             
        public FavoritelistBusiness(IUnitOfWork unitOfWork,
                                    IMapper mapper,
                                    IGameBusiness gameBusiness,
                                    IUserBusiness userBusiness,
                                    ILogger<FavoritelistBusiness> logger)
                                    :base(unitOfWork, mapper, logger)      
        {
            this.gameBusiness = gameBusiness;
            this.userBusiness = userBusiness;
        }

        public List<Game> GetUserFavoritelist(int userId)
        {
            bool user = CheckUserAwaillabity(userId);
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

        public void AddOrDeleteFavoriteListItem(int userId, int gameId)
        {
            string favoritelistId = GetFavoritelistId(userId);
            FavoritelistItems existingFavoritelistItem = GetFavoriteWithGameIdAndFavoriteListId(gameId,favoritelistId);
             
            if (existingFavoritelistItem == null)
            {
                FavoritelistItems newFavoritelistItem = new FavoritelistItems{ FavoritelistId = favoritelistId,ProductId = gameId };
                UnitOfWork.FavoritelistItemsRepository.Insert(newFavoritelistItem);
            }
            else
            {
                UnitOfWork.FavoritelistItemsRepository.Delete(existingFavoritelistItem);               
            }

            UnitOfWork.Save();
        }

        public int ClearFavoritelist(int userId)
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
        
        public string GetFavoritelistId(int userId)
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

        #region Validations

        private bool isUserExists (string userName)
        {     
            UserMaster user = GetUSerWithUserName(userName);
            return user != null;
        }

        private bool CheckUserAwaillabity(int userId)
        {
            UserMaster user = GetUserWithId(userId);
            return  user != null;            
        }

        #endregion

        #region Helper    

        private string CreateFavoritelist(int userId)
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

        private Favoritelist GetFavoritelistWithUserId(int userId)
        {
            return GetListFavoritelist().Where(x => x.UserId == userId ).FirstOrDefault(); 
        }

        private FavoritelistItems GetFavoriteWithGameIdAndFavoriteListId(int gameId, string favoritelistId)
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

        private UserMaster GetUserWithId(int userId)
        {
            return UnitOfWork.UserMasterRepository.Get(x => x.UserId == userId).FirstOrDefault();
        }

        private UserMaster GetUSerWithUserName(string userName)
        {
            return UnitOfWork.UserMasterRepository.Get(x => x.UserName == userName).FirstOrDefault();
        }

        #endregion       
    }
}
