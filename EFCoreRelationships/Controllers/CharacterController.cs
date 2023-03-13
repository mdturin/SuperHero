using EFCoreRelationships.Builder;
using EFCoreRelationships.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly CharacterBuilder _characterBuilder;

        public CharacterController(
            ICharacterService characterService, CharacterBuilder characterBuilder)
        {
            _characterService = characterService;
            _characterBuilder = characterBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get()
        {
            var chareacters = await _characterService.Get();
            return Ok(chareacters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> Get(int id)
        {
            var character = await _characterService.Get(id);
            if (character == null)
            {
                return NotFound($"Character with id - {id} not found!");
            }

            return Ok(character);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Character>>> GetByUserId(int userId)
        {
            var characters = await _characterService.GetByUserId(userId);
            if (characters == null)
            {
                return NotFound($"Character don't have any user with user id {userId}!");
            }

            return Ok(characters);
        }

        [HttpPut("add")]
        public async Task<ActionResult<List<Character>>> AddCharacter(CharacterDto request)
        {
            var character = _characterBuilder.Build(request);
            if (character == null)
            {
                return BadRequest($"Can't create {request}!");
            }

            return Ok(await _characterService.Add(character));
        }

        [HttpPut("add-weapon")]
        public async Task<ActionResult<Character>> AddWeapon(WeaponDto request)
        {
            var character = await _characterService.Get(request.CharacterId);
            if (character == null)
                return NotFound($"Character with id {request.CharacterId} not found!");
            await _characterService.UpdateWeapon(character, request);
            return Ok($"Character with id {request.CharacterId} updated.");
        }
    }
}
