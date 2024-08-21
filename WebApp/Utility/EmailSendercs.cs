using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApp.Utility
{
	public class EmailSendercs : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			///implement how to send email
			return Task.CompletedTask;
		}
	}
}
