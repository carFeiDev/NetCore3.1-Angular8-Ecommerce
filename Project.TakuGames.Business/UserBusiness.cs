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

namespace Project.TakuGames.Business
{
    public class UserBusiness : BaseBusiness, IUserBusiness
    {
        readonly IConfiguration _config;
        public UserBusiness(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UserBusiness> logger,
            IConfiguration config) : base(unitOfWork, mapper, logger)
        {
            this._config = config;
        }

        public UserMaster AuthenticateUser(UserMaster loginCredentials)
        {
            UserMaster user = new UserMaster();
            
            var userDetails = UnitOfWork.UserMasterRepository
                                .Get(u => u.UserName == loginCredentials.UserName && u.Password == loginCredentials.Password)
                                .FirstOrDefault();

            if (userDetails != null)
            {
                user = new UserMaster
                {
                    UserName = userDetails.UserName,
                    UserId = userDetails.UserId,
                    UserTypeId = userDetails.UserTypeId
                };
                return user;
            }
            else
            {
                return userDetails;
            }

        }

        public bool CheckUserAwaillabity(string userName)
        {     
            string user = UnitOfWork.UserMasterRepository.Get(x => x.UserName == userName).FirstOrDefault()?.ToString();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public  int? RegisterUser(UserMaster userData)
        {
                     
            try
            {                
                string spassword = Encrypt.GetSHA256(userData.Password);
                var user = UnitOfWork.UserMasterRepository.Get().Where(d => d.UserName == userData.UserName).FirstOrDefault();
                if(!(user == null)) return null;          
                userData.UserTypeId = 2;
                userData.Password = spassword;
                UnitOfWork.UserMasterRepository.Insert(userData);
                UnitOfWork.Save();
                return 1;
            }
            catch
            {
                throw;
            }            
       
        }
         public bool isUserExists(int userId)
        {
            string user = UnitOfWork.UserMasterRepository.Get(x => x.UserId == userId).FirstOrDefault()?.ToString();

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
