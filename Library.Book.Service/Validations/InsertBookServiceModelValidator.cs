using FluentValidation;
using Library.Book.Service.ServiceModels;
using Library.Services.Common.Resources;

namespace Library.Book.Service.Validations
{
    public class InsertBookServiceModelValidator : AbstractValidator<InsertBookServiceModel>
    {
        public InsertBookServiceModelValidator()
        {
            RuleFor(t => t.Name)
                .NotNull().WithMessage(ValidationResources.BookNameCannotBeNull);
            RuleFor(p => p.UserId)
                .GreaterThan(0);
        }
    }
}