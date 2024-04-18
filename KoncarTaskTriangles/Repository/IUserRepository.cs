using KoncarTaskTriangles.Models;
using KoncarTaskTriangles.Models.ViewModels;
using System.Linq.Expressions;

namespace KoncarTaskTriangles.Repository
{
    public interface IUserRepository
    {
        int Insert(UserModel registerModel);
        UserModel GetUserByFilter(Expression<Func<UserModel, bool>> filter);
        //Task Login(object userLogin);
    }
}
