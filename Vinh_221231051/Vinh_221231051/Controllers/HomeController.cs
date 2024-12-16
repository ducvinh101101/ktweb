using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Vinh_221231051.Models;
using Vinh_221231051.ViewModels;

namespace Vinh_221231051.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly QLHangHoaContext _context = new QLHangHoaContext();
        private int pageSize = 3;
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
        
        public IActionResult Index()
		{
            ViewData["TotalPages"] = _context.HangHoas.Where(m => m.Gia >= 100).Count()/pageSize+1;
            ViewData["CurrentPage"] = 1;
			var products = _context.HangHoas.Where(m => m.Gia >= 100).Skip((1 - 1) * pageSize).Take(pageSize).ToList();
			return View(products);
		}
        public IActionResult GetPage(int page = 1)
        {
            var totalItems = _context.HangHoas.Where(m => m.Gia >= 100).Count();
            var products = _context.HangHoas.Where(m => m.Gia >= 100).Skip((page - 1) * pageSize).Take(pageSize).ToList();
			ViewData["CurrentPage"] = page;
			return PartialView("Items", products);
        }
        public IActionResult FillToLoaiHang(int id)
        {
            var product = _context.HangHoas.Where(l => l.MaLoai == id).Where(m => m.Gia >= 100).ToList();
            return PartialView("Items", product);
        }
        public IActionResult Search(string search)
        {
            var product = _context.HangHoas.Where(m => m.TenHang.Contains(search)).Where(m => m.Gia >= 100).ToList();
            return PartialView("Items", product);
        }
		public IActionResult Edit(int id)
        {
            var product = _context.HangHoas.Find(id);
            var loaiHang = _context.LoaiHangs.ToList();
            ViewData["loaiHang"] = loaiHang;
            SuaHang suaHang = new SuaHang
            {
                MaHang = product.MaHang,
                MaLoai = product.MaLoai,
                TenHang = product.TenHang,
                Gia = product.Gia,
                Anh = product.Anh
            };
            return View(suaHang);
        }
        [HttpPost]
		public IActionResult Edit(SuaHang hangHoa)
        {
            ViewData["loaiHang"] = _context.LoaiHangs.ToList();
            if (!ModelState.IsValid)
            {
                return View(hangHoa);
            }
			var hang = _context.HangHoas.FirstOrDefault(m=>m.MaHang == hangHoa.MaHang);
			if (hang != null)
			{
				hang.Anh = hangHoa.Anh;
				hang.TenHang = hangHoa.TenHang;
				hang.Gia = hangHoa.Gia;
				hang.MaLoai = hangHoa.MaLoai;
                hang.MaHang = hangHoa.MaHang;
                _context.SaveChanges();
                return RedirectToAction("Index");
			}
            
            else
            {
                return View(hangHoa);
            }
			
		}

        public IActionResult Create()
        {
            var product = _context.LoaiHangs.ToList();
            ViewData["loaiHang"] = product;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Hang hangHoa)
        {
            if (!ModelState.IsValid)
            {
                ViewData["loaiHang"] = _context.LoaiHangs.ToList();
                return View(hangHoa);
            }
            HangHoa hangHoas = new HangHoa
            {
                MaLoai = hangHoa.MaLoai,
                TenHang = hangHoa.TenHang,
                Gia = hangHoa.Gia,
                Anh = hangHoa.Anh
            };
            _context.HangHoas.Add(hangHoas);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = _context.HangHoas.Find(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(HangHoa hangHoa)
        {
            var product = _context.HangHoas.Find(hangHoa.MaHang);
            if (product != null)
            {
                _context.HangHoas.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
