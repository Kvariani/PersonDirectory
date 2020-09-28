using System.Text.Json.Serialization;

namespace PersonDirectory.Core.Entities
{
    public class EntityBase
    {
        [JsonIgnore]
        public int ID { get; set; }
    }
}
