using LearningTest.Model;

namespace LearningTest;

public interface IBmiDeterminator
{
    BmiClassification DetermineBmi(double bmi);
}