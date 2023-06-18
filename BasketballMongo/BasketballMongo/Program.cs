using MongoDataAccess;
using MongoDataAccess.DataAccess;
using MongoDataAccess.Models;
using System.Runtime.ExceptionServices;

var db = new DataAccess();
UserModel? userLogged = null;

int choice;
bool access = false;
bool isNumber;

while (true)
{
    Console.Clear();
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Register");
    Console.WriteLine("3. Exit");

    do
    {
        isNumber = int.TryParse(Console.ReadLine(), out choice);

    } while (!isNumber);

    Console.Clear();

    switch (choice)
    {
        case 1:
            string msg = "Enter login: ";
            string temp = Validate.StringInput(msg);
            string login = temp.ToLower();

            msg = "Enter password: ";
            string password = Validate.SetPassword(msg);

            userLogged = db.Login(login, password);

            if (userLogged == null)
            {
                Console.WriteLine("\nInvalid username or password!\n");
                Console.ReadKey();
            }
            else if (userLogged != null)
            {
                access = true;
                ConsoleKeyInfo i;
                do
                {
                    Console.Clear();
                    Console.WriteLine(
                        "Logged as:\n" +
                        $"User ID: {userLogged._id}\n" +
                        $"Login: {userLogged.Login}\n" +
                        $"\nPlease type enter to continue");
                    i = Console.ReadKey();
                    if (i.Key != ConsoleKey.Enter)
                    {
                        continue;
                    }
                } while (i.Key != ConsoleKey.Enter);
            }
            break;

        case 2:
            db.CreateUser(UserModel.CreateUser());
            break;

        case 3:
            Environment.Exit(0);
            break;
    }
    while (access == true)
    {
        while (access == true)
        {
            Console.Clear();
            Console.WriteLine("1. Add player");
            Console.WriteLine("2. Read players");
            Console.WriteLine("3. Change players");
            Console.WriteLine("4. Delete players");
            Console.WriteLine("5. Logout");
            isNumber = int.TryParse(Console.ReadLine(), out choice);

            if (!isNumber)
            {
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    await db.CreatePlayer(PlayerModel.CreatePlayer(userLogged));
                    break;

                case 2:
                    Console.Clear();
                    ConsoleKeyInfo i;
                    var playersResult = await db.GetPlayersByUser(userLogged);

                    do
                    {
                        PlayerModel.Print(playersResult);
                        Console.WriteLine("Please type enter to continue");
                        i = Console.ReadKey();
                    } while(i.Key != ConsoleKey.Enter);
                    
                    break;

                case 3:
                    Console.Clear();
                    playersResult = await db.GetPlayersByUser(userLogged);
                    PlayerModel.Print(playersResult);

                    string msg = "Enter jeresey number of player you want to update: ";
                    string jerseyNumber = Validate.StringInput(msg).ToUpper();

                    var isPlayerExist = db.FindPlayerByNumber(jerseyNumber);
                    if (isPlayerExist)
                    {
                        var updatePlayer = PlayerModel.CreatePlayer(userLogged);
                        await db.UpdatePlayerByNumber(updatePlayer, jerseyNumber);
                    }

                    break;

                case 4:
                    Console.Clear();

                    playersResult = await db.GetPlayersByUser(userLogged);
                    PlayerModel.Print(playersResult);

                    msg = "Enter jersey number of player you want to delete: ";
                    jerseyNumber = Validate.StringInput(msg).ToUpper();
                    await db.DeletePlayerByNumber(jerseyNumber);
                    break;
           
                case 5:
                    userLogged = null;
                    access = false;
                    break;
            }
        }
    }
}


