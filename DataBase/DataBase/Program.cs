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

switch (choice)
{
    case 1:
        await db.CreateUser(CreateUser());
        break;
    case 2:
        await db.CreateValve(CreateValve());
        break;
    case 3:
        //await db.CreateCan(CreateCan());
        break;
    case 4:
        var results = await db.GetAllUsers();
        foreach (var user in results)
        {
            Console.WriteLine(
                $"User ID: {user._id}\n" +
                $"First name: {user._firstName}\n" +
                $"Last name: {user._lastName}\n" +
                $"Date of birth: {user._dateOfBirth}\n" +
                $"Email adress: {user._email}\n\n");
        }
        break;
    case 5:
        var valvesResults = await db.GetAllValves();
        foreach (var valve in valvesResults)
        {
            Console.WriteLine(
                $"Valve ID: {valve._id}\n" +
                $"Index: {valve._index}\n" +
                $"Supplier: {valve._supplier}\n" +
                $"Full name: {valve._fullName}\n" +
                $"Short name: {valve._shortName}\n" +
                $"Tube length: {valve._tubeLenght}\n" +
                $"Acceptance date: {valve._acceptanceDate}\n" +
                $"Expiration date: {valve._expiriationDate}\n" +
                $"Destiny product: {valve._destiny}\n" +
                $"Amount: {valve._amount}\n" +
                $"Storage place: {valve._storagePlace}\n" +
                $"Last update: {valve._lastUpdate}\n" +
                $"Last user: {valve._lastUser}\n");
        }
        break;
    case 6:
        var cansResults = await db.GetAllCans();
        foreach (var can in cansResults)
        {
            Console.WriteLine(
                $"Can ID: {can._id}\n" +
                $"Index: {can._index}\n" +
                $"Height: {can._height}\n" +
                $"Diameter {can._diameter}" +
                $"Type of internal varnish: {can._typeOfInternalVarnish}\n" +
                $"Supplier: {can._supplier}\n" +
                $"Full name: {can._fullName}\n" +
                $"Short name: {can._shortName}\n" +
                $"Acceptance date: {can._acceptanceDate}\n" +
                $"Expiration date: {can._expiriationDate}\n" +
                $"Amount: {can._amount}\n" +
                $"Storage place: {can._storagePlace}\n" +
                $"Last update: {can._lastUpdate}\n" +
                $"Last user: {can._lastUser}\n");
        }
        break;
}

//var results = await db.GetAllUsers();

//foreach (var user in results)
//{
//    Console.WriteLine(user._fullName);
//}


#region methods
static ValveModel CreateValve()
{
    ValveModel valve = new ValveModel();

    Console.Write("Enter supplier name: ");
    valve._supplier = Console.ReadLine();

    Console.Write("Enter full name of valve: ");
    valve._fullName = Console.ReadLine();

    Console.Write("Enter short name of valve: ");
    valve._shortName = Console.ReadLine();

    Console.Write("Enter the tube lenght: ");
    valve._tubeLenght = Console.ReadLine();

    Console.Write("Enter the index of valve: ");
    valve._index = Console.ReadLine();

    Console.Write("Enter the date of acceptance: ");
    valve._acceptanceDate = DateOnly.Parse(Console.ReadLine());

    Console.Write("Enter the date of expiration: ");
    valve._expiriationDate = DateOnly.Parse(Console.ReadLine());

    Console.Write("Enter the name destiny product: ");
    valve._destiny = Console.ReadLine();

    Console.Write("Enter the amount of valves (must be a number): ");
    valve._amount = int.Parse(Console.ReadLine());

    Console.Write("Enter the number of storage place: ");
    valve._storagePlace = Console.ReadLine();

    DateTime lastUpdate = DateTime.Now;
    
    return valve;
}
<<<<<<< HEAD
static CanModel CreateCan()
{
    CanModel can = new CanModel();

    Console.Write("Enter supplier name: ");
    can._supplier = Console.ReadLine();

    Console.Write("Enter full name of can: ");
    can._fullName = Console.ReadLine();

    Console.Write("Enter short name of can: ");
    can._shortName = Console.ReadLine();

    Console.Write("Enter the index of can: ");
    can._index = Console.ReadLine();

    Console.Write("Enter the date of acceptance: ");
    can._acceptanceDate = DateOnly.Parse(Console.ReadLine());

    Console.Write("Enter the date of expiration: ");
    can._expiriationDate = DateOnly.Parse(Console.ReadLine());

    Console.Write("Enter the aount of cans (must be a number): ");
    can._amount = int.Parse(Console.ReadLine());

    Console.Write("Enter the number of storage place: ");
    can._storagePlace = Console.ReadLine();

    Console.Write("Enter height of can: ");
    can._height = Console.ReadLine();

    Console.Write("Enter the diameter of can: ");
    can._diameter = Console.ReadLine();

    Console.WriteLine("Enter type of internal varnish of a can: ");
    can._typeOfInternalVarnish = Console.ReadLine();

    return can;
}
=======

//CanModel CreateCan()
//{
//    //CanModel can = new CanModel();

//    Console.Write("Enter supplier name: ");
//    string supplier = Console.ReadLine();

//    Console.Write("Enter full name of can: ");
//    string fullName = Console.ReadLine();

//    Console.Write("Enter short name of can: ");
//    string shortName = Console.ReadLine();

//    Console.Write("Enter the index of can: ");
//    string index = Console.ReadLine();

//    Console.Write("Enter the date of acceptance: ");
//    DateOnly dateOfAcceptance = DateOnly.Parse(Console.ReadLine());

//    Console.Write("Enter the date of expiration: ");
//    DateOnly dateOfExpiration = DateOnly.Parse(Console.ReadLine());

//    Console.Write("Enter the amount of cans (must be a number): ");
//    int amount = int.Parse(Console.ReadLine());

//    Console.Write("Enter the number of storage place: ");
//    string storagePlace = Console.ReadLine();

//    return can;
//}
>>>>>>> 273c1da7af2c7fc547a30faefdd6187e7f855bc6
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