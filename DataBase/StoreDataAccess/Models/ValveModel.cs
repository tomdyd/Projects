using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace StoreDataAccess.Models;
public class ValveModel : Model
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }
    public string _tubeLenght { get; set; }
    public string _destiny { get; set; } //do jakiego produktu
}
