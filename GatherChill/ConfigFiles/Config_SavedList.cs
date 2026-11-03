using GatherChill.Enums;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    #region Types

    public enum ListSortMode
    {
        Name,
        RecentlyModified,
        Manual
    }

    public class Class_Folder
    {
        public int Id { get; set; }
        public string Name { get; set; } = "New Folder";
        public int SortOrder { get; set; } = 0;
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

    public class Class_ItemList
    {
        public uint ItemId { get; set; } = 0;
        public HashSet<uint> EnabledRoutes { get; set; } = new();
        public int GatherAmount { get; set; } = 0;
        public int KeepAmount { get; set; } = 0;
        public GatherMode Mode { get; set; } = GatherMode.Gather;
    }

    #endregion

    #region Saved Data

    public List<Class_List> Saved_List = new();
    public List<Class_Folder> Saved_Folders = new();
    public ListSortMode SortMode = ListSortMode.Name;

    #endregion

    #region Lists

    public Class_List? FindPlaylist(int id) => Saved_List.Find(c => c.Id == id);

    public void SaveNewGatherList(string Name, string description)
    {
        var playlist = new List<Class_ItemList>();
        foreach (var item in C.GatherList)
        {
            playlist.Add(new()
            {
                ItemId = item.ItemId,
                // Copy so the saved list doesn't alias the live route set
                EnabledRoutes = new HashSet<uint>(ItemRoutes[item.ItemId].EnabledRoutes),
                GatherAmount = item.GatherAmount,
                KeepAmount = item.KeepAmount,
                Mode = item.Mode
            });
        }

        Saved_List.Add(new()
        {
            Id = GenerateListId(),
            Name = Name,
            Description = description,
            LastModified = DateTime.UtcNow,
            SortOrder = NextListSortOrder(null),
            ItemList = playlist
        });
    }

    public void RenameList(Class_List list, string name)
    {
        list.Name = name;
    }

    #endregion

    #region Folders

    public Class_Folder? FindFolder(int id) => Saved_Folders.Find(f => f.Id == id);

    public List<Class_Folder> GetSortedFolders() => Saved_Folders
        .OrderBy(f => f.SortOrder)
        .ThenBy(f => f.Name, StringComparer.OrdinalIgnoreCase)
        .ToList();

    public void AddFolder(string name = "New Folder")
    {
        Saved_Folders.Add(new()
        {
            Id = NextFolderId(),
            Name = name,
            SortOrder = NextFolderSortOrder()
        });
    }

    public void DeleteFolder(int folderId)
    {
        // Lists fall back to the root instead of being deleted
        foreach (var list in Saved_List.Where(l => l.FolderId == folderId))
            list.FolderId = null;

        Saved_Folders.RemoveAll(f => f.Id == folderId);
    }

    #endregion

    #region Ordering

    // Lists dropped into a folder go to the bottom of it
    public void MoveListToFolder(Class_List list, int? folderId)
    {
        if (list.FolderId == folderId)
            return;

        list.SortOrder = NextListSortOrder(folderId);
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

    // Puts `dragged` right before/after `target`, in target's folder
    public void MoveListNextTo(Class_List dragged, Class_List target, bool after)
    {
        if (dragged.Id == target.Id)
            return;

        // Treat a dangling FolderId as root
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

    #endregion

    #region Id / Order Helpers

    private int GenerateListId()
    {
        var rng = new Random();
        var id = rng.Next(1, 50000);
        while (FindPlaylist(id) != null)
            id = rng.Next(1, 50000);

        return id;
    }

    private int NextFolderId() =>
        Saved_Folders.Count == 0 ? 1 : Saved_Folders.Max(f => f.Id) + 1;

    private int NextFolderSortOrder() =>
        Saved_Folders.Count == 0 ? 0 : Saved_Folders.Max(f => f.SortOrder) + 1;

    // Bottom of a folder, or of the root when folderId is null
    private int NextListSortOrder(int? folderId)
    {
        var siblings = Saved_List.Where(l => l.FolderId == folderId).ToList();
        return siblings.Count == 0 ? 0 : siblings.Max(l => l.SortOrder) + 1;
    }

    #endregion

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

    // Takes subsets too, so a single list or folder can be exported
    public string Export(IEnumerable<Class_Folder> folders, IEnumerable<Class_List> lists)
    {
        var data = new ShareData { Folders = folders.ToList(), Lists = lists.ToList() };
        var json = JsonSerializer.SerializeToUtf8Bytes(data, ShareOptions);

        using var output = new MemoryStream();
        using (var gzip = new GZipStream(output, CompressionLevel.Optimal, leaveOpen: true))
            gzip.Write(json, 0, json.Length);

        return SharePrefix + Convert.ToBase64String(output.ToArray());
    }

    public ImportResult Import(string? text)
    {
        var data = Decode(text);
        if (data == null)
            return new(false, "The clipboard doesn't contain valid GatherChill data.");

        if (data.Folders.Count == 0 && data.Lists.Count == 0)
            return new(false, "There was nothing to import.");

        var folderMap = ImportFolders(data);
        var (importedLists, skippedItems) = ImportLists(data, folderMap);

        var message = $"Imported {importedLists} list(s) and {data.Folders.Count} folder(s).";
        if (skippedItems > 0)
            message += $" Skipped {skippedItems} unknown item(s).";

        return new(true, message);
    }

    // Returns exporter's folder id -> new folder id on this machine
    private Dictionary<int, int> ImportFolders(ShareData data)
    {
        var folderMap = new Dictionary<int, int>();

        foreach (var source in data.Folders.OrderBy(f => f.SortOrder))
        {
            var folder = new Class_Folder
            {
                Id = NextFolderId(),
                Name = string.IsNullOrWhiteSpace(source.Name) ? "Imported Folder" : source.Name,
                SortOrder = NextFolderSortOrder(),
            };

            Saved_Folders.Add(folder);
            folderMap[source.Id] = folder.Id;
        }

        return folderMap;
    }

    private (int Imported, int SkippedItems) ImportLists(ShareData data, Dictionary<int, int> folderMap)
    {
        var imported = 0;
        var skipped = 0;

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
                    skipped++;
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

            Saved_List.Add(new()
            {
                Id = GenerateListId(),
                Name = string.IsNullOrWhiteSpace(source.Name) ? "Imported List" : source.Name,
                Description = source.Description ?? "",
                FolderId = folderId,
                SortOrder = NextListSortOrder(folderId),
                LastModified = DateTime.UtcNow,
                ItemList = items
            });

            imported++;
        }

        return (imported, skipped);
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

    #endregion
}