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
    public class QueueController : ControllerBase
    {
        private readonly QueueDbContext DBContext;

        /*GET USERSSS*/
        public QueueController(QueueDbContext dbContext)
        {
            this.DBContext = dbContext;
        }

        [HttpGet("GetQueue")]
        public async Task<ActionResult<List<QueueDTO>>> Get()
        {
            var List = await DBContext.Queues.Select(
                s => new QueueDTO
                {
                    Idqueue = s.Idqueue,
                    Queue_number = s.Queue_number,
                    Student_id = s.Student_id
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
        [HttpGet("GetQueueById")]
        public async Task<ActionResult<QueueDTO>> GetQueueById(int Idqueue)
        {
            QueueDTO Queue = await DBContext.Queues.Select(s => new QueueDTO
            {
                Idqueue = s.Idqueue,
                Queue_number = s.Queue_number,
                Student_id = s.Student_id
            }).FirstOrDefaultAsync(s => s.Idqueue == Idqueue);
            if (Queue == null)
            {
                return NotFound();
            }
            else
            {
                return Queue;
            }
        }
        /*Insert NEW USER*/
        [HttpPost("InsertQueue")]
        public async Task<HttpStatusCode> InsertQueue(QueueDTO Queue)
        {
            var entity = new Queue()
            {
                Idqueue = Queue.Idqueue,
                Queue_number = Queue.Queue_number ?? 0,
                Student_id = Queue.Student_id
            };

            DBContext.Queues.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }
        /*Update USER*/
        [HttpPut("UpdateQueue")]
        public async Task<ActionResult> UpdateQueue(QueueDTO Queues_update)
        {
            var entity = await DBContext.Queues.FirstOrDefaultAsync(s => s.Idqueue == Queues_update.Idqueue);
            entity.Queue_number = (int)Queues_update.Queue_number;
            entity.Student_id = Queues_update.Student_id;
            await DBContext.SaveChangesAsync();
            return Ok(entity);
        }
        /*DELETE USER BY ID*/
        [HttpDelete("DeleteQueue/{Idqueue}")]
        public async Task<HttpStatusCode> DeleteQueue(int Idqueue)
        {
            var entity = new Queue()
            {
                Idqueue = Idqueue
            };
            DBContext.Queues.Attach(entity);
            DBContext.Queues.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
