using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;

public class SentimentController : Controller
{
    private static readonly PredictionEngine<SentimentInput, SentimentPrediction> engine =
        System.IO.File.Exists("MLModels/sentiment-model.zip")
        ? SentimentModelBuilder.LoadModel()
        : SentimentModelBuilder.BuildAndTrain();

    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult Index(string opinion)
    {
        var input = new SentimentInput { Text = opinion };
        var result = engine.Predict(input);
        ViewBag.Opinion = opinion;
        ViewBag.Result = result.Prediction ? "Positiva" : "Negativa";
        ViewBag.Score = result.Score;
        ViewBag.Probability = result.Probability;
        return View();
    }
}
