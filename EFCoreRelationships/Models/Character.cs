namespace EFCoreRelationships.Models
{
    public enum RPG
    {
        Knight, Wizard, Archer
    }

    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public RPG RPGClass { get; set; } = RPG.Knight;
        public User? user { get; set; }
        public int UserId { get; set; }
    }
}
