using Newtonsoft.Json;
using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;

namespace W4_assignment_template.Services;

public class JsonFileHandler : IFileHandler
{
    public List<Character> ReadCharacters(string filePath)
    {
        var jsonString = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<Character>>(jsonString) ?? new List<Character>();
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        var text = JsonConvert.SerializeObject(characters, Formatting.Indented);
        File.WriteAllText(filePath, text);
    }
}