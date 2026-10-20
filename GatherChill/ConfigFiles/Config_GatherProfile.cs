using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    [JsonProperty("GatherProfilesV2", ObjectCreationHandling = ObjectCreationHandling.Replace)]
    public List<GatherProfile> GatherProfiles { get; set; } = new()
    {
        DefaultProfile,
    };

    public void AddNewGatheringProfile()
    {
        var rng = new Random();
        int profileId = rng.Next(1, 50000);
        while (FindGatherProfile(profileId) != null)
            profileId = rng.Next(1, 50000);

        GatherProfiles.Add(new GatherProfile
        {
            ProfileId = profileId
        });
        SaveDebounced();
    }

    public GatherProfile? FindGatherProfile(int id) => GatherProfiles.Find(c => c.ProfileId == id);
}