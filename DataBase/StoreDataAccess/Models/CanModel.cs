using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDataAccess.Models;
public class CanModel : Model
{
    public CanModel(string index, string supplier, string fullName, string shortName, DateOnly acceptanceDate, DateOnly expiriationDate, int amount, string storagePlace, DateTime lastUpdate) : base(index, supplier, fullName, shortName, acceptanceDate, expiriationDate, amount, storagePlace, lastUpdate)
    {
    }

    public string _height { get; set; }
    public string _diameter { get; set; }
    public bool _internalVarnish { get; set; }
    public string _typeOfInternalVarnish { get; set; }
}
