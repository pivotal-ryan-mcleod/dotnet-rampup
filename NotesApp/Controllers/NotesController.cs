using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;
using NotesApp.Repositories;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly NoteRepository _noteRepository;

        public NotesController(NoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // GET api/notes
        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return _noteRepository.GetNotes();
        }

        // GET api/notes/5
        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult Get(int id)
        {
            var note = _noteRepository.GetById(id);
            if (note == null)
            {
                return NotFound();
            }
            return new ObjectResult(note);
        }

        // POST api/notes
        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest();
            }

            _noteRepository.Add(note);

            return CreatedAtRoute("GetNote", new Note {Id = note.Id}, note);
        }

        // PUT api/notes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/notes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}