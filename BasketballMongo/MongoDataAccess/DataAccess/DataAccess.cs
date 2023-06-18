using MongoDataAccess.Models;
using MongoDB.Driver;


namespace MongoDataAccess.DataAccess
{
    public class DataAccess
    {
        private const string _connectionString = "mongodb://localhost:27017";
        private const string _databaseName = "NBA";
        private const string _playerCollection = "player_collection";
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
            var userName = user.Login.ToString();
            ConsoleKeyInfo i;

            if (UserExists(userName))
            {                
                do
                {
                    Console.Clear();
                    Console.WriteLine("This login adress is already in use! Use another one.");
                    Console.WriteLine("Please type enter to continue");
                    i = Console.ReadKey();
                } while(i.Key != ConsoleKey.Enter);

                return false;
            }
            else
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("User created successfully!");
                    Console.WriteLine("Please type enter to continue");
                    i = Console.ReadKey();
                } while (i.Key != ConsoleKey.Enter);
                userCollection.InsertOneAsync(user);
                return true;
            }

        }
        public Task CreatePlayer(PlayerModel player)
        {
            var playerCollection = ConnectToMongo<PlayerModel>(_playerCollection);
            ConsoleKeyInfo i;
            if (PlayerExists(player.JerseyNumber))
            {
                do
                {
                    Console.WriteLine("Player with this jersey number already exist! Use another one.");
                    Console.WriteLine("Please type enter to continue");
                    i = Console.ReadKey();
                } while (i.Key != ConsoleKey.Enter);
            }
            else
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Player has been created successfully!");
                    Console.WriteLine("Please type enter to continue");
                    i = Console.ReadKey();

                } while (i.Key != ConsoleKey.Enter);
            }

            return playerCollection.InsertOneAsync(player);
        }
        public async Task<List<PlayerModel>> GetPlayersByUser(UserModel? user)
        {
            var playerCollection = ConnectToMongo<PlayerModel>(_playerCollection);
            var results = await playerCollection.FindAsync(x => x.AddedBy.Login == user.Login);
            return results.ToList();
        }
        public Task UpdatePlayerByNumber(PlayerModel player, string jerseyNumber)
        {
            var playerCollection = ConnectToMongo<PlayerModel>(_playerCollection);
            var filter = Builders<PlayerModel>.Filter.Eq(n => n.JerseyNumber, jerseyNumber);
            var OldPlayer = playerCollection.Find(filter).FirstOrDefault();

            var oldId = OldPlayer.Id;
            player.Id = oldId;
            ConsoleKeyInfo i;

            do
            {
                Console.WriteLine("\nPlayer has been updated successfully!");
                Console.WriteLine("Please type enter to continue");
                i = Console.ReadKey();
            }while (i.Key != ConsoleKey.Enter);

            return playerCollection.ReplaceOneAsync(filter, player);
        }
        public Task DeletePlayerByNumber(string number)
        {
            var playerCollection = ConnectToMongo<PlayerModel>(_playerCollection);
            var filter = Builders<PlayerModel>.Filter.Eq(p => p.JerseyNumber, number);
            var oldPlayer = playerCollection.Find(filter).FirstOrDefault();
            ConsoleKeyInfo i;

            if (oldPlayer == null)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Jersey number of player doesn't exist!");
                    Console.WriteLine("Please type enter to continue");
                    i = Console.ReadKey();
                }while(i.Key != ConsoleKey.Enter);
                return Task.CompletedTask;
            }
            else
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Player has been deleted successfully!");
                    Console.WriteLine("Please type enter to continue");
                    i = Console.ReadKey();
                } while (i.Key != ConsoleKey.Enter);
                return playerCollection.DeleteOneAsync(filter);
            }
        }
        public bool FindPlayerByNumber(string number)
        {
            var playerCollection = ConnectToMongo<PlayerModel>(_playerCollection);
            var filter = Builders<PlayerModel>.Filter.Eq(i => i.JerseyNumber, number);
            var player = playerCollection.Find(filter).FirstOrDefault();

            if (player == null)
            {
                ConsoleKeyInfo i;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Jersey number of player doesn't exist!");
                    Console.WriteLine("Please type enter to continue");
                    i = Console.ReadKey();
                } while (i.Key != ConsoleKey.Enter);
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
            var filter = Builders<UserModel>.Filter.Eq(u => u.Login, userName);
            var user = userCollection.Find(filter).FirstOrDefault();
            return user != null;
        }
        private bool PlayerExists(string jerseyNumber)
        {
            var playerCollection = ConnectToMongo<PlayerModel>(_playerCollection);
            var filter = Builders<PlayerModel>.Filter.Eq(j => j.JerseyNumber, jerseyNumber);
            var user = playerCollection.Find(filter).FirstOrDefault();
            return user != null;
        }
        public UserModel Login(string login, string password)
        {
            var userCollection = ConnectToMongo<UserModel>(_userCollection);
            var filter = Builders<UserModel>.Filter.Where(u => u.Login == login);
            var user = userCollection.Find(filter).FirstOrDefault();

            if (user != null && user.Password == password)
            {
                return user;
            }

            else
            {
                return null;
            }
        }

    }
}
