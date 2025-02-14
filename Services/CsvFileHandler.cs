using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;

namespace W4_assignment_template.Services;

public class CsvFileHandler : IFileHandler
{
    public List<Character> ReadCharacters(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);

        var characters = new List<Character>();

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("Name"))
            {
                continue;
            }

            string[] values = lines[i].Split(',');
            if (values[0].StartsWith("\""))
            {
                var name = values[1].Substring(0, values[1].Length - 1).Trim() + " " + values[0].Substring(1).Trim();
                characters.Add(new Character
                {
                    Name = name,
                    Class = values[2],
                    Level = int.Parse(values[3]),
                    HP = int.Parse(values[4]),
                    Equipment = values[5].Split('|').ToList()
                });
            }
            else
            {
                characters.Add(new Character
                {
                    Name = values[0],
                    Class = values[1],
                    Level = int.Parse(values[2]),
                    HP = int.Parse(values[3]),
                    Equipment = values[4].Split('|').ToList()
                });
            }
        }
        return characters;
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        var lines = new List<string> { "Name,Class,Level,HP,Equipment" };
        foreach (var character in characters)
        {
            string name = GetName(character.Name);
            string equipment = string.Join("|", character.Equipment);
            lines.Add($"{name},{character.Class},{character.Level},{character.HP},{equipment}");
        }
        File.WriteAllLines(filePath, lines);
    }

    private string GetName(string name)
    {
        if (name.Contains(" "))
        {
            int index = name.IndexOf(" ");
            name = $"\"{name.Substring(index + 1)}, {name.Substring(0, index)}\"";
        }
        return name;
    }
}