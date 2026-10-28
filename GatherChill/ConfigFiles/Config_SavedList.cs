using GatherChill.Enums;
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
        public int KeepAmount { get; set; } = 0;
        public GatherMode Mode { get; set; } = GatherMode.Gather;
    }

    public List<Class_List> Saved_List = new();

    public void SaveNewGatherList(string Name, string description)
    {
        var rng = new Random();
        int profileId = rng.Next(1, 50000);
        while (FindPlaylist(profileId) != null)
            profileId = rng.Next(1, 50000);

        List<Class_ItemList> playlist = new();
        foreach (var item in C.GatherList)
        {
            var itemId = item.ItemId;
            var enabledRoutes = ItemRoutes[itemId].EnabledRoutes;
            var gatherAmount = item.GatherAmount;
            var keepAmount = item.KeepAmount;
            var mode = item.Mode;

            playlist.Add(new() 
            { 
                ItemId = item.ItemId, 
                EnabledRoutes = enabledRoutes, 
                GatherAmount = gatherAmount, 
                KeepAmount = keepAmount, 
                Mode = mode 
            });
        }
        Saved_List.Add(new()
        {
            Id = profileId,
            Name = Name,
            Description = description,
            ItemList = playlist
        });
    }

    public Class_List? FindPlaylist(int id) => Saved_List.Find(c => c.Id == id);
}
