using System.Net;
using System.Net.Mail;

namespace Company.Seif.PL.Helbers
{
	public static class EmailSetting
	{
		public static bool SendEmail (Email email)
		{
			//Mail server 
			//SMTB

			try
			{
				var client = new SmtpClient("smtp.gmail.com", 587);
				client.EnableSsl = true;
				client.Credentials = new NetworkCredential("elslimanysief@gmail.com", "gujfcazxnjnkcvrc"); //sender
				client.Send("elslimanysief@gmail.com", email.To, email.Subject, email.Body);
				//gujfcazxnjnkcvrc
				return true;
			}
			catch (Exception e) 
			{ 
			 return false;
			}
		}
	}
}
