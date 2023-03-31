using CMS_Dashboard_v1.Models.ModelForm;
using CMS_Dashboard_v1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CMS_Dashboard_v1.Areas.LandingPage.Controllers
{
    [Area("LandingPage")]
    public class HabakuController : Controller
    {
        GlobalListApi _globallist = new GlobalListApi();

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var content = await _globallist.GetListContent();
            var section = await _globallist.GetListSection();
            var menu = await _globallist.GetListMenu();

            ViewData["Beranda"] = menu[0].menu_name;
            ViewData["Produk"] = menu[1].menu_name;
            ViewData["Artikel"] = menu[2].menu_name;
            ViewData["TentangKami"] = menu[3].menu_name;
            ViewData["HubungiKami"] = menu[4].menu_name;

            return View();
        }

        [Route("/produk")]
        public async Task<IActionResult> Produk()
        {
            var content = await _globallist.GetListContent();
            var section = await _globallist.GetListSection();
            var menu = await _globallist.GetListMenu();

            ViewData["Beranda"] = menu[0].menu_name;
            ViewData["Produk"] = menu[1].menu_name;
            ViewData["Artikel"] = menu[2].menu_name;
            ViewData["TentangKami"] = menu[3].menu_name;
            ViewData["HubungiKami"] = menu[4].menu_name;

            return View();
        }

        [Route("/artikel")]
        public async Task<IActionResult> Artikel()
        {
            var content = await _globallist.GetListContent();
            var section = await _globallist.GetListSection();
            var menu = await _globallist.GetListMenu();
            var Model = new Article();

            ViewData["Beranda"] = menu[0].menu_name;
            ViewData["Produk"] = menu[1].menu_name;
            ViewData["Artikel"] = menu[2].menu_name;
            ViewData["TentangKami"] = menu[3].menu_name;
            ViewData["HubungiKami"] = menu[4].menu_name;

            try
            {
                if (content != null && section != null && menu != null)
                {
                    Model.Artikel = (from a in content.Where(ss => ss.status).ToList()
                                   join b in section.Where(ss => ss.status && ss.section_id == 12).ToList() on a.section_id equals b.section_id
                                   join c in menu.Where(ss => ss.status).ToList() on b.menu_id equals c.menu_id
                                   select new Article
                                   {
                                       id = a.content_id.ToString(), // IdArtikel
                                       title = a.header, // Judul
                                       category = a.title, // Category
                                       published = a.created_at.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), // Tanggal Artikel Dibuat,
                                       desc = Regex.Replace(a.description, @"<[^>]*>|&nbsp;", ""), // Description
                                       image = a.image, // Sampul
                                       link_detail = "DetailArtikel/Read/?ArtikelKe=" + a.content_id
                                       //new string(Enumerable.Repeat("0123", a.content_id)
                                       //   .Select(s => s[new Random().Next(s.Length)]).Take(5).ToArray())
                                   }).ToList();
                }
            }
            catch (Exception e)
            {

            }
            return View(Model);
        }

        [Route("/tentangkami")]
        public async Task<IActionResult> TentangKami()
        {
            var content = await _globallist.GetListContent();
            var section = await _globallist.GetListSection();
            var menu = await _globallist.GetListMenu();

            ViewData["Beranda"] = menu[0].menu_name;
            ViewData["Produk"] = menu[1].menu_name;
            ViewData["Artikel"] = menu[2].menu_name;
            ViewData["TentangKami"] = menu[3].menu_name;
            ViewData["HubungiKami"] = menu[4].menu_name;

            return View();
        }

        [Route("/hubungikami")]
        public async Task<IActionResult> HubungiKami()
        {
            var content = await _globallist.GetListContent();
            var section = await _globallist.GetListSection();
            var menu = await _globallist.GetListMenu();

            ViewData["Beranda"] = menu[0].menu_name;
            ViewData["Produk"] = menu[1].menu_name;
            ViewData["Artikel"] = menu[2].menu_name;
            ViewData["TentangKami"] = menu[3].menu_name;
            ViewData["HubungiKami"] = menu[4].menu_name;

            return View();
        }

        [Route("/DetailArtikel/Read")]
        public async Task<IActionResult> DetailArtikel(string ArtikelKe)
        {
            var content = await _globallist.GetListContent();
            var section = await _globallist.GetListSection();
            var menu = await _globallist.GetListMenu();
            var Model = new Article();

            ViewData["Beranda"] = menu[0].menu_name;
            ViewData["Produk"] = menu[1].menu_name;
            ViewData["Artikel"] = menu[2].menu_name;
            ViewData["TentangKami"] = menu[3].menu_name;
            ViewData["HubungiKami"] = menu[4].menu_name;

            try
            {
                var artikel = Convert.ToInt64(ArtikelKe);
                if (content != null && section != null && menu != null)
                {
                    var artikelContent = content.Where(ss => ss.content_id == artikel).FirstOrDefault();

                    Model.id = artikelContent.content_id.ToString();
                    Model.title = artikelContent.header;
                    Model.category = artikelContent.title;
                    Model.published = artikelContent.created_at.ToString("dd MMMM yyyy", new CultureInfo("id-ID")); // Tanggal Artikel Dibuat;
                    Model.desc = artikelContent.description;
                    Model.image = artikelContent.image;
                    Model.link_detail = "?ArtikelKe=" + artikelContent.content_id;
                    //new string(Enumerable.Repeat("0123", artikelContent.content_id)
                    //   .Select(s => s[new Random().Next(s.Length)]).Take(5).ToArray());


                    Model.Artikel = (from a in content.Where(ss => ss.status).ToList()
                                     join b in section.Where(ss => ss.status && ss.section_id == 12).ToList() on a.section_id equals b.section_id
                                     join c in menu.Where(ss => ss.status).ToList() on b.menu_id equals c.menu_id
                                     //where a.content_id != artikel
                                     select new Article
                                     {
                                         id = a.content_id.ToString(), // IdArtikel
                                         title = a.header, // Judul
                                         category = a.title, // Category
                                         published = a.created_at.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), // Tanggal Artikel Dibuat,
                                         desc = Regex.Replace(a.description, @"<[^>]*>|&nbsp;", ""), // Description
                                         image = a.image, // Sampul
                                         link_detail = "?ArtikelKe=" + a.content_id
                                         //new string(Enumerable.Repeat("0123", a.content_id)
                                         //   .Select(s => s[new Random().Next(s.Length)]).Take(5).ToArray())
                                     }).ToList();
                }
            }
            catch (Exception e)
            {

            }

            return View(Model);
        }

        #region Get Artikel         


        [HttpGet]
        [Route("Get/GetArtikel")]
        public async Task<IActionResult> GetArtikel()
        {
            var content = await _globallist.GetListContent();
            var section = await _globallist.GetListSection();
            var menu = await _globallist.GetListMenu();
            var ModelArtike = new List<Article>();

            ViewData["Beranda"] = menu[0].menu_name;
            ViewData["Produk"] = menu[1].menu_name;
            ViewData["Artikel"] = menu[2].menu_name;
            ViewData["TentangKami"] = menu[3].menu_name;
            ViewData["HubungiKami"] = menu[4].menu_name;

            try
            {
                if (content != null && section != null && menu != null)
                {
                    string randomString = new string(Enumerable.Repeat("0123456789", 6)
                                          .Select(s => s[new Random().Next(s.Length)]).ToArray());
                    ModelArtike = (from a in content.Where(ss => ss.status).ToList()
                                   join b in section.Where(ss => ss.status && ss.section_id == 12).ToList() on a.section_id equals b.section_id
                                   join c in menu.Where(ss => ss.status).ToList() on b.menu_id equals c.menu_id
                                   select new Article
                                   {
                                       id = a.content_id.ToString(), // IdArtikel
                                       title = a.header, // Judul
                                       category = a.title, // Category
                                       published = a.created_at.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), // Tanggal Artikel Dibuat,
                                       desc = a.description, // Description
                                       image = a.image, // Sampul
                                       link_detail = "DetailArtikel/Read/?ArtikelKe"+
                                       new string(Enumerable.Repeat("0123456789", a.content_id)
                                          .Select(s => s[new Random().Next(s.Length)]).ToArray())
                                   }).ToList();
                }
            }
            catch (Exception e)
            {

            }

            return Json(ModelArtike);
        }

        #endregion

    }
}
