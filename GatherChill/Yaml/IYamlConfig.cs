namespace GatherChill.Yaml;

public interface IYamlConfig
{
    void Save();
    static abstract string ConfigPath { get; }
}
