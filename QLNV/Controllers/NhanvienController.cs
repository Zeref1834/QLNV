using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using QLNV.Models;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using X.PagedList;

namespace QLNV.Controllers
{
    public class NhanvienController : Controller
    {
        Uri apiAddress = new Uri("https://localhost:7101/api");
        private readonly HttpClient httpClient;

        public NhanvienController() 
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = apiAddress;

        }
        public IActionResult Index(int? page)
        {
            List<NhanvienViewModel> listNV = new List<NhanvienViewModel>();
            HttpResponseMessage response = httpClient.GetAsync(httpClient.BaseAddress + "/NhanVien/Get").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listNV  = JsonConvert.DeserializeObject<List<NhanvienViewModel>>(data);
            }

            int pageNumber = page ?? 1;
            int pageSize = 10;

            IPagedList<NhanvienViewModel> items = listNV.ToPagedList(pageNumber, pageSize);

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync()
        {
            //Kiểm tra có file hay không
            if (Request.Form.Files.Count == 0)
            {
                return BadRequest("Không có file được chọn!");
            }

            //Kiểm tra 
            string extension = Path.GetExtension(Request.Form.Files[0].FileName).ToLower();
            if (extension != ".xlsx" && extension != ".xls" && extension != ".xlsm" && extension != ".xlsb")
            {
                return BadRequest("File không phải là một file Excel. Vui lòng chọn lại file Excel!");
            }

            var file = Request.Form.Files[0];

            if (file != null && file.Length > 0)
            {
                var formData = new MultipartFormDataContent();
                formData.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);
                var response = await httpClient.PostAsync(httpClient.BaseAddress + "/NhanVien/ImportEmployees/import", formData);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Import thành công!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Import thất bại";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Message = "Vui lòng chọn file để import.";
                return RedirectToAction("Index");
            }
        }
    }
}
