﻿namespace KoncarTaskTriangles.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<TriangleModel> Triangles { get; set; }
    }
}
