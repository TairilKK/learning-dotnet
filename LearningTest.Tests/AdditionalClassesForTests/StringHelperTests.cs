using LearningTest.AdditionalClassesForTests;

namespace LearningTest.Tests.AdditionalClassesForTests;

public class StringHelperTests
{
    [Theory]
    [InlineData("ala ma kota", "kota ma ala")]
    [InlineData("to jest test", "test jest to")]
    [InlineData("bardzo ciekawe jak dlugie zdanie to byc moze",
        "moze byc to zdanie dlugie jak ciekawe bardzo")]
    public void ReverseWords_ForGivenSentence_ReturnsReversedSentence(string input, string correctResult)
    {
        var res = StringHelper.ReverseWords(input);

        Assert.Equal(correctResult, res);
    }

    [Theory]
    [InlineData("ala", true)]
    [InlineData("kajak", true)]
    public void IsPalindrome_ForAWord_ReturnsTrueValue(string input, bool correctResult)
    {
        var res = StringHelper.IsPalindrome(input);

        Assert.Equal(correctResult, res);
    }
    [Theory]
    [InlineData("ola", false)]
    [InlineData("Ala", false)]
    public void IsPalindrome_ForAWord_ReturnsFalseValue(string input, bool correctResult)
    {
        var res = StringHelper.IsPalindrome(input);

        Assert.Equal(correctResult, res);
    }

}