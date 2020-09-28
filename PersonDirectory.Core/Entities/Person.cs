using Newtonsoft.Json;
using System.Collections.Generic;

namespace PersonDirectory.Core.Entities
{
    public class Person : BasePerson
    {
     
        public virtual ICollection<RelatedPersonToPerson> RelatedPersons { get; set; }
        [JsonIgnore]
        public virtual ICollection<RelatedPersonToPerson> RelatedOn { get; set; }

    }
}
