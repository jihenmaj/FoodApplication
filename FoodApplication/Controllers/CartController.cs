
using FoodApplication.Models;
using Microsoft.AspNetCore.Mvc;
using FoodApplication.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.X509Certificates;
namespace FoodApplication.Controllers
{
	[Authorize]
	public class CartController : Controller
	{

		private readonly IData data;
		private readonly FoodApplicationDBContext context;
		public CartController(IData data, FoodApplicationDBContext context)
		{
			this.data = data;
			this.context = context;
		}

		public async Task<ActionResult> Index()
		{
			var user = data.GetUser(HttpContext.User);
			var cartList = context.Carts.Where(c => c.UserId == user.Id).ToList();
			return View(cartList);
		}

		[HttpPost]
		public async Task<IActionResult> SaveCart(Cart cart)
		{
			var user = await data.GetUser(HttpContext.User);
			cart.UserId = user?.Id;
			if (ModelState.IsValid)
			{
				context.Carts.Add(cart);
				context.SaveChanges();
				return Ok();
			}
			return BadRequest();
		}
		[HttpGet]
		public IActionResult GetaddedCarts()
		{
			var user = data.GetUser(HttpContext.User);
			var carts = context.Carts.Where(context => c.UserId == user.Id).Select(c => c.RecipeId).ToList();
			return Ok(carts);
		}
		[HttpPost]
		public IActionResult RemoveCartFromList(string Id)
		{
			if (!string.IsNullOrEmpty(Id))
			{
				var cart = context.Carts.Where(c => c.RecipeId == Id).FirstOrDefault();
				if (cart != null)
				{
					context.Carts.Remove(cart);
					context.SaveChanges();
					return Ok();
				}
				return BadRequest();
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetCartList()
		{
			var user = await data.GetUser(HttpContext.User);
			var cartlist = context.Carts.Where(context => context.UserId == user?.Id).Take(3).ToList();
			return PartialView("_CartList",cartList);
		}
	
	}
}