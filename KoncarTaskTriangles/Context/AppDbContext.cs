using KoncarTaskTriangles.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KoncarTaskTriangles.Context
{
    public class AppDbContext: DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public DbSet<TriangleModel> Triangles { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
