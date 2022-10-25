using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Microsoft.Spatial;
using Newtonsoft.Json;

namespace AzureCognitiveSearch
{
    public partial class Hotel
    {
        [SimpleField(IsKey = true, IsFilterable = true)]
        public string HotelId { get; set; }

        [SearchableField(IsSortable = true)]
        public string HotelName { get; set; }

        [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene)]
        public string Description { get; set; }

        [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.FrLucene)]
        [JsonProperty("Description_fr")]
        public string DescriptionFr { get; set; }

        [SearchableField(IsSortable = true, IsFacetable = true, IsFilterable = true)]
        public string Category { get; set; }

        [SearchableField(IsFacetable = true, IsFilterable = true)]
        public string[] Tags { get; set; }

        [SimpleField(IsSortable = true, IsFacetable = true, IsFilterable = true)]
        public bool? ParkingIncluded { get; set; }

        [SimpleField(IsSortable = true, IsFacetable = true, IsFilterable = true)]
        public DateTimeOffset? LastRenovationDate { get; set; }

        [SimpleField(IsSortable = true, IsFacetable = true, IsFilterable = true)]
        public double? Rating { get; set; }

        [SimpleField(IsSortable = true, IsFilterable = true)]
        public GeographyPoint Location { get; set; }

        [SimpleField(IsSortable = true, IsFacetable = true, IsFilterable = true)]
        public double? BaseRate { get; set; }
        
        [SimpleField(IsFacetable = true, IsFilterable = true)]
        public bool? SmokingAllowed { get; set; }  
    }
}