<<<<<<< HEAD
using MyMonkeyApp;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

// Enhanced ASCII Art Arrays
string[] monkeyArt = [
    """
        🐒 Welcome to the Monkey Console App! 🐒
    
           .-.   .-.     .--.
          | OO| | OO|   / _.-' .-.   .-.
          |   | |   |   \  '-. '-'   '-'
          '^^^' '^^^'    '--'
        """,
    """
        🐵 Discover the Amazing World of Primates! 🐵
    
             ."-"-.              /      \
           |  ^  ^  |
           |    >   |
           |   ---  |
            \      /
             '-..-'
        """,
    """
        🙊 Monkey Data Explorer 🙊
    
            _____
           (     )
         __/  o  o  \__
        (  \   <    /  )
         \__) '---' (__/
           |       |
           |_______|
        """
];

string currentArt = monkeyArt[0];

// Display welcome message with ASCII art
Console.Clear();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine(currentArt);
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("===============================================");
Console.WriteLine("   🌿 Monkey Species Information System 🌿   ");
Console.WriteLine("===============================================");
Console.ResetColor();
Console.WriteLine();

bool keepRunning = true;
int artIndex = 0;

while (keepRunning)
{
    DisplayMenu();
    
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("Enter your choice (1-6): ");
    Console.ResetColor();
    
    string? input = Console.ReadLine();
    Console.WriteLine();

    switch (input?.Trim())
    {
        case "1":
            ListAllMonkeys();
            break;
        case "2":
            GetMonkeyByName();
            break;
        case "3":
            GetRandomMonkey();
            // Cycle through ASCII art for fun
            artIndex = (artIndex + 1) % monkeyArt.Length;
            currentArt = monkeyArt[artIndex];
            break;
        case "4":
            ShowStatistics();
            break;
        case "5":
            SearchMonkeys();
            break;
        case "6":
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("🐒 Thanks for exploring the monkey world! Goodbye! 🐒");
            Console.ResetColor();
            keepRunning = false;
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Invalid choice. Please enter a number between 1 and 6.");
            Console.ResetColor();
            break;
    }

    if (keepRunning)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Press any key to continue...");
        Console.ResetColor();
        Console.ReadKey();
        Console.Clear();
        
        // Display current ASCII art
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(currentArt);
        Console.ResetColor();
    }
}

void DisplayMenu()
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("┌─────────────────────────────────────────┐");
    Console.WriteLine("│              🐵 MAIN MENU 🐵              │");
    Console.WriteLine("├─────────────────────────────────────────┤");
    Console.WriteLine("│ 1. 📋 List all monkeys                 │");
    Console.WriteLine("│ 2. 🔍 Get monkey by name               │");
    Console.WriteLine("│ 3. 🎲 Get random monkey                │");
    Console.WriteLine("│ 4. 📊 Show statistics                  │");
    Console.WriteLine("│ 5. 🔎 Search monkeys                   │");
    Console.WriteLine("│ 6. 🚪 Exit                             │");
    Console.WriteLine("└─────────────────────────────────────────┘");
    Console.ResetColor();
    Console.WriteLine();
}

void ListAllMonkeys()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("🌟 === ALL MONKEY SPECIES === 🌟");
    Console.ResetColor();
    Console.WriteLine();

    var monkeys = MonkeyHelper.GetAllMonkeys();
    
    if (!monkeys.Any())
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("⚠️  No monkeys found in the database.");
        Console.ResetColor();
        return;
    }

    for (int i = 0; i < monkeys.Count; i++)
    {
        var monkey = monkeys[i];
        var statusColor = GetConservationStatusColor(monkey.ConservationStatus);
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"{i + 1:D2}. ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{monkey.Name}");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write($" ({monkey.Species})");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("    📍 ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write($"{monkey.Location}");
        
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(" | 👥 ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write($"{monkey.Population:N0}");
        
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(" | ");
        Console.ForegroundColor = statusColor;
        Console.WriteLine($"{monkey.ConservationStatus}");
        
        Console.ResetColor();
    }
    
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"📊 Total species: {monkeys.Count}");
    Console.ResetColor();
}

void GetMonkeyByName()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("🔍 === FIND MONKEY BY NAME === 🔍");
    Console.ResetColor();
    Console.WriteLine();
    
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("Enter monkey name: ");
    Console.ResetColor();
    
    string? name = Console.ReadLine()?.Trim();
    
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("❌ Please enter a valid monkey name.");
        Console.ResetColor();
        return;
    }

    var monkey = MonkeyHelper.GetMonkeyByName(name);
    
    if (monkey == null)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"😔 Monkey with name '{name}' not found.");
        Console.ResetColor();
        
        // Suggest similar names
        var similarMonkeys = MonkeyHelper.SearchMonkeysByName(name).Take(3);
        if (similarMonkeys.Any())
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("💡 Did you mean one of these?");
            Console.ResetColor();
            foreach (var similar in similarMonkeys)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"   • {similar.Name}");
                Console.ResetColor();
            }
        }
        return;
    }

    DisplayMonkeyDetails(monkey);
}

void GetRandomMonkey()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("🎲 === RANDOM MONKEY DISCOVERY === 🎲");
    Console.ResetColor();
    Console.WriteLine();

    var monkey = MonkeyHelper.GetRandomMonkey();
    
    if (monkey == null)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("⚠️  No monkeys available for random selection.");
        Console.ResetColor();
        return;
    }

    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"🎉 Random monkey #{MonkeyHelper.RandomAccessCount}:");
    Console.ResetColor();
    Console.WriteLine();
    
    DisplayMonkeyDetails(monkey);
}

void ShowStatistics()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("📊 === MONKEY STATISTICS === 📊");
    Console.ResetColor();
    Console.WriteLine();

    var stats = MonkeyHelper.GetStatistics();
    
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("┌─────────────────────────────────────────┐");
    Console.WriteLine("│            POPULATION DATA              │");
    Console.WriteLine("├─────────────────────────────────────────┤");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"│ Total Species:      {stats.TotalSpecies,15} │");
    Console.WriteLine($"│ Total Population:   {stats.TotalPopulation,15:N0} │");
    Console.WriteLine($"│ Endangered Species: {stats.EndangeredSpecies,15} │");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("├─────────────────────────────────────────┤");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"│ Most Populous:      {stats.MostPopulousSpecies,15} │");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"│ Rarest Species:     {stats.RarestSpecies,15} │");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("└─────────────────────────────────────────┘");
    Console.ResetColor();
    Console.WriteLine();
    
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine($"📅 Last updated: {MonkeyHelper.LastDataRefresh:yyyy-MM-dd HH:mm:ss}");
    Console.WriteLine($"🎯 Random accesses: {MonkeyHelper.RandomAccessCount}");
    Console.ResetColor();
}

void SearchMonkeys()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("🔎 === SEARCH MONKEYS === 🔎");
    Console.ResetColor();
    Console.WriteLine();
    
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("Enter search term: ");
    Console.ResetColor();
    
    string? searchTerm = Console.ReadLine()?.Trim();
    
    if (string.IsNullOrWhiteSpace(searchTerm))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("❌ Please enter a valid search term.");
        Console.ResetColor();
        return;
    }

    var results = MonkeyHelper.SearchMonkeysByName(searchTerm).ToList();
    
    if (!results.Any())
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"😔 No monkeys found matching '{searchTerm}'.");
        Console.ResetColor();
        return;
    }

    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"🎯 Found {results.Count} monkey(s) matching '{searchTerm}':");
    Console.ResetColor();
    Console.WriteLine();

    for (int i = 0; i < results.Count; i++)
    {
        var monkey = results[i];
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"{i + 1}. ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(monkey.ToString());
        Console.ResetColor();
    }
}

void DisplayMonkeyDetails(Monkey monkey)
{
    var statusColor = GetConservationStatusColor(monkey.ConservationStatus);
    
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("┌─────────────────────────────────────────┐");
    Console.WriteLine("│              MONKEY PROFILE             │");
    Console.WriteLine("├─────────────────────────────────────────┤");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"│ Name: {monkey.Name,-30} │");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine($"│ Species: {monkey.Species,-27} │");
    Console.WriteLine($"│ Location: {monkey.Location,-26} │");
    Console.WriteLine($"│ Population: {monkey.Population,-24:N0} │");
    Console.ForegroundColor = statusColor;
    Console.WriteLine($"│ Status: {monkey.ConservationStatus,-28} │");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("├─────────────────────────────────────────┤");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("│ Details:                                │");
    
    // Word wrap for details
    var details = monkey.Details;
    const int maxWidth = 37;
    while (details.Length > maxWidth)
    {
        int breakPoint = details.LastIndexOf(' ', maxWidth);
        if (breakPoint == -1) breakPoint = maxWidth;
        
        Console.WriteLine($"│ {details[..breakPoint],-37} │");
        details = details[breakPoint..].TrimStart();
    }
    if (details.Length > 0)
    {
        Console.WriteLine($"│ {details,-37} │");
    }
    
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("└─────────────────────────────────────────┘");
    Console.ResetColor();
}

ConsoleColor GetConservationStatusColor(ConservationStatus status)
{
    return status switch
    {
        ConservationStatus.LeastConcern => ConsoleColor.Green,
        ConservationStatus.NearThreatened => ConsoleColor.Yellow,
        ConservationStatus.Vulnerable => ConsoleColor.DarkYellow,
        ConservationStatus.Endangered => ConsoleColor.Red,
        ConservationStatus.CriticallyEndangered => ConsoleColor.Magenta,
        ConservationStatus.ExtinctInWild => ConsoleColor.DarkRed,
        ConservationStatus.Extinct => ConsoleColor.DarkGray,
        _ => ConsoleColor.White
    };
}
=======
﻿namespace MyMonkeyApp;

/// <summary>
/// Main program class for the Monkey Console Application
/// </summary>
class Program
{
    private static readonly Random _random = new();
    private static readonly string[] _asciiArt = {
        """
        🐒 Welcome to Monkey World! 🐒
           .-.   .-.
          (   \ /   )
           \  'o'  /
            )  ~  (
           /  \_/  \
          (  /\_/\  )
           \/ |_| \/
        """,

        """
        🙈 See No Evil Monkey 🙈
           .-""-.
          /       \
         |  ^   ^  |
         |    o    |
         |   ___   |
          \  \_/  /
           '.___.'
        """,

        """
        🐵 Swinging Into Action! 🐵
             /|   /|  
            ( :v:  )
             |(_)|
            /  |  \
           /   |   \
          /_________|
        """,

        """
        🍌 Banana Time! 🍌
           ___
          (   )
         (     )
        (  ___  )
        | |   | |
        | |___| |
         \_____/
          |||||
        """,

        """
        🐒 Monkey Business! 🐒
           .="=.
          / _@_ \
         |  (_)  |
          \  U  /
           |||||
           |||||
        """
    };

    /// <summary>
    /// Main entry point of the application
    /// </summary>
    /// <param name="args">Command line arguments</param>
    static void Main(string[] args)
    {
        Console.Clear();
        DisplayRandomAsciiArt();

        Console.WriteLine("🐒 Welcome to the Monkey Console Application! 🐒");
        Console.WriteLine("=================================================");
        Console.WriteLine($"📊 Loaded {MonkeyHelper.GetAllMonkeys().Count} monkey species from MCP server");
        Console.WriteLine($"🎲 Random monkey accessed {MonkeyHelper.RandomAccessCount} times");
        Console.WriteLine();

        bool continueRunning = true;
        while (continueRunning)
        {
            DisplayMenu();
            var userChoice = GetUserChoice();

            Console.Clear();

            switch (userChoice)
            {
                case 1:
                    ListAllMonkeys();
                    break;
                case 2:
                    GetMonkeyByName();
                    break;
                case 3:
                    GetRandomMonkey();
                    break;
                case 4:
                    continueRunning = false;
                    DisplayGoodbyeMessage();
                    break;
                default:
                    Console.WriteLine("❌ Invalid option. Please try again.");
                    break;
            }

            if (continueRunning)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();

                // Randomly display ASCII art
                if (_random.Next(1, 4) == 1) // 33% chance
                {
                    DisplayRandomAsciiArt();
                }
            }
        }
    }

    /// <summary>
    /// Displays the main menu options
    /// </summary>
    private static void DisplayMenu()
    {
        Console.WriteLine("🍌 What would you like to do? 🍌");
        Console.WriteLine("================================");
        Console.WriteLine("1. 📋 List all monkeys");
        Console.WriteLine("2. 🔍 Get details for a specific monkey by name");
        Console.WriteLine("3. 🎲 Get a random monkey");
        Console.WriteLine("4. 🚪 Exit app");
        Console.WriteLine();
        Console.Write("👉 Enter your choice (1-4): ");
    }

    /// <summary>
    /// Gets and validates user input for menu selection
    /// </summary>
    /// <returns>Valid menu option number</returns>
    private static int GetUserChoice()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 4)
            {
                return choice;
            }

            Console.Write("❌ Please enter a valid number (1-4): ");
        }
    }

    /// <summary>
    /// Lists all available monkeys in a formatted table
    /// </summary>
    private static void ListAllMonkeys()
    {
        Console.WriteLine("📋 All Available Monkeys");
        Console.WriteLine("========================");

        var monkeys = MonkeyHelper.GetAllMonkeys();

        if (!monkeys.Any())
        {
            Console.WriteLine("❌ No monkeys found in the database.");
            return;
        }

        Console.WriteLine($"{"Name",-20} {"Species",-25} {"Location",-25} {"Population",-12} {"Status",-15}");
        Console.WriteLine(new string('=', 100));

        foreach (var monkey in monkeys)
        {
            var statusIcon = GetConservationStatusIcon(monkey.ConservationStatus);
            Console.WriteLine($"{monkey.Name,-20} {monkey.Species,-25} {monkey.Location,-25} {monkey.Population,-12:N0} {statusIcon} {monkey.ConservationStatus,-13}");
        }

        var stats = MonkeyHelper.GetStatistics();
        Console.WriteLine(new string('=', 100));
        Console.WriteLine($"📊 Total Species: {stats.TotalSpecies} | Total Population: {stats.TotalPopulation:N0} | Endangered: {stats.EndangeredSpecies}");
    }

    /// <summary>
    /// Prompts user for a monkey name and displays detailed information
    /// </summary>
    private static void GetMonkeyByName()
    {
        Console.WriteLine("🔍 Find Monkey by Name");
        Console.WriteLine("======================");
        Console.Write("👉 Enter the monkey name: ");

        var name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("❌ Please enter a valid monkey name.");
            return;
        }

        var monkey = MonkeyHelper.GetMonkeyByName(name);

        if (monkey == null)
        {
            Console.WriteLine($"❌ No monkey found with the name '{name}'.");

            // Suggest similar names
            var suggestions = MonkeyHelper.SearchMonkeysByName(name).Take(3);
            if (suggestions.Any())
            {
                Console.WriteLine("\n💡 Did you mean one of these?");
                foreach (var suggestion in suggestions)
                {
                    Console.WriteLine($"   • {suggestion.Name}");
                }
            }
            return;
        }

        DisplayMonkeyDetails(monkey);
    }

    /// <summary>
    /// Gets and displays a random monkey
    /// </summary>
    private static void GetRandomMonkey()
    {
        Console.WriteLine("🎲 Random Monkey Selection");
        Console.WriteLine("==========================");

        var monkey = MonkeyHelper.GetRandomMonkey();

        if (monkey == null)
        {
            Console.WriteLine("❌ No monkeys available for random selection.");
            return;
        }

        Console.WriteLine($"🎯 Random monkey #{MonkeyHelper.RandomAccessCount} selected!");
        Console.WriteLine();

        DisplayMonkeyDetails(monkey);
    }

    /// <summary>
    /// Displays detailed information about a specific monkey
    /// </summary>
    /// <param name="monkey">The monkey to display details for</param>
    private static void DisplayMonkeyDetails(Monkey monkey)
    {
        var statusIcon = GetConservationStatusIcon(monkey.ConservationStatus);

        Console.WriteLine("🐒 Monkey Details");
        Console.WriteLine("=================");
        Console.WriteLine($"📛 Name: {monkey.Name}");
        Console.WriteLine($"🧬 Species: {monkey.Species}");
        Console.WriteLine($"🌍 Location: {monkey.Location}");
        Console.WriteLine($"👥 Population: {monkey.Population:N0}");
        Console.WriteLine($"🛡️  Conservation Status: {statusIcon} {monkey.ConservationStatus}");
        Console.WriteLine($"📝 Details: {monkey.Details}");
    }

    /// <summary>
    /// Gets an emoji icon representing the conservation status
    /// </summary>
    /// <param name="status">Conservation status</param>
    /// <returns>Emoji icon</returns>
    private static string GetConservationStatusIcon(ConservationStatus status)
    {
        return status switch
        {
            ConservationStatus.LeastConcern => "🟢",
            ConservationStatus.NearThreatened => "🟡",
            ConservationStatus.Vulnerable => "🟠",
            ConservationStatus.Endangered => "🔴",
            ConservationStatus.CriticallyEndangered => "🆘",
            ConservationStatus.ExtinctInWild => "⚫",
            ConservationStatus.Extinct => "💀",
            _ => "❓"
        };
    }

    /// <summary>
    /// Displays random ASCII art
    /// </summary>
    private static void DisplayRandomAsciiArt()
    {
        var randomIndex = _random.Next(_asciiArt.Length);
        Console.WriteLine(_asciiArt[randomIndex]);
        Console.WriteLine();
    }

    /// <summary>
    /// Displays goodbye message when user exits
    /// </summary>
    private static void DisplayGoodbyeMessage()
    {
        Console.WriteLine("👋 Thanks for exploring the monkey world!");
        Console.WriteLine("🍌 Hope you had a wild time! 🍌");
        Console.WriteLine();
        DisplayRandomAsciiArt();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
>>>>>>> b8de0a9 (로컬 변경사항 커밋 후 원격과 rebase)
