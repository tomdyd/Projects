using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using StoreDataAccess.Models;

namespace StoreDataAccess.DataAccess;
public class DataAccess
{
<<<<<<< HEAD
    private const string _connectionString = "mongodb://localhost:27017";
=======
    private const string _connectionString = "mongodb://localhost:27017/";
>>>>>>> 273c1da7af2c7fc547a30faefdd6187e7f855bc6
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
    public async Task<List<ValveModel>> GetAllValves()
    {
        var valvesCollection = ConnectToMongo<ValveModel>(_valveCollection);
        var results = await valvesCollection.FindAsync(_ => true);
        return results.ToList();
    }
    public async Task<List<CanModel>> GetAllCans()
    {
        var canCollection = ConnectToMongo<CanModel>(_canCollection);
        var results = await canCollection.FindAsync(_ => true);
        return results.ToList();
    }
    public Task UpdateUser(UserModel user)
    {
        var userCollection = ConnectToMongo<UserModel>(_userCollection);
        var filter = Builders<UserModel>.Filter.Eq("_index", user._id);
        return userCollection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
    }
    public Task UpdateValve(ValveModel valve)
    {
        var valveCollection = ConnectToMongo<ValveModel>(_valveCollection);
<<<<<<< HEAD
        var filter = Builders<ValveModel>.Filter.Eq("_index", valve._index);
        return valveCollection.ReplaceOneAsync(filter, valve, new ReplaceOptions { IsUpsert = true });
    }

    public Task UpdateCan(CanModel can)
    {
        var canCollection = ConnectToMongo<CanModel>(_canCollection);
        var filter = Builders<CanModel>.Filter.Eq("_index", can._index);
        return canCollection.ReplaceOneAsync(filter, can, new ReplaceOptions { IsUpsert = true});
=======
        var filter = Builders<ValveModel>.Filter.Eq("_index", valve.GetIndex());
        return valveCollection.ReplaceOneAsync(filter, valve, new ReplaceOptions { IsUpsert = true});
>>>>>>> 273c1da7af2c7fc547a30faefdd6187e7f855bc6
    }

}

