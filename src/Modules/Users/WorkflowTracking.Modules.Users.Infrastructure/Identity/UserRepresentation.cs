namespace WorkflowTracking.Modules.Users.Infrastructure.Identity;
internal sealed record UserRepresentation(
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string Mobile,
    bool EmailVerified,
    bool Enabled,
    CredentialRepresentation[] Credentials);
