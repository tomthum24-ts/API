using System.Text.Json.Serialization;

namespace API.APPLICATION.Parameters.Menu
{
    public class MenuFilterParam
    {
        [JsonIgnore]
        public int IdUser { get; set; }
    }
}
