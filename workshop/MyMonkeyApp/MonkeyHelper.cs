namespace MyMonkeyApp;

/// <summary>
/// Static helper class for managing monkey data and operations
/// </summary>
public static class MonkeyHelper
{
    private static readonly List<Monkey> _monkeys = new();
    private static readonly Random _random = new();
    private static int _randomAccessCount = 0;
    private static DateTime _lastDataRefresh = DateTime.MinValue;

    /// <summary>
    /// Gets the total number of times a random monkey has been accessed
    /// </summary>
    public static int RandomAccessCount => _randomAccessCount;

    /// <summary>
    /// Gets the timestamp of the last data refresh from the MCP server
    /// </summary>
    public static DateTime LastDataRefresh => _lastDataRefresh;

    /// <summary>
    /// Initializes the monkey data by loading from the Monkey MCP server
    /// </summary>
    static MonkeyHelper()
    {
        LoadMonkeyDataFromMcpServer();
    }

    /// <summary>
    /// Gets all available monkeys
    /// </summary>
    /// <returns>Read-only collection of all monkeys</returns>
    public static IReadOnlyList<Monkey> GetAllMonkeys()
    {
        RefreshDataIfNeeded();
        return _monkeys.AsReadOnly();
    }

    /// <summary>
    /// Gets a random monkey from the collection and increments the access counter
    /// </summary>
    /// <returns>A randomly selected monkey, or null if no monkeys are available</returns>
    public static Monkey? GetRandomMonkey()
    {
        RefreshDataIfNeeded();
        
        if (_monkeys.Count == 0)
        {
            return null;
        }

        var randomIndex = _random.Next(_monkeys.Count);
        _randomAccessCount++;
        
        return _monkeys[randomIndex];
    }

    /// <summary>
    /// Finds a monkey by its name (case-insensitive search)
    /// </summary>
    /// <param name="name">The name of the monkey to find</param>
    /// <returns>The monkey with the matching name, or null if not found</returns>
    public static Monkey? GetMonkeyByName(string name)
    {
        RefreshDataIfNeeded();
        
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        return _monkeys.FirstOrDefault(m => 
            string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Searches for monkeys by partial name match (case-insensitive)
    /// </summary>
    /// <param name="searchTerm">The search term to match against monkey names</param>
    /// <returns>Collection of monkeys with names containing the search term</returns>
    public static IEnumerable<Monkey> SearchMonkeysByName(string searchTerm)
    {
        RefreshDataIfNeeded();
        
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Enumerable.Empty<Monkey>();
        }

        return _monkeys.Where(m => 
            m.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets monkeys filtered by conservation status
    /// </summary>
    /// <param name="status">The conservation status to filter by</param>
    /// <returns>Collection of monkeys with the specified conservation status</returns>
    public static IEnumerable<Monkey> GetMonkeysByConservationStatus(ConservationStatus status)
    {
        RefreshDataIfNeeded();
        return _monkeys.Where(m => m.ConservationStatus == status);
    }

    /// <summary>
    /// Gets statistics about the monkey collection
    /// </summary>
    /// <returns>MonkeyStatistics object with aggregated data</returns>
    public static MonkeyStatistics GetStatistics()
    {
        RefreshDataIfNeeded();
        
        return new MonkeyStatistics
        {
            TotalSpecies = _monkeys.Count,
            TotalPopulation = _monkeys.Sum(m => m.Population),
            EndangeredSpecies = _monkeys.Count(m => 
                m.ConservationStatus == ConservationStatus.Endangered || 
                m.ConservationStatus == ConservationStatus.CriticallyEndangered),
            MostPopulousSpecies = _monkeys.OrderByDescending(m => m.Population).FirstOrDefault()?.Name ?? string.Empty,
            RarestSpecies = _monkeys.OrderBy(m => m.Population).FirstOrDefault()?.Name ?? string.Empty
        };
    }

    /// <summary>
    /// Forces a refresh of monkey data from the MCP server
    /// </summary>
    public static void RefreshData()
    {
        LoadMonkeyDataFromMcpServer();
    }

    /// <summary>
    /// Refreshes data if it's older than 1 hour
    /// </summary>
    private static void RefreshDataIfNeeded()
    {
        if (DateTime.Now - _lastDataRefresh > TimeSpan.FromHours(1))
        {
            LoadMonkeyDataFromMcpServer();
        }
    }

    /// <summary>
    /// Loads monkey data from the Monkey MCP server
    /// </summary>
    private static void LoadMonkeyDataFromMcpServer()
    {
        try
        {
            // TODO: Implement actual MCP server integration
            // For now, using sample data that would typically come from the MCP server
            _monkeys.Clear();
            _monkeys.AddRange(GetSampleMonkeyData());
            _lastDataRefresh = DateTime.Now;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading monkey data from MCP server: {ex.Message}");
            // Fallback to sample data if MCP server is unavailable
            if (_monkeys.Count == 0)
            {
                _monkeys.AddRange(GetSampleMonkeyData());
                _lastDataRefresh = DateTime.Now;
            }
        }
    }

    /// <summary>
    /// Gets sample monkey data (represents data that would come from MCP server)
    /// </summary>
    /// <returns>Collection of sample monkey data</returns>
    private static IEnumerable<Monkey> GetSampleMonkeyData()
    {
        return new List<Monkey>
        {
            new() { Name = "Babouin", Species = "Papio", Location = "Africa, Arabia", Population = 200000, 
                   Details = "Large terrestrial primates with distinctive dog-like snouts", ConservationStatus = ConservationStatus.LeastConcern },
            new() { Name = "Capuchin", Species = "Cebus", Location = "Central/South America", Population = 100000, 
                   Details = "Intelligent New World monkeys known for tool use", ConservationStatus = ConservationStatus.LeastConcern },
            new() { Name = "Howler Monkey", Species = "Alouatta", Location = "Central/South America", Population = 100000, 
                   Details = "Known for their loud howling calls that can be heard for miles", ConservationStatus = ConservationStatus.LeastConcern },
            new() { Name = "Japanese Macaque", Species = "Macaca fuscata", Location = "Japan", Population = 100000, 
                   Details = "Also known as snow monkeys, famous for bathing in hot springs", ConservationStatus = ConservationStatus.LeastConcern },
            new() { Name = "Proboscis Monkey", Species = "Nasalis larvatus", Location = "Borneo", Population = 7000, 
                   Details = "Endangered species with distinctive large noses", ConservationStatus = ConservationStatus.Endangered },
            new() { Name = "Golden Lion Tamarin", Species = "Leontopithecus rosalia", Location = "Brazil Atlantic Forest", Population = 3200, 
                   Details = "Critically endangered small monkey with golden mane", ConservationStatus = ConservationStatus.CriticallyEndangered },
            new() { Name = "Vervet Monkey", Species = "Chlorocebus pygerythrus", Location = "Eastern Africa", Population = 500000, 
                   Details = "Medium-sized Old World monkeys with blue faces", ConservationStatus = ConservationStatus.LeastConcern },
            new() { Name = "Spider Monkey", Species = "Ateles", Location = "Central/South America", Population = 250000, 
                   Details = "Long-limbed New World monkeys excellent at swinging", ConservationStatus = ConservationStatus.Vulnerable },
            new() { Name = "Mandrill", Species = "Mandrillus sphinx", Location = "Equatorial Africa", Population = 600000, 
                   Details = "Largest monkey species with colorful facial markings", ConservationStatus = ConservationStatus.Vulnerable },
            new() { Name = "Squirrel Monkey", Species = "Saimiri", Location = "Central/South America", Population = 300000, 
                   Details = "Small, agile monkeys with distinctive Roman helmet markings", ConservationStatus = ConservationStatus.LeastConcern }
        };
    }
}