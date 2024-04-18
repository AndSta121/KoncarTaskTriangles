using KoncarTaskTriangles.Models;

namespace KoncarTaskTriangles.Repository
{
    public interface ITriangleRepository
    {
        void InsertTriangle(TriangleModel triangleModel);
        void UpdateTriangle(TriangleModel triangleModel);
        void DeleteTriangle(int triangleId);
        Task<List<TriangleModel>> GetTrianglesByUserId(int id);
        Task<TriangleModel> GetTriangleById(int id);
    }
}
