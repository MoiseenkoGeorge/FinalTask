
namespace ORM
{
    
    public partial class ProfileArea
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int AreaId { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
