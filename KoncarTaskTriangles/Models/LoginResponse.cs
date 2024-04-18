namespace KoncarTaskTriangles.Models
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<TriangleModel> Triangles { get; set; }
        public string Token { get; set; }
    }
}
