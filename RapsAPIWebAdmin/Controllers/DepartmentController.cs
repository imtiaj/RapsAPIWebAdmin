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
    public class DepartmentController : ApplicationBaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDepartments()
        {
            return Ok(await _departmentService.GetAllDepartmentAsync());
        }

        [HttpGet("{deptCode}")]
        public async Task<ActionResult> GetADepartment(string deptCode)
        {
            return Ok(await _departmentService.GetADepartmentAsync(deptCode));
        }

        [HttpPost]
        public async Task<ActionResult> InsertDepartment(DepartmentAddRequest aDepartment)
        {
            return Ok(await _departmentService.AddDepartmentAsync(aDepartment));
        }

        [HttpPut("{code}")]
        public async Task<ActionResult> UpdateEmployeeBasicProfile(string code, DepartmentUpdateRequest aDepartment)
        {
            return Ok(await _departmentService.UpdateDepartmentAsync(code, aDepartment));
        }

        [HttpDelete("{code}")]
        public async Task<ActionResult> DeleteEmployeeBasicProfile(string code)
        {
            return Ok(await _departmentService.DeleteDepartmentAsync(code));
        }
    }
}