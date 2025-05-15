using LearningTest.Model;

namespace LearningTest.Tests;

public class BmiDeterminatorTest
{
    [Fact]
    public void DetermineBmi_ForBmiBelow18_5_ReturnsUnderweightClassification()
    {
        var bmiDeterminator = new BmiDeterminator();
        double bmi = 10;

        var result = bmiDeterminator.DetermineBmi(bmi);

        Assert.Equal(BmiClassification.Underweight, result);
    }
}