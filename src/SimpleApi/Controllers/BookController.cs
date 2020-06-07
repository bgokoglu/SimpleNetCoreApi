using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Commands;
using SimpleAPI.Models;
using SimpleAPI.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var query = new GetAllBooksQuery();
            var response = await _mediator.Send(query).ConfigureAwait(false);
            return Ok(response.Result);
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var query = new GetBookByIdQuery(id);
            var response = await _mediator.Send(query).ConfigureAwait(false);

            if (response != null)
                return Ok(response.Result);

            return NoContent();
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateBookCommand command)
        {
            var response = await _mediator.Send(command).ConfigureAwait(false);

            if (response == null)
                return NoContent();

            if (response.Errors.Any())
                return BadRequest(new { error = response.Errors });

            return CreatedAtAction("Post", response.Result);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] BookModel model)
        {
            var response = await _mediator.Send(new UpdateBookCommand { Id = id, Title = model.Title }).ConfigureAwait(false);

            if (response != null)
                return AcceptedAtAction("Put", response.Result);

            return NoContent();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteBookCommand { Id = id }).ConfigureAwait(false);

            if (response != null)
                return AcceptedAtAction("Delete", response.Result);

            return NoContent();
        }
    }
}
