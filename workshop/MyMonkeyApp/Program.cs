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