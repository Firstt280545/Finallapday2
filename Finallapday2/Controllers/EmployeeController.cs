using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Database;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class EmployeeController : ControllerBase
        {
            private readonly DataDbContext _dbContext;

            public EmployeeController(DataDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            //Get Method
            //-getEmployees : แสดงข้อมูล Employee ทุกคน
            [HttpGet]
            public async Task<ActionResult<List<employees>>> getEmployees()
            {
                var employees = await _dbContext.employees.ToListAsync();

                if (employees.Count == 0)
                {
                    return NotFound();

                }

                return Ok(employees);
            }
            //-getEmployeeById : แสดงข้อมูล Employee ที่ตรงกับ Id ที่ระบุมา      
            [HttpGet("{id}")]
            public async Task<ActionResult<employees>> getEmployeeById(string id)
            {
                var employees = await _dbContext.employees.FindAsync(id);

                if (employees == null)
                {
                    return NotFound();
                }

                return Ok(employees);
            }


        //-getEmployeeSalary : แสดงเงินเดือนปัจจุบันของ Employee ที่ระบุผ่าน Id
        [HttpGet("Current Salary")]
        public async Task<ActionResult<employees>> getEmployeeSalary(string id, int year)
        {

            var employee = _dbContext.employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            var position = _dbContext.positions.Find(employee.positionId);
            if (position == null)
            {
                return NotFound();
            }
            var salary = (position.baseSalary + (position.baseSalary * position.salaryIncreaseRate)) * (year - 1);

            return Ok(salary);
        }

        //-calEmpSalaryInYear : แสดงผลการคำนวนเงินเดือนของ Employee ที่ระบุในอนาคตอีก n ปีข้างหน้า
        [HttpGet("Future Salary")]
        public async Task<ActionResult<employees>> calEmpSalaryInYear(string id, int year)
        {
            var employee = _dbContext.employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            var position = _dbContext.positions.Find(employee.positionId);
            if (position == null)
            {
                return NotFound();
            }

            var currentYear = DateTime.Now.Year;
            var totalYear = year - 1;
            var salary = position.baseSalary;

            if (totalYear <= currentYear)
            {
                return BadRequest();
            }

            for (int i = currentYear + 1; i <= totalYear; i++)
            {
                salary = (float)(salary * 1.15);
            }

            return Ok(salary);
        }



        //Post Method
        //-createEmployees : เพิ่มข้อมูลพนักงานใหม่ โดยมีเงื่อนไขคือ positionId ของพนักงานจะต้องมีอยู่ใน Table Positions
        [HttpPost]
        public async Task<ActionResult<employees>> createEmployees(employees employees)
        {
            try
            {
                var position = _dbContext.positions.FirstOrDefault(p => p.positionId == employees.positionId);
                if (position == null)
                {
                    return BadRequest("Invalid position ID");
                }

                _dbContext.employees.Add(employees);

                _dbContext.SaveChanges();
            }

            catch (DbUpdateException)

            {
                return BadRequest();
            }
            return Ok(employees);
        }


        //Put Method
        //- updateEmployees : อัพเดทข้อมูลพนักงานโดยมีเงื่อนไขคือถ้ามีการอัพเดท positionId จะต้องเป็น position ที่มีอยู่ใน Table positions
        [HttpPut]
        public async Task<ActionResult<employees>> putEmployees(string id, employees newEmployees)
        {
            try
            {
                if (_dbContext.positions.FirstOrDefault(p => p.positionId == newEmployees.positionId) == null)
                {
                    return BadRequest("Invalid position ID");
                }
                var employees = await _dbContext.employees.FindAsync(id);
                if (employees == null)
                {
                    return NotFound();
                }
                employees.empName = newEmployees.empName;
                employees.Email = newEmployees.Email;
                employees.phoneNumber = newEmployees.phoneNumber;
                employees.hireDate = newEmployees.hireDate;
                employees.positionId = newEmployees.positionId;

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return Ok(newEmployees);
        }


        //Delete Method
        //- deleteEmployees : ลบข้อมูลพนักงาน
        [HttpDelete]
            public async Task<ActionResult<employees>> deleteEmployees(int id)
            {
                var employees = await _dbContext.employees.FindAsync(id);

                if (employees == null)
                {
                    return NotFound();
                }

                //Remove employees
                _dbContext.employees.Remove(employees);

                //save
                await _dbContext.SaveChangesAsync();

                return Ok(employees);
            }

        }
    }

