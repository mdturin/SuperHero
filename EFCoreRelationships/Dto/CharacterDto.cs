namespace EFCoreRelationships.Dto
{
    public class CharacterDto
    {
        public string Name { get; set; } = string.Empty;
        public RPG RPGClass { get; set; } = RPG.Knight;
        public int UserId { get; set; }
    }
}
