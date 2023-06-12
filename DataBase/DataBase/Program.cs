using StoreDataAccess.DataAccess;
using StoreDataAccess.Models;
using System.Text;

DataAccess db = new DataAccess();
UserModel? userLogged = null;

int choice;
bool access = false;
bool isNumber;

#region Main menu
while (true)
{
    Console.Clear();
    var dateTime = DateTime.Now;
    Console.WriteLine(dateTime);
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
        #region login
        case 1:
            string msg = "Enter email address: ";
            string email = ValidateStringInput(msg).ToLower();

            msg = "Enter password: ";
            string password = SetPassword(msg);

            userLogged = db.Login(email.ToLower(), password);

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
                        $"First name: {userLogged._firstName}\n" +
                        $"Last name: {userLogged._lastName}\n" +
                        $"Date of birth: {userLogged._dateOfBirth}\n" +
                        $"Email address: {userLogged._email}\n" +
                        $"\nPlease type enter to continue");
                    i = Console.ReadKey();
                    if (i.Key != ConsoleKey.Enter)
                    {
                        continue;
                    }
                } while (i.Key != ConsoleKey.Enter);
            }
            break;
        #endregion

        case 2:
            db.CreateUser(CreateUser());
            break;
        case 3:
            Environment.Exit(0);
            break;
    }
#endregion

    while (access == true)
    {
        Console.Clear();
        Console.WriteLine("1. Add components");
        Console.WriteLine("2. Read components");
        Console.WriteLine("3. Change components");
        Console.WriteLine("4. Delete components");
        Console.WriteLine("5. Logout");
        isNumber = int.TryParse(Console.ReadLine(), out choice);

        if (!isNumber)
        {
            continue;
        }
        
        switch (choice)
        {
            #region Creating components
            case 1:
                Console.Clear();
                Console.WriteLine("1. Add valve");
                Console.WriteLine("2. Add can");
                Console.WriteLine("3. Go back");
                isNumber = int.TryParse(Console.ReadLine(), out choice);

                if (!isNumber)
                {
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        await db.CreateValve(CreateValve());
                        break;
                    case 2:
                        Console.Clear();
                        await db.CreateCan(CreateCan());
                        break;
                    case 3:
                        break;
                }
                break;
            #endregion

            #region Reading components
            case 2:
                Console.Clear();
                Console.WriteLine("1. Read valves list");
                Console.WriteLine("2. Read cans list");
                Console.WriteLine("3. Go back");
                isNumber = int.TryParse(Console.ReadLine(), out choice);

                ConsoleKeyInfo key;
                switch (choice)
                {
                    case 1:
                        do
                        {
                            Console.Clear();
                            ReadValvesByUser();
                            key = Console.ReadKey();
                        } while (key.Key != ConsoleKey.Enter);
                        break;
                    case 2:
                        do
                        {
                            Console.Clear();
                            ReadCansByUser();
                            key = Console.ReadKey();
                        } while (key.Key != ConsoleKey.Enter);
                        break;
                    case 3:
                        break;
                }
                break;
            #endregion

            #region Updating components
            case 3:
                Console.Clear();
                Console.WriteLine("1. Update valve");
                Console.WriteLine("2. Update can");
                Console.WriteLine("3. Go back");
                isNumber = int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        string msg = "Enter index of valve you want to update: ";
                        string valveIndex = ValidateStringInput(msg).ToUpper();

                        var isValveExist = db.FindValveByIndex(valveIndex);

                        if (isValveExist)
                        {
                            var updateValve = CreateValve();
                            await db.UpdateValveByIndex(updateValve, valveIndex);
                        }
                        else
                        {
                            Console.WriteLine("Valve index doesn't exist!");
                            Console.ReadKey();
                        }
                        break;
                    case 2:
                        Console.Clear();
                        msg = "Enter index of can you want to update: ";
                        string canIndex = ValidateStringInput(msg).ToUpper();

                        var isCanExist = db.FindCanByIndex(canIndex);

                        if (isCanExist)
                        {
                            var updateCan = CreateCan();
                            await db.UpdateCanByIndex(updateCan, canIndex);
                        }
                        else
                        {
                            Console.WriteLine("Can index doesn't exist!");
                            Console.ReadKey();
                        }
                        break;
                    case 3:
                        break;
                }
                break;
            #endregion

            #region Deleting components
            case 4:
                Console.Clear();
                Console.WriteLine("1. Delete valve");
                Console.WriteLine("2. Delete can");
                Console.WriteLine("3. Go back");
                isNumber = int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        string msg = "Enter index of valve you want to delete: ";
                        string valveIndex = ValidateStringInput(msg).ToUpper();
                        await db.DeleteValveByIndex(valveIndex);
                        break;
                    case 2:
                        Console.Clear();
                        msg = "Enter index of can you want to delete: ";
                        string canIndex = ValidateStringInput(msg).ToUpper();
                        await db.DeleteCanByIndex(canIndex);
                        break;
                    case 3:
                        break;
                }
                break;
            #endregion

            case 5:
                userLogged = null;
                access = false;
                break;                
        }
    }
}
#region methods
UserModel CreateUser()
{
    UserModel user = new UserModel();
    string msg, temp, temp1;
    bool isCorrect;

    msg = "Enter your first name: ";
    temp = ValidateStringInput(msg);
    user._firstName = ValidateLetters(temp);

    msg = "Enter your last name: ";
    temp = ValidateStringInput(msg);
    user._lastName = ValidateLetters(temp);

    msg = "Enter your date of birth (DD-MM-YYYY): ";
    user._dateOfBirth = ValidateDateInput(msg);    

    msg = "Enter your email: ";
    temp = ValidateStringInput(msg);
    while(!ValidateEmail(temp))
    {
        Console.Clear();
        Console.WriteLine("Wrong email address, try again.");
        temp = ValidateStringInput(msg);
    }
    user._email = temp.ToLower();

    do
    {
        msg = "Set your password: ";
        temp = SetPassword(msg);
        while (!ValidatePassword(temp))
        {
            Console.Clear();
            Console.WriteLine("The password must contain at least one uppercase letter, one special" +
                " character and one digit. Try again: ");
            temp = SetPassword(msg);
        }

        Console.WriteLine("\nEnter your password again.");
        temp1 = SetPassword(msg);

        isCorrect = ComparePasswords(temp, temp1);

        if(!isCorrect)
        {
            Console.WriteLine("\nPassword do not match, try again!");
            Console.ReadKey();
            Console.Clear();
        }
    } while (!isCorrect);
    user._password = temp;

    return user;
}
ValveModel CreateValve()
{
    ValveModel valve = new ValveModel();
    string msg;

    msg = "Enter supplier of valve: ";
    valve._supplier = ValidateStringInput(msg).ToUpper();

    msg = "Enter full name of valve: ";
    valve._fullName = ValidateStringInput(msg).ToUpper();

    msg = "Enter short name of valve: ";
    valve._shortName = ValidateStringInput(msg).ToUpper();

    msg = "Enter the tube lenght: ";
    valve._tubeLenght = ValidateStringInput(msg).ToUpper();

    msg = "Enter the index of valve: ";
    valve._index = ValidateStringInput(msg).ToUpper();

    msg = "Enter the date of acceptance: ";
    valve._acceptanceDate = ValidateDateInput(msg);

    valve._expiriationDate = valve._acceptanceDate.AddYears(2);

    msg = "Enter the name destiny product: ";
    valve._destiny = ValidateStringInput(msg).ToUpper();

    msg = "Enter the amount of valves (must be a number): ";
    valve._amount = ValidateIntInput(msg);

    msg = "Enter the number of storage place: ";
    valve._storagePlace = ValidateStringInput(msg).ToUpper();

    valve._lastUpdate = DateTime.Now;

    valve._lastUser = userLogged;

    return valve;
}
CanModel CreateCan()
{
    CanModel can = new CanModel();
    string msg;

    msg = "Enter supplier name: ";
    can._supplier = ValidateStringInput(msg).ToUpper();

    msg = "Enter full name of can: ";
    can._fullName = ValidateStringInput(msg).ToUpper();

    msg = "Enter short name of can: ";
    can._shortName = ValidateStringInput(msg).ToUpper();

    msg = "Enter the index of can: ";
    can._index = ValidateStringInput(msg).ToUpper();

    msg = "Enter the date of acceptance: ";
    can._acceptanceDate = ValidateDateInput(msg);

    can._expiriationDate = can._acceptanceDate.AddYears(2);

    msg = "Enter the amount of cans (must be a number): ";
    can._amount = ValidateIntInput(msg);

    msg = "Enter the number of storage place: ";
    can._storagePlace = ValidateStringInput(msg).ToUpper();

    can._lastUpdate = DateTime.Now;

    can._lastUser = userLogged;

    Console.Write("Enter the diameter of can: ");
    can._diameter = ValidateStringInput(msg).ToUpper();

    msg = "Enter height of can: ";
    can._height = ValidateStringInput(msg).ToUpper();
    
    msg = "Enter type of internal varnish of a can: ";
    can._typeOfInternalVarnish = ValidateStringInput(msg).ToUpper();

    return can;
}
async void ReadValvesByUser()
{
    var valvesResults = await db.GetValvesByUser(userLogged);
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
async void ReadCansByUser()
{
    var result = await db.GetCansByUser(userLogged);
    foreach (var can in result)
    {
        Console.WriteLine(
            $"Can ID: {can._id}\n" +
            $"Index: {can._index}\n" +
            $"Height: {can._height}\n" +
            $"Diameter {can._diameter}\n" +
            $"Type of internal varnish: {can._typeOfInternalVarnish}\n" +
            $"Supplier: {can._supplier}\n" +
            $"Full name: {can._fullName}\n" +
            $"Short name: {can._shortName}\n" +
            $"Acceptance date: {can._acceptanceDate}\n" +
            $"Expiration date: {can._expiriationDate}\n" +
            $"Amount: {can._amount}\n" +
            $"Storage place: {can._storagePlace}\n" +
            $"Last update: {can._lastUpdate}\n" +
            $"Last user: {can._lastUser._email}\n");
    }

    Console.WriteLine("Please type enter key to continue");
}
static string SetPassword(string msg)
{
    var password = new StringBuilder();
    do
    {
        Console.Write(msg);
        while (true)
        {
            ConsoleKeyInfo i = Console.ReadKey(true);
            if (i.Key == ConsoleKey.Enter)
            {
                break;
            }
            else if (i.Key == ConsoleKey.Backspace)
            {
                if (password.Length > 0)
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
        }
        if (string.IsNullOrEmpty(password.ToString()))
        {
            Console.Clear();
            Console.WriteLine("Password can not be empty! Try again: ");
        }
    } while (string.IsNullOrEmpty(password.ToString()));

    return password.ToString();
}
static bool ComparePasswords(string psw1, string psw2)
{
    if (psw1 == psw2)
    {
        return true;
    }
    else
    {
        return false;
    }
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
}// Funkcja potrzebna poza zaliczeniem zadania
static string ValidateStringInput(string msg)
{
    Console.Write(msg);
    string? temp = Console.ReadLine();

    while(string.IsNullOrEmpty(temp))
    {
        Console.Clear();
        Console.WriteLine("This field can not be empty. Try again!");
        Console.Write(msg);
        temp = Console.ReadLine();
    }
    return temp;
}
static int ValidateIntInput(string msg)
{
    Console.Write(msg);
    bool isNumber = int.TryParse(Console.ReadLine(), out int temp);

    while (!isNumber || temp < 0)
    {
        Console.Clear();
        Console.WriteLine("This field can not be empty or less than zero. Try again!");
        Console.Write(msg);
        isNumber = int.TryParse(Console.ReadLine(), out temp);
    }
    return temp;
}
static DateOnly ValidateDateInput(string msg)
{
    Console.Write(msg);
    bool isDate = DateOnly.TryParse(Console.ReadLine(), out DateOnly temp);

    while (!isDate)
    {
        Console.Clear();
        Console.WriteLine("This field can not be empty and must be in correct format" +
            " (DD-MM-YYYY). Try again!");
        Console.Write(msg);
        isDate = DateOnly.TryParse(Console.ReadLine(), out temp);
    }
    return temp;
}
static string ValidateLetters(string input)
{
    StringBuilder sb = new StringBuilder();
    for (int i = 0; i < input.Length; i++)
    {
        if (i == 0)
        {
            sb.Append(char.ToUpper(input[i]));
        }
        else
        {
            sb.Append(char.ToLower(input[i]));
        }
    }
    return sb.ToString();
}
static bool ValidatePassword(string password)
{
    bool isCapital = false;
    bool isSpecial = false;
    bool isNumber = false;

    foreach (char c in password)
    {
        if (char.IsLetter(c) && char.IsUpper(c))
        {
            isCapital = true;
        }
        else if(c == '!' || c == '@' || c == '#' || c == '$' || c == '%' || c == '^' || c == '&'
            || c == '*' || c == '(' || c == ')')
        {
            isSpecial = true;
        }
        else if(char.IsNumber(c))
        {
            isNumber = true;
        }
    }
    if (isCapital == true && isSpecial == true && isNumber == true)
    {
        return true;
    }
    else
    {
        return false;
    }
}
static bool ValidateEmail(string email)
{
    bool isAt = false;
    bool dotPosition = true;

    for (int i = 0; i < email.Length; i++)
    {
        // Sprawdzenie czy input zawiera znak '@' i czy nie znajduje sie on na poczatku ani
        // koncu adresu email
        if (email[i] == '@')
        {
            if (email[email.Length - 1] != '@' && email[0] != '@')
            {
                isAt = true;
            }
            else
            {
                return false;
            }
        }
        // Sprawdzenie czy kropka znajduje sie na poczatku lub koncu adresu email
        if (email[email.Length - 1] == '.' || email[0] == '.')
        {
            return false;
        }
        // Sprawdzenie czy bezposrednio po znaku '@' znajduje sie kropka
        if ((email.IndexOf(email[i]) == email.IndexOf('@') + 1) && email[i] == '.')
        {
            return false;
        }

        // Sprawdzenie czy po znaku '@' wystepuje podciag zawieracjacy kropke
        if (email[i] == '@' && !email.Substring(email.IndexOf("@")).Contains("."))
        {
            return false;
        }
        // Sprawdzenie czy wystepuje tylko jeden znak '@' w adresie email
        if (email[i] == '@' && email.Substring(email.IndexOf("@")+1).Contains("@"))
        {
            return false;
        }
    }

    if(isAt == true && dotPosition == true)
    {
        return true;
    }
    else
    {
        return false;
    }
}
#endregion