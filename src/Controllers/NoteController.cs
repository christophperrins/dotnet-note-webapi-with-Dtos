using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using src.Data;
using src.Model.Dto;
using src.Model.Entity;

namespace src.Controllers
{
    [ApiController]
    [Route("/note")]
    public class NoteController : ControllerBase
    {

        private readonly ILogger<NoteController> _logger;
        private readonly MyContext _context;

        public NoteController(ILogger<NoteController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<List<Note>> GetAll()
        {
            return await _context.Note.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Note> GetAsync(int id)
        {
            return await _context.Note.FindAsync(id);
            
        }

        [HttpPost]
        public async Note posty([FromBody] Note note)
        {
            _context.Note.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        [HttpPut]
        public async Task<int> Putty([FromBody] NoteDto noteDto)
        {
            Note note = _mapper.Map<Note>(noteDto);
            _context.Note.Update(note);
            return await _context.SaveChangesAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<int> delettyAsync(int id)
        {
            Note note = await _context.Note.FindAsync(id);
            _context.Note.Remove(note);
            return await _context.SaveChangesAsync();
        }
    }
}
