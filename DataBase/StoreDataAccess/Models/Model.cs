using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StoreDataAccess.Models;
public abstract class Model
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }
    public string _index { get; set; }
    public string _supplier { get; set; }
    public string _fullName { get; set; }
    public string _shortName { get; set; }
    public DateOnly _acceptanceDate { get; set; }
    public DateOnly _expiriationDate { get; set; }
    public int _amount { get; set; }
    public string _storagePlace { get; set; }
    public DateTime _lastUpdate { get; set; }
    public UserModel _lastUser { get; set; }

}
    

