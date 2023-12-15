using Microsoft.AspNetCore.Mvc;
using NoteTakingApp;
using System.Collections.Generic;
using System.Linq;
using WebService;

[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly NoteContext _context;

    public NotesController(NoteContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Note>> Get()
    {
        return _context.Notes.ToList();
    }

    [HttpGet("{id}", Name = "GetNote")]
    public ActionResult<Note> Get(int id)
    {
        var note = _context.Notes.Find(id);

        if (note == null)
        {
            return NotFound();
        }

        return note;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Note note)
    {
        _context.Notes.Add(note);
        _context.SaveChanges();

        return CreatedAtRoute("GetNote", new { id = note.Number }, note);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Note note)
    {
        var existingNote = _context.Notes.Find(id);

        if (existingNote == null)
        {
            return NotFound();
        }

        existingNote.Author = note.Author;
        existingNote.Title = note.Title;
        existingNote.Content = note.Content;
        existingNote.Privacy = note.Privacy;

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var note = _context.Notes.Find(id);

        if (note == null)
        {
            return NotFound();
        }

        _context.Notes.Remove(note);
        _context.SaveChanges();

        return NoContent();
    }
}
