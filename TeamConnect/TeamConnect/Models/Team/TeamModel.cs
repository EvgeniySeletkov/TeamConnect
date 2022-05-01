using SQLite;

namespace TeamConnect.Models.Team
{
    public class TeamModel : IEntityBase
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
