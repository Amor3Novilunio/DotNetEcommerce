using System.Text.Json;
using DataAccess.Repository.IRepository;
using IO = System.IO;
using DumpLogger;
using Ecommerce.Models;
using Ecommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductsController : Controller
{

    private readonly Logger _logger = new(loggerFolder: nameof(ProductsController));

    private readonly IUnitOfWork _unitOfWork;
    private readonly string _wwwRootPath;

    public ProductsController(IUnitOfWork db, IWebHostEnvironment iWebHostEnvironment)
    {
        _unitOfWork = db;
        _wwwRootPath = iWebHostEnvironment.WebRootPath;
    }
    [HttpGet("Index")]
    public IActionResult Index()
    {
        try
        {
            List<Product> objData = _unitOfWork.Product.ReadAll(includeProps: "Category").ToList();
            _logger.Read($"item : {JsonSerializer.Serialize(objData)} | successfully retrieved");
            return View(objData);
        }
        catch (System.Exception e)
        {
            _logger.Read($"Exception : {e}");
            return View();
        }
    }

    [HttpGet]
    public IActionResult Forms(int? id)
    {
        TempData.Keep("routeStatus");
        TempData["routeStatus"] = "Create";
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.ReadAll().Select(i => new SelectListItem
        {
            Text = i.Name,
            Value = i.Id.ToString()
        }
        );
        ProductVM productVM = new()
        {
            Product = new Product(),
            CategoryList = CategoryList
        };

        if (id != 0 && id != null)
        {
            Product objData = _unitOfWork.Product.ReadFirstOrDefault(i => i.Id == id);
            if (objData == null)
            {
                return NotFound();
            }
            productVM.Product = objData;
            TempData["routeStatus"] = "Update";
        }
        return View(productVM);
    }
    [HttpPost]
    public IActionResult Forms(ProductVM objData, IFormFile? file)
    {
        if (file is not null)
        {
            // fileStructure
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string productPath = Path.Combine(_wwwRootPath, @"media\products");
            if (!Directory.Exists(productPath))
            {
                Directory.CreateDirectory(productPath);
            }
            // fileSaving
            using var filestream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create);
            // delete if update
            if (!string.IsNullOrEmpty(objData.Product.ImageUrl))
            {
                IO.File.Delete(@$"{_wwwRootPath}\{objData.Product.ImageUrl}");
            }
            file.CopyTo(filestream);
            objData.Product.ImageUrl = @$"media\products\{fileName}";
        }

        if (!ModelState.IsValid)
        {
            ModelStateCheck(productVModel: objData.Product, Error: true);
        }
        else
        {
            switch (TempData["routeStatus"])
            {
                case "Update":
                    Update(objData.Product);
                    ModelStateCheck(productVModel: objData.Product, Error: false);
                    return RedirectToAction(nameof(Index));
                case "Create":
                    Create(objData.Product);
                    ModelStateCheck(productVModel: objData.Product, Error: false);
                    return RedirectToAction(nameof(Index));
                default:
                    ModelStateCheck(productVModel: objData.Product, Error: true);
                    break;
            }
        }
        return View(objData);
    }

    private void Create(Product objData)
    {
        _unitOfWork.Product.Create(objData);
        _unitOfWork.Save();
    }

    private void Update(Product objData)
    {
        _unitOfWork.Product.Update(objData);
        _unitOfWork.Save();
        ModelStateCheck(productVModel: objData, Error: false);
    }

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == 0 || id == null)
        {
            _logger.Delete($"item : {id}| not found");
            return NotFound();
        }

        Product? dataFound = _unitOfWork.Product.ReadFirstOrDefault(i => i.Id == id);
        if (dataFound == null)
        {
            _logger.Delete($"item : {JsonSerializer.Serialize(dataFound)} | not found");
            return NotFound();
        }

        string productPath = Path.Combine(_wwwRootPath, @"media\products");
        if (!string.IsNullOrEmpty(dataFound.ImageUrl) && Directory.Exists(productPath))
        {
            IO.File.Delete(@$"{_wwwRootPath}\{dataFound.ImageUrl}");
        }
        _unitOfWork.Product.Delete(dataFound);
        _unitOfWork.Save();
        TempData["Status"] = "Delete";
        TempData["Message"] = "Product Item Deleted Successfully";
        _logger.Delete($"item : {JsonSerializer.Serialize(dataFound)} | successfully deleted");
        return RedirectToAction(nameof(Index));
    }

    // Helper
    private void ModelStateCheck(Product productVModel, bool Error = false)
    {
        if (Error)
        {
            TempData["Status"] = "Error";
            TempData["Message"] = $"Failed to {TempData["routeStatus"]} Product Item";
            _logger.Update($"item : {JsonSerializer.Serialize(productVModel)} | {TempData["routeStatus"]} Failed");
        }
        else
        {
            TempData["Status"] = TempData["routeStatus"];
            TempData["Message"] = $"Product Item {TempData["routeStatus"]}ed Successfully";
            _logger.Create($"item : {JsonSerializer.Serialize(productVModel)} | successfully updated");
        }
    }

    // Region API CALLS
    public IActionResult Request_Get()
    {
        List<Product> objData = _unitOfWork.Product.ReadAll(includeProps: "Category").ToList();
        return Json(new { data = objData });
    }
    // End Region
}
