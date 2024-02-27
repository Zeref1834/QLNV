using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLNV.Data.EF;
using QLNV.Data.Model;

namespace QLNV.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly QLNV_DbContext _context;

        public NhanVienController(QLNV_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {

            var listNV =  _context.Nhanviens.ToList();
            var result = new List<object>();

            foreach (var nv in listNV)
            {
                var age = DateTime.Now.Year - nv.NGAYSINH.Year;
                result.Add(new
                {
                    nv.MANV,
                    nv.TENNV,
                    nv.NGAYSINH,
                    Age = age
                });
            }
            return Ok(result);
        }

        [HttpPost("import")]
        public IActionResult ImportEmployees(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("File not selected");
            }
            var stream = new MemoryStream();
            file.CopyTo(stream);
            var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);
            var rowCount = worksheet.RowsUsed().Count();

            var soNV = _context.Nhanviens.ToList().Count;
            var employeesToAdd = new List<Nhanvien>();

            for (int row = 2; row <= rowCount; row++)
            {
                var maNV = "";
                if (soNV > 0)
                {
                    maNV = $"NV_{row + soNV - 1}";
                }
                else
                {
                    maNV = $"NV_{row - 1}";
                }
                var tenNV = worksheet.Cell(row, 2).Value.ToString();
                var ngaysinh = DateTime.Parse(worksheet.Cell(row, 3).Value.ToString());

                var nv = new Nhanvien
                {
                    MANV = maNV,
                    TENNV = tenNV,
                    NGAYSINH = ngaysinh
                };
                employeesToAdd.Add(nv);
            }
            _context.Nhanviens.AddRange(employeesToAdd);
            _context.SaveChanges();

            return Ok("Import successful");
        }
    }
}
