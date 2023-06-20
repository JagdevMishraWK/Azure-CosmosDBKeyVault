using Newtonsoft.Json;

namespace ShoppingWeb.Models
{
    public class TodoItemsModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("taskname")]
        public string TaskName { get; set; }
        [JsonProperty("taskdesc")]
        public string TaskDesc { get; set; }
    }
}
