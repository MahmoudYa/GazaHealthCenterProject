using AutoMapper;
using Microsoft.EntityFrameworkCore;
using GazaHealthCenter_2.Objects;

namespace GazaHealthCenter_2.Data.Migrations;

public sealed class Configuration : IDisposable
{
    private DbContext Context { get; }
    private IUnitOfWork UnitOfWork { get; }

    public Configuration(DbContext context, IMapper mapper)
    {
        UnitOfWork = new AuditedUnitOfWork(context, mapper, 0);
        Context = context;
    }

    public void Migrate()
    {
        Context.Database.Migrate();

        Seed();
    }
    public void Seed()
    {
        SeedPermissions();
        SeedRoles();

        SeedAccounts();
    }

    private void SeedPermissions()
    {
        List<Permission> permissions = new()
        {
            new() { Area = "Administration", Controller = "Accounts", Action = "Create" },
            new() { Area = "Administration", Controller = "Accounts", Action = "Edit" },
            new() { Area = "Administration", Controller = "Accounts", Action = "Index" },
            new() { Area = "Administration", Controller = "Roles", Action = "Create" },
            new() { Area = "Administration", Controller = "Roles", Action = "Delete" },
            new() { Area = "Administration", Controller = "Roles", Action = "Edit" },
            new() { Area = "Administration", Controller = "Roles", Action = "Index" },
            new() { Area = "Advertisement", Controller = "Advertisement", Action = "Index" },
            new() { Area = "Advertisement", Controller = "Advertisement", Action = "Create" },
            new() { Area = "Advertisement", Controller = "Advertisement", Action = "Edit" },
            new() { Area = "Advertisement", Controller = "Advertisement", Action = "Delete" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "Index" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "Create" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "Edit" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "Delete" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "Details" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "DetailsHospitals" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "DetailsHealthCenters" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "DetailsMedicalPoints" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "DetailsPharmacies" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "Hospitals" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "HealthCenters" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "MedicalPoints" },
            new() { Area = "MedicalLocation", Controller = "MedicalLocation", Action = "Pharmacies" },
            new() { Area = "Consultation", Controller = "Doctor", Action = "Index" },
            new() { Area = "Consultation", Controller = "Doctor", Action = "ShowDoctors" },
            new() { Area = "Consultation", Controller = "Doctor", Action = "Edit" },
            new() { Area = "Consultation", Controller = "Doctor", Action = "Create" },
            new() { Area = "Consultation", Controller = "Doctor", Action = "Delete" },
            new() { Area = "Consultation", Controller = "Department", Action = "Consultations" },
            new() { Area = "Consultation", Controller = "Department", Action = "ConsultationsShow" },
            new() { Area = "Consultation", Controller = "Department", Action = "Create" },
            new() { Area = "Consultation", Controller = "Department", Action = "CreateConsultation" },
            new() { Area = "Consultation", Controller = "Department", Action = "CreateConsultationShow" },
            new() { Area = "Consultation", Controller = "Department", Action = "Edit" },
            new() { Area = "Consultation", Controller = "Department", Action = "EditConsultation" },
            new() { Area = "Consultation", Controller = "Department", Action = "EditResponse" },
            new() { Area = "Consultation", Controller = "Department", Action = "Index" },
            new() { Area = "Consultation", Controller = "Department", Action = "Respond" },
            new() { Area = "Consultation", Controller = "Department", Action = "Responses" },
            new() { Area = "Consultation", Controller = "Department", Action = "ResponsesShow" },
            new() { Area = "Consultation", Controller = "Department", Action = "DeleteConsultation" },
            new() { Area = "Consultation", Controller = "Department", Action = "DeleteResponse" },
            new() { Area = "Consultation", Controller = "Department", Action = "Delete" },
            new() { Area = "Consultation", Controller = "Department", Action = "ShowDepartment" },
            new() { Area = "PsychologicalSession", Controller = "PsychologicalSession", Action = "Index" },
            new() { Area = "PsychologicalSession", Controller = "PsychologicalSession", Action = "Delete" },
            new() { Area = "PsychologicalSession", Controller = "PsychologicalSession", Action = "Create" },
            new() { Area = "PsychologicalSession", Controller = "PsychologicalSession", Action = "Book" },
            new() { Area = "PsychologicalSession", Controller = "PsychologicalSession", Action = "CancelBooking" }


        };

        foreach (Permission permission in UnitOfWork.Select<Permission>().ToArray())
            if (permissions.RemoveAll(p => p.Area == permission.Area && p.Controller == permission.Controller && p.Action == permission.Action) == 0)
            {
                UnitOfWork.DeleteRange(UnitOfWork.Select<RolePermission>().Where(role => role.PermissionId == permission.Id));
                UnitOfWork.Delete(permission);
            }

        UnitOfWork.InsertRange(permissions);
        UnitOfWork.Commit();
    }

    private void SeedRoles()
    {
        if (!UnitOfWork.Select<Role>().Any(role => role.Title == "Sys_Admin"))
        {
            UnitOfWork.Insert(new Role { Title = "Sys_Admin", Permissions = new List<RolePermission>() });
            UnitOfWork.Commit();
        }

        Role admin = UnitOfWork.Select<Role>().Single(role => role.Title == "Sys_Admin");
        Int64[] permissions = admin.Permissions.Select(role => role.PermissionId).ToArray();

        foreach (Permission permission in UnitOfWork.Select<Permission>())
            if (!permissions.Contains(permission.Id))
                UnitOfWork.Insert(new RolePermission { RoleId = admin.Id, PermissionId = permission.Id });

        UnitOfWork.Commit();
    }

    private void SeedAccounts()
    {
        Account[] accounts =
        {
            new()
            {
                Username = "admin",
                Passhash = "$2b$13$DIMDOgBUPiekvqt3U07UpuYxXOBttkiAmqXVPfjObUU1ttUJsnOxS",
                Email = "admin@test.domains.com",
                IsLocked = false,

                RoleId = UnitOfWork.Select<Role>().Single(role => role.Title == "Sys_Admin").Id
            }
        };

        foreach (Account account in accounts)
        {
            if (UnitOfWork.Select<Account>().FirstOrDefault(model => model.Username == account.Username) is Account currentAccount)
            {
                currentAccount.IsLocked = account.IsLocked;
                currentAccount.RoleId = account.RoleId;

                UnitOfWork.Update(currentAccount);
            }
            else
            {
                UnitOfWork.Insert(account);
            }
        }

        UnitOfWork.Commit();
    }

    public void Dispose()
    {
        UnitOfWork.Dispose();
        Context.Dispose();
    }
}
