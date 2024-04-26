using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Config;

class Config
{
    public DateTime Day { get; set; }
    public List<OriginDestination>? Combinations { get; set; }

    public static List<Config> ReadFromFile(string filename)
    {
        var fileContent = File.ReadAllText(filename);
        var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

        return deserializer.Deserialize<List<Config>>(fileContent);
    }
}

class OriginDestination
{
    public string? Origin { get; set; }
    public string? Destination { get; set; }
}
