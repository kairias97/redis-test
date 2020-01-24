using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RedisTest.Models;

namespace RedisTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCache cache;

        public HomeController(ILogger<HomeController> logger, IDistributedCache cache)
        {
            _logger = logger;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var test = new Test
            {
                Id = "asdas",
                Name = "asdasdsa",
                Testing = new MiniTest
                {
                    Id = "adasdsaaas",
                    Name = "assdsdss"
                }
            };
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, test);
                var bytes = stream.ToArray();
                cache.Set("test", bytes);
            }
            cache.SetString("why", "testing");
            return View();
        }

        public IActionResult Privacy()
        {
            var bytes = cache.Get("test");
            using (var stream = new MemoryStream(bytes))
            {
                var model = new BinaryFormatter().Deserialize(stream) as Test;
            }
            TempData.Keep("test");
            var test = TempData["test"] as int?;
            var test2 = cache.GetString("why");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
