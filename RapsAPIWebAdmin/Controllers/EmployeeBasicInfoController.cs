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
using Newtonsoft.Json;

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
        public async Task<ActionResult> UpdateEmployeeBasicProfile(string email, EmployeeBasicInfoUpdateRequest aEmployeeBasicInfos)
        {
            return Ok(await _employeeBasicInfoService.UpdateEmployeeBasicProfile(email, aEmployeeBasicInfos));
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult> DeleteEmployeeBasicProfile(string email)
        {
            return Ok(await _employeeBasicInfoService.DeleteEmployeeAsync(email));
        }
    }
}