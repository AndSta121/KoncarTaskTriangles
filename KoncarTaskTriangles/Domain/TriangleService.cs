using KoncarTaskTriangles.Models;
using KoncarTaskTriangles.Models.ViewModels;
using KoncarTaskTriangles.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;

namespace KoncarTaskTriangles.Domain
{
    public class TriangleService : ITriangleService
    {
        private readonly ITriangleRepository _triangleRepository;

        public TriangleService(ITriangleRepository triangleRepository)
        {
            _triangleRepository = triangleRepository;
        }

        public void InsertTriangle(TriangleModel triangleModel)
        {
            CalculateTriangleProperties(triangleModel);
            _triangleRepository.InsertTriangle(triangleModel);
        }

        public void UpdateTriangle(TriangleModel triangleModel)
        {
            CalculateTriangleProperties(triangleModel);
            _triangleRepository.UpdateTriangle(triangleModel);
        }
        public void DeleteTriangle(int triangleId)
        {
            _triangleRepository.DeleteTriangle(triangleId);
        }

		public async Task<List<TriangleModel>> GetTrianglesByUserId(int id)
        {
            return await _triangleRepository.GetTrianglesByUserId(id);
        }

        public async Task<TriangleModel> GetTriangleById(int id)
        {
            return await _triangleRepository.GetTriangleById(id);
        }

        public void CalculateTriangleProperties(TriangleModel triangleModel)
        {
            triangleModel.Perimeter=CalculatePerimeter(triangleModel.Ax, triangleModel.Ay, triangleModel.Bx, triangleModel.By, triangleModel.Cx, triangleModel.Cy);
            triangleModel.Area=CalculateArea(triangleModel.Ax, triangleModel.Ay, triangleModel.Bx, triangleModel.By, triangleModel.Cx, triangleModel.Cy);
            triangleModel.TypeBySideLength=DetermineTypeBySideLength(triangleModel.Ax, triangleModel.Ay, triangleModel.Bx, triangleModel.By, triangleModel.Cx, triangleModel.Cy);
            triangleModel.TypeByAngle=DeterimneTypeByAngle(triangleModel.Ax, triangleModel.Ay, triangleModel.Bx, triangleModel.By, triangleModel.Cx, triangleModel.Cy);
        }
        public double CalculatePerimeter(int Ax, int Ay, int Bx, int By, int Cx, int Cy) 
        {
            double perimiter = CalculateSideLength(Ax, Ay, Bx, By) //Side AB
                + CalculateSideLength(Ax, Ay, Cx, Cy)  //Side AC
                + CalculateSideLength(Bx, By, Cx, Cy); //Side BC
            return perimiter;
        }
        public double CalculateArea(int Ax, int Ay, int Bx, int By, int Cx, int Cy) 
        {
            double area =(double) Math.Abs(Ax * (By - Cy)
                + Bx * (Cy - Ay) 
                + Cx * (Ay - By)) / 2;
            return area;
        }

        public string DetermineTypeBySideLength(int Ax, int Ay, int Bx, int By, int Cx, int Cy)
        {
            double a = CalculateSideLength(Bx, By, Cx, Cy);
            double b = CalculateSideLength(Ax, Ay, Cx, Cy);
            double c = CalculateSideLength(Ax, Ay, Bx, By);

            if ((a == b) && (a == c))
            {
                return "Equilateral triangle";
            }
            else if ((a == b) || (a == c) || (b == c))
            {
                return "Isosceles triangle";
            }
            else 
            {
                return "Multilateral triangle";
            }
        }

        public string DeterimneTypeByAngle(int Ax, int Ay, int Bx, int By, int Cx, int Cy)
        {
            double a = CalculateSideLength(Bx, By, Cx, Cy);
            double b = CalculateSideLength(Ax, Ay, Cx, Cy);
            double c = CalculateSideLength(Ax, Ay, Bx, By);

            double alfa = Math.Acos((Math.Pow(b,2) + Math.Pow(c,2) - Math.Pow(a,2) / (2 * b * c)));
            double beta = Math.Acos((Math.Pow(a, 2) + Math.Pow(c, 2) - Math.Pow(b, 2) / (2 * a * c)));
            double gama = Math.Acos((Math.Pow(a, 2) + Math.Pow(b, 2) - Math.Pow(c, 2) / (2 * a * b)));

            alfa=ConvertAngleToDegres(alfa);
            beta=ConvertAngleToDegres(beta);
            gama=ConvertAngleToDegres(gama);

            if ((alfa > 90) || (beta > 90) || (gama > 90)) 
            {
                return "Obtuse triangle";
            }
            else if ((alfa == 90) || (beta == 90) || (gama == 90))
            {
                return "Right triangle";
            }
            else
            {
                return "Acute triangle";
            }
        }

        public double CalculateSideLength(int Ax, int Ay, int Bx, int By)
        {
            double sideLength = Math.Sqrt(Math.Pow((Ax - Bx),2) + Math.Pow((Ay - By), 2));
            return sideLength;
        }

        public double ConvertAngleToDegres(double angle)
        {
            return angle * 180 / Math.PI;
        }

    }
}
