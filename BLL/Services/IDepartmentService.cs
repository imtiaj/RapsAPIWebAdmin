using BLL.Request;
using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        Task<Department> AddDepartmentAsync(DepartmentAddRequest request);
        Task<List<Department>> GetAllDepartmentAsync();
        Task<Department> GetADepartmentAsync(string deptCode);

        Task<bool> IsDepartmentCodeAlreadyExistAsync(string code);
        Task<bool> IsDepartmentNameAlreadyExistAsync(string name);
        Task<Department> DeleteDepartmentAsync(string code);
        Task<Department> UpdateDepartmentAsync(string code, DepartmentUpdateRequest aDepartment);
        // Task<Department> UpdateDepartmentAsync(string code, DepartmentUpdateRequest aDepartment);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<Department> AddDepartmentAsync(DepartmentAddRequest request)
        {
            Department department = new Department()
            {
                DepartmentName = request.Name,
                DepartmentCode = request.Code
            };

            await _departmentRepository.CreateAsync(department);
            
            if(await _departmentRepository.ApplySaveChangesAsync())
            {
                return department;
            }
            throw new RapsAppException("somthing went worng");
        }

        public async Task<Department> DeleteDepartmentAsync(string code)
        {
            var department = await _departmentRepository.GetAAsync(dept => dept.DepartmentCode == code);

            if(department == null)
            {
                throw new RapsAppException("Department not found");
            }
            _departmentRepository.DeleteAsync(department);

            if (await _departmentRepository.ApplySaveChangesAsync())
            {
                return department;
            }
            throw new RapsAppException("somthing went worng");
        }

        public async Task<Department> GetADepartmentAsync(string deptCode)
        {
            var department = await _departmentRepository.GetAAsync(dept => dept.DepartmentCode == deptCode);
            if (department == null)
            {
                throw new RapsAppException("no data found");
            }
            return department;
        }

        public async Task<List<Department>> GetAllDepartmentAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<bool> IsDepartmentCodeAlreadyExistAsync(string code)
        {
            var isDepartmentExist = await _departmentRepository.GetAAsync(dept => dept.DepartmentCode == code);

            if(isDepartmentExist != null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsDepartmentNameAlreadyExistAsync(string name)
        {
            var isDepartmentExist = await _departmentRepository.GetAAsync(dept => dept.DepartmentName == name);

            if (isDepartmentExist != null)
            {
                return false;
            }
            return true;
        }

        public async Task<Department> UpdateDepartmentAsync(string code, DepartmentUpdateRequest aDepartment)
        {
            var department = await _departmentRepository.GetAAsync(dept => dept.DepartmentCode == code);

            if (department == null)
            {
                throw new RapsAppException("Department not found");
            }

            if (!string.IsNullOrWhiteSpace(aDepartment.Code))
            {
                var isCodeExistInOtherDepartment = await _departmentRepository.GetAAsync(
                                                    dept => dept.DepartmentCode == aDepartment.Code 
                                                    && dept.DepartmentID != department.DepartmentID);
                if (isCodeExistInOtherDepartment == null)
                {
                    department.DepartmentCode = aDepartment.Code;
                }
                else
                {
                    throw new RapsAppException("code already exist in another department");
                }
            }

            if (!string.IsNullOrWhiteSpace(aDepartment.Name))
            {
                var isCodeExistInOtherDepartment = await _departmentRepository.GetAAsync(
                                                    dept => dept.DepartmentName == aDepartment.Name
                                                    && dept.DepartmentID != department.DepartmentID);
                if (isCodeExistInOtherDepartment == null)
                {
                    department.DepartmentName = aDepartment.Name;
                }
                else
                {
                    throw new RapsAppException("name already exist in another department");
                }
            }
            _departmentRepository.UpdateAsync(department);

            if (await _departmentRepository.ApplySaveChangesAsync())
            {
                return department;
            }
            throw new RapsAppException("somthing went worng");
        }
    }
}
