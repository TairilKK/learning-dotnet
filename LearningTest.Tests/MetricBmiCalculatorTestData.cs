using System.Collections;
using Newtonsoft.Json;

namespace LearningTest.Tests;

public class MetricBmiCalculatorTestData : IEnumerable<object[]>
{
    private const string JSON_PATH = "Data/MetricBmiCalculatorData.json";
    public IEnumerator<object[]> GetEnumerator()
    {
        var currDir = Directory.GetCurrentDirectory();
        var jsonFullPath = Path.GetRelativePath(currDir, JSON_PATH);

        if (!File.Exists(jsonFullPath))
        {
            throw new ArgumentException($"Couldn't find file {jsonFullPath}");
        }

        var jsonData = File.ReadAllText(jsonFullPath);
        var allCases = JsonConvert.DeserializeObject<IEnumerable<object[]>>(jsonData);

        return allCases.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}