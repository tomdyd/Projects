using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreDataAccess.Models;
public class ValveModel : Model
{    
    public string _tubeLenght { get; set; }
    public string _destiny { get; set; }

}
