using Newtonsoft.Json.Linq;

namespace TrackDiscovery.Repository
{
    public class TrackRepository : ITrackRepository
    {
        private static TrackRepository? _instance = null;
        private static readonly object _lock = new object();
        private readonly HttpClient _httpClient;

        private TrackRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public static TrackRepository GetInstance(HttpClient httpClient)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new TrackRepository(httpClient);
                    }
                }
            }
            return _instance;
        }

        public async Task<JObject> GetArtistAlbumAsync(string artistId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://musicbrainz.org/ws/2/release-group?artist={artistId}&fmt=json");

            request.Headers.Add("User-Agent", "TrackDiscoveryApp/1.0 (contact@yourdomain.com)");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode(); 

            var responseBody = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseBody);
        }

        public async Task<JObject> GetArtistDetailAsync(string artistName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://musicbrainz.org/ws/2/artist/?query=artist:{artistName}&fmt=json");

            request.Headers.Add("User-Agent", "TrackDiscoveryApp/1.0 (contact@yourdomain.com)");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseBody);
        }

    }
}
