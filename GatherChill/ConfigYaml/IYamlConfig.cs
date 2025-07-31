using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.ConfigYaml;

public interface IYamlConfig
{
    void Save();
    static abstract string ConfigPath { get; }
}