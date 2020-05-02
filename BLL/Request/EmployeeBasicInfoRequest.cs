using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Request
{
    public class EmployeeBasicInfoRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string NID { get; set; }
        public string Religion { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
    }

    public class EmployeeBasicInfoRequestValidator : AbstractValidator<EmployeeBasicInfoRequest>
    {
        private readonly IServiceProvider _serviceProvider;

        public EmployeeBasicInfoRequestValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _ = RuleFor(emp => emp.FirstName).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.LastName).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.FathersName).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.MothersName).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.DateOfBirth).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.Gender).NotEmpty().NotNull().MaximumLength(6);
            _ = RuleFor(emp => emp.MaritalStatus).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.Nationality).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.NID).NotEmpty().NotNull().MustAsync(NidIsUnique).WithMessage("National ID must be unique");
            _ = RuleFor(emp => emp.Religion).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.MobileNumber).NotEmpty().NotNull().MaximumLength(14).MustAsync(MobileNumberIsUnique).WithMessage("Mobile Number must be unique");
            _ = RuleFor(emp => emp.Email).NotEmpty().NotNull().EmailAddress().MustAsync(EmailIsUnique).WithMessage("Email must be unique");
        }

        private async Task<bool> NidIsUnique(string nid, CancellationToken CancellationToken)
        {
            if (string.IsNullOrEmpty(nid))
            {
                return true;
            }
            var empInfo = _serviceProvider.GetRequiredService<IEmployeeBasicInfoService>();
            return await empInfo.IsNIDAlreadyExistAsync(nid);
        }

        private async Task<bool> EmailIsUnique(string email, CancellationToken CancellationToken)
        {
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }

            var empInfo = _serviceProvider.GetRequiredService<IEmployeeBasicInfoService>();
            return await empInfo.IsEmailAlreadyExistAsync(email);
        }

        private async Task<bool> MobileNumberIsUnique(string mobilenumber, CancellationToken CancellationToken)
        {
            if (string.IsNullOrEmpty(mobilenumber))
            {
                return true;
            }

            var empInfo = _serviceProvider.GetRequiredService<IEmployeeBasicInfoService>();
            return await empInfo.IsMobileNumberAlreadyExist(mobilenumber);
        }
    }
}
