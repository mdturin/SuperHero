using EFCoreRelationships.Dto;

namespace EFCoreRelationships.Interfaces
{
    public interface ICharacterService
    {
        Task<List<Character>> Get();
        Task<Character?> Get(int id);
        Task<List<Character>> GetByUserId( int userId );
        Task<List<Character>> Add( Character request );
        Task<Character> UpdateWeapon( Character character, WeaponDto request );
    }
}
