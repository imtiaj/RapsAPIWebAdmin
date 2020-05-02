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
    public class DepartmentAddRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class DepartmentAddRequestValidator : AbstractValidator<DepartmentAddRequest>
    {
        private readonly IServiceProvider _serviceProvider;

        public DepartmentAddRequestValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var deparment = _serviceProvider.GetRequiredService<IDepartmentService>();

            _ = RuleFor(d => d.Name).NotNull().NotEmpty().MustAsync(NameIsUnique).WithMessage("department name must be unique");
            _ = RuleFor(d => d.Code).NotNull().NotEmpty().MustAsync(CodeIsUnique).WithMessage("department code must be unique");            
        }


        private async Task<bool> NameIsUnique(string name, CancellationToken CancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return true;
            }

            var deparment = _serviceProvider.GetRequiredService<IDepartmentService>();

            return await deparment.IsDepartmentNameAlreadyExistAsync(name);
        }

        private async Task<bool> CodeIsUnique(string code, CancellationToken CancellationToken)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return true;
            }

            var department = _serviceProvider.GetRequiredService<IDepartmentService>();

            return await department.IsDepartmentCodeAlreadyExistAsync(code);
        }
    }
}
