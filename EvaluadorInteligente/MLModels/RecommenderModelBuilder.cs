using Microsoft.ML;

using Microsoft.ML.Trainers;

public class RecommenderModelBuilder
{
    private static readonly string DataPath = "Data/ratings-data.csv";
    private static readonly string ModelPath = "MLModels/recommender-model.zip";

    public static ITransformer TrainModel(MLContext context)
    {
        IDataView data = context.Data.LoadFromTextFile<RecommendationInput>(DataPath, hasHeader: true, separatorChar: ',');

        var options = new MatrixFactorizationTrainer.Options
        {
            MatrixColumnIndexColumnName = nameof(RecommendationInput.UserId),
            MatrixRowIndexColumnName = nameof(RecommendationInput.ProductId),
            LabelColumnName = nameof(RecommendationInput.Label),
            NumberOfIterations = 20,
            ApproximationRank = 100
        };

        var pipeline = context.Transforms.Conversion.MapValueToKey("userIdEncoded", nameof(RecommendationInput.UserId))
            .Append(context.Transforms.Conversion.MapValueToKey("productIdEncoded", nameof(RecommendationInput.ProductId)))
            .Append(context.Recommendation().Trainers.MatrixFactorization(options));

        var model = pipeline.Fit(data);
        context.Model.Save(model, data.Schema, ModelPath);
        return model;
    }

    public static List<string> Recommend(string userId)
    {
        var context = new MLContext();
        var model = context.Model.Load(ModelPath, out _);

        var predictionEngine = context.Model.CreatePredictionEngine<RecommendationInput, RegressionPrediction>(model);


        var products = new[] { "P1", "P2", "P3", "P4", "P5" };
        var recommendations = products
            .Select(p => new
            {
                ProductId = p,
                Score = predictionEngine.Predict(new RecommendationInput
                {
                    UserId = userId,
                    ProductId = p
                }).Score
            })
            .OrderByDescending(p => p.Score)
            .Take(5)
            .Select(p => p.ProductId)
            .ToList();

        return recommendations;
    }
}

public class RegressionPrediction
{
    public float Score { get; set; }
}
