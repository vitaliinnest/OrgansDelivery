using FluentResults;
using Microsoft.AspNetCore.Identity;
using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Services;

public interface IEmployeeService
{
    List<User> GetEmployees();
    Task<Result> DeleteEmployeeAsync(Guid employeeId);
}

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<User> _userManager;

    public EmployeeService(
        AppDbContext appDbContext,
        UserManager<User> userManager)
    {
        _appDbContext = appDbContext;
        _userManager = userManager;
    }

    public List<User> GetEmployees()
    {
        return _appDbContext.Users.ToList();
    }

    public async Task<Result> DeleteEmployeeAsync(Guid employeeId)
    {
        var user = await _userManager.FindByIdAsync(employeeId.ToString());
        if (user == null)
        {
            return Result.Fail("User not found");
        }
        await _userManager.DeleteAsync(user);
        return Result.Ok();
    }
}
