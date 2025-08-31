namespace WorkflowTracking.Modules.Users.Domain.Users;
public sealed class Role
{
    public static readonly Role Administrator = new("Administrator");
    public static readonly Role Teacher = new("Teacher");
    public static readonly Role Student = new("Student");
    public static readonly Role Editor = new("Editor");

    private Role(string name)
    {
        Name = name;
    }

    private Role() { }

    public string Name { get; private set; }
}

