using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IEmployeeService
{
    List<EmployeeDto> GetEmployees();
    Task<Result> DeleteEmployeeAsync(Guid employeeId);
}

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public EmployeeService(
        AppDbContext appDbContext,
        UserManager<User> userManager,
        IMapper mapper)
    {
        _appDbContext = appDbContext;
        _userManager = userManager;
        _mapper = mapper;
    }

    public List<EmployeeDto> GetEmployees()
    {
        var users = _appDbContext.Users.ToList();
        return _mapper.Map<List<EmployeeDto>>(users);
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
