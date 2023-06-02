using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDataAccess.Models;
public class CanModel : Model
{
    public string _height { get; set; }
    public string _diameter { get; set; }
    public bool _internalVarnish { get; set; }
    public string _typeOfInternalVarnish { get; set; }
}
