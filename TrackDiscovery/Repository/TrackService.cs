using Newtonsoft.Json.Linq;
namespace TrackDiscovery.Repository
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;

        public TrackService(HttpClient httpClient)
        {
            _trackRepository = TrackRepository.GetInstance(httpClient); // Get singleton instance of repository
        }

        public async Task<JObject> GetArtistDetails(string artistName)
        {
            return await _trackRepository.GetArtistDetailAsync(artistName);
        }

        public async Task<JObject> GetArtistAlbums(string artistId)
        {
            return await _trackRepository.GetArtistAlbumAsync(artistId);
        }
    }
}

