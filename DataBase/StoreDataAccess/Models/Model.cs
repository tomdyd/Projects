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

<<<<<<< HEAD
=======
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

    public string GetIndex()
    {
        return _index;
    }
    public void SetSupplier(string supplier)
    {
        _supplier = supplier;
    }

    public string GetSupplier()
    {
        return _supplier;
    }
    public void SetFullName(string fullName)
    {
        _fullName = fullName;
    }
    public string GetFullName()
    {
        return _fullName;
    }
    public void SetShortName(string shortName)
    {
        _shortName = shortName;
    }

    public string SetShortName()
    {
        return _shortName;
    }
    public void SetAcceptanceDate(DateOnly acceptanceDate)
    {
        _acceptanceDate = acceptanceDate;
    }

    public DateOnly GetAccpetanceDate()
    {
        return _acceptanceDate;
    }
    public void SetExpiriationDate(DateOnly expiriationDate)
    {
        _expiriationDate = expiriationDate;
    }

    public DateOnly GetExpirationDate()
    {
        return _expiriationDate;
    }
    public void SetAmount(int amount)
    {
        _amount = amount;
    }

    public int GetAmount()
    {
        return _amount;
    }
    public void SetStoragePlace(string storagePlace)
    {
        _storagePlace = storagePlace;
    }

    public string GetStoragePlace()
    {
        return _storagePlace;
    }

    public void SetLastUpdate(DateTime lastUpdate)
    {
        _lastUpdate = lastUpdate;
    }

    public DateTime GetLastUpdate()
    {
        return _lastUpdate;
    }
>>>>>>> 273c1da7af2c7fc547a30faefdd6187e7f855bc6
}
    

