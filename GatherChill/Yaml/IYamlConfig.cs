namespace GatherChill.Yaml;

public interface IYamlConfig
{
    void Save();
    static abstract string ConfigPath { get; }
}

public interface IYamlConfigWithPath
{
    void Save(string path);
    string GetDefaultPath();
}
