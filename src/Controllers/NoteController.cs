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
        private readonly IMapper _mapper;
        private readonly MyContext _context;

        public NoteController(ILogger<NoteController> logger, IMapper mapper, MyContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<int> PostyAsync([FromBody] NoteDtoWithoutId noteDto)
        {
            Note note = _mapper.Map<Note>(noteDto);
            _context.Note.Add(note);
            return await _context.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<List<NoteDto>> GetAll()
        {
            List<Note> notes = await _context.Note.ToListAsync();
            return _mapper.Map<List<NoteDto>>(notes);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<NoteDto> GetAsync(int id)
        {
            Note note = await _context.Note.FindAsync(id);
            return _mapper.Map<NoteDto>(note);
        }

        [HttpPut]
        public async Task<int> Putty([FromBody] NoteDto noteDto)
        {
            Note note = _mapper.Map<Note>(noteDto);
            _context.Note.Update(note);
            return await _context.SaveChangesAsync();
        }

        //[HttpPut]
        //public async Task<int> PuttyAlso([FromBody] NoteDto noteDto)
        //{
        //    Note noteEntity = await _context.Note.FirstAsync(note => note.Id == noteDto.Id);
        //    noteEntity.Text = noteDto.Text;
        //    return await _context.SaveChangesAsync();
        //}

        [HttpDelete]
        [Route("{id}")]
        public async Task<int> DelettyAsync(int id)
        {
            Note note = await _context.Note.FindAsync(id);
            _context.Note.Remove(note);
            return await _context.SaveChangesAsync();
        }
    }
}
