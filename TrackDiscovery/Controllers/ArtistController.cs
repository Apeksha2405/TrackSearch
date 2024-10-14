using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using TrackDiscovery.Repository;

public class ArtistController : Controller
{
    private readonly ITrackService _trackService;

    public ArtistController(ITrackService trackService)
    {
        _trackService = trackService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [EnableCors]
    public async Task<IActionResult> SearchArtist(string artistName)
    {
        var artistInfo = await _trackService.GetArtistDetails(artistName);
        var artist = artistInfo["artists"]?.FirstOrDefault();

        if (artist == null)
        {
            return View("Error", "Artist not found.");
        }

        var albums = await _trackService.GetArtistAlbums(artist["id"]?.ToString());

        var viewModel = new
        {
            Artist = artist,
            Albums = albums["release-groups"]
        };

        return View("ArtistDetails", viewModel);
    }
}
