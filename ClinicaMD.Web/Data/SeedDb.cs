using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaMD.Web.Data;
using ClinicaMD.Web.Data.Entities;
using ClinicaMD.Web.Enums;
using ClinicaMD.Web.Helpers;
using ClinicaMD.Web.Models;

public class SeedDb
{
    private readonly ApplicationDbContext _context;
    private readonly IUserHelper _userHelper;

    public SeedDb(ApplicationDbContext context, IUserHelper userHelper)
    {
        _context = context;
        _userHelper = userHelper;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();


        await CheckRolesAsync();
        await CheckUserAsync("1", "Diego", "R", "diegoramirez1949@gmail.com", "3000000000", "Calle Luna Calle Sol", UserType.Admin);

    }
    private async Task CheckRolesAsync()
    {
        await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
        await _userHelper.CheckRoleAsync(UserType.User.ToString());
    }

    private async Task<User> CheckUserAsync(
        string document,
        string firstName,
        string lastName,
        string email,
        string phone,
        string address,
        UserType userType)
    {
        User user = await _userHelper.GetUserAsync(email);
        if (user == null)
        {
            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Address = address,
                Document = document,
                doctor = _context.Doctors.FirstOrDefault(),
                UserType = userType
            };

            await _userHelper.AddUserAsync(user, "123456"); //password debe tener una longitud de 6 caracteres
            await _userHelper.AddUserToRoleAsync(user, userType.ToString());
        }

        return user;
    }




}





