using FluentAssertions;
using LearningTest.AdditionalClassesForTests;

namespace LearningTest.Tests.AdditionalClassesForTests;

public class ListHelperTests
{

    [Fact]
    public void FilterOddNumber_ForIntList_ReturnsOnlyOddNumbers()
    {
        var input = new List<int>() { 1, 2, 2, 2, 3 };
        var correctResult = new List<int>() { 1, 3 };

        var res = ListHelper.FilterOddNumber(input);

        res.Should().Equal(correctResult);
    }
}