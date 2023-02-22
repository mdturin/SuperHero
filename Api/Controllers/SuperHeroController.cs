using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok( await _context.SuperHeroes.ToListAsync() );
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync( id );
            return (hero == null) ? NotFound("Hero not found!") : Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add( hero );

            await _context.SaveChangesAsync();

            return Accepted( await _context.SuperHeroes.ToListAsync() );
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero( SuperHero request )
        {
            var hero = await _context.SuperHeroes.FindAsync( request.Id );
            if(hero != null)
            {
                hero.FirstName = request.FirstName;
                hero.LastName = request.LastName;
                hero.Name = request.Name;
                hero.Place = request.Place;
                
                await _context.SaveChangesAsync();

                return Accepted( await _context.SuperHeroes.ToListAsync() );
            }

            return NotFound( "Hero not found!" );
        }

        [HttpDelete( "{id:int}" )]
        public async Task<ActionResult<List<SuperHero>>> Delete( int id )
        {
            var hero = await _context.SuperHeroes.FindAsync( id );
            if(hero == null)
                return NotFound( "Hero not found!" );
            
            _context.SuperHeroes.Remove( hero );
            
            await _context.SaveChangesAsync();

            return Ok( await _context.SuperHeroes.ToListAsync() );
        }
    }
}
