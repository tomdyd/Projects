﻿using MongoDB.Driver;
using StoreDataAccess.Models;

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
    public Task CreateCan(CanModel can)
    {
        var canCollection = ConnectToMongo<CanModel>(_canCollection);

        Console.WriteLine("\nCan has been created successfully!");
        Console.ReadKey();

        return canCollection.InsertOneAsync(can);
    }
    public async Task<List<UserModel>> GetAllUsers()
    {
        var userCollection = ConnectToMongo<UserModel>(_userCollection);
        var results = await userCollection.FindAsync(_ => true);
        return results.ToList();
    }// Funkcja potrzebna poza zaliczeniem zadania
    public async Task<List<UserModel>> GetUsers(string email)
    {
        var userCollection = ConnectToMongo<UserModel>(_userCollection);
        var results = await userCollection.FindAsync(x => x._email == email);
        return results.ToList();
    }// Funkcja potrzebna poza zaliczeniem zadania
    public async Task<List<ValveModel>> GetAllValves()
    {
        var valvesCollection = ConnectToMongo<ValveModel>(_valveCollection);
        var results = await valvesCollection.FindAsync(_ => true);
        return results.ToList();
    }// Funkcja potrzebna poza zaliczeniem zadania
    public async Task<List<ValveModel>> GetValvesByUser(UserModel? user)
    {
        var valvesCollection = ConnectToMongo<ValveModel>(_valveCollection);
        var results = await valvesCollection.FindAsync(x => x._lastUser._email == user._email);
        return results.ToList();
    }// Funkcja potrzebna poza zaliczeniem zadania
    public async Task<List<ValveModel>> GetValvesByIndex(string index)
    {
        var valvesCollection = ConnectToMongo<ValveModel>(_valveCollection);
        var results = await valvesCollection.FindAsync(x => x._index == index);
        return results.ToList();
    }// Funkcja potrzebna poza zaliczeniem zadania
    public async Task<List<CanModel>> GetAllCans()
    {
        var canCollection = ConnectToMongo<CanModel>(_canCollection);
        var results = await canCollection.FindAsync(_ => true);
        return results.ToList();
    }// Funkcja potrzebna poza zaliczeniem zadania
    public async Task<List<CanModel>> GetCansByUser(UserModel? user)
    {
        var cansCollection = ConnectToMongo<CanModel>(_canCollection);
        var results = await cansCollection.FindAsync(x => x._lastUser._email == user._email);
        return results.ToList();
    }
    public Task UpdateUserByUserName(string email, UserModel user)
    {        
        var userCollection = ConnectToMongo<UserModel>(_userCollection);
        var filter = Builders<UserModel>.Filter.Eq(e => e._email, email);
        var oldUser =  userCollection.Find(filter).FirstOrDefault();
        if (oldUser == null)
        {
            Console.WriteLine("\nUsername was not found!");
            Console.ReadKey();
            return Task.CompletedTask;
        }
        else
        {
            var oldId = oldUser._id;
            user._id = oldId;
            return userCollection.ReplaceOneAsync(filter, user);
        }
    }// Funkcja potrzebna poza zaliczeniem zadania
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
    public Task UpdateCanByIndex(CanModel can, string index)
    {
        var canCollection = ConnectToMongo<CanModel>(_canCollection);
        var filter = Builders<CanModel>.Filter.Eq(c => c._index, index);
        var oldCan = canCollection.Find(filter).First();
        if (oldCan == null)
        {
            Console.WriteLine("Can was not found!");
            Console.ReadKey();
            return Task.CompletedTask;
        }
        else
        {
            var oldId = oldCan._id;
            can._id = oldId;

            Console.WriteLine("\nCan has been updated successfully!");
            Console.ReadKey();

            return canCollection.ReplaceOneAsync(filter, can);
        }
    }
    public Task DeleteUserByUserName(string email)
    {
        var userCollection = ConnectToMongo<UserModel>(_userCollection);
        var filter = Builders<UserModel>.Filter.Eq(e => e._email, email);
        var oldUser = userCollection.Find(filter).FirstOrDefault();
        if (oldUser == null)
        {
            Console.WriteLine("\nUsername was not found!");
            Console.ReadKey();
            return Task.CompletedTask;
        }
        else
        {
            return userCollection.DeleteOneAsync(filter);
        }
    }// Funkcja potrzebna poza zaliczeniem zadania
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
    public Task DeleteCanByIndex(string index)
    {
        var canCollection = ConnectToMongo<CanModel>(_canCollection);
        var filter = Builders<CanModel>.Filter.Eq(c => c._index, index);
        var oldCan = canCollection.Find(filter).First();
        if (oldCan == null)
        {
            Console.WriteLine("Can was not found!");
            Console.ReadKey();
            return Task.CompletedTask;
        }
        else
        {
            Console.WriteLine("\nCan has been deleted successfully!");
            Console.ReadKey();
            return canCollection.DeleteOneAsync(filter);
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
    public bool FindCanByIndex(string index)
    {
        var canCollection = ConnectToMongo<CanModel>(_canCollection);
        var filter = Builders<CanModel>.Filter.Eq(i => i._index, index);
        var can = canCollection.Find(filter).FirstOrDefault();

        if (can == null)
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

