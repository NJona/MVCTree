namespace MvcJsTreeTwoEntities2.Models
{
    public class Legimitation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public virtual Role Role { get; set; }
    }
}
