using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using elective.Entities;
using elective.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Mysqlx.Crud;

namespace elective.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ElectiveDbContext DBContext;

            /*GET USERSSS*/
        public UserController(ElectiveDbContext dbContext)
        {
            this.DBContext = dbContext;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var List = await DBContext.Users.Select(
                s => new UserDTO
                {
                    Id = s.Id,
                    FirstName = s.Firstname,
                    LastName = s.Lastname,
                    Username = s.Username,
                    Password = s.Password,
                    EnrollmentDate = s.Enrollmentdate,
                    Queue_number = s.Queue_number
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }
        /*Obtain the data of a specific user according to their Id.*/
        [HttpGet("GetUserById")]
        public async Task<ActionResult<UserDTO>> GetUserById(int Id)
        {
            UserDTO User = await DBContext.Users.Select(s => new UserDTO
            {
                Id = s.Id,
                FirstName = s.Firstname,
                LastName = s.Lastname,
                Username = s.Username,
                Password = s.Password,
                EnrollmentDate = s.Enrollmentdate,
                Queue_number = s.Queue_number
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return User;
            }
        }
        /*Insert NEW USER*/
        [HttpPost("InsertUser")]
        public async Task<HttpStatusCode> InsertUser(UserDTO User)
        {
            var entity = new User()
            {
                Firstname = User.FirstName,
                Lastname = User.LastName,
                Username = User.Username,
                Password = User.Password,
                Enrollmentdate = User.EnrollmentDate,
                Queue_number = User.Queue_number
            };
            DBContext.Users.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }
        /*Update USER*/
        [HttpPut("UpdateUser")]
        public async Task<HttpStatusCode> UpdateUser(UserDTO User)
        {
            var entity = await DBContext.Users.FirstOrDefaultAsync(s => s.Id == User.Id);
            entity.Firstname = User.FirstName;
            entity.Lastname = User.LastName;
            entity.Username = User.Username;
            entity.Password = User.Password;
            entity.Enrollmentdate = User.EnrollmentDate;
            entity.Queue_number = User.Queue_number;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        /*DELETE USER BY ID*/
        [HttpDelete("DeleteUser/{Id}")]
        public async Task<HttpStatusCode> DeleteUser(int Id)
        {
            var entity = new User()
            {
                Id = Id
            };
            DBContext.Users.Attach(entity);
            DBContext.Users.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
