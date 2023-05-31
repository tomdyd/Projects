//using MongoDB.Driver;
//using StoreDataBase;

//string connectionString = "mongodb://127.0.0.1:27017";
//string dataBaseName = "simple_db";
//string collectionName = "people";

//var client = new MongoClient(connectionString);
//var db = client.GetDatabase(dataBaseName);
//var collection = db.GetCollection<PersonModel>(collectionName);

//var person = new PersonModel { FirstName = "Tomasz", LastName = "Dyda" };

//await collection.InsertOneAsync(person);

//var results = await collection.FindAsync(_ => true);

//foreach (var result in results.ToList())
//{
//    Console.WriteLine($"{result.Id} {result.FirstName} {result.LastName}");
//}



using MongoDataAccess.DataAccess;
using MongoDataAccess.Models;

ChoreDataAccess db = new ChoreDataAccess();

await db.CreateUser(new UserModel { FirstName = "Tomasz", LastName = "Dyda" });
