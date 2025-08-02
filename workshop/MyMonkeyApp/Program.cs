using MyMonkeyApp;

// Display welcome screen with ASCII art
AsciiArt.DisplayWelcome();

// Main application loop
bool isRunning = true;
while (isRunning)
{
    try
    {
        DisplayMenu();
        var choice = GetUserChoice();

        switch (choice)
        {
            case 1:
                await ListAllMonkeysAsync();
                break;
            case 2:
                await GetMonkeyByNameAsync();
                break;
            case 3:
                await GetRandomMonkeyAsync();
                break;
            case 4:
                isRunning = false;
                DisplayExitMessage();
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid choice. Please select a number between 1-4.");
                Console.ResetColor();
                break;
        }

        if (isRunning)
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"❌ An error occurred: {ex.Message}");
        Console.ResetColor();
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}

/// <summary>
/// Displays the main menu options to the user.
/// </summary>
static void DisplayMenu()
{
    Console.Clear();
    
    // Randomly display ASCII art 30% of the time
    var random = new Random();
    if (random.NextDouble() < 0.3)
    {
        AsciiArt.DisplayRandomMonkeyArt();
    }

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("┌─────────────────────────────────────┐");
    Console.WriteLine("│           MONKEY EXPLORER           │");
    Console.WriteLine("├─────────────────────────────────────┤");
    Console.WriteLine("│ 1. 📋 List all monkeys              │");
    Console.WriteLine("│ 2. 🔍 Find monkey by name           │");
    Console.WriteLine("│ 3. 🎲 Get random monkey             │");
    Console.WriteLine("│ 4. 🚪 Exit application              │");
    Console.WriteLine("└─────────────────────────────────────┘");
    Console.ResetColor();
    
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"\n📊 Random monkeys accessed: {MonkeyHelper.RandomMonkeyAccessCount}");
    Console.ResetColor();
}

/// <summary>
/// Gets the user's menu choice and validates input.
/// </summary>
/// <returns>The user's choice as an integer.</returns>
static int GetUserChoice()
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("\nEnter your choice (1-4): ");
    Console.ResetColor();
    
    if (int.TryParse(Console.ReadLine(), out int choice))
    {
        return choice;
    }
    
    return -1; // Invalid choice
}

/// <summary>
/// Lists all available monkey species in a formatted table.
/// </summary>
static async Task ListAllMonkeysAsync()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("🐒 ALL MONKEY SPECIES 🐒");
    Console.WriteLine(new string('=', 80));
    Console.ResetColor();

    var monkeys = MonkeyHelper.GetAllMonkeys();
    
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"{"Name",-25} {"Location",-30} {"Population",-12} {"Status",-15}");
    Console.WriteLine(new string('-', 80));
    Console.ResetColor();

    foreach (var monkey in monkeys)
    {
        var statusColor = monkey.ConservationStatus switch
        {
            "Endangered" => ConsoleColor.Red,
            "Vulnerable" => ConsoleColor.DarkYellow,
            "Least Concern" => ConsoleColor.Green,
            _ => ConsoleColor.White
        };

        Console.Write($"{monkey.Name,-25} {monkey.Location,-30} ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"{monkey.Population,-12:N0} ");
        Console.ForegroundColor = statusColor;
        Console.WriteLine($"{monkey.ConservationStatus,-15}");
        Console.ResetColor();
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n📈 Total species: {monkeys.Count}");
    Console.ResetColor();
    
    await Task.CompletedTask; // Simulating async operation
}

/// <summary>
/// Gets detailed information about a specific monkey by name.
/// </summary>
static async Task GetMonkeyByNameAsync()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("🔍 FIND MONKEY BY NAME 🔍");
    Console.WriteLine(new string('=', 40));
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("Enter monkey name: ");
    Console.ResetColor();
    
    var searchName = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(searchName))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("❌ Please enter a valid monkey name.");
        Console.ResetColor();
        return;
    }

    var monkey = MonkeyHelper.GetMonkeyByName(searchName);

    if (monkey != null)
    {
        DisplayMonkeyDetails(monkey);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"❌ No monkey found with the name '{searchName}'.");
        Console.ResetColor();
        
        // Offer suggestions
        var suggestions = MonkeyHelper.SearchMonkeys(searchName);
        if (suggestions.Any())
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n💡 Did you mean one of these?");
            Console.ResetColor();
            foreach (var suggestion in suggestions.Take(3))
            {
                Console.WriteLine($"  • {suggestion.Name}");
            }
        }
    }
    
    await Task.CompletedTask; // Simulating async operation
}

/// <summary>
/// Gets a random monkey and displays its information.
/// </summary>
static async Task GetRandomMonkeyAsync()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("🎲 RANDOM MONKEY SELECTION 🎲");
    Console.WriteLine(new string('=', 40));
    Console.ResetColor();

    // Display some suspense
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Selecting a random monkey");
    for (int i = 0; i < 3; i++)
    {
        await Task.Delay(300);
        Console.Write(".");
    }
    Console.WriteLine();
    Console.ResetColor();

    var randomMonkey = MonkeyHelper.GetRandomMonkey();
    
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("\n🎉 You got:");
    Console.ResetColor();
    
    DisplayMonkeyDetails(randomMonkey);
    
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"\n📊 This is random monkey #{MonkeyHelper.RandomMonkeyAccessCount}");
    Console.ResetColor();
}

/// <summary>
/// Displays detailed information about a specific monkey.
/// </summary>
/// <param name="monkey">The monkey to display details for.</param>
static void DisplayMonkeyDetails(Monkey monkey)
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("┌─────────────────────────────────────┐");
    Console.WriteLine($"│ {monkey.Name.PadRight(35)} │");
    Console.WriteLine("├─────────────────────────────────────┤");
    Console.ResetColor();
    
    Console.WriteLine($"│ 🌍 Location: {monkey.Location.PadRight(23)} │");
    Console.WriteLine($"│ 👥 Population: {monkey.Population.ToString("N0").PadRight(21)} │");
    Console.WriteLine($"│ 🍃 Diet: {monkey.Diet.PadRight(27)} │");
    Console.WriteLine($"│ ⏰ Lifespan: {monkey.Lifespan} years{new string(' ', 18 - monkey.Lifespan.ToString().Length)} │");
    
    var statusColor = monkey.ConservationStatus switch
    {
        "Endangered" => ConsoleColor.Red,
        "Vulnerable" => ConsoleColor.DarkYellow,
        "Least Concern" => ConsoleColor.Green,
        _ => ConsoleColor.White
    };
    
    Console.Write("│ 🛡️  Status: ");
    Console.ForegroundColor = statusColor;
    Console.Write(monkey.ConservationStatus);
    Console.ResetColor();
    Console.WriteLine(new string(' ', 35 - 11 - monkey.ConservationStatus.Length) + "│");
    
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("├─────────────────────────────────────┤");
    Console.ResetColor();
    
    // Word wrap description
    var descWords = monkey.Description.Split(' ');
    var line = "│ ";
    foreach (var word in descWords)
    {
        if (line.Length + word.Length + 1 > 37)
        {
            Console.WriteLine(line.PadRight(37) + " │");
            line = "│ " + word + " ";
        }
        else
        {
            line += word + " ";
        }
    }
    if (line.Length > 2)
    {
        Console.WriteLine(line.PadRight(37) + " │");
    }
    
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("└─────────────────────────────────────┘");
    Console.ResetColor();
}

/// <summary>
/// Displays a farewell message when the user exits the application.
/// </summary>
static void DisplayExitMessage()
{
    Console.Clear();
    AsciiArt.DisplayRandomMonkeyArt();
    
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Thank you for exploring the world of monkeys! 🐒");
    Console.WriteLine("Keep learning about our primate friends! 🌟");
    Console.ResetColor();
    
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"\n📊 Final stats: You accessed {MonkeyHelper.RandomMonkeyAccessCount} random monkey(s)!");
    Console.ResetColor();
}
