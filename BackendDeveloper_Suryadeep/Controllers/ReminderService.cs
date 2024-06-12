using BackendDeveloper_Suryadeep.Data;
using BackendDeveloper_Suryadeep.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BackendDeveloper_Suryadeep.Controllers
{
    public class ReminderService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public ReminderService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckReminders, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private async void CheckReminders(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IReminderRepository>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var reminders = await repository.GetRemindersAsync();

                foreach (var reminder in reminders)
                {
                    if (reminder.DateTime <= DateTime.Now)
                    {
                        // Send email notification
                        await emailService.SendEmailAsync("recipient@example.com", "Reminder: " + reminder.Title, $"This is a reminder for: {reminder.Title} scheduled at {reminder.DateTime}.");

                        // Remove or update the reminder after sending the email
                        await repository.DeleteReminderAsync(reminder.Id);
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}
