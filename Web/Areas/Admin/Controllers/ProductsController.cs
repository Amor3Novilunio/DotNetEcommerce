using System.Text.Json;
using DataAccess.Repository.IRepository;
using DumpLogger;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductsController : Controller
{

    private readonly Logger _logger = new(loggerFolder: nameof(ProductsController));

    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IUnitOfWork db) => _unitOfWork = db;
    public IActionResult Index()
    {
        try
        {
            List<Product> objData = _unitOfWork.Product.ReadAll().ToList();
            _logger.Read($"item : {JsonSerializer.Serialize(objData)} | successfully retrieved");
            return View(objData);
        }
        catch (System.Exception e)
        {
            _logger.Read($"Exception : {e}");
            return View();
        }
    }

    public IActionResult Forms(){
        TempData.Keep("routeStatus");
        switch (TempData["routeStatus"])
        {
            case "Update":
                TempData.Keep("formData");
                return View(JsonSerializer.Deserialize<Product>(TempData["formData"]!.ToString()!));
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

        Product? objData = _unitOfWork.Product.ReadFirstOrDefault(i => i.Id == id);
        if (objData == null)
        {
            return NotFound();
        }

        TempData["routeStatus"] = nameof(Update);
        TempData["formData"] = JsonSerializer.Serialize(objData);
        return RedirectToAction("Forms");
    }

    [HttpPost]
    public IActionResult Create(Product objData)
    {
        if (!ModelState.IsValid)
        {
            TempData["Status"] = "Error";
            TempData["Message"] = "Failed to update Product Item";
            _logger.Create($"item : {JsonSerializer.Serialize(objData)} | update Failed");
            return RedirectToAction(nameof(Index));

        }
        _unitOfWork.Product.Create(objData);
        _unitOfWork.Save();
        TempData["Status"] = "Create";
        TempData["Message"] = "Product Created Successfully";
        _logger.Create($"item : {JsonSerializer.Serialize(objData)} | successfully created");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Update(Product objData)
    {
        if (!ModelState.IsValid)
        {
            TempData["Status"] = "Error";
            TempData["Message"] = "Failed to update Product Item";
            _logger.Update($"item : {JsonSerializer.Serialize(objData)} | update Failed");
            return RedirectToAction(nameof(Index));
        }

        _unitOfWork.Product.Update(objData);
        _unitOfWork.Save();
        TempData["Status"] = "Update";
        TempData["Message"] = "Product Item Updated Successfully";
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

        Product? dataFound = _unitOfWork.Product.ReadFirstOrDefault(i => i.Id == id);
        if (dataFound == null)
        {
            _logger.Delete($"item : {JsonSerializer.Serialize(dataFound)} | not found");
            return NotFound();
        }

        _unitOfWork.Product.Delete(dataFound);
        _unitOfWork.Save();
        TempData["Status"] = "Delete";
        TempData["Message"] = "Product Item Deleted Successfully";
        _logger.Delete($"item : {JsonSerializer.Serialize(dataFound)} | successfully deleted");
        return RedirectToAction(nameof(Index));
    }
}
