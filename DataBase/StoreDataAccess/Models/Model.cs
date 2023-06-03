using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using MongoDB.Bson.Serialization.IdGenerators;

namespace StoreDataAccess.Models;
public abstract class Model
{
    private string _index { get; set; }
    private string _supplier { get; set; }
    private string _fullName { get; set; }
    private string _shortName { get; set; }
    private DateOnly _acceptanceDate { get; set; }
    private DateOnly _expiriationDate { get; set; }
    private int _amount { get; set; }
    private string _storagePlace { get; set; }
    private DateTime _lastUpdate { get; set; }
    private UserModel _lastUser { get; set; }

    protected Model(string index, string supplier, string fullName, string shortName, DateOnly acceptanceDate, DateOnly expiriationDate, int amount, string storagePlace, DateTime lastUpdate)
    {
        _index = index;
        _supplier = supplier;
        _fullName = fullName;
        _shortName = shortName;
        _acceptanceDate = acceptanceDate;
        _expiriationDate = expiriationDate;
        _amount = amount;
        _storagePlace = storagePlace;
        _lastUpdate = lastUpdate;
    }
    public void SetIndex(string index)
    {
        _index = index;
    }
    public void SetSupplier(string supplier)
    {
        _supplier = supplier;
    }
    public void SetFullName(string fullName)
    {
        _fullName = fullName;
    }
    public void SetShortName(string shortName)
    {
        _shortName = shortName;
    }
    public void SetAcceptanceDate(DateOnly acceptanceDate)
    {
        _acceptanceDate = acceptanceDate;
    }
    public void SetExpiriationDate(DateOnly expiriationDate)
    {
        _expiriationDate = expiriationDate;
    }
    public void SetAmount(int amount)
    {
        _amount = amount;
    }
    public void SetStoragePlace(string storagePlace)
    {
        _storagePlace = storagePlace;
    }

    public void SetLastUpdate(DateTime lastUpdate)
    {
        _lastUpdate = lastUpdate;
    }
}
    

