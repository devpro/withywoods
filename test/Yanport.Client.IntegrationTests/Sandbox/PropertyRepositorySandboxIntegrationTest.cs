using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Devpro.Yanport.Abstractions.Repositories;
using Devpro.Yanport.Client.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Devpro.Yanport.Client.IntegrationTests.Sandbox
{
    [Trait("Environment", "Sandbox")]
    public class PropertyRepositorySandboxIntegrationTest : RepositoryIntegrationTestBase<SandboxYanportClientConfiguration>
    {
        public PropertyRepositorySandboxIntegrationTest()
            : base(new SandboxYanportClientConfiguration())
        {
        }

        [Fact]
        public async Task PropertyRepositorySandboxFindAllAsync_ReturnToken()
        {
            // Arrange
            var repository = BuildRepository();

            // Act
            var output = await repository.FindAllAsync("?from=0&size=5&marketingTypes=SALE&active=true&published=true");

            // Assert
            output.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task RentalRouenPropertyRepositorySandboxFindAllAsync_ReturnToken()
        {
            // Arrange
            var repository = BuildRepository();

            // Act
            // &roomCounts=2
            var output = await repository.FindAllAsync("?from=0&size=100&marketingTypes=RENT&zipCodes=76000&surfaceMin=50&surfaceMax=90&active=true&published=true&priceMax=700&q=balcon");

            // Assert
            output.Should().NotBeNull();
            output.Should().NotBeEmpty();

            var sb = new StringBuilder();
            sb.AppendLine("ZipCode;ParcelIds;Ads0Url;ConstructionYear;Equipments;HeatingMode;Surface;RoomCount;RelevantPrice;RentalExpensesIncluded");
            output.ForEach(x => sb.AppendLine($"{x.Address.ZipCode};{string.Join(",", x.Address.ParcelIds)};{x.Ads[0].Url};{x.Features.Construction.Year};{x.Features.Descriptive.Equipments.Furniture};"
                + $"{x.Features.Energy.HeatingMode};{x.Features.Geometry.Surface};{x.Features.Geometry.RoomCount};{x.Marketing.RelevantPrice};{x.Marketing.RentalExpensesIncluded}"));
            var csv = sb.ToString();
            csv.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task RentalAnnecyPropertyRepositorySandboxFindAllAsync_ReturnToken()
        {
            // Arrange
            var repository = BuildRepository();

            // Act
            var zipCodes = string.Join("&zipCodes=", "74000,74370,74600,74940,74960".Split(','));
            var output = await repository.FindAllAsync($"?from=0&size=5&marketingTypes=RENT&zipCodes={zipCodes}&surfaceMin=80&surfaceMax=180&active=true&published=true&priceMax=3000");

            // Assert
            output.Should().NotBeNull();
            output.Should().NotBeEmpty();

            var sb = new StringBuilder();
            sb.AppendLine("ZipCode;ParcelIds;Ads0Url;ConstructionYear;Equipments;HeatingMode;Surface;RoomCount;RelevantPrice;RentalExpensesIncluded");
            output.ForEach(x => sb.AppendLine($"{x.Address.ZipCode};{string.Join(",", x.Address.ParcelIds)};{x.Ads[0].Url};{x.Features.Construction.Year};{x.Features.Descriptive.Equipments.Furniture};"
                + $"{x.Features.Energy.HeatingMode};{x.Features.Geometry.Surface};{x.Features.Geometry.RoomCount};{x.Marketing.RelevantPrice};{x.Marketing.RentalExpensesIncluded}"));
            var csv = sb.ToString();
            csv.Should().NotBeNullOrEmpty();
        }

        private IPropertyRepository BuildRepository()
        {
            var logger = ServiceProvider.GetService<ILogger<PropertyRepository>>();
            var httpClientFactory = ServiceProvider.GetService<IHttpClientFactory>();

            return new PropertyRepository(Configuration, logger, httpClientFactory);
        }
    }
}
