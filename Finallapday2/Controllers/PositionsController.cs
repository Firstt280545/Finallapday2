using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Database;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly DataDbContext _dbContext;

        public PositionsController(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get Method
        //- getPositions : แสดงข้อมูล Positions ทั้งหมด
        [HttpGet]
        public async Task<ActionResult<List<positions>>> getpositions()
        {
            var positions = await _dbContext.positions.ToListAsync();

            if (positions.Count == 0)
            {
                return NotFound();

            }

            return Ok(positions);
        }


        //- getPositionById : แสดงข้อมูล Positions ที่ตรงกับ Id ที่ระบุมา
        [HttpGet("{id}")]
        public async Task<ActionResult<positions>> getPositionById(int id)
        {
            var positions = await _dbContext.positions.FindAsync(id);

            if (positions == null)
            {
                return NotFound();
            }

            return Ok(positions);
        }

        //- getEmpByPositionId : แสดงรายชื่อพนักงานทุกคนที่อยู่ในตำแหน่งที่ระบุ
        [HttpGet("Positions ID")]
        public async Task<ActionResult<List<employees>>> getEmpPositionsID(string positionId)
        {
            var employees = _dbContext.employees.Where(e => e.positionId == positionId).ToList();

            if (employees.Count == 0)
            {
                return NotFound();
            }

            return Ok(employees);
        }



        // Post Method
        //-createPositions : เพิ่มข้อมูลตำแหน่งใหม่
        [HttpPost]
        public async Task<ActionResult<positions>> createPositions(positions positions)
        {
            try
            {
                _dbContext.positions.Add(positions); //Add positions to Database
                await _dbContext.SaveChangesAsync(); //Save
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok(positions);
        }

        // Put Method
        //- updatePositions : อัพเดทข้อมูลตำแหน่ง
        [HttpPut("id")]
        public async Task<ActionResult<positions>> updatePositions(string id, positions newpositions)
        {
            var positions = await _dbContext.positions.FindAsync(id);
            if (positions == null)
            {
                return NotFound();
            }

            positions.positionId = newpositions.positionId;
            positions.positionName = newpositions.positionName;
            positions.baseSalary = newpositions.baseSalary;
            positions.salaryIncreaseRate = newpositions.salaryIncreaseRate;

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        // Delete Method
        //- deletePositions : ลบข้อมูลตำแหน่งโดยห้ามลบถ้าหากยังมีพนักงานที่อยู่ในตำแหน่งนั้น
        [HttpDelete]
        public async Task<ActionResult<positions>> deletePositions(string id)
        {
            var employees = _dbContext.employees.Where(e => e.positionId == id).ToList();
            if (employees != null && employees.Count > 0)
            {
                return BadRequest("Delete location data, do not delete it if there is still an employee at that location.");
            }
            var position = _dbContext.positions.SingleOrDefault(p => p.positionId == id);
            if (position == null)
            {
                return NotFound();
            }
            _dbContext.positions.Remove(position);
            _dbContext.SaveChanges();
            return Ok();

        }
    }
}
