using AutoMapper;
using FluentResults;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.BL.Services;

public interface IConditionsHistoryService
{
    Task<Result<ContainerConditionsRecord>> AddConditionsRecordAsync(Guid containerId, CreateConditionsRecordModel model);
}

public class ConditionsHistoryService : IConditionsHistoryService
{
    private readonly IMapper _mapper;
    private readonly IGenericValidator _genericValidator;
    private readonly AppDbContext _context;

    public ConditionsHistoryService(
        IMapper mapper,
        IGenericValidator genericValidator,
        AppDbContext context)
    {
        _mapper = mapper;
        _genericValidator = genericValidator;
        _context = context;
    }

    public async Task<Result<ContainerConditionsRecord>> AddConditionsRecordAsync(
        Guid containerId, CreateConditionsRecordModel model)
    {
        var containerExists = _context.Containers.Any(c => c.Id == containerId);
        if (containerExists)
        {
            return Result.Fail("Container not found");
        }

        var validationResult = await _genericValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.ToString());
        }

        var record = _mapper.Map<ContainerConditionsRecord>(model);
        record.ContainerId = containerId;

        _context.Add(record);
        _context.SaveChanges();

        return record;
    }
}
