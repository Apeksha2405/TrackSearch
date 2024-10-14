using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public interface ITrackService
{
    Task<JObject> GetArtistDetails(string artistName); // Method to get artist details

    Task<JObject> GetArtistAlbums(string artistId);    // Method to get artist albums

}
