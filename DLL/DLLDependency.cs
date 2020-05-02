using DLL.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL
{
    public class DLLDependency
    {
        public static void AllDllDependency(IServiceCollection services)
        {
            services.AddTransient<IEmpBasicInfoRepository, EmpBasicInfoRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
        }
    }
}
