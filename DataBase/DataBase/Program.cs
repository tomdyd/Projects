using MongoDB.Driver;
using StoreDataAccess.DataAccess;
using StoreDataAccess.Models;
using System.Text;
using static MongoDB.Driver.WriteConcern;

DataAccess db = new DataAccess();
UserModel userLogged = null;

int choice;

do
{
    Console.Clear();
    var dateTime = DateTime.Now;
    Console.WriteLine(dateTime);
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Register");
    Console.WriteLine("3. Exit");
    choice = int.Parse(Console.ReadLine());
    Console.Clear();

    switch (choice)
    {
        case 1:
            Console.Write("Enter email address: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = SetPassword();

            userLogged = db.Login(email, password);

            if (userLogged == null)
            {
                Console.WriteLine("\nInvalid username or password!\n");
                Console.ReadKey();
            }
            else if (userLogged != null)
            {
                ConsoleKeyInfo i;
                do
                {
                    Console.Clear();
                    Console.WriteLine(
                        "Logged as:\n" +
                        $"User ID: {userLogged._id}\n" +
                        $"First name: {userLogged._firstName}\n" +
                        $"Last name: {userLogged._lastName}\n" +
                        $"Date of birth: {userLogged._dateOfBirth}\n" +
                        $"Email address: {userLogged._email}\n" +
                        $"Password: {userLogged._password} \n" +
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
            db.CreateUser(CreateUser());
            break;
        case 3:
            Environment.Exit(0);
            break;
    }
} while (userLogged == null);

while (true)
{
    Console.Clear();
    Console.WriteLine("1. Add components");
    Console.WriteLine("2. Read components");
    Console.WriteLine("3. Change components");
    Console.WriteLine("4. Delete components");
    bool isNumber = int.TryParse(Console.ReadLine(), out choice);
    if (!isNumber)
    {
        continue;
    }

    switch(choice)
    {
        case 1:
            Console.Clear();
            Console.WriteLine("1. Add valve");
            Console.WriteLine("2. Add can");
            isNumber = int.TryParse(Console.ReadLine(), out choice);
            if (!isNumber)
            {
                continue;
            }
            switch(choice)
            {
                case 1:
                    Console.Clear();
                    await db.CreateValve(CreateValve());
                    break;
                case 2:
                    Console.Clear();
                    CreateCan();
                    break;
            }
            break;

        case 2:
            Console.Clear();
            Console.WriteLine("1. Read valves list");
            Console.WriteLine("2. Read cans list");
            isNumber = int.TryParse(Console.ReadLine(), out choice);

            ConsoleKeyInfo key;
            switch (choice)
            {                
                case 1:
                    Console.Clear();
                    do
                    {
                        ValvesByUsersDisplay();
                        key = Console.ReadKey();
                    } while (key.Key != ConsoleKey.Enter);
                    break;
                case 2:
                    Console.Clear();
                    do
                    {
                        CansDisplay();
                        key = Console.ReadKey();
                    } while (key.Key != ConsoleKey.Enter);
                    break;
            }
            break;

        case 3:
            Console.Clear();
            Console.WriteLine("1. Update valve");
            Console.WriteLine("2. Update can");
            isNumber = int.TryParse(Console.ReadLine(), out choice);
            switch(choice)
            {
                case 1:
                    Console.Clear();
                    Console.Write("Enter index of valve you want to update: ");
                    string valveIndex = Console.ReadLine();
                    if (string.IsNullOrEmpty(valveIndex))
                    {
                        Console.WriteLine("Index can not be empty!");
                        Console.ReadKey();
                        break;
                    }

                    var updateValve = CreateValve();
                    await db.UpdateValveByIndex(updateValve, valveIndex);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Enter index of valve you want to update: ");
                    string canIndex = Console.ReadLine();
                    if(string.IsNullOrEmpty(canIndex))
                    {
                        Console.WriteLine("Index can not be empty!");
                        Console.ReadKey();
                        break;
                    }

                    var updateCan = CreateCan();
                    await db.UpdateCanByIndex(updateCan, canIndex);
                    break;
            }
            break;

        case 4:
            Console.Clear();
            Console.WriteLine("1. Delete valve");
            Console.WriteLine("2. Delete can");
            isNumber = int.TryParse(Console.ReadLine(), out choice);
            switch(choice)
            {
                case 1:
                    Console.Clear();
                    Console.Write("Enter index of valve you want to delete: ");
                    string valveIndex = Console.ReadLine();
                    db.DeleteValveByIndex(valveIndex);
                    break;
                case 2:
                    Console.Clear();
                    Console.Write("Enter index of can you want to delete: ");
                    string canIndex = Console.ReadLine();
                    db.DeleteCanByIndex(canIndex);
                    break;
            }
            break;
    }
}
#region methods
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
    user._password = SetPassword();

    return user;
}
ValveModel CreateValve()
{
    ValveModel valve = new ValveModel();
    string temp;
    do
    {
        Console.Write("Enter supplier name: ");
        temp = Console.ReadLine();

        if (string.IsNullOrEmpty(temp))
        {
            Console.WriteLine("This field can not be empty");
        }
        else
        {
            valve._supplier = temp;
        }

    } while (string.IsNullOrEmpty(temp));

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

    //Console.Write("Enter the date of expiration: ");
    valve._expiriationDate = valve._acceptanceDate.AddYears(2);

    Console.Write("Enter the name destiny product: ");
    valve._destiny = Console.ReadLine();

    Console.Write("Enter the amount of valves (must be a number): ");
    valve._amount = int.Parse(Console.ReadLine());

    Console.Write("Enter the number of storage place: ");
    valve._storagePlace = Console.ReadLine();

    valve._lastUpdate = DateTime.Now;

    valve._lastUser = userLogged;
    
    return valve;
}
CanModel CreateCan()
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

    can._lastUpdate = DateTime.Now;

    can._lastUser = userLogged;

    Console.Write("Enter the diameter of can: ");
    can._diameter = Console.ReadLine();

    Console.Write("Enter height of can: ");
    can._height = Console.ReadLine();
    
    Console.WriteLine("Enter type of internal varnish of a can: ");
    can._typeOfInternalVarnish = Console.ReadLine();

    return can;
}
static string SetPassword()
{
    var password = new StringBuilder();
    string psw = "";
    while (true)
    {
        ConsoleKeyInfo i = Console.ReadKey(true);
        if (i.Key == ConsoleKey.Enter)
        {
            break;
        }
        else if (i.Key == ConsoleKey.Backspace)
        {
            if(password.Length > 0)
            {
                password.Remove(password.Length - 1, 1);
                Console.Write("\b \b");
            }
        }
        else
        {
            password.Append(i.KeyChar);
            Console.Write("*");
        }
        psw = password.ToString();
    }

    return psw;

}
async void UsersDisplay()
{
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

    Console.WriteLine("Please type enter key to continue");
}
async void ValvesByUsersDisplay()
{
    var valvesResults = await db.GetAllValvesByUser(userLogged);
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
            $"Last user: {valve._lastUser._email}\n");
    }

    Console.WriteLine("Please type enter key to continue");
}
async void CansDisplay()
{
    var result = await db.GetAllCans();
    foreach (var can in result)
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

    Console.WriteLine("Please type enter key to continue");
}
#endregion