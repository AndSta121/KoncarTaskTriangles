using KoncarTaskTriangles.Models;
using KoncarTaskTriangles.Models.ViewModels;
using System.Linq.Expressions;

namespace KoncarTaskTriangles.Domain
{
    public interface ITriangleService
    {
        void InsertTriangle(TriangleModel triangleModel);
        void UpdateTriangle(TriangleModel triangleModel);
        void DeleteTriangle(int triangleId);
        Task<List<TriangleModel>> GetTrianglesByUserId(int id);
        Task<TriangleModel> GetTriangleById(int id);
        void CalculateTriangleProperties(TriangleModel triangleModel);
        double CalculatePerimeter(int Ax, int Ay, int Bx, int By, int Cx, int Cy);
        double CalculateArea(int Ax, int Ay, int Bx, int By, int Cx, int Cy);
        string DetermineTypeBySideLength(int Ax, int Ay, int Bx, int By, int Cx, int Cy);
        string DeterimneTypeByAngle(int Ax, int Ay, int Bx, int By, int Cx, int Cy);
        double CalculateSideLength(int Ax, int Ay, int Bx, int By);
        double ConvertAngleToDegres(double angle);
    }
}
