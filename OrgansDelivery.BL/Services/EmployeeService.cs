using OrgansDelivery.DAL.Data;
using OrgansDelivery.DAL.Entities;

namespace OrgansDelivery.BL.Services;

public interface IEmployeeService
{
    List<User> GetEmployees();
}

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _appDbContext;

    public EmployeeService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public List<User> GetEmployees()
    {
        return _appDbContext.Users.ToList();
    }
}
