using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Service
{
    public interface IMailService
    {
        bool SendMail(string from, string[] to, string subject, string content, string smtpServer, string smtpUser, string smtpPassword, string smtpPort);
        bool SendMail(string[] to, string subject, string content);
        Task SendMail(string[] to, string subject, string content, int jobId);
    }
}