using AutoMapper;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Business;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Dal;
using Project.TakuGames.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.TakuGames.Dal
{
    public class CartBusiness : BaseBusiness, ICartBusiness
    {      
        public CartBusiness(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            ILogger<CartBusiness> logger)
                            : base(unitOfWork, mapper, logger){ }
                            
        public void MergeTempUserCartWithLoggedUserCart(int tempUserId, int permUserId)
        {
            try
            {
                if (tempUserId != permUserId && tempUserId > 0 && permUserId > 0)
                {
                    string tempCartId = GetCartId(tempUserId);
                    string permCartId = GetCartId(permUserId);
                    List<CartItems> tempCartItems = UnitOfWork.CartItemsRepository
                                                    .Get(x => x.CartId == tempCartId)
                                                    .ToList();

                    foreach (CartItems tempItem in tempCartItems)
                    {                                         
                        CartItems existingItem = UnitOfWork.CartItemsRepository
                                                    .Get(x => x.ProductId == tempItem.ProductId && x.CartId == permCartId)
                                                    .FirstOrDefault();

                        if (existingItem != null)
                        {
                            existingItem.Quantity += tempItem.Quantity;
                            UnitOfWork.CartItemsRepository.Update(existingItem);
                        }
                        else
                        {
                            CartItems newCartItem = new CartItems
                            {
                                CartId = permCartId,
                                ProductId = tempItem.ProductId,
                                Quantity = tempItem.Quantity
                            };
                            
                            UnitOfWork.CartItemsRepository.Insert(newCartItem);
                        }

                        UnitOfWork.CartItemsRepository.Delete(tempItem);
                        UnitOfWork.Save();
                    }
                    
                    DeleteTempCart(tempCartId);
                }
            }
            catch
            {
                throw;
            }
        }
        
        public void AddGameToCart(int userId, int gameId)
        {
            string cartId = GetCartId(userId);
            int quantity = 1;
          
            CartItems existingCartItem = GetCartItem(gameId, cartId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
                UnitOfWork.CartItemsRepository.Update(existingCartItem);
            }
            else
            {
                CartItems cartItems = new CartItems
                {
                    CartId = cartId,
                    ProductId = gameId,
                    Quantity = quantity

                };

                UnitOfWork.CartItemsRepository.Insert(cartItems);                
            }

            UnitOfWork.Save();
        }

        public string CreateCart(int userId)
        {
            try
            {
                Cart shoppingCart = new Cart
                {
                    CartId = Guid.NewGuid().ToString(),
                    UserId = userId,
                    DateCreated = DateTime.Now.Date
                };
                
                UnitOfWork.CartRepository.Insert(shoppingCart);
                UnitOfWork.Save();
                return shoppingCart.CartId;
            }
            catch
            {
                throw;
            }
        }

        public int CleanCart(int userId)
        {
            try
            {
                string cartId = GetCartId(userId);
                List<CartItems> cartItem = ListCartItems(cartId);
                if (!string.IsNullOrEmpty(cartId))
                {
                    foreach (CartItems item in cartItem)
                    {
                        UnitOfWork.CartItemsRepository.Delete(item);
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
      
        public void DeleteTempCart(string cartId)
        {           
            Cart cart = UnitOfWork.CartRepository.GetByID(cartId);         
            UnitOfWork.CartRepository.Delete(cart);
            UnitOfWork.Save();
        }

        public void DeleteOneCardItem(int userId, int gameId)
        {
            try
            {
                string cartId = GetCartId(userId);
                CartItems cartItem = GetCartItem(gameId, cartId);
                cartItem.Quantity -= 1;
                UnitOfWork.CartItemsRepository.Update(cartItem);
                UnitOfWork.Save();
            }
            catch
            {
                throw;
            }

        }
 
        public int GetCartItemCount(int userId)
        {
            string cartId = GetCartId(userId);
            if (!string.IsNullOrEmpty(cartId))
            {
                int cartItemCount = SumCartItem(cartId);
                return cartItemCount;
            }

            return 0;
        }
 
        public void RemoveCartItem(int userId, int gameId)
        {
            try
            {
                string cartId = GetCartId(userId);
                CartItems cartItem = GetCartItem(gameId, cartId);
                UnitOfWork.CartItemsRepository.Delete(cartItem);
                UnitOfWork.Save();
            }
            catch
            {
                throw;
            }
        }
        
        #region helper
        public string GetCartId(int userId)
        {
            try
            {
                Cart cart = GetCart(userId);
                if (cart != null)
                {
                    return cart.CartId;
                }
                else
                {
                    return CreateCart(userId);
                }
            }
            catch
            {
                throw;
            }
        }
    
        private Cart GetCart(int userId)
        {
            return UnitOfWork.CartRepository
                    .Get(x => x.UserId == userId)
                    .FirstOrDefault();     
        }

        private CartItems GetCartItem(int gameId, string cartId)
        {            
            return UnitOfWork.CartItemsRepository
                    .Get(x => x.ProductId == gameId && x.CartId == cartId)
                    .FirstOrDefault();
        }
     
        private List<CartItems> ListCartItems(string cartId)
        {
            return UnitOfWork.CartItemsRepository
                    .Get(x => x.CartId == cartId)
                    .ToList();
        }
    
        private int SumCartItem(string cartId)
        {
            return UnitOfWork.CartItemsRepository
                    .Get(x => x.CartId == cartId)
                    .Sum(x => x.Quantity);
        }

        #endregion
    }
}
