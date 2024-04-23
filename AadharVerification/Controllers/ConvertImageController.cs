using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace AadharVerification.Controllers
{
    public class ConvertImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("ImageToBase64")]
        public void ImageToBase64(IFormFile file) 
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    byte[] fileByte = ms.ToArray();
                    string Base64 = Convert.ToBase64String(fileByte);
                    // comment
                    // comment
                }
            }                      
        }
    }
}
