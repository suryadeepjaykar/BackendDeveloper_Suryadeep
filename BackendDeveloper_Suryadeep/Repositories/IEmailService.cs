﻿namespace BackendDeveloper_Suryadeep.Repositories
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
