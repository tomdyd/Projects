using StoreDataAccess.DataAccess;
using StoreDataAccess.Models;
using static MongoDB.Driver.WriteConcern;

DataAccess db = new DataAccess();

Console.WriteLine("1. Create user");
Console.WriteLine("2. Create valve");
Console.WriteLine("3. Create can");
Console.WriteLine("4. Read all users");
Console.WriteLine("5. Read all valves");
Console.WriteLine("6. Read all cans");

int choice = int.Parse(Console.ReadLine());

switch(choice)
{
    case 1:
        await db.CreateUser(CreateUser());
        break;
    case 2:
        await db.CreateValve(CreateValve());
        break;
    case 3:
        await db.CreateCan(CreateCan());
        break;
    case 4:
        var results = await db.GetAllUsers();
        foreach (var  user in results)
        {
            Console.WriteLine(
                $"User ID: {user._id}\n" +
                $"First name: {user._firstName}\n" +
                $"Last name: {user._lastName}\n" +
                $"Date of birth: {user._dateOfBirth}\n" +
                $"Email adress: {user._email}\n\n");
        }
        break;

    default:
        break;
}

//var results = await db.GetAllUsers();

//foreach (var user in results)
//{
//    Console.WriteLine(user._fullName);
//}


#region methods
ValveModel CreateValve()
{
    Console.Write("Enter supplier name: ");
    string supplier = Console.ReadLine();

    Console.Write("Enter full name of valve: ");
    string fullName = Console.ReadLine();

    Console.Write("Enter short name of valve: ");
    string shortName = Console.ReadLine();

    Console.Write("Enter the tube lenght: ");
    string tubeLenght = Console.ReadLine();

    Console.Write("Enter the index of valve: ");
    string index = Console.ReadLine();

    Console.Write("Enter the date of acceptance: ");
    DateOnly acceptanceDate = DateOnly.Parse(Console.ReadLine());

    Console.Write("Enter the date of expiration: ");
    DateOnly expirationDate = DateOnly.Parse(Console.ReadLine());

    Console.Write("Enter the name destiny product: ");
    string destiny = Console.ReadLine();

    Console.Write("Enter the amount of valves (must be a number): ");
    int amount = int.Parse(Console.ReadLine());

    Console.Write("Enter the number of storage place: ");
    string storagePlace = Console.ReadLine();

    DateTime lastUpdate = DateTime.Now;

    ValveModel valve = new ValveModel(tubeLenght, destiny, index, supplier, fullName, shortName, acceptanceDate, expirationDate, amount, storagePlace, lastUpdate);
    
    return valve;
}

CanModel CreateCan()
{
    //CanModel can = new CanModel();

    Console.Write("Enter supplier name: ");
    string supplier = Console.ReadLine();

    Console.Write("Enter full name of can: ");
    string fullName = Console.ReadLine();

    Console.Write("Enter short name of can: ");
    string shortName = Console.ReadLine();

    Console.Write("Enter the index of can: ");
    string index = Console.ReadLine();

    Console.Write("Enter the date of acceptance: ");
    DateOnly dateOfAcceptance = DateOnly.Parse(Console.ReadLine());

    Console.Write("Enter the date of expiration: ");
    DateOnly dateOfExpiration = DateOnly.Parse(Console.ReadLine());

    Console.Write("Enter the amount of cans (must be a number): ");
    int amount = int.Parse(Console.ReadLine());

    Console.Write("Enter the number of storage place: ");
    string storagePlace = Console.ReadLine();

    //return can;
}
UserModel CreateUser()
{
    UserModel user = new UserModel();

    Console.Write("Enter your first name: ");
    user._firstName = Console.ReadLine();
    Console.Write("Enter your last name: ");
    user._lastName = Console.ReadLine();
    Console.Write("Enter your date of birth: ");
    user._dateOfBirth = DateOnly.Parse(Console.ReadLine());
    Console.Write("Enter your email: ");
    user._email = Console.ReadLine();
    Console.Write("Set your password: ");
    user._password = Console.ReadLine();

    return user;
}
#endregion