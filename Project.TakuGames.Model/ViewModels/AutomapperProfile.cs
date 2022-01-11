using AutoMapper;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Dto;

namespace Project.TakuGames.Model.ViewModels
{
  public class AutomapperProfile : Profile
  {
    public AutomapperProfile()
    {
      CreateMap<Games, GamesVM>()
        .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));
      
      CreateMap<GamesVM,Games >()
        .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));

      CreateMap<Game, GameVM>()
        .ForMember(dest =>  dest.GameId, opt => opt.MapFrom(src => src.GameId))
        .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        .ForMember(dest => dest.Developer, opt => opt.MapFrom(src => src.Developer))
        .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
        .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform))
        .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
        .ForMember(dest => dest.CoverFileName, opt => opt.MapFrom(src => src.CoverFileName));
      
      CreateMap<GameVM, Game>()
        .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId))
        .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        .ForMember(dest => dest.Developer, opt => opt.MapFrom(src => src.Developer))
        .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
        .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform))
        .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
        .ForMember(dest => dest.CoverFileName, opt => opt.MapFrom(src => src.CoverFileName));

      CreateMap<Categories, CategoriesVM>()
        .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));

      CreateMap<CartItemDto, CartItemDtoVM>()
        .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game))
        .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

      CreateMap<CartItemDtoVM, CartItemDto>()
        .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game))
        .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

      CreateMap<OrdersUserDto, OrdersUserDtoVM>()
        .ForMember(dest => dest.CustomerOrderDetails, opt => opt.MapFrom(src => src.CustomerOrderDetails))
        .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated));

      CreateMap<CustomerOrders, CustumerOrdersVM>()
        .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
        .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
        .ForMember(dest => dest.CartTotal, opt => opt.MapFrom(src => src.CartTotal))
        .ForMember(dest => dest.CartTotal, opt => opt.MapFrom(src => src.CartTotal));

      CreateMap<CustumerOrdersVM, CustomerOrders>()
        .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
        .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
        .ForMember(dest => dest.CartTotal, opt => opt.MapFrom(src => src.CartTotal));
      
      CreateMap<Favoritelist, FavoritelistVM>()
        .ForMember(dest => dest.FavoritelistId, opt => opt.MapFrom(src => src.FavoritelistId))
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
        .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated));

      CreateMap<FavoritelistVM, Favoritelist>()
        .ForMember(dest => dest.FavoritelistId, opt => opt.MapFrom(src => src.FavoritelistId))
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
        .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated));

      CreateMap<FavoritelistItems, FavoritelistItemsVM>()
        .ForMember(dest => dest.FavoritelistItemId, opt => opt.MapFrom(src => src.FavoritelistItemId))
        .ForMember(dest => dest.FavoritelistId, opt => opt.MapFrom(src => src.FavoritelistId))
        .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));



      CreateMap<UserMasterVM, UserMaster>()
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
        .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
        .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
        .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
        .ForMember(dest => dest.UserTypeId, opt => opt.MapFrom(src => src.UserTypeId));

        CreateMap<UserMaster, UserMasterVM>()
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
        .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
        .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
        .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
        .ForMember(dest => dest.UserTypeId, opt => opt.MapFrom(src => src.UserTypeId))
        ;


      CreateMap<FavoritelistItems, FavoritelistItemsVM>()
        .ForMember(dest => dest.FavoritelistItemId, opt => opt.MapFrom(src => src.FavoritelistItemId))
        .ForMember(dest => dest.FavoritelistId, opt => opt.MapFrom(src => src.FavoritelistId))
        .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
    }
  }
}


