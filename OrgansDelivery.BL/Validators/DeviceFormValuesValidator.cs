using FluentValidation;
using OrganStorage.BL.Consts;
using OrganStorage.DAL.Entities;
using static OrganStorage.BL.Consts.ValidatorConsts;

namespace OrganStorage.BL.Validators;

public class DeviceFormValuesValidator : AbstractValidator<DeviceFormValues>
{
    public DeviceFormValuesValidator()
	{
		RuleFor(d => d.Id)
			.NotEmpty();

		RuleFor(t => t.Name)
			.NotNull()
			.NotEmpty()
			.Length(GeneralConsts.MIN_LENGTH,
					GeneralConsts.MAX_LENGTH);

		RuleFor(c => c.ConditionsIntervalCheckInMs)
			.InclusiveBetween(
				ConditionConsts.ConditionsIntervalCheckInMs.MIN,
				ConditionConsts.ConditionsIntervalCheckInMs.MAX);
	}
}
