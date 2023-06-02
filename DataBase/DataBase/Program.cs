using StoreDataAccess.DataAccess;
using StoreDataAccess.Models;

DataAccess db = new DataAccess();

//await db.CreateValve(new ValveModel
//{
//    _index = "AD300000",
//    _supplier = "KHN",
//    _fullName = "VALVE U-13-12-T",
//    _shortName = "VALVE U-13-12-T",
//    _tubeLenght = "148mm",
//    _destiny = "pianka do włosów",
//    _storagePlace = "A01",
//    _acceptanceDate = new DateOnly(2023, 06, 01),
//    _expiriationDate = new DateOnly(2025, 06, 01),
//    _amount = 100,
//    _lastUpdate = DateTime.Now
////});

//var valves = await db.GetAllValves();

ValveModel valve = new ValveModel() { _id = "6478e32bb7776203d9867874",
    _index = "AD300000",
    _supplier = "KHN",
    _fullName = "VALVE U-13-12-T",
    _shortName = "VALVE U-13-12-T",
    _tubeLenght = "148mm",
    _destiny = "pianka do włosów",
    _storagePlace = "A01",
    _acceptanceDate = new DateOnly(2023, 06, 01),
    _expiriationDate = new DateOnly(2025, 06, 01),
    _amount = 100,
    _lastUpdate = DateTime.Now
};

//await db.CreateUser(new UserModel { _firstName = "Tomasz", _lastName = "Dyda" });

await db.UpdateValve(valve);


Console.WriteLine("DONE");