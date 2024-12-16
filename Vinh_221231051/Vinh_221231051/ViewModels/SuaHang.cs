using System.ComponentModel.DataAnnotations;

namespace Vinh_221231051.ViewModels
{
    public class SuaHang
    {
        public int MaHang { get; set; }
        public int MaLoai { get; set; }
        public string TenHang { get; set; } = null!;
        [Required(ErrorMessage = "Không được để trống")]
        [Range(100, 5000, ErrorMessage = "Giá từ 100 đến 5000")]
        public decimal? Gia { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [RegularExpression(@".*\.(jpg|png|.gif|.tiff)$", ErrorMessage = "Ảnh có định dạng .jpg | .png | .gif | .tiff ")]
        public string? Anh { get; set; }
    }
}
