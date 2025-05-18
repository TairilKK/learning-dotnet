using System.Collections;
using LearningTest.AdditionalClassesForTests;

namespace LearningTest.Tests.AdditionalClassesForTests;

public class ValidatorTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {new List<DateRange>()
        {
            new DateRange(
                new DateTime(2025, 4, 1),
                new DateTime(2025, 4, 15)
            ),
            new DateRange(
                new DateTime(2025, 5, 1),
                new DateTime(2025, 5, 15)
            )
        }};
        yield return new object[] { new List<DateRange>()
        {
            new DateRange(
                new DateTime(2025, 4, 15),
                new DateTime(2025, 4, 25)
            )
        }};
        yield return new object[] {new List<DateRange>()
        {
            new DateRange(
                new DateTime(2025, 4, 8),
                new DateTime(2025, 4, 25)
            )
        }};

        yield return new object[] {new List<DateRange>()
        {
            new DateRange(
                new DateTime(2025, 4, 12),
                new DateTime(2025, 4, 14)
            )
        }};
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}