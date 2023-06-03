using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreDataAccess.Models;
public class ValveModel : Model
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    private string _id { get; set; }
    private string _tubeLenght { get; set; }
    private string _destiny { get; set; }
    public ValveModel(string tubeLenght, string destiny, string index, string supplier, string fullName, string shortName, DateOnly acceptanceDate,
        DateOnly expiriationDate, int amount, string storagePlace, DateTime lastUpdate) :
        base(index, supplier, fullName, shortName, acceptanceDate, expiriationDate, amount, storagePlace, lastUpdate)
    {
        _tubeLenght = tubeLenght;
        _destiny = destiny;
        SetIndex(index);
        SetSupplier(supplier);
        SetFullName(fullName);
        SetShortName(shortName);
        SetAcceptanceDate(acceptanceDate);
        SetExpiriationDate(expiriationDate);
        SetAmount(amount);
        SetStoragePlace(storagePlace);
        SetLastUpdate(lastUpdate);
    }
}
