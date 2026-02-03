using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HotelUI.Controllers
{
    public class FileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var client = _httpClientFactory.CreateClient();

            if (file != null && file.Length > 0)
            {
                using var uploadContent = new MultipartFormDataContent();
                using var stream = file.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                uploadContent.Add(fileContent, "File", file.FileName);

                var uloadResponse = await client.PostAsync("https://localhost:7243/api/FileUpload", uploadContent);

                if (!uloadResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "File Yükleme Hatası!");
                    return View(file);
                }

                var uploadJson = await uloadResponse.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(uploadJson);
                var fileName = doc.RootElement.GetProperty("fileName").GetString();

                ViewBag.ImageUrl = $"https://localhost:7243/images/{fileName}";
                
            }
            return View();

        }
    }
}