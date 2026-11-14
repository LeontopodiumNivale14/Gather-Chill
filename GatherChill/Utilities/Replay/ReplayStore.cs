using GatherChill.Utilities.Tools;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace GatherChill.Utilities.Replay;

internal static class ReplayStore
{
    private const string Tag = "Replay";
    public const string Extension = ".gcreplay";

    private static readonly object WriteLock = new();

    public static string Folder
    {
        get
        {
            var path = Path.Combine(Svc.PluginInterface.GetPluginConfigDirectory(), "Replays");
            Directory.CreateDirectory(path);
            return path;
        }
    }

    public static string NewFilePath(ReplaySession session)
        => Path.Combine(Folder, $"{session.StartedUtc:yyyyMMdd-HHmmss}-{session.Id.ToString("N")[..6]}{Extension}");

    /// <summary>Newest first.</summary>
    public static List<FileInfo> List()
    {
        try
        {
            return new DirectoryInfo(Folder)
                .GetFiles($"*{Extension}")
                .OrderByDescending(f => f.LastWriteTimeUtc)
                .ToList();
        }
        catch (Exception ex)
        {
            IceLogging.Error($"Couldn't list replays: {ex.Message}", Tag);
            return new();
        }
    }

    public static void Save(string path, ReplaySession session, int keep, bool sync)
    {
        string json;
        try
        {
            // Serialized here, on the calling thread, so the recorder can keep mutating the session
            json = JsonConvert.SerializeObject(session);
        }
        catch (Exception ex)
        {
            IceLogging.Error($"Couldn't serialize replay: {ex.Message}", Tag);
            return;
        }

        void Write()
        {
            lock (WriteLock)
            {
                try
                {
                    var temp = path + ".tmp";

                    using (var file = File.Create(temp))
                    using (var gzip = new GZipStream(file, CompressionLevel.Optimal))
                    using (var writer = new StreamWriter(gzip))
                    {
                        writer.Write(json);
                    }

                    File.Move(temp, path, true);
                    Prune(keep);
                }
                catch (Exception ex)
                {
                    IceLogging.Error($"Couldn't save replay: {ex.Message}", Tag);
                }
            }
        }

        if (sync)
            Write();
        else
            Task.Run(Write);
    }

    public static ReplaySession? Load(string path)
    {
        try
        {
            using var file = File.OpenRead(path);
            using var gzip = new GZipStream(file, CompressionMode.Decompress);
            using var reader = new StreamReader(gzip);

            var session = JsonConvert.DeserializeObject<ReplaySession>(reader.ReadToEnd());

            // A newer plugin wrote this, and we can't promise to read it
            if (session is null || session.Version > ReplaySession.CurrentVersion)
                return null;

            return session;
        }
        catch (Exception ex)
        {
            IceLogging.Error($"Couldn't read replay '{path}': {ex.Message}", Tag);
            return null;
        }
    }

    /// <summary>Deletes the oldest replays beyond the keep amount. Replays opened from elsewhere live outside this folder, so they're never touched.</summary>
    public static void Prune(int keep)
    {
        keep = Math.Max(keep, 1);

        foreach (var file in List().Skip(keep))
        {
            try
            {
                file.Delete();
            }
            catch (Exception ex)
            {
                IceLogging.Error($"Couldn't delete old replay {file.Name}: {ex.Message}", Tag);
            }
        }
    }
}