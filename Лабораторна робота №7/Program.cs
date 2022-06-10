using static System.Console;
using ASD_Lab7__2;

string commands = "Commands:\n" +
    "example\n" +
    "add [object] [courierName] [Street],[HouseNumber],[AppartmentNumber] [dd].[mm].[yyyy]\n" +
    "find (deliveryID)\n" +
    "remove (deliveryID)\n" +
    "print\n" +
    "exit\n";

    WriteLine(commands);
   
    OHashTable table = new ();
    CHashTable couriers = new();

while (true)
{
    WriteLine("Enter command:");

    string command = ReadLine();
    string[] parts = command.Split(' ');

    if (command == "example")
    {
        if (parts.Length != 1)
            WriteLine("Must be written only 1 word: example");

        ShowControExample(table, couriers);
    }
    else if (command.StartsWith("add "))
    {
        if (CheckCommand(command))
        {
            string[] parameters = new string[] { parts[1], parts[2], parts[3], parts[4] };
            AddOrder(table, couriers, parameters);
        }
    }
    else if (command.StartsWith("remove "))
    {
        if (CheckCommand(command) == true)
            RemoveOrder(table, couriers, parts[1]);
    }
    else if (command.StartsWith("find "))
    {
        if (CheckCommand(command) == true)
            FindOrder(table, parts[1]);
    }
    else if (command == "print")
    {
        if (parts.Length != 1)
            WriteLine($"Must be wriiten only 1 word: print");

        else
        {
            WriteLine();
            table.Print();
        }
    }
    else if (command == "exit")
        break;

    else
        WriteLine($"I do not understand the command {command}, write again");
    
}

static Address ConstructAddress(string address)
{
    string[] parts = address.Split(',');
    return new Address(parts);
}

static Date ConstructDate(string date)
{
    string[] parts = date.Split('.');
    return new Date(parts);
}

static Value ConstructValue(string[] arguments)
{
    return new Value(arguments[0], arguments[1], ConstructAddress(arguments[2]), ConstructDate(arguments[3]));
}

static void AddOrder(OHashTable table, CHashTable couriers, string[] arguments)
{
    WriteLine();

    Random rnd = new ();

    int deliveryID = rnd.Next(100000, 1000000);

    if (IsCourierAvailable(table, couriers, arguments[1]))
    {
        couriers.InsertEntry(arguments[1], deliveryID);

        table.InsertEntry(new Entry(new Key(deliveryID.ToString()), ConstructValue(arguments)));
    }
    else
    {
        string s = FindFreeCourier(table, couriers);

        if (s == null)
        {
            WriteLine($"No free couriers found, please enter the name of the courier:");

            string name;

            while (true)
            {
                bool flag = true;

                name = ReadLine();

                if (CheckTheEnteredString(name) == false)
                {
                    WriteLine("The courier name must consist of English letters only");
                    flag = false;
                }

                if (flag)
                {
                    break;
                }
            }

            couriers.InsertEntry(name, deliveryID);

            table.InsertEntry(new Entry(new Key(deliveryID.ToString()),
                ConstructValue(new string[4]{ arguments[0], name, arguments[2], arguments[3]})));
        }
        else
        {
            WriteLine($"Courier {arguments[1]} is not available");

            WriteLine($"delivery is reassigned to: {s}");

            couriers.InsertEntry(s, deliveryID);

            table.InsertEntry(new Entry(new Key(deliveryID.ToString()),
                ConstructValue(new string[4] { arguments[0], s, arguments[2], arguments[3] })));
        }
    }
    WriteLine();
}
static void RemoveOrder(OHashTable table, CHashTable couriers, string id)
{
    couriers.RemoveEntry(Convert.ToInt32(id));

    table.RemoveEntry(new Key(id));
}
static void FindOrder(OHashTable table, string id)
{
    string res = table.FindEntry(new Key(id));

    if (res.StartsWith("Cant find "))
        WriteLine(res);
    
    else
        WriteLine(res);

}
static void ShowControExample(OHashTable table, CHashTable couriers)
{
    WriteLine();

    string[] couriersName = { "Oleg", "Ivan", "Maksim", "James", "Pavlo", "Vladyslav", "Kristina", "Olha", "Alex", "Dasha" };
    string[] objectType = { "laptop", "smartphone", "food", "clothes", "makeup", "stationery" };
    string[] streets = { "Zelensky", "Mudrogo", "Kotlovanska", "Richna", "Vishneva", "Kievska" };

    Random rnd = new ();

    for (int i = 0; i < 10; i++)
    {
        string[] argumentss = {
                    $"{objectType[rnd.Next(0,objectType.Length)]}",
                    $"{couriersName[i]}",
                    $"{streets[rnd.Next(0,streets.Length)]},{rnd.Next(10,100)},{rnd.Next(1,101)}",
                    $"{rnd.Next(0,29)}.{rnd.Next(0,13)}.{rnd.Next(2019,2022)}"
        };

        AddOrder(table, couriers, argumentss);
    }

    table.Print();

}
static string FindFreeCourier(OHashTable table, CHashTable couriers)
{
    string[] couriersarray = couriers.GetCouriersArray();

    foreach (string i in couriersarray)
        if (i != null)
            if (IsCourierAvailable(table, couriers, i))
                return i;
            
    return null;
}
static bool IsCourierAvailable(OHashTable table, CHashTable couriers, string courierName)
{
    int[] ordersID = couriers.GetCourierOrders(courierName);

    if (ordersID != null)
    {
        string[] ordersStreet = new string[ordersID.Length];

        string[] ordersDate = new string[ordersID.Length];

        for (int i = 0; i < ordersID.Length; i++)
        {
            string str = table.FindEntry(new Key(ordersID[i].ToString()));

            string TrimStr = str.Substring(40).Trim('[').Trim(']');

            ordersDate[i] = TrimStr.Split("],[")[3];

            ordersStreet[i] = TrimStr.Split("],[")[2].Split(',')[0];
        }

        int countUniqueOrder = 0;

        string[] UniqueDateOrder = ordersDate.Distinct().ToArray();

        for (int i = 0; i < UniqueDateOrder.Length; i++)
        {
            int count = 0;

            string[] streetsAtThisDate = Array.Empty<string>();

            for (int j = 0; j < ordersStreet.Length; j++)
            {
                if (ordersDate[j] == UniqueDateOrder[i])
                {
                    Array.Resize(ref streetsAtThisDate, streetsAtThisDate.Length + 1);

                    streetsAtThisDate[count] = ordersStreet[j];

                    count++;
                }
            }

            string[] UniqueStreetsAtThisDate = streetsAtThisDate.Distinct().ToArray();

            countUniqueOrder += UniqueStreetsAtThisDate.Length;
        }
        if (countUniqueOrder < 10)
        {
            return true;
        }
        return false;
    }
    return true;
}

static bool CheckCommand(string command)
{
    string[] parts = command.Split(' ');

    if (parts[0] == "add")
    {
        if (parts.Length != 5)
        {
            WriteLine($"Must be written 5 words;");
            return false;
        }

        if (CheckTheEnteredString(parts[2]) == false)
        {
            WriteLine("The courier name must consist of English letters only");
            return false;
        }

        string[] addressparts = parts[3].Split(',');

        if (CheckTheEnteredString(addressparts[0]) == false)
        {
            WriteLine("The name of street must consist of English letters only");
            return false;
        }

        if (!((int.TryParse(addressparts[1], out int hn) && hn > 0) && (int.TryParse(addressparts[2], out int an) && an > 0)))
        {
            WriteLine("HouseNumber and AppartmentNumber must be integer and > 0");
            return false;
        }
        string[] dateorder = parts[4].Split('.');
        
        if (!((int.TryParse(dateorder[1], out int m) && (m > 0 && m < 13)) && 
            (int.TryParse(dateorder[2], out int y) && y > 2000 && y <= 2022) && 
            (int.TryParse(dateorder[0],out int d) && d > 0 && d < DateTime.DaysInMonth(y, m))))
        {
            WriteLine("day, month and year must be correct integers, and the year must be> 2000 and <= 2022");
            return false;
        }

        return true;
    }
    else if (parts[0] == "remove")
    {
        if (parts.Length != 2)
        {
            WriteLine("Must be written only 2 words: remove and deliveryID");
            return false;
        }
        if (!(int.TryParse(parts[1], out int id) && id > 99999 && id < 1000000))
        {
            WriteLine("DeliveryID must be a number: XXXXXX, where x is digit");
            return false;
        }

        return true;
    }
    else if (parts[0] == "find")
    {
        if (parts.Length != 2)
        {
            WriteLine("Must be written only 2 words: find and deliveryID");
            return false;
        }
        if (!(int.TryParse(parts[1], out int id) && id > 99999 && id < 1000000))
        {
            WriteLine("DeliveryID must be a number: XXXXXX, where x is digit");
            return false;
        }

        return true;
    }

    return true;
}
static bool CheckTheEnteredString(string str)
{
    for (int i = 0; i < str.Length; i++)
    {
        if (((int)str[i] >= 65 && (int)str[i] <= 90) || ((int)str[i] >= 97 && (int)str[i] <= 122))
            continue;
        else
            return false;
    }
    return true;
}