namespace SportsStore.Domain.Entities
{
    public class EmailSettings
    {
        public string MailToAddress { get; set; } = "test@durscherleupp.ch";
        public string MailFromAddress { get; set; } = "test@durscherleupp.ch";
        public bool EnableSsl { get; set; } = true;
        public string Username { get; set; } = "test@durscherleupp.ch";
        public string Pw { get; set; } = "KRvbyTjSrkShT29iqefg";
        public string Host { get; set; } = "smtp.onlime.ch";
        public int Port { get; set; } = 25;
        public bool WriteAsFile { get; set; } = false;
        public string FileLocation { get; set; } = System.IO.Path.GetTempPath();
    }
}
