namespace WorkflowTracking.Modules.Users.Domain.Users;
public sealed class Permission
{
    // Users management
    public static readonly Permission GetUsers = new("users:read");
    public static readonly Permission CreateUser = new("users:create");
    public static readonly Permission ModifyUser = new("users:update");
   
    // Classroom management
    public static readonly Permission GetClassrooms = new("classrooms:read");
    public static readonly Permission CreateClassroom = new("classrooms:create");
    public static readonly Permission ModifyClassroom = new("classrooms:update");
    public static readonly Permission DeleteClassroom = new("classrooms:delete");

    // Student assignment
    public static readonly Permission AddStudentToClassroom = new("classrooms:add-student");
    public static readonly Permission RemoveStudentFromClassroom = new("classrooms:remove-student");
    public static readonly Permission InviteStudent = new("students:invite");

    // Attendance
    public static readonly Permission GetAttendance = new("attendance:read");
    public static readonly Permission RecordAttendance = new("attendance:record");
    public static readonly Permission ModifyAttendance = new("attendance:update");

    // QR Sessions
    public static readonly Permission GenerateQrSession = new("qr:generate");
    public static readonly Permission ScanQr = new("qr:scan");

    // Spiritual activities
    public static readonly Permission GetSpiritualActivities = new("spiritual:read");
    public static readonly Permission RecordSpiritualActivity = new("spiritual:record");

    // Roles & Permissions
    public static readonly Permission GetRoles = new("roles:read");
    public static readonly Permission ModifyRoles = new("roles:update");


    public Permission(string code)
    {
        Code = code;
    }

    public string Code { get; }
}

