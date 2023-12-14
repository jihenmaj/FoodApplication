using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FoodApplication.Repository
{
	public class Data : IData

	{
		private readonly UserManager<ApplicationUser> _userManager;
		public Data(UserManager<ApplicationUser> userManager)
		{
			_userManager = manager;
		}

		public async Task<ApplicationUser> GetUser(ClaimsPrincipal claims)
		{
			return await _userManagerg.GetUserAsync(claims);
		}
	}
}
