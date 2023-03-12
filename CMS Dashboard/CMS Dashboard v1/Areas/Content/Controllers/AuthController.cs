using CMS_Dashboard_v1.Models;
using CMS_Dashboard_v1.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;
using CMS_Dashboard_v1.Models.ModelModule;
using System.Collections.Generic;
using System.Data;

namespace CMS_Dashboard_v1.Areas.Content.Controllers
{
    [Area("Content")]
    public class AuthController : Controller
    {
        readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Login")]
        public async Task<IActionResult> Login(LoginModel model)
            {
            MasterDataService _masterDataService = new MasterDataService();

            if (model.Username == null)
                ModelState.AddModelError("Username", "Email tidak boleh kosong");
            if (model.Password == null)
                ModelState.AddModelError("Password", "Password tidak boleh kosong");

            var List = new List<UserModel>();

            try
            {
                if (ModelState.IsValid)
                {
                    var data = new BaseResponse<List<UserModel>>();
                    var baseadd = _configuration.GetValue<string>("Api-CMS:BaseAddress");
                    var enpoint = _configuration.GetValue<string>("Api-CMS:User");
                    var response = await _masterDataService.GetAsync(baseadd+ enpoint);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        data = JsonConvert.DeserializeObject<BaseResponse<List<UserModel>>>(jsonString);
                        List = data.data;

                        if (List.Any(ss => ss.user_name == model.Username && ss.pasasword == model.Password))
                        {
                            var user = (from a in List.Where(ss =>
                                        ss.user_name == model.Username &&
                                        ss.pasasword == model.Password)
                                        select new LoginModel
                                        {
                                            Id = a.user_id,
                                            Username = a.user_name,
                                            Password = a.pasasword,
                                            Role = a.role
                                        }).FirstOrDefault();

                            // Pembentukan session dan cookies
                            CreateSession(user);

                            TempData["success"] = "success";
                            TempData["message"] = "Selamat Datang, " + user.Username;

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("Message", "Pastikan Email dan Password anda benar !!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Message", "Problem Server!, Pastikan Internet Anda Aman !!");
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Message", "error " + e.Message);
            }
            return View("Login",model);
        }
        public void CreateSession(LoginModel Data)
        {
            SaveTokenToCookie(Data);
            HttpContext.Session.SetString("Id", Data.Id.ToString());
            HttpContext.Session.SetString("Username", Data.Username);
            HttpContext.Session.SetString("Role", Data.Role);
        }
        private async Task<IActionResult> SaveTokenToCookie(LoginModel Data, string returdssnUrl = null)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Data.Username)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return LocalRedirect(returdssnUrl ?? "/");

        }

        [Route("Auth/Logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            string nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

    }
}
