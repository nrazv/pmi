namespace pmi.Project.Models;
public enum ProjectStatus
{
    NotStarted,         // Testing has not yet begun
    InProgress,         // Testing is currently underway
    PendingVerification, // A finding is under further validation
    Exploited,          // Vulnerability has been successfully exploited
    RemediationVerified, // Fix has been validated through retesting
    Mitigated,          // Risk has been reduced to an acceptable level
    FalsePositive,      // Finding was not valid
    Informational,      // Issue is informational with no immediate risk
    AcceptedRisk,       // Acknowledged but not remediated
    Resolved,           // Vulnerability has been completely fixed
    Deferred,           // Issue deferred for future consideration
    Closed,             // Task or finding is complete, no action required
    UnderInvestigation, // Additional analysis is ongoing
    NoExploitFound,     // Testing confirmed no exploitability
    OutOfScope          // Issue falls outside the engagement's boundaries
}