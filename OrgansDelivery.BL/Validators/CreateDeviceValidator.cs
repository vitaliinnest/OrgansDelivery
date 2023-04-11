using FluentValidation;
using OrganStorage.BL.Consts;
using OrganStorage.DAL.Entities;
using static OrganStorage.BL.Consts.ValidatorConsts;

namespace OrganStorage.BL.Validators;

public class CreateDeviceValidator : AbstractValidator<AddDeviceModel>
{
    public CreateDeviceValidator()
	{
		RuleFor(d => d.Id)
			.NotEmpty();

		RuleFor(t => t.Name)
			.NotNull()
			.NotEmpty()
			.Length(GeneralConsts.MIN_LENGTH,
					GeneralConsts.MAX_LENGTH);

		RuleFor(t => t.Description)
			.MaximumLength(GeneralConsts.MAX_LENGTH);

		RuleFor(c => c.ConditionsIntervalCheckInMs)
			.InclusiveBetween(
				ConditionConsts.ConditionsIntervalCheckInMs.MIN,
				ConditionConsts.ConditionsIntervalCheckInMs.MAX);

		RuleFor(d => d.ContainerId)
			.NotEmpty();
	}
}
