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
        public CartBusiness(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CartBusiness> logger)
            : base(unitOfWork, mapper, logger) { }

        public void AddGameToCart(int userId, int gameId)
        {
            string cartId = GetCartId(userId);
            int quantity = 1;
          
            CartItems existingCartItem = GetCartItem(gameId, cartId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
                UnitOfWork.CartItemsRepository.Update(existingCartItem);
                UnitOfWork.Save();
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
                UnitOfWork.Save();
            }
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
        public void DeleteCart(string cartId)
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

        public void MergeCart(int tempUserId, int permUserId)
        {
            try
            {
                if (tempUserId != permUserId && tempUserId > 0 && permUserId > 0)
                {
                    string tempCartId = GetCartId(tempUserId);
                    string permCartId = GetCartId(permUserId);
                    List<CartItems> tempCartItems = UnitOfWork.CartItemsRepository.Get(x => x.CartId == tempCartId).ToList();
                    foreach (CartItems item in tempCartItems)
                    {                                         
                        CartItems cartItem = UnitOfWork.CartItemsRepository.Get(x => x.ProductId == item.ProductId && x.CartId == item.CartId).FirstOrDefault();
                        if (cartItem != null)
                        {
                            cartItem.Quantity += item.Quantity;
                            UnitOfWork.CartItemsRepository.Update(cartItem);
                        }
                        else
                        {
                            CartItems newCartItem = new CartItems
                            {
                                CartId = permCartId,
                                ProductId = item.ProductId,
                                Quantity = item.Quantity
                            };
                            
                            UnitOfWork.CartItemsRepository.Insert(newCartItem);
                        }

                        UnitOfWork.CartItemsRepository.Delete(item);
                        UnitOfWork.Save();
                    }
                    
                    DeleteCart(tempCartId);
                }
            }
            catch
            {
                throw;
            }
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
            return UnitOfWork.CartRepository.Get(x => x.UserId == userId).FirstOrDefault();     
        }

        private CartItems GetCartItem(int gameId, string cartId)
        {            
            return UnitOfWork.CartItemsRepository.Get(x => x.ProductId == gameId && x.CartId == cartId).FirstOrDefault();
        }
        private List<CartItems> ListCartItems(string cartId)
        {
            return UnitOfWork.CartItemsRepository.Get(x => x.CartId == cartId).ToList();
        }
        private int SumCartItem(string cartId)
        {
            return UnitOfWork.CartItemsRepository.Get(x => x.CartId == cartId).Sum(x => x.Quantity);
        }

        #endregion
    }
}
