using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleApp.Brokers.Storages;
using SimpleApp.Models.Users;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleApp.Controllers
{
    [ApiController]
    [Route("[controller]/api")]
    public class StudentsController:Controller
    {
        private readonly StorageBroker broker;

        public StudentsController(StorageBroker broker)
        {
            this.broker = broker;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var students = await broker.Students.Include(s => s.User).ToListAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var role = User.FindFirst(ClaimTypes.Role)!.Value;

            var student = await broker.Students.FindAsync(id);
            if (student == null) return NotFound();

            if (role == "Student" && student.UserId != userId)
                return Forbid();

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var role = User.FindFirst(ClaimTypes.Role)!.Value;

            if (role == "Student")
                student.UserId = userId; 

            broker.Students.Add(student);
            await broker.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student updated)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var role = User.FindFirst(ClaimTypes.Role)!.Value;

            var student = await broker.Students.FindAsync(id);
            if (student == null) return NotFound();

            if (role == "Student" && student.UserId != userId)
                return Forbid();

            student.FullName = updated.FullName;
            student.BirthDate = updated.BirthDate;

            await broker.SaveChangesAsync();
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await broker.Students.FindAsync(id);
            if (student == null) return NotFound();

            broker.Students.Remove(student);
            await broker.SaveChangesAsync();
            return Ok(new { message = "O‘chirildi" });
        }
    }
}

