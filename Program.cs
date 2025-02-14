using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;
using W4_assignment_template.Services;

namespace W4_assignment_template;

class Program
{
    static IFileHandler fileHandler;
    static List<Character> characters;
    string filePath;

    static void Main()
    {
        filePath = "input.csv"; // Default to CSV file
        fileHandler = new CsvFileHandler(); // Default to CSV handler
        characters = fileHandler.ReadCharacters(filePath);

        while (true)
        {
            Console.WriteLine("Menu:"
                + "\n1. Display Characters"
                + "\n2. Find Character"
                + "\n3. Add Character"
                + "\n4. Level Up Character"
                + "\n5. Change File Format"
                + "\n6. Exit"
                + "\n\tEnter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters();
                    break;
                case "2"
                    FindCharacter();
                    break;
                case "3":
                    AddCharacter();
                    break;
                case "4":
                    LevelUpCharacter();
                    break;
                case "5":
                    ChangeFileFormat();
                    break;
                case "6":
                    fileHandler.WriteCharacters(filePath, characters);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAllCharacters()
    {
        foreach (var character in characters)
        {
            Console.WriteLine(character);
        }
    }

    public void FindCharacter()
    {
        Console.Write("Search for: ");
        var name = Console.ReadLine();

        var matchingCharacters = characters.Where(c => c.Name.Contains(name)).ToList();

        if (matchingCharacters.Any())
        {
            foreach (var character in matchingCharacters)
            {
                Console.WriteLine(character);
            }
        }
        else
        {
            Console.WriteLine($"No character(s) found that matches {name}.");
        }
    }

    static void AddCharacter()
    {
        Console.Write("Enter your character's name: ");
        var name = Console.ReadLine();

        Console.Write("Enter your character's class: ");
        var cclass = Console.ReadLine();

        Console.Write("Enter your character's level: ");
        var level = int.Parse(Console.ReadLine());

        Console.Write("Enter your character's HP: ");
        var health = int.Parse(Console.ReadLine());

        Console.Write("Enter your character's equipment (separate items with a '|'): ");
        var equipment = Console.ReadLine();

        characters.Add(new Character
        {
            Name = name,
            Class = cclass,
            Level = level,
            HP = health,
            Equipment = equipment.Split('|').ToList()
        });

        Console.WriteLine("New character added successfully!");
    }

    static void LevelUpCharacter()
    {
        Console.Write("Enter the name of the character to level up: ");
        string nameToLevelUp = Console.ReadLine();

        var character = characters.Find(c => c.Name.Equals(nameToLevelUp, StringComparison.OrdinalIgnoreCase));
        if (character != null)
        {
            // TODO: Implement logic to level up the character
            character.Level++;
            Console.WriteLine($"Character {character.Name} leveled up to level {character.Level}!");
        }
        else
        {
            Console.WriteLine("Character not found.");
        }
    }

    static void ChangeFileFormat()
    {
        if (fileHandler is CsvFileHandler)
        {
            fileHandler = new JsonFileHandler();
            filePath = "input.json";
            Console.WriteLine("File format changed to JSON.");
        }
        else
        {
            fileHandler = new CsvFileHandler();
            filePath = "input.csv";
            Console.WriteLine("File format changed to CSV.");
        }
    }
}