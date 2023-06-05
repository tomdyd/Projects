using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using StoreDataAccess.Models;
using static MongoDB.Driver.WriteConcern;

namespace StoreDataAccess.DataAccess;
public class DataAccess
{
    private const string _connectionString = "mongodb://localhost:27017";
    private const string _databaseName = "Store_db";
    private const string _valveCollection = "valve_collection";
    private const string _canCollection = "can_collection";
    private const string _userCollection = "user_collection";

    public IMongoCollection<T> ConnectToMongo<T>(in string collection)
    {
        var client = new MongoClient(_connectionString);
        var db = client.GetDatabase(_databaseName);
        return db.GetCollection<T>(collection);
    }
    public Task CreateUser(UserModel user)
    {
        var userCollection = ConnectToMongo<UserModel>(_userCollection);
        return userCollection.InsertOneAsync(user);
    }
    public Task CreateValve(ValveModel valve)
    {
        var valveCollection = ConnectToMongo<ValveModel>(_valveCollection);
        return valveCollection.InsertOneAsync(valve);
    }

    public Task CreateCan(CanModel can)
    {
        var canCollection = ConnectToMongo<CanModel>(_canCollection);
        return canCollection.InsertOneAsync(can);
    }
    public async Task<List<UserModel>> GetAllUsers()
    {
        var userCollection = ConnectToMongo<UserModel>(_userCollection);
        var results = await userCollection.FindAsync(_ => true);
        return results.ToList();
    }

    public async Task<List<UserModel>> GetAllUsers(string email)
    {
        var userCollection = ConnectToMongo<UserModel>(_userCollection);
        var results = await userCollection.FindAsync(x => x._email == email);
        return results.ToList();
    }

    // Metoda wyszukująca i zwracająca wszystkie elementy z kolekcji
    public async Task<List<ValveModel>> GetAllValves()
    {
        var valvesCollection = ConnectToMongo<ValveModel>(_valveCollection);
        var results = await valvesCollection.FindAsync(_ => true);
        return results.ToList();
    }

    // Metoda wyszukująca i zwracająca elementy z kolekcji przypisane do zalogowanego użytkownika
    public async Task<List<ValveModel>> GetAllValves(UserModel user)
    {
        var valvesCollection = ConnectToMongo<ValveModel>(_valveCollection);
        var results = await valvesCollection.FindAsync(x => x._lastUser._email == user._email);
        return results.ToList();
    }
    // Metoda wyszukująca i zwracająca element o konkretnym indeksie
    public async Task<List<ValveModel>> GetAllValves(ValveModel valve, string index)
    {
        var valvesCollection = ConnectToMongo<ValveModel>(_valveCollection);
        var results = await valvesCollection.FindAsync(x => x._index == index);
        return results.ToList();
    }
    public async Task<List<CanModel>> GetAllCans()
    {
        var canCollection = ConnectToMongo<CanModel>(_canCollection);
        var results = await canCollection.FindAsync(_ => true);
        return results.ToList();
    }
    public void UpdateUser(string email, UserModel user)
    {        
        var userCollection = ConnectToMongo<UserModel>(_userCollection);
        var filter = Builders<UserModel>.Filter.Eq(e => e._email, email);
        //var filter = Builders<UserModel>.Filter.Eq("_email", user._email);
        var oldUser = userCollection.Find(filter).First();
        var oldId = oldUser._id;
        user._id = oldId;
        userCollection.FindOneAndReplace(filter, user/* new ReplaceOptions { IsUpsert = true}*/);
    }
    public Task UpdateValve(ValveModel valve)
    {
        var valveCollection = ConnectToMongo<ValveModel>(_valveCollection);
        var filter = Builders<ValveModel>.Filter.Eq("_index", valve._index);
        return valveCollection.ReplaceOneAsync(filter, valve, new ReplaceOptions { IsUpsert = true });
    }

    public Task UpdateCan(CanModel can)
    {
        var canCollection = ConnectToMongo<CanModel>(_canCollection);
        var filter = Builders<CanModel>.Filter.Eq("_index", can._index);
        return canCollection.ReplaceOneAsync(filter, can, new ReplaceOptions { IsUpsert = true});
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

