using WorkflowTracking.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkflowTracking.Modules.Users.Infrastructure.Users;
internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Code);

        builder.Property(p => p.Code).HasMaxLength(100);

        builder.HasData(
            Permission.GetUsers,
            Permission.CreateUser,
            Permission.ModifyUser,

            Permission.GetClassrooms,
            Permission.CreateClassroom,
            Permission.ModifyClassroom,
            Permission.DeleteClassroom,

            Permission.AddStudentToClassroom,
            Permission.RemoveStudentFromClassroom,
            Permission.InviteStudent,

            Permission.GetAttendance,
            Permission.RecordAttendance,
            Permission.ModifyAttendance,

            Permission.GenerateQrSession,
            Permission.ScanQr,

            Permission.GetSpiritualActivities,
            Permission.RecordSpiritualActivity,

            Permission.GetRoles,
            Permission.ModifyRoles);

        builder
            .HasMany<Role>()
            .WithMany()
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("role_permissions");

                joinBuilder.HasData(
                    // Admin permissions
                    CreateRolePermission(Role.Administrator, Permission.GetUsers),
                    CreateRolePermission(Role.Administrator, Permission.CreateUser),
                    CreateRolePermission(Role.Administrator, Permission.ModifyUser),
                    CreateRolePermission(Role.Administrator, Permission.GetClassrooms),
                    CreateRolePermission(Role.Administrator, Permission.CreateClassroom),
                    CreateRolePermission(Role.Administrator, Permission.ModifyClassroom),
                    CreateRolePermission(Role.Administrator, Permission.DeleteClassroom),
                    CreateRolePermission(Role.Administrator, Permission.AddStudentToClassroom),
                    CreateRolePermission(Role.Administrator, Permission.RemoveStudentFromClassroom),
                    CreateRolePermission(Role.Administrator, Permission.InviteStudent),
                    CreateRolePermission(Role.Administrator, Permission.GetAttendance),
                    CreateRolePermission(Role.Administrator, Permission.ModifyAttendance),
                    CreateRolePermission(Role.Administrator, Permission.RecordAttendance),
                    CreateRolePermission(Role.Administrator, Permission.GenerateQrSession),
                    CreateRolePermission(Role.Administrator, Permission.GetSpiritualActivities),
                    CreateRolePermission(Role.Administrator, Permission.GetRoles),
                    CreateRolePermission(Role.Administrator, Permission.ModifyRoles),

                    // Teacher permissions
                    CreateRolePermission(Role.Teacher, Permission.GetClassrooms),
                    CreateRolePermission(Role.Teacher, Permission.CreateClassroom),
                    CreateRolePermission(Role.Teacher, Permission.ModifyClassroom),
                    CreateRolePermission(Role.Teacher, Permission.DeleteClassroom),
                    CreateRolePermission(Role.Teacher, Permission.ModifyUser),
                    CreateRolePermission(Role.Teacher, Permission.AddStudentToClassroom),
                    CreateRolePermission(Role.Teacher, Permission.RemoveStudentFromClassroom),
                    CreateRolePermission(Role.Teacher, Permission.InviteStudent),
                    CreateRolePermission(Role.Teacher, Permission.GetAttendance),
                    CreateRolePermission(Role.Teacher, Permission.ModifyAttendance),
                    CreateRolePermission(Role.Teacher, Permission.RecordAttendance),
                    CreateRolePermission(Role.Teacher, Permission.GenerateQrSession),
                    CreateRolePermission(Role.Teacher, Permission.ScanQr),
                    CreateRolePermission(Role.Teacher, Permission.GetSpiritualActivities),

                    // Student permissions
                    CreateRolePermission(Role.Student, Permission.GetUsers),
                    CreateRolePermission(Role.Student, Permission.ModifyUser),
                    CreateRolePermission(Role.Student, Permission.GetAttendance),
                    CreateRolePermission(Role.Student, Permission.RecordAttendance),
                    CreateRolePermission(Role.Student, Permission.ScanQr),
                    CreateRolePermission(Role.Student, Permission.GetSpiritualActivities),
                    CreateRolePermission(Role.Student, Permission.RecordSpiritualActivity)
                    );
            });
    }

    private static object CreateRolePermission(Role role, Permission permission)
    {
        return new
        {
            RoleName = role.Name,
            PermissionCode = permission.Code
        };
    }
}
