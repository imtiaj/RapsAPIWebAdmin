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
    public interface IEmployeeBasicInfoService
    {
        Task<List<EmployeeBasicInformation>> GetAllEmployeeBasicInfoAsync();
        Task<EmployeeBasicInformation> GetAEmployeeBasicInfoAsync(string email);
        Task<EmployeeBasicInformation> AddEmployeeBasicInfoAsync(EmployeeBasicInfoRequest request);
        Task<EmployeeBasicInformation> UpdateEmployeeBasicProfile(string email, EmployeeBasicInfoUpdateRequest request);
        Task<EmployeeBasicInformation> DeleteEmployeeAsync(string email);

        Task<bool> IsNIDAlreadyExistAsync(string nid);
        Task<bool> IsEmailAlreadyExistAsync(string email);
        Task<bool> IsMobileNumberAlreadyExist(string mobilenumber);
    }

    public class EmployeeBasicInfoService : IEmployeeBasicInfoService
    {
        private readonly IEmpBasicInfoRepository _empBasicInfoRepository;

        public EmployeeBasicInfoService(IEmpBasicInfoRepository empBasicInfoRepository)
        {
            _empBasicInfoRepository = empBasicInfoRepository;
        }
        
        public async Task<bool> IsEmailAlreadyExistAsync(string email)
        {
            var empInfo = await _empBasicInfoRepository.GetAAsync(emp => emp.Email == email);
            if (empInfo != null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsMobileNumberAlreadyExist(string mobilenumber)
        {
            var empInfo = await _empBasicInfoRepository.GetAAsync(emp => emp.MobileNumber == mobilenumber);
            if (empInfo != null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsNIDAlreadyExistAsync(string nid)
        {
            var empInfo = await _empBasicInfoRepository.GetAAsync(emp => emp.NID == nid);
            if (empInfo != null)
            {
                return false;
            }
            return true;
        }
        

        public async Task<EmployeeBasicInformation> GetAEmployeeBasicInfoAsync(string email)
        {
            var aEmpInfo = await _empBasicInfoRepository.GetAAsync(emp => emp.Email == email);

            if (aEmpInfo == null)
            {
                throw new RapsAppException("No data found");
            }
            return aEmpInfo;
        }

        public async Task<List<EmployeeBasicInformation>> GetAllEmployeeBasicInfoAsync()
        {
            return await _empBasicInfoRepository.GetAllAsync();
        }

        public async Task<EmployeeBasicInformation> AddEmployeeBasicInfoAsync(EmployeeBasicInfoRequest request)
        {
            EmployeeBasicInformation employeeBasicInformation = new EmployeeBasicInformation()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                FathersName = request.FathersName,
                MothersName = request.MothersName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                MaritalStatus = request.MaritalStatus,
                NID = request.NID,
                Nationality = request.Nationality,
                Religion = request.Religion,
                MobileNumber = request.MobileNumber,
                Email = request.Email
            };
            await _empBasicInfoRepository.CreateAsync(employeeBasicInformation);

            if (await _empBasicInfoRepository.ApplySaveChangesAsync())
            {
                return employeeBasicInformation;
            }
            throw new RapsAppException("somthing went worng");
        }


        public async Task<EmployeeBasicInformation> UpdateEmployeeBasicProfile(string email, EmployeeBasicInfoUpdateRequest request)
        {
            var aEmpInfo = await _empBasicInfoRepository.GetAAsync(emp => emp.Email == email);

            if (aEmpInfo == null)
            {
                throw new RapsAppException("Employee not found");
            }

            aEmpInfo.FirstName = request.FirstName;
            aEmpInfo.LastName = request.LastName;
            aEmpInfo.FathersName = request.FathersName;
            aEmpInfo.MothersName = request.MothersName;
            aEmpInfo.DateOfBirth = request.DateOfBirth;
            aEmpInfo.Nationality = request.Nationality;
            aEmpInfo.MaritalStatus = request.MaritalStatus;
            aEmpInfo.Religion = request.Religion;
            aEmpInfo.Gender = request.Gender;
            aEmpInfo.Email = request.Email;

            //if (!string.IsNullOrWhiteSpace(request.Email))
            //{
            //    var isEmailExistInOtherEmpInfo = await _empBasicInfoRepository.GetAAsync(
            //                                        emp => emp.Email == request.Email
            //                                        && emp.EmployeeID != aEmpInfo.EmployeeID);
            //    if (isEmailExistInOtherEmpInfo == null)
            //    {
            //        aEmpInfo.Email = request.Email;
            //    }
            //    else
            //    {
            //        throw new RapsAppException("Email already exist in another employees info.");
            //    }
            //}

            if (!string.IsNullOrWhiteSpace(request.MobileNumber))
            {
                var isMobileNumberExistInOtherEmpInfo = await _empBasicInfoRepository.GetAAsync(
                                                    emp => emp.MobileNumber == request.MobileNumber
                                                    && emp.EmployeeID != aEmpInfo.EmployeeID);
                if (isMobileNumberExistInOtherEmpInfo == null)
                {
                    aEmpInfo.MobileNumber = request.MobileNumber;
                }
                else
                {
                    throw new RapsAppException("Mobile number already exist in another employees info.");
                }
            }

            if (!string.IsNullOrWhiteSpace(request.NID))
            {
                var isNIDExistInOtherEmpInfo = await _empBasicInfoRepository.GetAAsync(
                                                    emp => emp.NID == request.NID
                                                    && emp.EmployeeID != aEmpInfo.EmployeeID);
                if (isNIDExistInOtherEmpInfo == null)
                {
                    aEmpInfo.NID = request.NID;
                }
                else
                {
                    throw new RapsAppException("National ID already exist in another employees info.");
                }
            }

            _empBasicInfoRepository.UpdateAsync(aEmpInfo);
            // var a = await _empBasicInfoRepository.testUpdate(aEmpInfo);

            if (await _empBasicInfoRepository.ApplySaveChangesAsync())
            {
                return aEmpInfo;
            }
            throw new RapsAppException("somthing went worng");
        }

        public async Task<EmployeeBasicInformation> DeleteEmployeeAsync(string email)
        {
            var empInfo = await _empBasicInfoRepository.GetAAsync(emp => emp.Email == email);

            if (empInfo == null)
            {
                throw new RapsAppException("Employee not found");
            }
            _empBasicInfoRepository.DeleteAsync(empInfo);

            if (await _empBasicInfoRepository.ApplySaveChangesAsync())
            {
                return empInfo;
            }
            throw new RapsAppException("somthing went worng");
        }
    }
}
