using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PetShop.BLL.DTO;
using PetShop.BLL.Interfaces;
using PetShop.WebUI.Models;
using System.Web;
using System.Web.Script.Serialization;

namespace PetShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IOrderService _orderService;

        public HomeController(IOrderService service)
        {
            _orderService = service;
        }
        // GET: Home
        public ActionResult Products()
        {
            var products = Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductViewModel>>(_orderService.GetProducts());
            var cookie = Request.Cookies["Order"];
            if (cookie == null || cookie.Value == "")
            CreateCookie();
            return View(products);
        }
        /// <summary>
        /// Creates cookie for user's order.
        /// </summary>
        private void CreateCookie()
        {
            var order = new OrderViewModel()
            {
                Products = new List<ProductViewModel>()
            };
            var m = SerializeToJson(order);
            var cookie = new HttpCookie("Order")
            {
                Expires = DateTime.Now.AddDays(1),
                Value = m
            };
            Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// Seriliazes object of type T to json string.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="model">Seriliazing ojbect.</param>
        /// <returns></returns>
        private string SerializeToJson<T>(T model)
        {
            var jss = new JavaScriptSerializer();
            string json = jss.Serialize(model);
            return json;
        }
        /// <summary>
        /// Deserilizes json string to object type T.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="json">Json string</param>
        /// <returns></returns>
        public T DeserilizeToModel<T>(string json)
        {
            return (new JavaScriptSerializer().Deserialize<T>(json));
        }
        public ActionResult Busket(int? id)
        {
            var cookie = Request.Cookies["Order"];
            OrderViewModel order = null;
            if (cookie != null && cookie.Value == "")
            {
                CreateCookie();
                return View();
            }
            var json = cookie.Value;
            order = DeserilizeToModel<OrderViewModel>(json);
            if (id != null)
            {
                var product = Mapper.Map<ProductDTO, ProductViewModel>(_orderService.GetProduct(id));
                if(!order.Products.Exists(p=> p.Title == product.Title))
                order.Products.Add(product);
            }
            cookie.Value = SerializeToJson(order);
            Response.Cookies.Add(cookie);
            ViewBag.Price = order.Products.Sum(x => x.PricePerItem);
            return View(order.Products);
        }

        public ActionResult RemoveProductFromOrder(int id)
        {
            var cookie = Request.Cookies["Order"];
            var json = cookie.Value;
            var order = DeserilizeToModel<OrderViewModel>(json);
            var product = order.Products.Find(x => x.Id == id);
            order.Products.Remove(product);
            cookie.Value = SerializeToJson(order);
            Response.Cookies.Add(cookie);
            //return PartialView("_ChengedOrder",order.Products);
            return RedirectToAction("Busket");
        }

        public ActionResult CompletePurchases()
        {
            var cookie = Request.Cookies["Order"];
            var json = cookie.Value;
            var order = DeserilizeToModel<OrderViewModel>(json);

            var nOrder = Mapper.Map<OrderViewModel, OrderDTO>(order);
            if (order.Products.Any())
            {
                _orderService.MakeOrder(nOrder);
                cookie.Value = "";
                CreateCookie();
            }
            return View();
        }
    }
}