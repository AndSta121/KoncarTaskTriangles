using KoncarTaskTriangles.Domain;
using KoncarTaskTriangles.Models;
using KoncarTaskTriangles.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace KoncarTaskTriangles.Controllers
{
    //[Authorize]
    public class TriangleController : Controller
    {
        private readonly ITriangleService _triangleService;
		private readonly IUserService _userService;

        public TriangleController(ITriangleService triangleService, IUserService userService)
        {
            _triangleService = triangleService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int logedUserId)
        {
            ViewBag.LoggedUserId = logedUserId;
            var triangles = await _triangleService.GetTrianglesByUserId(logedUserId);
            return View(triangles);
        }

		[HttpGet]
		public IActionResult Add(int userId)
		{
			ViewBag.userId = userId;
			return View();
		}

		[HttpPost]
		public IActionResult Add(int userId, [FromForm]TriangleViewModel triangleViewModel)
		{
			if (ModelState.IsValid)
			{
				double perimeter = _triangleService.CalculateArea(triangleViewModel.Ax, triangleViewModel.Ay, triangleViewModel.Bx, triangleViewModel.By, triangleViewModel.Cx, triangleViewModel.Cy);
				if (perimeter == 0)
				{
					ModelState.AddModelError("", "The entered points do not form a triangle.");
				}
				else 
				{
                    var triangle = new TriangleModel()
                    {
                        Ax = triangleViewModel.Ax,
                        Ay = triangleViewModel.Ay,
                        Bx = triangleViewModel.Bx,
                        By = triangleViewModel.By,
                        Cx = triangleViewModel.Cx,
                        Cy = triangleViewModel.Cy,
                        User = _userService.GetUserByFilter(i => i.Id == userId)
                    };
                    _triangleService.InsertTriangle(triangle);
					return RedirectToAction("Index", "Triangle", new { logedUserId = userId });
				}
			}
			else 
			{
				ModelState.AddModelError("", "Add form data is not valid.");
			}
			return View(triangleViewModel);
		}

        [HttpGet]
        public async Task<IActionResult> Edit(int triangleId)
        {
            var triangle= await _triangleService.GetTriangleById(triangleId);
            ViewBag.userId=triangle.UserId;
			ViewBag.Ax = triangle.Ax;
			ViewBag.Ay = triangle.Ay;
            ViewBag.Bx = triangle.Bx;
            ViewBag.By = triangle.By;
            ViewBag.Cx = triangle.Cx;
            ViewBag.Cy = triangle.Cy;
            ViewBag.triangleId=triangleId;
			return View();
        }

        [HttpPost]
        public IActionResult Update(int userId, int triangleId, [FromForm] TriangleViewModel updatedTriangle)
        {
            if(ModelState.IsValid) {
                double perimeter = _triangleService.CalculateArea(updatedTriangle.Ax, updatedTriangle.Ay, updatedTriangle.Bx, updatedTriangle.By,updatedTriangle.Cx, updatedTriangle.Cy);
                if (perimeter == 0)
                {
                    ModelState.AddModelError("", "The entered points do not form a triangle.");
                }
                else
                {
                    var triangleModel = new TriangleModel
                    {
                        Id = triangleId,
                        Ax = updatedTriangle.Ax,
                        Ay = updatedTriangle.Ay,
                        Bx = updatedTriangle.Bx,
                        By = updatedTriangle.By,
                        Cx = updatedTriangle.Cx,
                        Cy = updatedTriangle.Cy,
                    };
                    _triangleService.UpdateTriangle(triangleModel);
                    return RedirectToAction("Index", new { logedUserId = userId });
                }
            }
            else
            {
                ModelState.AddModelError("", "Edit form data is not valid.");
            }
            
            ViewBag.userId = userId;
            ViewBag.triangleId = triangleId;
			return View("Edit",updatedTriangle);
		}

        [HttpGet]
        public IActionResult Delete(int userId, int triangleId) {
            _triangleService.DeleteTriangle(triangleId);
			return RedirectToAction("Index", new { logedUserId = userId });
		}
    }
}
