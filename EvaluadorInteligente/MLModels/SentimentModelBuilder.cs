using Microsoft.ML;
public class SentimentModelBuilder
{
    private static readonly string DataPath = "Data/sentiment-data.tsv";
    private static readonly string ModelPath = "MLModels/sentiment-model.zip";

    public static PredictionEngine<SentimentInput, SentimentPrediction> BuildAndTrain()
    {
        var context = new MLContext();
        IDataView data = context.Data.LoadFromTextFile<SentimentInput>(
            path: DataPath, hasHeader: true, separatorChar: '\t');

        var pipeline = context.Transforms.Text.FeaturizeText("Features", nameof(SentimentInput.Text))
            .Append(context.BinaryClassification.Trainers.SdcaLogisticRegression());

        var model = pipeline.Fit(data);
        context.Model.Save(model, data.Schema, ModelPath);

        return context.Model.CreatePredictionEngine<SentimentInput, SentimentPrediction>(model);
    }

    public static PredictionEngine<SentimentInput, SentimentPrediction> LoadModel()
    {
        var context = new MLContext();
        ITransformer model = context.Model.Load(ModelPath, out _);
        return context.Model.CreatePredictionEngine<SentimentInput, SentimentPrediction>(model);
    }
}
