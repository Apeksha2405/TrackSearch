using Newtonsoft.Json.Linq;

namespace TrackDiscovery.Repository
{
    public interface ITrackRepository
    {
        Task<JObject> GetArtistDetailAsync(string artistName);
        Task<JObject> GetArtistAlbumAsync(string artistId);

    }
}
