using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using movie.Data;
using movie.Data.Entities;
using movie.Helpers;
using movie.ViewModels;

namespace movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieDbContext _db;

        public MovieController(MovieDbContext db)
        {
            this._db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var data = await _db.Movies.ToListAsync();

            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> PostMovie([FromBody] Movie request)
        {

            _db.Movies.Add(request);

            var result = await _db.SaveChangesAsync();

            if (result > 0)
                return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
            else           
                return BadRequest(new ApiBadRequestResponse("Create movie failed"));
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var movie = await _db.Movies.FindAsync(id);

            if (movie == null)
                return NotFound(new ApiNotFoundResponse($"movie with id: {id} is not found"));

            return Ok(movie);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(Guid id, [FromBody] Movie request)
        {
            var movie = await _db.Movies.FindAsync(id);

            if (movie == null)
                return NotFound(new ApiNotFoundResponse($"movie with id: {id} is not found"));

           
            _db.Movies.Update(request);

            var result = await _db.SaveChangesAsync();

            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest(new ApiBadRequestResponse("Update movie failed"));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetMoviePaging(string filter, int pageIndex, int pageSize)
        {
            var query = _db.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter)
                || x.Name.Contains(filter));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var pagination = new Pagination<Movie>
            {
                Items = items,
                TotalRecords = totalRecords,
            };

            return Ok(pagination);
        }
    }
}
