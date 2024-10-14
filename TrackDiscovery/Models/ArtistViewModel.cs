using Newtonsoft.Json.Linq;

namespace TrackDiscovery.Models
{
    public class ArtistViewModel
    {
        public required string ArtistName { get; set; }
        public required JObject ArtistDetails { get; set; } // To hold artist info
        public required JObject ArtistAlbums { get; set; } // To hold album info
    }
}
