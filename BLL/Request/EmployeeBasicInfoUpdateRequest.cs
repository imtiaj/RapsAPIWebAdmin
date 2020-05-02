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
    public class EmployeeBasicInfoUpdateRequest
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

    public class EmployeeBasicInfoUpdateRequestValidator : AbstractValidator<EmployeeBasicInfoUpdateRequest>
    {
        public EmployeeBasicInfoUpdateRequestValidator()
        {
            _ = RuleFor(emp => emp.FirstName).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.LastName).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.FathersName).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.MothersName).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.DateOfBirth).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.Gender).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.MaritalStatus).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.Nationality).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.NID).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.Religion).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.MobileNumber).NotEmpty().NotNull();
            _ = RuleFor(emp => emp.Email).NotEmpty().NotNull();
        }
    }
}
