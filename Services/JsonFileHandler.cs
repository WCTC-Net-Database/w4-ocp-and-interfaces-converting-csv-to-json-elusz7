using Newtonsoft.Json;
using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;

namespace W4_assignment_template.Services;

public class JsonFileHandler : IFileHandler
{
    public List<Character> ReadCharacters(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(jsonString) ?? new List<Character>();

        foreach (var character in characters)
        {
            if (character.Name.Contains(","))
            {
                string[] names = character.Name.Split(", ");
                character.Name = $"{names[1]} {names[0]}";
            }
        }

        return characters;
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        File.WriteAllText(filePath, JsonConvert.SerializeObject(characters, Formatting.Indented));
    }
}