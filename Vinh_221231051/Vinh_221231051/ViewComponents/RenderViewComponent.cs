using Microsoft.AspNetCore.Mvc;
using Vinh_221231051.Models;
namespace WebApplication3.ViewComponents
{
	public class RenderViewComponent : ViewComponent
	{
		private List<LoaiHang> LoaiHangs  = new List<LoaiHang>();
		private readonly QLHangHoaContext _context = new QLHangHoaContext();
		public RenderViewComponent()
		{
			LoaiHangs = _context.LoaiHangs.ToList();
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("RenderMenu",LoaiHangs);
		}
	}
}
