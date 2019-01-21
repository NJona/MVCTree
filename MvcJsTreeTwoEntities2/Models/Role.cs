using System.Collections.Generic;

namespace MvcJsTreeTwoEntities2.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Legimitation> Legimitations { get; set; }
    }
}
