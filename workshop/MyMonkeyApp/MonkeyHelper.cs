namespace MyMonkeyApp;

/// <summary>
/// A static helper class to manage monkey data operations and provide monkey information.
/// </summary>
public static class MonkeyHelper
{
    private static int _randomMonkeyAccessCount = 0;
    
    /// <summary>
    /// Gets the number of times a random monkey has been accessed.
    /// </summary>
    public static int RandomMonkeyAccessCount => _randomMonkeyAccessCount;

    private static readonly List<Monkey> _monkeys = new()
    {
        new Monkey
        {
            Name = "Proboscis Monkey",
            Location = "Borneo",
            Population = 7000,
            Description = "Known for their distinctive large nose and reddish-brown fur.",
            ConservationStatus = "Endangered",
            Diet = "Herbivore",
            Lifespan = 13
        },
        new Monkey
        {
            Name = "Golden Snub-nosed Monkey",
            Location = "China",
            Population = 15000,
            Description = "Beautiful primates with golden fur and upturned noses.",
            ConservationStatus = "Endangered",
            Diet = "Herbivore",
            Lifespan = 22
        },
        new Monkey
        {
            Name = "Howler Monkey",
            Location = "Central and South America",
            Population = 100000,
            Description = "Known for their loud howling calls that can be heard for miles.",
            ConservationStatus = "Least Concern",
            Diet = "Herbivore",
            Lifespan = 15
        },
        new Monkey
        {
            Name = "Japanese Macaque",
            Location = "Japan",
            Population = 114000,
            Description = "Also known as snow monkeys, famous for bathing in hot springs.",
            ConservationStatus = "Least Concern",
            Diet = "Omnivore",
            Lifespan = 27
        },
        new Monkey
        {
            Name = "Spider Monkey",
            Location = "Central and South America",
            Population = 50000,
            Description = "Agile primates with long limbs and prehensile tails.",
            ConservationStatus = "Vulnerable",
            Diet = "Frugivore",
            Lifespan = 22
        },
        new Monkey
        {
            Name = "Mandrill",
            Location = "Equatorial Africa",
            Population = 800000,
            Description = "Colorful primates with distinctive blue and red facial markings.",
            ConservationStatus = "Vulnerable",
            Diet = "Omnivore",
            Lifespan = 20
        },
        new Monkey
        {
            Name = "Capuchin Monkey",
            Location = "Central and South America",
            Population = 150000,
            Description = "Intelligent primates known for using tools and problem-solving.",
            ConservationStatus = "Least Concern",
            Diet = "Omnivore",
            Lifespan = 25
        },
        new Monkey
        {
            Name = "Squirrel Monkey",
            Location = "South America",
            Population = 300000,
            Description = "Small, agile monkeys with bright yellow and orange coloring.",
            ConservationStatus = "Least Concern",
            Diet = "Omnivore",
            Lifespan = 15
        },
        new Monkey
        {
            Name = "Vervet Monkey",
            Location = "Eastern and Southern Africa",
            Population = 500000,
            Description = "Medium-sized monkeys with distinctive black faces and long tails.",
            ConservationStatus = "Least Concern",
            Diet = "Omnivore",
            Lifespan = 12
        },
        new Monkey
        {
            Name = "Bonobo",
            Location = "Democratic Republic of Congo",
            Population = 50000,
            Description = "Peaceful great apes known for their intelligence and social behavior.",
            ConservationStatus = "Endangered",
            Diet = "Omnivore",
            Lifespan = 40
        }
    };

    /// <summary>
    /// Retrieves all available monkey species.
    /// </summary>
    /// <returns>A read-only list of all monkey species.</returns>
    public static IReadOnlyList<Monkey> GetAllMonkeys()
    {
        return _monkeys.AsReadOnly();
    }

    /// <summary>
    /// Selects a random monkey species and increments the access counter.
    /// </summary>
    /// <returns>A randomly selected monkey species.</returns>
    public static Monkey GetRandomMonkey()
    {
        var random = new Random();
        var randomIndex = random.Next(_monkeys.Count);
        _randomMonkeyAccessCount++;
        return _monkeys[randomIndex];
    }

    /// <summary>
    /// Finds a monkey species by its name (case-insensitive search).
    /// </summary>
    /// <param name="name">The name of the monkey species to find.</param>
    /// <returns>The monkey species if found, otherwise null.</returns>
    public static Monkey? GetMonkeyByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        return _monkeys.FirstOrDefault(m => 
            string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Searches for monkeys containing the specified search term in their name.
    /// </summary>
    /// <param name="searchTerm">The term to search for in monkey names.</param>
    /// <returns>A list of monkeys whose names contain the search term.</returns>
    public static List<Monkey> SearchMonkeys(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<Monkey>();

        return _monkeys.Where(m => 
            m.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}