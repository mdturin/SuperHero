namespace EFCoreRelationships.Interfaces
{
    public interface IWeaponService
    {
        Task<List<Weapon>> Get();
        Task<Weapon?> Get( int id );
        Task<Weapon?> GetByCharacterId( int characterId );
    }
}
