using EFCoreRelationships.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationships.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _context;

        public CharacterService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Character>> Add( Character request )
        {
            _context.Characters.Add( request );

            _context.SaveChanges();

            return await _context.Characters.ToListAsync();
        }

        public async Task<List<Character>> Get()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character?> Get( int id )
        {
            return await _context
                .Characters.FirstOrDefaultAsync(character => character.Id == id);
        }

        public async Task<List<Character>> GetByUserId( int userId )
        {
            return await _context
                .Characters
                .Where( character => character.UserId == userId )
                .ToListAsync();
        }
    }
}
