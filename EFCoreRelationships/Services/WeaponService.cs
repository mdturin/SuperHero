using EFCoreRelationships.Dto;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationships.Services
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;

        public WeaponService(DataContext context)
        {
            _context = context;
        }

        public async Task Add(WeaponDto request)
        {
            Weapon weapon = new Weapon();
            weapon.CharacterId = request.CharacterId;
            weapon.Name = request.Name;
            weapon.Damage = request.Damage;
            await _context.Weapons.AddAsync(weapon);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Weapon>> Get()
        {
            return await _context.Weapons.ToListAsync();
        }

        public async Task<Weapon?> Get( int id )
        {
            return await _context.Weapons.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Weapon?> GetByCharacterId( int characterId )
        {
            return await _context.Weapons.FirstOrDefaultAsync( w => w.CharacterId == characterId );
        }
    }
}
