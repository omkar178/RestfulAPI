using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestfulAPI.Data;
using RestfulAPI.Model;
using RestfulAPI.Repository.IRepository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RestfulAPI.Repository
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSetting _appsetting;

        public UserRepository(ApplicationDbContext db, IOptions<AppSetting> appsetting)
        {
            _db = db;
            _appsetting = appsetting.Value;
        }
        public Users Authenticate(string username, string password)
        {
            var user = _db.users.SingleOrDefault(x => x.Name == username && x.Password == password);

            //user not found
            if (user == null)
            {
                return null;
            }

            //if user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appsetting.secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            return user;
        }  
        public bool IsUniqueUser(string username)
        {
            var user = _db.users.SingleOrDefault(x => x.Name == username);

            // return null if user not found
            if (user == null)
                return true;

            return false;
        }
        public Users register(string username, string password)
        {
            Users userObj = new Users()
            {
                Name = username,
                Password = password,
                Role = "Admin"
            };

            _db.users.Add(userObj);
            _db.SaveChanges();
            userObj.Password = "";
            return userObj;
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
