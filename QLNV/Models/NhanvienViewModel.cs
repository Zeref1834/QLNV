using System.ComponentModel;

namespace QLNV.Models
{
    public class NhanvienViewModel
    {
        [DisplayName("Mã Nhân Viên")]
        public string? MANV { get; set; }

        [DisplayName("Tên Nhân Viên")]
        public string? TENNV { get; set; }

        [DisplayName("Ngày Sinh")]
        public DateTime NGAYSINH { get; set; }

        [DisplayName("Tuổi")]
        public int? Age { get; set; }
    }
}
