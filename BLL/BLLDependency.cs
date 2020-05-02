using BLL.Request;
using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BLLDependency
    {
        public static void AllBllDependency(IServiceCollection services)
        {
            GetAllServices(services);
            GetAllRequestValidator(services);
        }

        private static void GetAllRequestValidator(IServiceCollection services)
        {
            services.AddTransient<IValidator<EmployeeBasicInfoRequest>, EmployeeBasicInfoRequestValidator>();
            services.AddTransient<IValidator<EmployeeBasicInfoUpdateRequest>, EmployeeBasicInfoUpdateRequestValidator>();
            
            services.AddTransient<IValidator<DepartmentAddRequest>, DepartmentAddRequestValidator>();
            services.AddTransient<IValidator<DepartmentUpdateRequest>, DepartmentUpdateRequestValidator>();

        }

        private static void GetAllServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeeBasicInfoService, EmployeeBasicInfoService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
        }
    }
}
