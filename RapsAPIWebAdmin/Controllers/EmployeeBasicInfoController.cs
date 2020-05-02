using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using DLL.Models;
using DLL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RapsAPIWebAdmin.Controllers
{    
    public class EmployeeBasicInfoController : ApplicationBaseController
    {
        private readonly IEmployeeBasicInfoService _employeeBasicInfoService;

        public EmployeeBasicInfoController(IEmployeeBasicInfoService employeeBasicInfoService)
        {
            _employeeBasicInfoService = employeeBasicInfoService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEmployeeBasicProfile()
        {
            return Ok(await _employeeBasicInfoService.GetAllEmployeeBasicInfoAsync());
        }

        [HttpGet("{email}")]
        public async Task<ActionResult> GetAEmployeeBasicProfile(string email)
        {
            return Ok(await _employeeBasicInfoService.GetAEmployeeBasicInfoAsync(email));
        }

        [HttpPost]
        public async Task<ActionResult> InsertEmployeeBasicInfo(EmployeeBasicInfoRequest aEmployeeBasicInfos)
        {
            return Ok(await _employeeBasicInfoService.AddEmployeeBasicInfoAsync(aEmployeeBasicInfos));
        }

        [HttpPut("{email}")]
        public ActionResult UpdateEmployeeBasicProfile(string email, EmployeeBasicInfoUpdateRequest aEmployeeBasicInfos)
        {
            return Ok(_employeeBasicInfoService.UpdateEmployeeBasicProfile(email, aEmployeeBasicInfos));
        }

        [HttpDelete("{email}")]
        public ActionResult DeleteEmployeeBasicProfile(string email)
        {
            return Ok(_employeeBasicInfoService.DeleteEmployeeAsync(email));
        }
    }
}