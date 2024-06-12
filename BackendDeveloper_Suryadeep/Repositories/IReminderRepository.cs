using BackendDeveloper_Suryadeep.Models;

namespace BackendDeveloper_Suryadeep.Repositories
{
    public interface IReminderRepository
    {
        Task<IEnumerable<Reminder>> GetRemindersAsync();
        Task<Reminder> GetReminderByIdAsync(int id);
        Task AddReminderAsync(Reminder reminder);
        Task UpdateReminderAsync(Reminder reminder);
        Task DeleteReminderAsync(int id);
    }
}
