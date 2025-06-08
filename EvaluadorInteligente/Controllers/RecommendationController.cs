using Microsoft.AspNetCore.Mvc;

namespace EvaluadorInteligente.Controllers
{
    public class RecommendationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string opinion)
        {
            if (string.IsNullOrWhiteSpace(opinion))
            {
                ViewBag.Recommendation = "Por favor, ingrese una opinión.";
                return View();
            }

            // Aquí puedes colocar la lógica de predicción con ML.NET
            ViewBag.Recommendation = $"(Simulación) Basado en tu opinión: \"{opinion}\", recomendamos el Producto X.";
            return View();
        }
    }
}

