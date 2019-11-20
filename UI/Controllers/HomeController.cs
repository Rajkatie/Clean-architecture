using Core.Interfaces;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ActionResult Index()
        {
            var model = _productRepository.GetById(1);

            ViewBag.Message = model.Name;

            return View();
        }
    }
}