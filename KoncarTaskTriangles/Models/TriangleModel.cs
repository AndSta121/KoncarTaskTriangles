using System.Text.Json.Serialization;

namespace KoncarTaskTriangles.Models
{
    public class TriangleModel
    {
        public int Id { get; set; }
        public int Ax { get; set; }
        public int Ay { get; set; }
        public int Bx { get; set; }
        public int By { get; set; }
        public int Cx { get; set; }
        public int Cy { get; set; }
        public double Perimeter { get; set; }
        public double Area { get; set; }
        public string TypeByAngle { get; set; }
        public string TypeBySideLength { get; set; }
        public int UserId {  get; set; }
        [JsonIgnore]
        public UserModel User { get; set; }
    }
}
