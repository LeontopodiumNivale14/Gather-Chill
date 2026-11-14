using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    // Replay information, so I don't go fucking crazy lol

    public bool Replay_Enabled { get; set; } = true;
    public int Replay_KeepAmount { get; set; } = 20;
}
