using Microsoft.ML.Data;

public class RecommendationInput
{
    [LoadColumn(0)]
    public string UserId { get; set; }

    [LoadColumn(1)]
    public string ProductId { get; set; }

    [LoadColumn(2)]
    public float Label { get; set; }
}
