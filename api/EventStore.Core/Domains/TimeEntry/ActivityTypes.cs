using System;

namespace EventStore.Core.Domains.TimeEntry;

/// <summary>
/// Defines the types of activities that can be logged in time entries
/// </summary>
public enum ActivityTypes
{
    /// <summary>
    /// Analysis and requirements gathering
    /// </summary>
    Analysis = 1,

    /// <summary>
    /// Software development and coding
    /// </summary>
    Development = 2,

    /// <summary>
    /// Testing and quality assurance
    /// </summary>
    Testing = 3,

    /// <summary>
    /// Code review activities
    /// </summary>
    CodeReview = 4,

    /// <summary>
    /// Documentation writing
    /// </summary>
    Documentation = 5,

    /// <summary>
    /// Meetings and discussions
    /// </summary>
    Meeting = 6,

    /// <summary>
    /// Bug fixing and troubleshooting
    /// </summary>
    BugFix = 7,

    /// <summary>
    /// Deployment and release activities
    /// </summary>
    Deployment = 8,

    /// <summary>
    /// Research and learning
    /// </summary>
    Research = 9,

    /// <summary>
    /// Other activities not covered above
    /// </summary>
    Other = 99
}

/// <summary>
/// Extension methods for ActivityTypes enum
/// </summary>
public static class ActivityTypesExtensions
{
    /// <summary>
    /// Gets the display name for the activity type
    /// </summary>
    public static string GetDisplayName(this ActivityTypes activityTypes)
    {
        return activityTypes switch
        {
            ActivityTypes.Analysis => "Analysis",
            ActivityTypes.Development => "Development",
            ActivityTypes.Testing => "Testing",
            ActivityTypes.CodeReview => "Code Review",
            ActivityTypes.Documentation => "Documentation",
            ActivityTypes.Meeting => "Meeting",
            ActivityTypes.BugFix => "Bug Fix",
            ActivityTypes.Deployment => "Deployment",
            ActivityTypes.Research => "Research",
            ActivityTypes.Other => "Other",
            _ => throw new ArgumentException($"Unknown activity type: {activityTypes}")
        };
    }

    /// <summary>
    /// Checks if the activity type is billable by default
    /// Can be overridden at time entry level
    /// </summary>
    public static bool IsDefaultBillable(this ActivityTypes activityTypes)
    {
        return activityTypes switch
        {
            ActivityTypes.Analysis => true,
            ActivityTypes.Development => true,
            ActivityTypes.Testing => true,
            ActivityTypes.CodeReview => true,
            ActivityTypes.Documentation => true,
            ActivityTypes.BugFix => true,
            ActivityTypes.Deployment => true,
            ActivityTypes.Meeting => false, // Usually internal
            ActivityTypes.Research => false, // Usually internal
            ActivityTypes.Other => false,
            _ => false
        };
    }

    /// <summary>
    /// Parses a string to ActivityTypes enum
    /// </summary>
    public static ActivityTypes Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Activity type cannot be null or empty", nameof(value));

        if (Enum.TryParse<ActivityTypes>(value, ignoreCase: true, out var result))
            return result;

        throw new ArgumentException($"Invalid activity type: {value}", nameof(value));
    }

    /// <summary>
    /// Tries to parse a string to ActivityTypes enum
    /// </summary>
    public static bool TryParse(string value, out ActivityTypes activityTypes)
    {
        return Enum.TryParse(value, ignoreCase: true, out activityTypes);
    }
}