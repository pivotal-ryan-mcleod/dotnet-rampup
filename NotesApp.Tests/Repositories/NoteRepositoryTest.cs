using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using NotesApp.Models;
using NotesApp.Repositories;
using Xunit;

namespace NotesApp.Tests.Repositories
{
    public class NoteRepositoryTest : IDisposable
    {
        private readonly NoteRepository _repository;
        private NoteContext _noteContext;

        public NoteRepositoryTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<NoteContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _noteContext = new NoteContext(dbContextOptions);
            _repository = new NoteRepository(_noteContext);
        }

        public void Dispose()
        {
            _noteContext.Dispose();
        }
        
        [Fact]
        public void SavesNote()
        {
            var note = _repository.Add(new Note() {Body = "Note"});

            Assert.Equal("Note", note.Body);
        }

        [Fact]
        public void GetsListOfNotes()
        {
            _repository.Add(new Note() {Body = "Note A"});
            _repository.Add(new Note() {Body = "Note B"});

            var notes = _repository.GetNotes();

            Assert.Equal(3, notes.Count);
            Assert.Equal("Note 1", notes.ElementAt(0).Body);
            Assert.Equal("Note A", notes.ElementAt(1).Body);
            Assert.Equal("Note B", notes.ElementAt(2).Body);
        }

        [Fact]
        public void GetsNoteById()
        {
            var item = _repository.Add(new Note() {Body = "Note X"});

            var note = _repository.GetById(item.Id);

            Assert.Equal("Note X", note.Body);
        }
    }
}