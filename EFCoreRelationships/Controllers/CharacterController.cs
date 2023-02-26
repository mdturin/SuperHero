using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationships.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController( ICharacterService characterService )
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get()
        {
            var chareacters = await _characterService.Get();
            return Ok( chareacters );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> Get(int id )
        {
            var character = await _characterService.Get( id );
            if(character == null )
            {
                return NotFound($"Character with id - {id} not found!");
            }

            return Ok( character );
        }

        [HttpGet( "user/{userId}" )]
        public async Task<ActionResult<List<Character>>> GetByUserId(int userId )
        {
            var characters = await _characterService.GetByUserId( userId );
            if(characters == null)
            {
                return NotFound( $"Character don't have any user with user id {userId}!" );
            }

            return Ok( characters );
        }
    }
}
