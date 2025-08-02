namespace MyMonkeyApp;

/// <summary>
/// Represents a monkey species with its characteristics and population data
/// </summary>
public class Monkey
{
    /// <summary>
    /// Common name of the monkey species
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Scientific genus classification
    /// </summary>
    public string Species { get; set; } = string.Empty;

    /// <summary>
    /// Geographic habitat range where the monkey is found
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Estimated current population count
    /// </summary>
    public int Population { get; set; }

    /// <summary>
    /// Key characteristics and notable features of the monkey
    /// </summary>
    public string Details { get; set; } = string.Empty;

    /// <summary>
    /// Conservation status of the species
    /// </summary>
    public ConservationStatus ConservationStatus { get; set; }

    /// <summary>
    /// Returns a formatted string representation of the monkey
    /// </summary>
    /// <returns>String containing monkey name and location</returns>
    public override string ToString()
    {
        return $"{Name} ({Species}) - {Location}";
    }

    /// <summary>
    /// Gets a detailed description of the monkey including all properties
    /// </summary>
    /// <returns>Comprehensive string description</returns>
    public string GetDetailedDescription()
    {
        return $"""
            Name: {Name}
            Species: {Species}
            Location: {Location}
            Population: {Population:N0}
            Conservation Status: {ConservationStatus}
            Details: {Details}
            """;
    }
}

/// <summary>
/// Enumeration representing different conservation status levels
/// </summary>
public enum ConservationStatus
{
    LeastConcern,
    NearThreatened,
    Vulnerable,
    Endangered,
    CriticallyEndangered,
    ExtinctInWild,
    Extinct
}

/// <summary>
/// Represents statistical information about monkey populations
/// </summary>
public class MonkeyStatistics
{
    public int TotalSpecies { get; set; }
    public int TotalPopulation { get; set; }
    public int EndangeredSpecies { get; set; }
    public string MostPopulousSpecies { get; set; } = string.Empty;
    public string RarestSpecies { get; set; } = string.Empty;
}

/// <summary>
/// Response model for monkey data from MCP server
/// </summary>
public class MonkeyResponse
{
    public List<Monkey> Monkeys { get; set; } = new();
    public MonkeyStatistics Statistics { get; set; } = new();
    public DateTime LastUpdated { get; set; }
}