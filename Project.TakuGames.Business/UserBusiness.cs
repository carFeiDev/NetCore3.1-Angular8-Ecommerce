using AutoMapper;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Dal;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Helpers;
using System;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Project.TakuGames.Model.Exceptions;
using System.Collections.Generic;

namespace Project.TakuGames.Business
{
    public class UserBusiness : BaseBusiness, IUserBusiness
    {
        readonly IConfiguration _config;
        public UserBusiness(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserBusiness> logger, IConfiguration config)
                        : base(unitOfWork, mapper, logger)
        {
            this._config = config;
        }
        public UserMaster AuthenticateUser(UserMaster loginCredentials)
        {
            return  UnitOfWork.UserMasterRepository
                            .Get(user => user.UserName == loginCredentials.UserName 
                                      && user.Password == Encrypt.GetSHA256(loginCredentials.Password))
                            .FirstOrDefault();                                         
        }
         public UserMaster RegisterUser(UserMaster newUser)
        {                   
            ValidateUserCreate(newUser);          
            newUser.UserTypeId = 2;
            newUser.Password = Encrypt.GetSHA256(newUser.Password);;
            UnitOfWork.UserMasterRepository.Insert(newUser);
            UnitOfWork.Save();
            return newUser;                       
        }    
        public UserMaster GetUser(int userId)
        {
            return UserSearch(userId);
        }
        private  UserMaster UserSearch(int userId)
        {
            return ListAllFromDatabase()
                  .Where(x => x.UserId == userId)
                  .FirstOrDefault();
        }
        public string GenerateJSONWebToken(UserMaster userInfo)
        {
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("userid", userInfo.UserId.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                new Claim("userTypeId", userInfo.UserTypeId.ToString(CultureInfo.InvariantCulture)),
                new Claim("userImage", userInfo.UserImage.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Role, userInfo.UserTypeId.ToString(CultureInfo.InvariantCulture)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
               issuer: _config["Jwt:Issuer"],
               audience: _config["Jwt:Audience"],
               claims: claims,
               expires: DateTime.Now.AddMinutes(60),
               signingCredentials: credentials
              );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

         #region Validations
        private void ValidateUserCreate(UserMaster user)
        {
            ValidateUserExists(user);
            if (!ComponentError.IsValid)
            {
                throw new BadRequestException(ComponentError);
            }
        }
        private void ValidateUserExists(UserMaster user)
        {
            var userExists = ListAllFromDatabase().Any(b => b.UserName.ToUpperInvariant() == user.UserName.ToUpperInvariant());
            if (userExists)
            {
                ComponentError.AddModelError(nameof(user.UserName), new ApplicationException($"Ya existe un usuario:{user.UserName}"));
            }
        }
        #endregion

        #region helpers
        private List<UserMaster> ListAllFromDatabase()
        {
            var resp = UnitOfWork.UserMasterRepository.Get().ToList();
            return resp;
        }
        #endregion
    }    
}


      
