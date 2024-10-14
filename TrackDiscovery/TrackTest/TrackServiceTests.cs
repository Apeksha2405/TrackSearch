using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TrackDiscovery.Repository;

namespace TrackDiscovery.TrackTest
{
    [TestClass]
    public class TrackServiceTests
    {
        private Mock<ITrackRepository> _mockRepository;
        private TrackService _trackService;

        public TrackServiceTests()
        {
            var httpClient = new Mock<HttpClient>().Object; // Mock the HttpClient
            _mockRepository = new Mock<ITrackRepository>(); // Initialize the mock repository
            _trackService = new TrackService(httpClient); // Initialize the track service
        }

        [TestMethod]
        public async Task GetArtistDetails_ShouldReturnArtistDetails()
        {
            string artistName = "The Beatles";
            var mockArtistInfo = JObject.Parse("{ 'artists': [ { 'id': '123', 'name': 'The Beatles' } ] }");
            _mockRepository.Setup(repo => repo.GetArtistDetailAsync(artistName))
                           .ReturnsAsync(mockArtistInfo);

            var result = await _trackService.GetArtistDetails(artistName);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result["artists"]);
            Assert.IsTrue(result["artists"]!.HasValues);
            var firstArtist = result["artists"]![0];
            Assert.IsNotNull(firstArtist);
            Assert.AreEqual("The Beatles", firstArtist["name"]?.ToString());
        }

        [TestMethod]
        public async Task GetArtistAlbums_ShouldReturnAlbums()
        {
            string artistId = "859d0860-d480-4efd-970c-c05d5f1776b8";
            var mockAlbums = JObject.Parse("{ 'releases': [ { 'title': 'Abbey Road' } ] }");
            _mockRepository.Setup(repo => repo.GetArtistAlbumAsync(artistId))
                           .ReturnsAsync(mockAlbums);

            var result = await _trackService.GetArtistAlbums(artistId);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count,3);
        }
    }
}
