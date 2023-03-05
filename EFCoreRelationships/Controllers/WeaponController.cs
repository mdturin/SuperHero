using EFCoreRelationships.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationships.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController( IWeaponService weaponService )
        {
            _weaponService = weaponService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Weapon>>> Get()
        {
            return Ok( await _weaponService.Get() );
        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult<Weapon>> Get(int id)
        {
            var weapon = await _weaponService.Get( id );
            if(weapon == null)
                return BadRequest( $"Weapon with {id} not found!" );
            return Ok( weapon );
        }

        [HttpGet( "character/{characterId}" )]
        public async Task<ActionResult<Weapon>> GetByCharacterId( int characterId )
        {
            var weapon = await _weaponService.GetByCharacterId( characterId );
            if(weapon == null)
                return BadRequest( $"Character with {characterId} no weapon found!" );
            return Ok( weapon );
        }

        [HttpPut( "add" )]
        public async Task<ActionResult<Weapon>> Add(WeaponDto request)
        {
            await _weaponService.Add( request );
            return Ok("Weapon added!");
        }
    }
}
