using BookCatalogApi.Models;
using BookCatalogApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookRepository repository, ILogger<BookController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            var book = _repository.GetById(id);
            if (book == null) return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> Add([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Add(book);
            _logger.LogInformation("Book added: " + System.Text.Json.JsonSerializer.Serialize(book));
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updatedBook)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_repository.Exists(id))
            {
                _logger.LogWarning("Attempted to update non-existing book with id: {Id}", id);
                return NotFound();
            }

            updatedBook.Id = id;
            _repository.Update(updatedBook);
            _logger.LogInformation("Book updated: " + System.Text.Json.JsonSerializer.Serialize(updatedBook));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_repository.Exists(id))
            {
                _logger.LogWarning("Attempted to delete non-existing book with id: {Id}", id);
                return NotFound();
            }

            _repository.Delete(id);
            _logger.LogInformation("Book deleted with id: {Id}", id);
            return NoContent();
        }
    }
}
