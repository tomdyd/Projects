using System.Linq.Expressions;
using System.Text.RegularExpressions;

public class students
{
    public string _firstName;
    public string _lastName;
    public double _average;
    public Threshold _treshold;
    public students(string firstname, string lastName, double average)
    {
        _firstName = firstname;
        _lastName = lastName;
        _average = average;
    }

    public string GetFirstName()
    {
        return _firstName;
    }
    public string GetLastName()
    {
        return _lastName;
    }
    public double GetAverage()
    {
        return _average;
    }
    public void GetTreshold()
    {
        if (_average >= 4.00 && _average <= 4.25)
        {
            _treshold = Threshold.firstThreshold;
        }
        else if (_average >= 4.26 && _average <= 4.50)
        {
            _treshold = Threshold.secondThreshold;
        }
        else if (_average >= 4.51 && _average <= 4.75)
        {
            _treshold = Threshold.thirdThreshold;
        }
        else if (_average >= 4.76 && _average <= 5.00)
        {
            _treshold = Threshold.fourthThreshold;
        }
    }
}
public enum Threshold
{
    firstThreshold,
    secondThreshold,
    thirdThreshold,
    fourthThreshold
}


namespace Z2_14._05._2023_tomasz_dyda
{
    class program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the path to the file: ");
            string path = Console.ReadLine();
            StreamReader sr = File.OpenText(path);

            string line;

            // lista uczniów
            List<students> students = new List<students>();

            // Zmienna logiczna
            bool isFirstLine = true;

            // Odczytuj linie pliku
            while ((line = sr.ReadLine()) != null)
            {
                // Podziel linię na kolumny
                string[] columns = Regex.Split(line, @"\s+");
                // Pomiń pierwszą linie
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                if (!string.IsNullOrEmpty(columns[0]))
                {
                    students.Add(new students(columns[0], columns[1], Math.Round(double.Parse(columns[2]), 2)));
                }
                else
                {
                    break;
                }
            }

            List<students> schoolarship = new List<students>();

            // z listy wszystkich studentow przepisujemy tylko tych ktorych srednia wynosi co najmniej 4.00
            foreach (students element in students)
            {
                double average;

                if (element.GetAverage() > 3.99)
                {
                    schoolarship.Add(element);
                    continue;
                }
            }

            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            foreach (students student in schoolarship)
            {
                // Na podstawie średniej przypisujemy uczniów do odpowiednich progów stpendialnych i zliczamy ilość
                // studentów z każdego progu
                student.GetTreshold();
                if (student._treshold == Threshold.firstThreshold)
                {
                    a++;
                }
                else if (student._treshold == Threshold.secondThreshold)
                {
                    b++;
                }
                else if (student._treshold == Threshold.thirdThreshold)
                {
                    c++;
                }
                else if (student._treshold == Threshold.fourthThreshold)
                {
                    d++;
                }
            }

            int number = schoolarship.Count();
            Console.WriteLine("The number of students eligible for the scholarship is " + number);
            Console.WriteLine("With an average above 4,00: " + a);
            Console.WriteLine("With an average above 4,25: " + b);
            Console.WriteLine("With an average above 4,50: " + c);
            Console.WriteLine("With an average above 4,75: " + d);

            Console.Write("Please provide the monthly scholarship budget (Full amount): ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int budget);
            while (!isNumber)
            {
                Console.Write("Please provide the monthly scholarship budget (Full amount): ");
                isNumber = int.TryParse(Console.ReadLine(), out budget);
            }

            // Obliczamy kwotę dla najniższego progu na podstawie wag każdego progu oraz liczby studentów przypadających na każdy próg
            double weightedAverage = a + b * 1.2 + c * 1.44 + d * 1.728;
            // dzieląc budżet przez średnią ważoną otrzymujemy najniższy (pierwszy) próg stypendialny
            int firstThreshold = (int)(budget / weightedAverage);
            // mnożąc pierwszy próg stypendialny przez 1,2 otrzymujemy drugi próg stypendialny
            int secondThreshold = (int)(firstThreshold * 1.2);
            // mnożąc pierwszy próg stypendialny przez 1,2 otrzymujemy trzeci próg stypendialny
            int thirdThreshold = (int)(secondThreshold * 1.2);
            // mnożąc pierwszy próg stypendialny przez 1,2 otrzymujemy czwarty próg stypendialny
            int fourthThreshold = (int)(thirdThreshold * 1.2);

            Console.WriteLine();
            Console.WriteLine("The scholarship amount for students with a minimum GPA of 4.00 is: " + firstThreshold);
            Console.WriteLine("The scholarship amount for students with a minimum GPA of 4,25 is: " + secondThreshold);
            Console.WriteLine("The scholarship amount for students with a minimum GPA of 4,50 is: " + thirdThreshold);
            Console.WriteLine("The scholarship amount for students with a minimum GPA of 4,75 is: " + fourthThreshold);

            Console.WriteLine("Do you want to display information about students who have been awarded scholarships? (Y/N)");
            string input = Console.ReadLine();
            input = input.ToUpper();
            while (input != "Y" && input != "y" && input != "N" && input != "n")
            {
                Console.Clear();
                Console.WriteLine("Do you want to see more details? (Y/N)");
                input = Console.ReadLine();
                input = input.ToUpper();
            }
            switch (input)
            {
                case "Y":
                    int userInput = Menu();
                    Console.WriteLine();
                    switch (userInput)
                    {
                        case 1:
                            GetStudentsInfo(students);
                            break;
                        case 2:
                            GetSchoolarShipInfo(schoolarship);
                            break;
                        case 3:
                            GetSchoolarShipInfo(schoolarship, Threshold.firstThreshold);
                            break;
                        case 4:
                            GetSchoolarShipInfo(schoolarship, Threshold.secondThreshold);
                            break;
                        case 5:
                            GetSchoolarShipInfo(schoolarship, Threshold.thirdThreshold);
                            break;
                        case 6:
                            GetSchoolarShipInfo(schoolarship, Threshold.fourthThreshold);
                            break;
                    }
                    break;
                case "N":
                    break;
            }
        }
        #region methods
        static int Menu()
        {
            Console.Clear();
            Console.WriteLine("1. List of all students.");
            Console.WriteLine("2. List of all students awarded with schoolarship.");
            Console.WriteLine("3. List of students with GPA between 4.00 - 4.25");
            Console.WriteLine("4. List of students with GPA between 4.26 - 4.50");
            Console.WriteLine("5. List of students with GPA between 4.51 - 4.75");
            Console.WriteLine("6. List of students with GPA between 4.76 - 5.00");
            Console.Write("Choose option from list above: ");
            bool isNumber = int.TryParse(Console.ReadLine(), out int userInput);

            while (!isNumber || (userInput < 1 || userInput > 6))
            {
                Console.Clear();
                Console.WriteLine("1. List of all students.");
                Console.WriteLine("2. List of all students awarded with schoolarship.");
                Console.WriteLine("3. List of students with GPA between 4.00 - 4.25");
                Console.WriteLine("4. List of students with GPA between 4.26 - 4.50");
                Console.WriteLine("5. List of students with GPA between 4.51 - 4.75");
                Console.WriteLine("6. List of students with GPA between 4.76 - 5.00");
                Console.Write("Choose option from list above: ");
                isNumber = int.TryParse(Console.ReadLine(), out userInput);
            }
            return userInput;
        }
        static void GetStudentsInfo(List<students> students)
        {
            foreach (students element in students)
            {
                Console.WriteLine("Firstname: " + element._firstName + "\tLastname: " + element._lastName +
                "\tAverage: " + element._average.ToString("0.00"));
            }
        }
        static void GetSchoolarShipInfo(List<students> schoolarship)
        {
            // Sortowanie tablicy elementów klasy students na podstawie parametru _threshold
            schoolarship.Sort((s1, s2) => s1._treshold.CompareTo(s2._treshold));
            foreach (students element in schoolarship)
            {
                Console.WriteLine("Firstname: " + element._firstName + "\tLastname: " + element._lastName +
                "\tAverage: " + element._average.ToString("0.00"));
            }
        }
        static void GetSchoolarShipInfo(List<students> schoolarship, Threshold threshold)
        {
            foreach (students element in schoolarship)
            {
                if (element._treshold == threshold)
                {
                    Console.WriteLine("Firstname: " + element._firstName + "\tLastname: " + element._lastName +
                        "\tAverage: " + element._average.ToString("0.00"));
                }
            }
        }
        #endregion
    }
}
