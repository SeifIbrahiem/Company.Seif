using Company.Seif.PL.Models;
using Company.Seif.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Company.Seif.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedServices scopedServices01;
        private readonly IScopedServices scopedServices02;
        private readonly ITransientServices transientServices01;
        private readonly ITransientServices transientServices02;
        private readonly ISingletonServices singletonServices01;
        private readonly ISingletonServices singletonServices02;

        public HomeController(
            ILogger<HomeController> logger,
            IScopedServices scopedServices01,
            IScopedServices scopedServices02,
            ITransientServices transientServices01,
            ITransientServices transientServices02,
            ISingletonServices singletonServices01,
            ISingletonServices singletonServices02

            )
        {
            _logger = logger;
            this.scopedServices01 = scopedServices01;
            this.scopedServices02 = scopedServices02;
            this.transientServices01 = transientServices01;
            this.transientServices02 = transientServices02;
            this.singletonServices01 = singletonServices01;
            this.singletonServices02 = singletonServices02;
        }

        public String TestLifeTime()
            {
            StringBuilder builder = new StringBuilder();

            builder.Append($"scopedServices01 :: {scopedServices01.GetGuid()}\n");
            builder.Append($"scopedServices02 :: {scopedServices02.GetGuid()}\n\n");
            builder.Append($"transientServices01 :: {transientServices01.GetGuid()}\n");
            builder.Append($"transientServices02 :: {transientServices02.GetGuid()}\n\n");
            builder.Append($"singletonServices01 :: {singletonServices01.GetGuid()}\n");
            builder.Append($"singletonServices02 :: {singletonServices02.GetGuid()}\n\n");
            return builder.ToString();
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
    }
}
