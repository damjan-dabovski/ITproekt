using ITproekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITproekt.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            List<CartItem> model = new List<CartItem>();
            if(Session["cart"] != null) {
                model = Session["cart"] as List<CartItem>;
            }
            return View(model);
        }

        // POST: ShoppingCart/AddToCart
        [HttpPost]
        public ActionResult AddToCart(int productId)
        {
            List<CartItem> cartProducts = new List<CartItem>();
            var product = db.Products.FirstOrDefault(prod => prod.ID == productId);

            if(product != null) {
                var itemToAdd = new CartItem { Product = product, Quantity = 1 };
                if (Session["cart"] != null) {
                    cartProducts = Session["cart"] as List<CartItem>;
                }

                if (cartProducts.FirstOrDefault(prod => prod.Product.ID == product.ID) != null) {
                    cartProducts.Find(prod => prod.Product.ID == product.ID).Quantity += 1;
                } else {
                    cartProducts.Add(itemToAdd);
                }

                Session["cart"] = cartProducts;
            }
            return RedirectToAction("Index", "Products");
        }

        // POST: ShoppingCart/SetQuantity
        [HttpPost]
        public ActionResult SetQuantity(int productId, int quantity)
        {
            var cartItems = Session["cart"] as List<CartItem>;
            if (cartItems != null) {
                var targetItem = cartItems.FirstOrDefault(item => item.Product.ID == productId);
                if (targetItem != null) {
                    targetItem.Quantity = quantity;
                    if(targetItem.Quantity < 1) {
                        cartItems.Remove(targetItem);
                    }
                }
            }
            return RedirectToAction("Index", "ShoppingCart");
        }

        // POST: ShoppingCart/Remove/5
        [HttpPost]
        public ActionResult Remove(int productId)
        {
            var cartItems = Session["cart"] as List<CartItem>;
            if(cartItems != null) {
                cartItems.Remove(cartItems.FirstOrDefault(item => item.Product.ID == productId));
            }
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
