﻿using StoreDataAccess.DataAccess;
using StoreDataAccess.Models;
using System.Text;

DataAccess db = new DataAccess();
UserModel userLogged = null;

int choice;
bool access = false;

while (true)
{
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
            case 2:
                db.CreateUser(CreateUser());
                break;
            case 3:
                Environment.Exit(0);
                break;
        }
    } while (userLogged == null);

    while (access == true)
    {
        Console.Clear();
        Console.WriteLine("1. Add components");
        Console.WriteLine("2. Read components");
        Console.WriteLine("3. Change components");
        Console.WriteLine("4. Delete components");
        Console.WriteLine("5. Logout");
        bool isNumber = int.TryParse(Console.ReadLine(), out choice);
        if (!isNumber)
        {
            continue;
        }

        switch (choice)
        {
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
                    case 3:
                        break;
                }
                break;

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
                        Console.Write("Enter index of valve you want to update: ");
                        string valveIndex = Console.ReadLine();                    
                        if (string.IsNullOrEmpty(valveIndex))
                        {
                            Console.WriteLine("Index can not be empty!");
                            Console.ReadKey();
                            break;
                        }

                        var exist = db.FindValveByIndex(valveIndex);

                        if (exist)
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
                        Console.WriteLine("Enter index of valve you want to update: ");
                        string canIndex = Console.ReadLine();
                        if (string.IsNullOrEmpty(canIndex))
                        {
                            Console.WriteLine("Index can not be empty!");
                            Console.ReadKey();
                            break;
                        }

                        var updateCan = CreateCan();
                        await db.UpdateCanByIndex(updateCan, canIndex);
                        break;
                    case 3:
                        break;
                }
                break;

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
                    case 3:
                        break;
                }
                break;
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
    string msg, temp;

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
        Console.WriteLine("Wrong email address, try again.");
        temp = ValidateStringInput(msg);
    }
    temp = temp.ToLower();
    user._email = temp; // todo regex maila

    temp = SetPassword();
    while(!ValidatePassword(temp))
    {
        Console.Write("The password must contain at least one uppercase letter, one special" +
            " character and one digit. Try again: ");
        temp = SetPassword();
    }
    user._password = temp;
    // todo walidacja hasła

    return user;
}
ValveModel CreateValve()
{
    ValveModel valve = new ValveModel();
    string msg;

    msg = "Enter supplier of valve: ";
    valve._supplier = ValidateStringInput(msg);

    msg = "Enter full name of valve: ";
    valve._fullName = ValidateStringInput(msg);

    msg = "Enter short name of valve: ";
    valve._shortName = ValidateStringInput(msg);

    msg = "Enter the tube lenght: ";
    valve._tubeLenght = ValidateStringInput(msg);

    msg = "Enter the index of valve: ";
    valve._index = ValidateStringInput(msg);

    msg = "Enter the date of acceptance: ";
    valve._acceptanceDate = ValidateDateInput(msg);

    valve._expiriationDate = valve._acceptanceDate.AddYears(2);

    msg = "Enter the name destiny product: ";
    valve._destiny = ValidateStringInput(msg);

    msg = "Enter the amount of valves (must be a number): ";
    valve._amount = ValidateIntInput(msg);

    msg = "Enter the number of storage place: ";
    valve._storagePlace = ValidateStringInput(msg);

    valve._lastUpdate = DateTime.Now;

    valve._lastUser = userLogged;
    
    return valve;
}
CanModel CreateCan()
{
    CanModel can = new CanModel();
    string msg;

    msg = "Enter supplier name: ";
    can._supplier = ValidateStringInput(msg);

    msg = "Enter full name of can: ";
    can._fullName = ValidateStringInput(msg);

    msg = "Enter short name of can: ";
    can._shortName = ValidateStringInput(msg);

    msg = "Enter the index of can: ";
    can._index = ValidateStringInput(msg);

    msg = "Enter the date of acceptance: ";
    can._acceptanceDate = ValidateDateInput(msg);

    can._expiriationDate = can._acceptanceDate.AddYears(2);

    msg = "Enter the amount of cans (must be a number): ";
    can._amount = ValidateIntInput(msg);

    msg = "Enter the number of storage place: ";
    can._storagePlace = ValidateStringInput(msg);

    can._lastUpdate = DateTime.Now;

    can._lastUser = userLogged;

    Console.Write("Enter the diameter of can: ");
    can._diameter = ValidateStringInput(msg);

    msg = "Enter height of can: ";
    can._height = ValidateStringInput(msg);
    
    msg = "Enter type of internal varnish of a can: ";
    can._typeOfInternalVarnish = ValidateStringInput(msg);

    return can;
}
static string SetPassword()
{
    var password = new StringBuilder();
    do
    {
        Console.Write("Set your password: ");
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
static string ValidateStringInput(string msg)
{
    Console.Write(msg);
    string temp = Console.ReadLine();

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
    int atIndex = email.IndexOf('@');

    foreach(char c in email)
    {
        // Sprawdzenie czy input zawiera znak '@' i czy nie znajduje sie on na poczatku ani
        // koncu adresu email
        if (c == '@')
        {
            if ((email.IndexOf(c) != 0) && (email.IndexOf(c) != email.Length - 1))
            {
                isAt = true;
            }
            else
            {
                return false;
            }
        }
        // Sprawdzenie czy bezposrednio po znaku '@' znajduje sie kropka
        if ((email.IndexOf(c) == atIndex + 1) && c == '.')
        {
            return false;
        }
        // Sprawdzenie czy po znaku '@' wystepuje podciag zawieracjacy kropke
        if (c == '@' && !email.Substring(email.IndexOf("@")).Contains("."))
        {
            return false;
        }
        // Sprawdzenie czy kropka znajduje sie na poczatku lub koncu adresu email
        int index = email.IndexOf(c);
        if (email.IndexOf(c) == 0 || email.IndexOf(c) == email.Length -1)
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