namespace MyMonkeyApp;

/// <summary>
/// Provides ASCII art functionality for the monkey console application.
/// </summary>
public static class AsciiArt
{
    private static readonly string[] _monkeyArt = 
    {
        @"
      .-""-""-.
     /        \
    |  o    o  |
    |    <>    |
    |   __     |
     \        /
      '------'
    🐒 Monkey Time! 🐒
",
        @"
         .-""-.
        /      \
       |  o  o  |
       |    ∩   |
       |  \___/ |
        \      /
         '----'
    🙊 See no evil! 🙊
",
        @"
        ,-.___,-.
       /  o   o  \
      |     ∩     |
      |   \___/   |
       \    ▽    /
        '-,___,-'
    🙈 Hear no evil! 🙈
",
        @"
         _____
        /     \
       | () () |
       |   >   |
       |  ---  |
        \     /
         '---'
    🙉 Speak no evil! 🙉
",
        @"
          .-.
         (o.o)
          > ^
    🐵 Curious monkey! 🐵
"
    };

    private static readonly string[] _banners = 
    {
        @"
 ███╗   ███╗ ██████╗ ███╗   ██╗██╗  ██╗███████╗██╗   ██╗
 ████╗ ████║██╔═══██╗████╗  ██║██║ ██╔╝██╔════╝╚██╗ ██╔╝
 ██╔████╔██║██║   ██║██╔██╗ ██║█████╔╝ █████╗   ╚████╔╝ 
 ██║╚██╔╝██║██║   ██║██║╚██╗██║██╔═██╗ ██╔══╝    ╚██╔╝  
 ██║ ╚═╝ ██║╚██████╔╝██║ ╚████║██║  ██╗███████╗   ██║   
 ╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝   ╚═╝   
                                                          
          ┌─┐┌─┐┌─┐                                      
          ├─┤├─┘├─┘                                      
          ┴ ┴┴  ┴                                        
",
        @"
╔╦╗╔═╗╔╗╔╦╔═╔═╗╦ ╦  ╔═╗╔═╗╔═╗
║║║║ ║║║║╠╩╗║╣ ╚╦╝  ╠═╣╠═╝╠═╝
╩ ╩╚═╝╝╚╝╩ ╩╚═╝ ╩   ╩ ╩╩  ╩  
🐒🐵🙈🙉🙊🐒🐵🙈🙉🙊🐒🐵🙈🙉🙊
"
    };

    /// <summary>
    /// Displays a random monkey ASCII art.
    /// </summary>
    public static void DisplayRandomMonkeyArt()
    {
        var random = new Random();
        var artIndex = random.Next(_monkeyArt.Length);
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(_monkeyArt[artIndex]);
        Console.ResetColor();
    }

    /// <summary>
    /// Displays a random banner for the application.
    /// </summary>
    public static void DisplayRandomBanner()
    {
        var random = new Random();
        var bannerIndex = random.Next(_banners.Length);
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(_banners[bannerIndex]);
        Console.ResetColor();
    }

    /// <summary>
    /// Displays a welcome message with random ASCII art.
    /// </summary>
    public static void DisplayWelcome()
    {
        Console.Clear();
        DisplayRandomBanner();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Welcome to the Monkey Species Explorer!");
        Console.WriteLine("Discover fascinating facts about primates from around the world.");
        Console.ResetColor();
        Console.WriteLine();
    }
}