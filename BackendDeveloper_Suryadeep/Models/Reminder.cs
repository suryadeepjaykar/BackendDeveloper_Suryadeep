namespace BackendDeveloper_Suryadeep.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReminderDateTime { get; set; }
        public DateTime DateTime { get; internal set; }
    }
}
