using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetShop.BLL.DTO;
using PetShop.BLL.Interfaces;
using PetShop.Domain.Interfaces;
using PetShop.WebUI.Models;


namespace PetShop.WebUI.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        private IOrderService _orderService;

        public SellerController(IOrderService service)
        {
            _orderService = service;
        }
        // GET: Seller
        public ActionResult Index()
        {
            var products = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductViewModel>>(_orderService.GetProducts());
            return View(products);
        }

        public ActionResult Edit(int? id)
        {
            var product = Mapper.Map<ProductDTO, ProductViewModel>(_orderService.GetProduct(id));
            if (product != null)
            {
                return View(product);
            }
            ViewBag.Error = $"Not found product with id {id}.";
            return View("Error");
        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel model, HttpPostedFileBase uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadedFile == null)
                {
                    ModelState.AddModelError("", "Doesn't correct e-mail or password");
                    return View("Edit",model.Id);
                }
                var fileName = System.IO.Path.GetFileName(uploadedFile.FileName);
                uploadedFile.SaveAs(Server.MapPath("~/Content/PetsImages/" +fileName));
                
                var t = model.Image;
                model.Image = "~/Content/PetsImages/" + uploadedFile.FileName;
                if (t != null)
                {
                    var fullPath = Server.MapPath(t);
                    System.IO.File.Delete(fullPath);
                }
                _orderService.UpdateProduct(Mapper.Map<ProductViewModel, ProductDTO>(model));
                return RedirectToAction("Index");
            }
            
            return View(model.Id);
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var fullPath = Server.MapPath(_orderService.GetProduct(id).Image);
                System.IO.File.Delete(fullPath);
                _orderService.DeleteProduct(id.Value);
                return View("Index");
            }
            ViewBag.Error = $"Product's id doesn't set.";
            return View("Error");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model, HttpPostedFileBase uploadedFile)
        {
            if (ModelState.IsValid)
            {

                if (uploadedFile != null)
                {
                    var fileName = System.IO.Path.GetFileName(uploadedFile.FileName);
                    uploadedFile.SaveAs(Server.MapPath("~/Content/PetsImages/" + fileName));
                    model.Image = "~/Content/PetsImages/" + uploadedFile.FileName;
                    _orderService.CreateProduct(Mapper.Map<ProductViewModel,ProductDTO>(model));
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Images doesn't choose.");
            }
            return View();
        }
    }
}