using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using AzureCognitiveSearch;
using System.Collections.Generic;

Console.WriteLine("Importar Data");

var searchIndexClient = CreateSearchIndexClient();
//CreateIndex(searchIndexClient);

List<Hotel> hoteles = ReadData();
SearchClient searchClient = searchIndexClient.GetSearchClient("hoteles");
ImportData(hoteles, searchClient);
Console.WriteLine("Fin Importar Data");

//METODOS

SearchIndexClient CreateSearchIndexClient()
{
    string searchServiceEndPoint = "----";
    string adminApiKey = "-------------------------------------";

    SearchIndexClient indexClient = new SearchIndexClient(new Uri(searchServiceEndPoint), new AzureKeyCredential(adminApiKey));
    return indexClient;
}

void CreateIndex(SearchIndexClient serviceClient)
{
    FieldBuilder fieldBuilder = new FieldBuilder();
    var searchFields = fieldBuilder.Build(typeof(Hotel));
    var definition = new SearchIndex("hoteles", searchFields);

    serviceClient.CreateIndex(definition);
}

List<Hotel> ReadData()
{
    string urlPath = Path.Combine(Environment.CurrentDirectory, "Hoteles_Data.txt");
    List<Hotel> hoteles = new List<Hotel>();
    var data = File.ReadAllLines(urlPath);
    for (int i = 1; i < data.Length; i++)
    {
        var infoHotel = data[i].Split("\t");
        hoteles.Add(
            new Hotel()
            {
                HotelId = infoHotel[0],
                HotelName = infoHotel[1],
                Description = infoHotel[2],
                DescriptionFr = infoHotel[3],
                Category = infoHotel[4],
                Tags = infoHotel[5].Split(","),
                ParkingIncluded = infoHotel[6] == "0" ? false : true,
                SmokingAllowed = infoHotel[7] == "0" ? false : true,
                LastRenovationDate = Convert.ToDateTime(infoHotel[8]),
                BaseRate = Convert.ToDouble(infoHotel[9]),
                Rating = (int)Convert.ToDouble(infoHotel[10])
            }
        );
    }

    return hoteles;
}

void ImportData(List<Hotel> hoteles, SearchClient searchClient)
{
    var actions = new List<IndexDocumentsAction<Hotel>>();
    foreach (var hotel in hoteles)
    {
        actions.Add(IndexDocumentsAction.Upload(hotel));
    }
    var batch = IndexDocumentsBatch.Create(actions.ToArray());

    try
    {
        IndexDocumentsResult result = searchClient.IndexDocuments(batch);
    }
    catch (Exception)
    {
        Console.WriteLine("Failed to index some of the documents: {0}");
    }
}