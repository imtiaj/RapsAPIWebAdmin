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
    public class DepartmentUpdateRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class DepartmentUpdateRequestValidator : AbstractValidator<DepartmentUpdateRequest>
    {        
        public DepartmentUpdateRequestValidator()
        {           
            _ = RuleFor(d => d.Name).NotNull().NotEmpty();
            _ = RuleFor(d => d.Code).NotNull().NotEmpty();            
        }
    }
}
