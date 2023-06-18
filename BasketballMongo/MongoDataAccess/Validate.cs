using System.Text;


namespace MongoDataAccess;

public class Validate
{
    public static string StringInput(string msg)
    {
        Console.Write(msg);
        string? temp = Console.ReadLine();

        while (string.IsNullOrEmpty(temp))
        {
            Console.Clear();
            Console.WriteLine("This field can not be empty. Try again!");
            Console.Write(msg);
            temp = Console.ReadLine();
        }
        return temp;
    }
    public static string SetPassword(string msg)
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
    public static string LettersSize(string input)
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
}
