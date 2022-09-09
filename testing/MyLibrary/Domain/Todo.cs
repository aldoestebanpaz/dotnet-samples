using System.Text.Json.Serialization;

namespace MyLibrary.Domain
{
    public class Todo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("completed")]
        public bool Completed { get; set; }
    }
}
