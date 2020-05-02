using DLL.DbContext;
using DLL.Models;
using DLL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public interface IEmpBasicInfoRepository : IRepositoryBase<EmployeeBasicInformation>
    {
        Task<bool> testUpdate(EmployeeBasicInformation model);
    }

    public class EmpBasicInfoRepository : RepositoryBase<EmployeeBasicInformation>, IEmpBasicInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public EmpBasicInfoRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<bool> testUpdate(EmployeeBasicInformation model)
        {
            var a = _context.EmployeeBasicInformations.Update(model);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
