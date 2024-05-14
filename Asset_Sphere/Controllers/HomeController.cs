using Asset_Sphere.Data;
using Asset_Sphere.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;

namespace Asset_Sphere.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly Asset_SphereContext _context;

        public HomeController(ILogger<HomeController> logger, Asset_SphereContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult UploadExcel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (file != null && file.Length > 0)
            {
                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads";

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filepath = Path.Combine(uploadsFolder, file.FileName);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var stream = System.IO.File.Open(filepath, FileMode.Open, FileAccess.Read))
                {

                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {

                        do
                        {
                            bool isHeaderSkipped = false;
                            while (reader.Read())
                            {
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }
                                Asset_list e = new Asset_list();
                                e.Asset_Number = (long)reader.GetValue(1);
                                e.Description = (string)reader.GetValue(2);
                                e.Catergory = (string)reader.GetValue(3);
                                e.Acq_Date = (string)reader.GetValue(4);
                                e.Location = (string)reader.GetValue(5);
                                e.Label = (string)reader.GetValue(6);
                                e.Custodian = (string)reader.GetValue(7);
                                e.Condition = (string)reader.GetValue(8);
                                e.PO_Number = (string)reader.GetValue(9);
                                e.Model = (string)reader.GetValue(10);
                                e.Serial_Number = (string)reader.GetValue(11);
                                e.Asset_Cost = (decimal)reader.GetValue(12);

                                _context.Add(e);
                                await _context.SaveChangesAsync();
                            }
                        } while (reader.NextResult());

                        ViewBag.Message = "Success";
                    }
                }
            }

            else
                ViewBag.Message = "Empty";
            return View();
        }
    }
}
