using Newtonsoft.Json;

namespace TypeTraversalConverter.Models
{
    [JsonConverter(typeof(TypeTraversalConverter))]
    public class BaseClass
    {
        public int Id { get; set; }
    }
}