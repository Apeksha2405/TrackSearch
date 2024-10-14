using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TrackDiscovery.Repository;

namespace TrackDiscovery.Views.Artist
{
    public class ArtistModel : PageModel
    {
            private readonly ITrackRepository _trackRepository;

            public ArtistModel(ITrackRepository trackRepository)
            {
                _trackRepository = trackRepository;
            }

            [BindProperty]
            public string ArtistName { get; set; }
            public JObject ArtistDetails { get; set; }
            public JObject ArtistAlbums { get; set; }

            public async Task OnPostAsync()
            {
                if (!string.IsNullOrWhiteSpace(ArtistName))
                {
                    // Fetch artist details
                    ArtistDetails = await _trackRepository.GetArtistDetailAsync(ArtistName);
                    if (ArtistDetails != null && ArtistDetails["artists"] != null)
                    {
                        // Fetch albums using the first artist ID
                        string artistId = ArtistDetails["artists"][0]["id"].ToString();
                        ArtistAlbums = await _trackRepository.GetArtistAlbumAsync(artistId);
                    }
                }
            }
        }
    }