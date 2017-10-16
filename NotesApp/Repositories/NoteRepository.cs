using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NotesApp.Models;

namespace NotesApp.Repositories
{
    public class NoteRepository
    {
        private readonly NoteContext _context;
        
        public NoteRepository(NoteContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();

            if (!_context.NotesSet.Any())
            {
                _context.NotesSet.Add(new Note() {Body = "Note 1"});
                _context.SaveChanges();
            }
        }

        public Note Add(Note note)
        {
            var entityEntry = _context.Add(note);
            _context.SaveChanges();
            return entityEntry.Entity;
        }

        public List<Note> GetNotes()
        {
            return _context.NotesSet.ToList();
        }

        public Note GetById(long id)
        {
            return _context.NotesSet.FirstOrDefault(t => t.Id == id);
        }
    }
}