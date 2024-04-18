using KoncarTaskTriangles.Helpers;
using KoncarTaskTriangles.Models;
using KoncarTaskTriangles.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace KoncarTaskTriangles.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int InsertUser(UserModel registerModel)
        {
            return _userRepository.Insert(registerModel);
        }

        public UserModel GetUserByFilter(Expression<Func<UserModel, bool>> filter)
        {
            return _userRepository.GetUserByFilter(filter);
        }

        public async Task<LoginResponse?> GetLoginResponse(Expression<Func<UserModel, bool>> filter) 
        {
            var user = _userRepository.GetUserByFilter(filter);
            if(user != null) 
            {
                var token = await Task.Run(() => TokenHelper.GenerateToken(user));
                return new LoginResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = token,
                    Triangles = user.Triangles
                };
            }
            return null;
        }
    }
}
