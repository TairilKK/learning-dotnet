using System.Reflection;
using Newtonsoft.Json;
using Xunit.Sdk;

namespace LearningTest.Tests;

public class JsonFileData(string jsonPath) : DataAttribute
{
    private readonly string _jsonPath = jsonPath;
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (testMethod == null) throw new ArgumentException(nameof(testMethod));
        var currDir = Directory.GetCurrentDirectory();
        var jsonFullPath = Path.GetRelativePath(currDir, _jsonPath);

        if (!File.Exists(jsonFullPath))
        {
            throw new ArgumentException($"Couldn't find file {jsonFullPath}");
        }

        var jsonData = File.ReadAllText(jsonFullPath);
        var allCases = JsonConvert.DeserializeObject<IEnumerable<object[]>>(jsonData);

        return allCases;
    }
}