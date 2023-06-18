using MongoDB.Driver;
using StoreDataAccess.Models;

namespace StoreDataAccess.DataAccess;
public class DataAccess
{
    private const string _connectionString = "mongodb://localhost:27017";
    private const string _databaseName = "NBA";
    private const string _valveCollection = "players";
    public IMongoCollection<T> ConnectToMongo<T>(in string collection)
    {
        var client = new MongoClient(_connectionString);
        var db = client.GetDatabase(_databaseName);
        return db.GetCollection<T>(collection);
    }
    public bool CreateUser(UserModel user)
    {
        var userCollection = ConnectToMongo<UserModel>(_userCollection);
        var userName = user._email.ToString();

        if (UserExists(userName))
        {
            Console.WriteLine("\nThis email adress is already in use! Use another one.");
            Console.ReadKey();
            return false;
        }
        else
        {
            userCollection.InsertOneAsync(user);
            return true;
        }
        
    }
    public Task CreateValve(ValveModel valve)
    {
        var valveCollection = ConnectToMongo<ValveModel>(_valveCollection);

        Console.WriteLine("\nValve has been created successfully!");
        Console.ReadKey();

        return valveCollection.InsertOneAsync(valve);
    }
    public async Task<List<ValveModel>> GetValvesByUser(UserModel? user)
    {
        var valvesCollection = ConnectToMongo<ValveModel>(_valveCollection);
        var results = await valvesCollection.FindAsync(x => x._lastUser._email == user._email);
        return results.ToList();
    }
    public Task UpdateValveByIndex(ValveModel valve, string index)
    {
        var valveCollection = ConnectToMongo<ValveModel>(_valveCollection);
        var filter = Builders<ValveModel>.Filter.Eq(i => i._index, index);
        var OldValve = valveCollection.Find(filter).FirstOrDefault();

        if (OldValve == null)
        {
            Console.WriteLine("Valve was not found!");
            Console.ReadKey();
            return Task.CompletedTask;
        }
        else
        {
            var oldId = OldValve._id;
            valve._id = oldId;

            Console.WriteLine("\nValve has been updated successfully!");
            Console.ReadKey();

            return valveCollection.ReplaceOneAsync(filter, valve);
        }
    }
    public Task DeleteValveByIndex(string index) 
    {
        var valveCollection = ConnectToMongo<ValveModel>(_valveCollection);
        var filter = Builders<ValveModel>.Filter.Eq(v => v._index, index);
        var oldValve = valveCollection.Find(filter).FirstOrDefault();
        if (oldValve == null)
        {
            Console.WriteLine("Valve was not found!");
            Console.ReadKey();
            return Task.CompletedTask;
        }
        else
        {
            Console.WriteLine("\nValve has been deleted successfully!");
            Console.ReadKey();
            return valveCollection.DeleteOneAsync(filter);
        }
    }
    public bool FindValveByIndex(string index)
    {
        var valveCollection = ConnectToMongo<ValveModel>(_valveCollection);
        var filter = Builders<ValveModel>.Filter.Eq(i => i._index, index);
        var valve = valveCollection.Find(filter).FirstOrDefault();

        if (valve == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool UserExists(string userName)
    {
        var userCollection = ConnectToMongo<UserModel>(_userCollection);
        var filter = Builders<UserModel>.Filter.Eq(u => u._email, userName);
        var user = userCollection.Find(filter).FirstOrDefault();
        return user != null;
    }
    public UserModel Login(string email, string password)
    {
        var userCollection = ConnectToMongo<UserModel>(_userCollection);
        var filter = Builders<UserModel>.Filter.Where(u => u._email == email);
        var user = userCollection.Find(filter).FirstOrDefault();

        if (user != null && user._password == password)
        {
            return user;
        }

        else
        {
            return null;
        }
    }

}

