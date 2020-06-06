using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Commands;
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
            var books = await _mediator.Send(query).ConfigureAwait(false);
            return Ok(books);
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var query = new GetBookByIdQuery(id);
            var book = await _mediator.Send(query).ConfigureAwait(false);

            if (book != null)
                return Ok(book);

            return (IActionResult)NoContent();
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateBookCommand command)
        {
            var book = await _mediator.Send(command).ConfigureAwait(false);

            if (book != null)
                return CreatedAtAction("Post", book);

            return NoContent();
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateBookCommand command)
        {
            command.Id = id;
            var book = await _mediator.Send(command).ConfigureAwait(false);

            if (book != null)
                return AcceptedAtAction("Put", book);

            return NoContent();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var book = await _mediator.Send(new DeleteBookCommand { Id = id }).ConfigureAwait(false);

            if (book != null)
                return AcceptedAtAction("Delete", book);

            return NoContent();
        }
    }
}
