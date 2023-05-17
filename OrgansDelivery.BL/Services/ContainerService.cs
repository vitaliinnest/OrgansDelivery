using AutoMapper;
using FluentResults;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IContainerService
{
    Task<Result<ContainerDto>> CreateContainerAsync(ContainerFormValues model);
    Task<Result<ContainerDto>> UpdateContainerAsync(Guid containerId, ContainerFormValues model);
	Result DeleteContainer(Guid containerId);
}

public class ContainerService : IContainerService
{
    private readonly IMapper _mapper;
    private readonly IGenericValidator _genericValidator;
    private readonly AppDbContext _context;

    public ContainerService(
        IMapper mapper,
        IGenericValidator genericValidator,
        AppDbContext context)
    {
        _mapper = mapper;
        _genericValidator = genericValidator;
        _context = context;
    }

    public async Task<Result<ContainerDto>> CreateContainerAsync(ContainerFormValues model)
    {
        var validationResult = await ValidateContainerFormValuesAsync(model);
		if (validationResult.IsFailed)
		{
			return validationResult;
		}

		if (_context.Containers.Any(c => c.Name.ToLower() == model.Name.ToLower()))
		{
			return Result.Fail("Container with given name already exists");
		}

		var container = _mapper.Map<Container>(model);
        _context.Add(container);
        _context.SaveChanges();

        return _mapper.Map<ContainerDto>(container);
    }

    public async Task<Result<ContainerDto>> UpdateContainerAsync(Guid containerId, ContainerFormValues model)
    {
		var validationResult = await ValidateContainerFormValuesAsync(model);
		if (validationResult.IsFailed)
		{
			return validationResult;
		}

		if (_context.Containers.Any(c => c.Id != containerId && c.Name.ToLower() == model.Name.ToLower()))
		{
			return Result.Fail("Container with given name already exists");
		}

		var findResult = FindContainer(containerId);
		if (findResult.IsFailed)
		{
			return findResult.ToResult();
		}

		var updatedContainer = _mapper.Map(model, findResult.Value);

        _context.Update(updatedContainer);
        _context.SaveChanges();

        return _mapper.Map<ContainerDto>(updatedContainer);
    }

    public Result DeleteContainer(Guid containerId)
    {
		var findResult = FindContainer(containerId);
		if (findResult.IsFailed)
		{
			return findResult.ToResult();
		}

        if (findResult.Value.Organ != null)
        {
            return Result.Fail("Container contains an organ");
        }

        _context.Remove(findResult.Value);
        _context.SaveChanges();

        return Result.Ok();
    }

	private Result<Container> FindContainer(Guid containerId)
	{
		var container = _context.Containers.FirstOrDefault(c => c.Id == containerId);
		if (container == null)
		{
			return Result.Fail("Container not found");
		}

		return container;
	}

	private async Task<Result> ValidateContainerFormValuesAsync(ContainerFormValues model)
    {
		var validationResult = await _genericValidator.ValidateAsync(model);
		if (!validationResult.IsValid)
		{
			return Result.Fail(validationResult.ToString());
		}

        var device = _context.Devices.FirstOrDefault(d => d.Id == model.DeviceId);
        if (device == null)
        {
            return Result.Fail("Device not found");
        }

        if (device.Container != null)
        {
            return Result.Fail("Device is already used");
        }

        return Result.Ok();
	}
}
