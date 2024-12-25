namespace clothing_store.Models
{
    public class UserEvent
    {
        public int Id { get; set; }
        public string EventType { get; set; }
        public string UserId { get; set; } 
        public string IPAddress { get; set; }
        public DateTime EventTime { get; set; }
        public string PageUrl { get; set; }
    }

}
