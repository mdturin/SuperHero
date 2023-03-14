using EFCoreRelationships.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationships.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _context;
        private readonly IWeaponService _weaponService;

        public CharacterService(DataContext context, IWeaponService weaponService)
        {
            _context = context;
            _weaponService = weaponService;
        }

        public async Task<List<Character>> Add( Character request )
        {
            _context.Characters.Add( request );

            _context.SaveChanges();

            return await _context.Characters.ToListAsync();
        }

        public async Task<List<Character>> Get()
        {
            return await _context
                .Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .ToListAsync();
        }

        public async Task<Character?> Get( int id )
        {
            return await _context
                .Characters
                .Where(c => c.UserId == id)
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Character>> GetByUserId( int userId )
        {
            return await _context
                .Characters
                .Where( character => character.UserId == userId )
                .ToListAsync();
        }

        public async Task<Character> UpdateWeapon(Character character, WeaponDto request)
        {
            var weapon = await _weaponService.Add(request);
            character.Weapon = weapon;
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();
            return character;
        }
    }
}
