using KoncarTaskTriangles.Context;
using KoncarTaskTriangles.Models;
using KoncarTaskTriangles.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq.Expressions;

namespace KoncarTaskTriangles.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<UserModel> _users;

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _users= context.Set<UserModel>();
        }

        public int Insert(UserModel userModel)
        {
            if (userModel == null) throw new ArgumentNullException("registerModel");
            _users.Add(userModel);
            _context.SaveChanges();
            return userModel.Id;
        }
        public UserModel GetUserByFilter(Expression<Func<UserModel, bool>> filter) {
            return _users.SingleOrDefault(filter);
        }
    }
}
