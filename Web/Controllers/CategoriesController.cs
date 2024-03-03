using Microsoft.AspNetCore.Mvc;
using DumpLogger;
using Ecommerce.DataAccess.ApplicationDbContext;
using Ecommerce.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly Logger _logger = new(loggerFolder: nameof(CategoriesController));

        public CategoriesController(ApplicationDbContext db) => _db = db;

        public IActionResult Index()
        {
            try
            {
                List<Category> objData = _db.Categories.ToList();
                _logger.Read($"item : {JsonSerializer.Serialize(objData)} | successfully retrieved");
                return View(objData);
            }
            catch (System.Exception e)
            {
                _logger.Read($"Exception : {e}");
                return View();
            }
        }
        public IActionResult Forms()
        {
            TempData.Keep("routeStatus");
            switch (TempData["routeStatus"])
            {
                case "Update":
                    TempData.Keep("formData");
                    return View(JsonSerializer.Deserialize<Category>(TempData["formData"]!.ToString()!));
                default:
                    return View();
            }
        }

        public IActionResult Create()
        {
            TempData["routeStatus"] = nameof(Create);
            return RedirectToAction("Forms");
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category? objData = _db.Categories.Find(id);
            if (objData == null)
            {
                return NotFound();
            }

            TempData["routeStatus"] = nameof(Update);
            TempData["formData"] = JsonSerializer.Serialize(objData);
            return RedirectToAction("Forms");
        }

        [HttpPost]
        public IActionResult Create(Category objData)
        {
            if (!ModelState.IsValid)
            {
                TempData["Status"] = "Error";
                TempData["Message"] = "Failed to update Category Item";
                _logger.Create($"item : {JsonSerializer.Serialize(objData)} | update Failed");
                return RedirectToAction(nameof(Index));

            }
            _db.Categories.Add(objData);
            _db.SaveChanges();
            TempData["Status"] = "Create";
            TempData["Message"] = "Category Created Successfully";
            _logger.Create($"item : {JsonSerializer.Serialize(objData)} | successfully created");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Update(Category objData)
        {
            if (!ModelState.IsValid)
            {
                TempData["Status"] = "Error";
                TempData["Message"] = "Failed to update Category Item";
                _logger.Update($"item : {JsonSerializer.Serialize(objData)} | update Failed");
                return RedirectToAction(nameof(Index));
            }

            _db.Categories.Update(objData);
            _db.SaveChanges();
            TempData["Status"] = "Update";
            TempData["Message"] = "Category Item Updated Successfully";
            _logger.Update($"item : {JsonSerializer.Serialize(objData)} | successfully updated");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                _logger.Delete($"item : {id}| not found");
                return NotFound();
            }
            Category? categoryDataFound = _db.Categories.Find(id);
            if (categoryDataFound == null)
            {
                _logger.Delete($"item : {JsonSerializer.Serialize(categoryDataFound)} | not found");
                return NotFound();
            }

            _db.Categories.Remove(categoryDataFound);
            _db.SaveChanges();
            TempData["Status"] = "Delete";
            TempData["Message"] = "Category Item Deleted Successfully";
            _logger.Delete($"item : {JsonSerializer.Serialize(categoryDataFound)} | successfully deleted");
            return RedirectToAction(nameof(Index));
        }

    }
}