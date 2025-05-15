using LearningTest.Model;

namespace LearningTest.Tests;

public class BmiDeterminatorTest
{
    [Theory]
    [InlineData(10.0)]
    [InlineData(13.0)]
    [InlineData(18.0)]
    [InlineData(8.0)]
    public void DetermineBmi_ForBmiBelow18_5_ReturnsUnderweightClassification(double bmi)
    {
        var bmiDeterminator = new BmiDeterminator();

        var result = bmiDeterminator.DetermineBmi(bmi);

        Assert.Equal(BmiClassification.Underweight, result);
    }
}