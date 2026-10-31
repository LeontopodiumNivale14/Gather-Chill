using GatherChill.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    public class Class_Folder
    {
        public int Id { get; set; }
        public string Name { get; set; } = "New Folder";
        public int SortOrder { get; set; } = 0;
    }

    public enum ListSortMode
    {
        Name,
        RecentlyModified,
        Manual
    }

    public class Class_List
    {
        public int Id { get; set; }
        public string Name { get; set; } = "New";
        public string Description { get; set; } = "";
        public int? FolderId { get; set; } = null;
        public int SortOrder { get; set; } = 0;
        public DateTime LastModified { get; set; } = DateTime.MinValue;
        public List<Class_ItemList> ItemList { get; set; } = new();

        public void Touch() => LastModified = DateTime.UtcNow;
    }

    public ListSortMode SortMode = ListSortMode.Name;

    public class Class_ItemList
    {
        public uint ItemId { get; set; } = 0;
        public HashSet<uint> EnabledRoutes { get; set; } = new();
        public int GatherAmount { get; set; } = 0;
        public int KeepAmount { get; set; } = 0;
        public GatherMode Mode { get; set; } = GatherMode.Gather;
    }

    public List<Class_List> Saved_List = new();
    public List<Class_Folder> Saved_Folders = new();

    public void SaveNewGatherList(string Name, string description)
    {
        var rng = new Random();
        int profileId = rng.Next(1, 50000);
        while (FindPlaylist(profileId) != null)
            profileId = rng.Next(1, 50000);

        var playlist = new List<Class_ItemList>();
        foreach (var item in C.GatherList)
        {
            playlist.Add(new()
            {
                ItemId = item.ItemId,
                EnabledRoutes = new HashSet<uint>(ItemRoutes[item.ItemId].EnabledRoutes),
                GatherAmount = item.GatherAmount,
                KeepAmount = item.KeepAmount,
                Mode = item.Mode
            });
        }

        var rootLists = Saved_List.Where(l => l.FolderId == null).ToList();

        Saved_List.Add(new()
        {
            Id = profileId,
            Name = Name,
            Description = description,
            LastModified = DateTime.UtcNow,
            SortOrder = rootLists.Count == 0 ? 0 : rootLists.Max(l => l.SortOrder) + 1,
            ItemList = playlist
        });
    }

    public Class_List? FindPlaylist(int id) => Saved_List.Find(c => c.Id == id);

    public Class_Folder? FindFolder(int id) => Saved_Folders.Find(f => f.Id == id);

    public void AddFolder(string name = "New Folder")
    {
        var id = Saved_Folders.Count == 0 ? 1 : Saved_Folders.Max(f => f.Id) + 1;
        var order = Saved_Folders.Count == 0 ? 0 : Saved_Folders.Max(f => f.SortOrder) + 1;
        Saved_Folders.Add(new() { Id = id, Name = name, SortOrder = order });
    }

    // Lists dropped into a folder go to the bottom of it
    public void MoveListToFolder(Class_List list, int? folderId)
    {
        if (list.FolderId == folderId)
            return;

        var destination = Saved_List.Where(l => l.FolderId == folderId).ToList();
        list.SortOrder = destination.Count == 0 ? 0 : destination.Max(l => l.SortOrder) + 1;
        list.FolderId = folderId;
    }

    // direction: -1 = up, +1 = down
    public void MoveList(Class_List list, int direction)
    {
        var siblings = Saved_List
            .Where(l => l.FolderId == list.FolderId)
            .OrderBy(l => l.SortOrder)
            .ThenBy(l => l.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();

        MoveWithin(siblings, list, direction, (l, order) => l.SortOrder = order);
    }

    public void MoveFolder(Class_Folder folder, int direction)
    {
        var ordered = GetSortedFolders();
        MoveWithin(ordered, folder, direction, (f, order) => f.SortOrder = order);
    }

    public List<Class_Folder> GetSortedFolders() => Saved_Folders
        .OrderBy(f => f.SortOrder)
        .ThenBy(f => f.Name, StringComparer.OrdinalIgnoreCase)
        .ToList();

    private static void MoveWithin<T>(List<T> ordered, T item, int direction, Action<T, int> setOrder)
    {
        var index = ordered.IndexOf(item);
        if (index < 0)
            return;

        var target = index + direction;
        if (target < 0 || target >= ordered.Count)
            return;

        (ordered[index], ordered[target]) = (ordered[target], ordered[index]);

        // Renumber 0..n-1 so lists that all started at SortOrder 0 get a clean order
        for (var i = 0; i < ordered.Count; i++)
            setOrder(ordered[i], i);
    }

    public void DeleteFolder(int folderId)
    {
        // Lists fall back to the root instead of being deleted
        foreach (var list in Saved_List.Where(l => l.FolderId == folderId))
            list.FolderId = null;

        Saved_Folders.RemoveAll(f => f.Id == folderId);
    }
}