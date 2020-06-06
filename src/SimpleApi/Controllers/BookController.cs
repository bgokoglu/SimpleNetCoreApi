using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using LazyCache;
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
            var books = await _mediator.Send(query).ConfigureAwait(false);
            return Ok(books);
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var query = new GetBookByIdQuery(id);
            var book = await _mediator.Send(query).ConfigureAwait(false);
            return book != null ? Ok(book) : (IActionResult)NotFound();
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateBookCommand command)
        {
            var book = await _mediator.Send(command).ConfigureAwait(false);

            if (book != null)
                return CreatedAtAction("Post", book);

            return BadRequest();
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
