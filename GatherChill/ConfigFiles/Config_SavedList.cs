using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    public class Class_List
    {
        public int Id { get; set; }
        public string Name { get; set; } = "New";
        public string Description { get; set; } = "";
        public List<Class_ItemList> ItemList { get; set; } = new();
    }
    public class Class_ItemList
    {
        public uint ItemId { get; set; } = 0;
        public HashSet<uint> EnabledRoutes { get; set; } = new();
        public int GatherAmount { get; set; } = 0;
    }

    public List<Class_List> Saved_List = new();
}
