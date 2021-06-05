using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;

namespace EmailAppAzure
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            string smtpName = Environment.GetEnvironmentVariable("smtp");
            string email = Environment.GetEnvironmentVariable("email");
            string emPassword = Environment.GetEnvironmentVariable("password");
            string receipeient = Environment.GetEnvironmentVariable("receipient");

            try
            {
                var smtpClient = new SmtpClient(smtpName)
                {
                    Port = 587,
                    Credentials = new NetworkCredential(email, emPassword),
                    EnableSsl = true
                };
                smtpClient.Send(email, receipeient, "This is test from Azure", "Hi! You have just received a test email from Azure function.Thanks!!");
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
            log.LogInformation($"Successfully sent!!");
        }
    }
}
