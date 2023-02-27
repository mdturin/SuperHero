using EFCoreRelationships.Dto;
using Microsoft.IdentityModel.Tokens;

namespace EFCoreRelationships.Builder
{
    public class CharacterBuilder
    {
        public Character? Build(CharacterDto characterDto )
        {
            Character character = new Character();

            if(characterDto != null)
            {
                character.Name = characterDto.Name;

                character.RPGClass = characterDto.RPGClass;

                character.UserId = characterDto.UserId;
            }

            return null;
        }
    }
}
