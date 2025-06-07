using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
public class RecommendationController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult Index(string userId)
    {
        var recommendations = RecommenderModelBuilder.Recommend(userId);
        ViewBag.UserId = userId;
        ViewBag.Recommendations = recommendations;
        return View();
    }
}
