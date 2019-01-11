using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Messaging.Data;
using Messaging.Models;
using System.Net;
using System;

namespace Messaging.Controllers
{
    [Produces("application/json")]
    [Route("api/Messages")]
    public class MessagesController : Controller
    {
        private readonly MessagingContext _context;

        public MessagesController(MessagingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Create
        // POST: api/Messages
        [HttpPost]
        [Route("message")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> PostMessage([FromBody] Message message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMessage", new { id = message.Id }, message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Read
        // GET: api/Messages
        [HttpGet]
        [Route("message")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Message), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMessages()
        {
            try
            {
                var messages = await _context.Messages.ToListAsync();

                if (messages == null)
                    return NotFound();

                return Json(Ok(messages));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        // GET: api/Messages/5
        [HttpGet]
        [Route("message/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Message), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var message = await _context.Messages.SingleOrDefaultAsync(m => m.Id == id);

                if (message == null)
                    return NotFound();

                return Json(Ok(message));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion

        #region Update
        // PUT: api/Messages/5
        [HttpPut]
        [Route("message/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> PutMessage([FromRoute] int id, [FromBody] Message message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Message oldMessage = await _context.Messages.FirstOrDefaultAsync(n => n.Id == id);
                
                if (oldMessage == null)
                    return NotFound();

                if (id != message.Id)
                    return BadRequest();

                oldMessage.Content = message.Content;
                oldMessage.SeenByUserTo = false;
            
                _context.Entry(message).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete
        // DELETE: api/Messages/5
        [HttpDelete]
        [Route("message/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Message), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var message = await _context.Messages.SingleOrDefaultAsync(m => m.Id == id);
                if (message == null)
                    return NotFound();
            
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();

                return Json(Ok(message));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}