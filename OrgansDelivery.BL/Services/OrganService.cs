using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IOrganService
{
	Result<List<OrganDto>> GetOrgans();
	Task<Result<OrganDto>> CreateOrganAsync(OrganFormValues model);
    Task<Result<OrganDto>> UpdateOrganAsync(Guid organId, OrganFormValues model);
    Result DeleteOrgan(Guid organId);
}

public class OrganService : IOrganService
{
    private readonly IMapper _mapper;
    private readonly IGenericValidator _genericValidator;
    private readonly AppDbContext _context;

    public OrganService(
        IMapper mapper,
        IGenericValidator genericValidator,
        AppDbContext context)
    {
        _mapper = mapper;
        _genericValidator = genericValidator;
        _context = context;
    }

    public Result<List<OrganDto>> GetOrgans()
    {
		var organs = _context.Organs
			.Include(o => o.Container)
			.Include(o => o.Conditions)
			.ToList();

        return _mapper.Map<List<OrganDto>>(organs);
	}

    public async Task<Result<OrganDto>> CreateOrganAsync(OrganFormValues model)
    {
        var validationResult = await ValidateOrganFormValuesAsync(model);
        if (validationResult.IsFailed)
        {
            return validationResult;
        }

		var container = _context.Containers.FirstOrDefault(c => c.Id == model.ContainerId);
		if (container.Organ != null)
		{
			return Result.Fail("Container already contains an organ");
		}

		if (_context.Organs.Any(p => p.Name.ToLower() == model.Name.ToLower()))
		{
			return Result.Fail("Organ with given name already exists");
		}

		var organ = _mapper.Map<Organ>(model);

        _context.Add(organ);
        _context.SaveChanges();
		var conditions = _context.Conditions.FirstOrDefault(c => c.Id == organ.ConditionsId);
		organ.Conditions = conditions;

        return _mapper.Map<OrganDto>(organ);
    }
    
    public async Task<Result<OrganDto>> UpdateOrganAsync(Guid organId, OrganFormValues model)
    {
		var findResult = FindOrgan(organId);
		if (findResult.IsFailed)
		{
			return findResult.ToResult();
		}

		if (_context.Organs.Any(o => o.Id != organId && o.Name.ToLower() == model.Name.ToLower()))
		{
			return Result.Fail("Organ with given name already exists");
		}

		var validationResult = await ValidateOrganFormValuesAsync(model);
		if (validationResult.IsFailed)
		{
			return validationResult;
		}

		var updatedOrgan = _mapper.Map(model, findResult.Value);

		_context.Update(updatedOrgan);
        _context.SaveChanges();

		var conditions = _context.Conditions.FirstOrDefault(c => c.Id == updatedOrgan.ConditionsId);
		updatedOrgan.Conditions = conditions;

		return _mapper.Map<OrganDto>(updatedOrgan);
    }

    public Result DeleteOrgan(Guid organId)
	{
		var findResult = FindOrgan(organId);
		if (findResult.IsFailed)
		{
			return findResult.ToResult();
		}

		_context.Remove(findResult.Value);
		_context.SaveChanges();

		return Result.Ok();
	}

	private Result<Organ> FindOrgan(Guid organId)
	{
		var organ = _context.Organs.FirstOrDefault(i => i.Id == organId);
		if (organ == null)
		{
			return Result.Fail("Organ not found");
		}

		return organ;
	}

	private async Task<Result> ValidateOrganFormValuesAsync(OrganFormValues model)
	{
		var validationResult = await _genericValidator.ValidateAsync(model);
		if (!validationResult.IsValid)
		{
			return Result.Fail(validationResult.ToString());
		}

		var conditionsExists = _context.Conditions.Any(c => c.Id == model.ConditionsId && !c.IsArchival);
		if (!conditionsExists)
		{
			return Result.Fail("Conditions don't exist");
		}

		var container = _context.Containers.FirstOrDefault(c => c.Id == model.ContainerId);
		if (container == null)
		{
			return Result.Fail("Container not found");
		}

		return Result.Ok();
	}
}
