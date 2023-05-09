using AutoMapper;
using FluentResults;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IOrganService
{
	Result<List<OrganDto>> GetOrgans();
	Task<Result<OrganDto>> CreateOrganAsync(CreateOrganModel model);
    Task<Result<OrganDto>> UpdateOrganAsync(Guid organId, UpdateOrganModel model);
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
		var organs = _context.Organs.ToList();
        return _mapper.Map<List<OrganDto>>(organs);
	}

    public async Task<Result<OrganDto>> CreateOrganAsync(CreateOrganModel model)
    {
        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var organ = _mapper.Map<Organ>(model);
        if (_context.Organs.Any(p => p.Name.ToLower() == organ.Name.ToLower()))
        {
            return Result.Fail("Organ with given name already exists");
        }

        _context.Add(organ);
        _context.SaveChanges();

        return _mapper.Map<OrganDto>(organ);
    }
    
    public async Task<Result<OrganDto>> UpdateOrganAsync(Guid organId, UpdateOrganModel model)
    {
        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var organ = _context.Organs.FirstOrDefault(o => o.Id == organId);
        if (organ == null)
        {
            return Result.Fail("Organ not found");
        }

        var updated = _mapper.Map(model, organ);

        _context.Update(updated);
        _context.SaveChanges();
        
        return _mapper.Map<OrganDto>(updated);
    }

    public Result DeleteOrgan(Guid organId)
    {
        var organ = _context.Organs.FirstOrDefault(i => i.Id == organId);
        if (organ == null)
        {
            return Result.Fail("Organ not found");
        }

        if (organ.ContainerId.HasValue)
        {
            return Result.Fail("Organ is in container. Get it out of the container first");
        }

        _context.Remove(organ);
        _context.SaveChanges();

        return Result.Ok();
    }
}
