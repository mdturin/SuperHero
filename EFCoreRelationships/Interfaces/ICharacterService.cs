using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationships.Interfaces
{
    public interface ICharacterService
    {
        Task<List<Character>> Get();

        Task<Character?> Get(int id);
        Task<List<Character>> GetByUserId( int userId );
    }
}
