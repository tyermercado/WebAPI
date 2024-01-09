using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonsContext _personContext;
        public PersonController(PersonsContext personContext)
        {
            _personContext = personContext;
        }

        public PersonsContext Get_personContext()
        {
            return _personContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persons>>> GetPerson()
        {
            if(_personContext.Persons == null)
            {
                return NotFound();
            }
            return await _personContext.Persons.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Persons>> GetPerson(int id)
        {
            if (_personContext.Persons == null)
            {
                return NotFound();
            }
            var person = await _personContext.Persons.FindAsync(id); 
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        [HttpPost]
        public async Task<ActionResult<Persons>> PostPerson(Persons persons)
        {
            _personContext.Persons.Add(persons);
            await _personContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPerson), new { id = persons.ID }, persons);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutPersons(int id, Persons persons)
        {
            if(id != persons.ID)
            {
                return BadRequest();
            }

            _personContext.Entry(persons).State = EntityState.Modified;
            try
            {
                await _personContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePersons(int id)
        {

            if(_personContext.Persons == null)
            {
                return NotFound();
            }
            var person = await _personContext.Persons.FindAsync(id);
            if(person == null)
            {
                return NotFound();
            }
            _personContext.Persons.Remove(person);
            await _personContext.SaveChangesAsync();

            return Ok();
        }
    }
}
