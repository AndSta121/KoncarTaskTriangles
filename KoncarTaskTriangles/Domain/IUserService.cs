using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq.Expressions;
using KoncarTaskTriangles.Models;
using Microsoft.AspNetCore.Mvc;

namespace KoncarTaskTriangles.Domain
{
    public interface IUserService
    { 
        int InsertUser(UserModel userModel);
        UserModel GetUserByFilter(Expression<Func<UserModel, bool>> filter);
        Task<LoginResponse?> GetLoginResponse(Expression<Func<UserModel, bool>> filter);
    }
}
