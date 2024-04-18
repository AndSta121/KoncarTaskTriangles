using KoncarTaskTriangles.Context;
using KoncarTaskTriangles.Models;
using Microsoft.EntityFrameworkCore;

namespace KoncarTaskTriangles.Repository
{
    public class TriangleRepository: ITriangleRepository
    {
        private readonly AppDbContext _context;

        public TriangleRepository(AppDbContext context) 
        {  
            _context = context; 
        }

        public void InsertTriangle(TriangleModel triangleModel)
        {
            if (triangleModel == null) throw new ArgumentNullException("triangle");
            _context.Triangles.Add(triangleModel);
            _context.SaveChanges();
        }

        public void UpdateTriangle(TriangleModel triangleModel)
        {
            if (triangleModel == null) throw new ArgumentNullException("triangle");
            var triangle = _context.Triangles.Where(t => t.Id == triangleModel.Id).FirstOrDefault();
            
            triangle.Ax = triangleModel.Ax;
            triangle.Ay = triangleModel.Ay;
            triangle.Bx = triangleModel.Bx;
            triangle.By = triangleModel.By;
            triangle.Cx = triangleModel.Cx;
            triangle.Cy = triangleModel.Cy;
            triangle.Perimeter = triangleModel.Perimeter;
            triangle.Area = triangleModel.Area;
            triangle.TypeByAngle = triangleModel.TypeByAngle;
            triangle.TypeBySideLength = triangleModel.TypeBySideLength;
                
            _context.SaveChanges();
        }

		public void DeleteTriangle(int triangleId)
        {
            var triangle = _context.Triangles.Where(t => t.Id == triangleId).FirstOrDefault();
			if (triangle == null) throw new ArgumentNullException("triangle");
			_context.Triangles.Remove(triangle);
            _context.SaveChanges();
        }
		public async Task<List<TriangleModel>> GetTrianglesByUserId(int id)
        {
            var triangles = await _context.Triangles.Where
                         (triangle => triangle.User.Id == id).ToListAsync();

            return triangles;
        }

        public async Task<TriangleModel> GetTriangleById(int id)
        {
            var triangle = await _context.Triangles.Include(u => u.User).Where
                (t => t.Id == id).FirstAsync();
            if(triangle == null) throw new ArgumentNullException("triangle");
            else triangle.User = _context.Users.Where(u => u.Id == triangle.UserId).FirstOrDefault(); 
            return triangle;
        }
    }
}
