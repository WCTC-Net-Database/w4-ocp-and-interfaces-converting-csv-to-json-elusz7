using Newtonsoft.Json;

namespace W4_assignment_template.Models;

public class Character
{
    [JsonIgnore]
    public string Name { get; set; }
    [JsonProperty("Name")]
    public string NameString
    {
        get
        {
            if (Name.Contains(" "))
            {
                int index = Name.IndexOf(" ");
                return $"{Name.Substring(index + 1)}, {Name.Substring(0, index)}";
            }
            return Name;
        }
        set
        {
            if (value.Contains(","))
            {
                string[] names = value.Split(", ");
                value = $"{names[1]} {names[0]}";
            }
            Name = value;
        }
    }

    public string Class { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    
    [JsonIgnore]
    public List<string> Equipment { get; set; }
    [JsonProperty("Equipment")]
    public string EquipmentString
    {
        get => string.Join("|", Equipment);
        set
        {
            Equipment = value.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }

    public override string ToString()
    {
        return $"Name: {Name}, Class: {Class}, Level: {Level}, HP: {HP}, Equipment: {string.Join(", ", Equipment)}";
    }
}