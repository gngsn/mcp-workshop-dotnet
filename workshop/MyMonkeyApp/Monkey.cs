namespace MyMonkeyApp;

/// <summary>
/// Represents a monkey species with its characteristics and details.
/// </summary>
public class Monkey
{
    /// <summary>
    /// Gets or sets the name of the monkey species.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the primary location where this monkey species is found.
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated population of this monkey species.
    /// </summary>
    public int Population { get; set; }

    /// <summary>
    /// Gets or sets a brief description of the monkey species.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the conservation status of the monkey species.
    /// </summary>
    public string ConservationStatus { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the diet type of the monkey species.
    /// </summary>
    public string Diet { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the average lifespan of the monkey species in years.
    /// </summary>
    public int Lifespan { get; set; }

    /// <summary>
    /// Returns a string representation of the monkey with its key details.
    /// </summary>
    /// <returns>A formatted string containing monkey details.</returns>
    public override string ToString()
    {
        return $"{Name} - {Location} (Population: {Population:N0})";
    }
}