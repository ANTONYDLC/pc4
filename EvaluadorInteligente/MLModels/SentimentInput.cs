using Microsoft.ML.Data;

public class SentimentInput
{
    [LoadColumn(0)]
    public bool Label { get; set; }

    [LoadColumn(1)]
    public string Text { get; set; }
}
