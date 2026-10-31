using GatherChill.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

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
    public void RenameList(Class_List list, string name)
    {
        list.Name = name;
    }

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
    public void MoveListNextTo(Class_List dragged, Class_List target, bool after)
    {
        if (dragged.Id == target.Id)
            return;

        var folderId = target.FolderId != null && FindFolder(target.FolderId.Value) != null
            ? target.FolderId
            : null;

        var siblings = Saved_List
            .Where(l => l.FolderId == folderId && l.Id != dragged.Id)
            .OrderBy(l => l.SortOrder)
            .ThenBy(l => l.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();

        var index = siblings.FindIndex(l => l.Id == target.Id);
        if (index < 0)
            index = siblings.Count;
        else if (after)
            index++;

        dragged.FolderId = folderId;
        siblings.Insert(index, dragged);

        for (var i = 0; i < siblings.Count; i++)
            siblings[i].SortOrder = i;
    }

    public void MoveFolderNextTo(Class_Folder dragged, Class_Folder target, bool after)
    {
        if (dragged.Id == target.Id)
            return;

        var ordered = GetSortedFolders();
        ordered.RemoveAll(f => f.Id == dragged.Id);

        var index = ordered.FindIndex(f => f.Id == target.Id);
        if (index < 0)
            index = ordered.Count;
        else if (after)
            index++;

        ordered.Insert(index, dragged);

        for (var i = 0; i < ordered.Count; i++)
            ordered[i].SortOrder = i;
    }

    #region Import / Export

    private const string SharePrefix = "GC1:";
    private const int MaxImportBytes = 5 * 1024 * 1024;

    private static readonly JsonSerializerOptions ShareOptions = new()
    {
        Converters = { new JsonStringEnumConverter() },
    };

    public class ShareData
    {
        public int Version { get; set; } = 1;
        public List<Class_Folder> Folders { get; set; } = new();
        public List<Class_List> Lists { get; set; } = new();
    }

    public record ImportResult(bool Success, string Message);

    public string ExportAll() => Export(Saved_Folders, Saved_List);

    // Takes subsets too, so a single list or folder can be exported later
    public string Export(IEnumerable<Class_Folder> folders, IEnumerable<Class_List> lists)
    {
        var data = new ShareData { Folders = folders.ToList(), Lists = lists.ToList() };
        var json = JsonSerializer.SerializeToUtf8Bytes(data, ShareOptions);

        using var output = new MemoryStream();
        using (var gzip = new GZipStream(output, CompressionLevel.Optimal, leaveOpen: true))
            gzip.Write(json, 0, json.Length);

        return SharePrefix + Convert.ToBase64String(output.ToArray());
    }

    private static ShareData? Decode(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        text = text.Trim();
        if (!text.StartsWith(SharePrefix, StringComparison.Ordinal))
            return null;

        try
        {
            var bytes = Convert.FromBase64String(text[SharePrefix.Length..]);
            using var input = new MemoryStream(bytes);
            using var gzip = new GZipStream(input, CompressionMode.Decompress);

            // Cap the decompressed size so a hostile string can't balloon in memory
            using var decompressed = new MemoryStream();
            var buffer = new byte[8192];
            int read;
            while ((read = gzip.Read(buffer, 0, buffer.Length)) > 0)
            {
                decompressed.Write(buffer, 0, read);
                if (decompressed.Length > MaxImportBytes)
                    return null;
            }

            decompressed.Position = 0;
            var data = JsonSerializer.Deserialize<ShareData>(decompressed, ShareOptions);
            if (data == null)
                return null;

            data.Folders = (data.Folders ?? new()).Where(f => f != null).ToList();
            data.Lists = (data.Lists ?? new()).Where(l => l != null).ToList();
            return data;
        }
        catch
        {
            return null;
        }
    }

    private int GenerateListId()
    {
        var rng = new Random();
        var id = rng.Next(1, 50000);
        while (FindPlaylist(id) != null)
            id = rng.Next(1, 50000);

        return id;
    }

    public ImportResult Import(string? text)
    {
        var data = Decode(text);
        if (data == null)
            return new(false, "The clipboard doesn't contain valid GatherChill data.");

        if (data.Folders.Count == 0 && data.Lists.Count == 0)
            return new(false, "There was nothing to import.");

        // Exporter's folder id -> new folder id on this machine
        var folderMap = new Dictionary<int, int>();
        foreach (var source in data.Folders.OrderBy(f => f.SortOrder))
        {
            var folder = new Class_Folder
            {
                Id = Saved_Folders.Count == 0 ? 1 : Saved_Folders.Max(f => f.Id) + 1,
                Name = string.IsNullOrWhiteSpace(source.Name) ? "Imported Folder" : source.Name,
                SortOrder = Saved_Folders.Count == 0 ? 0 : Saved_Folders.Max(f => f.SortOrder) + 1,
            };

            Saved_Folders.Add(folder);
            folderMap[source.Id] = folder.Id;
        }

        var importedLists = 0;
        var skippedItems = 0;
        foreach (var source in data.Lists.OrderBy(l => l.SortOrder))
        {
            // Lists whose folder wasn't part of the export land in the root
            int? folderId = null;
            if (source.FolderId is { } oldFolderId && folderMap.TryGetValue(oldFolderId, out var newFolderId))
                folderId = newFolderId;

            var items = new List<Class_ItemList>();
            foreach (var item in source.ItemList ?? new())
            {
                // Drops items that don't exist here (bad data, or removed in a patch)
                if (item == null || !ItemRoutes.ContainsKey(item.ItemId))
                {
                    skippedItems++;
                    continue;
                }

                items.Add(new()
                {
                    ItemId = item.ItemId,
                    EnabledRoutes = new HashSet<uint>(item.EnabledRoutes ?? new()),
                    GatherAmount = Math.Max(0, item.GatherAmount),
                    KeepAmount = Math.Max(0, item.KeepAmount),
                    Mode = item.Mode
                });
            }

            var siblings = Saved_List.Where(l => l.FolderId == folderId).ToList();

            Saved_List.Add(new()
            {
                Id = GenerateListId(),
                Name = string.IsNullOrWhiteSpace(source.Name) ? "Imported List" : source.Name,
                Description = source.Description ?? "",
                FolderId = folderId,
                SortOrder = siblings.Count == 0 ? 0 : siblings.Max(l => l.SortOrder) + 1,
                LastModified = DateTime.UtcNow,
                ItemList = items
            });

            importedLists++;
        }

        var message = $"Imported {importedLists} list(s) and {data.Folders.Count} folder(s).";
        if (skippedItems > 0)
            message += $" Skipped {skippedItems} unknown item(s).";

        return new(true, message);
    }

    #endregion
}