namespace Bondo.Application;
public class SendEmailRequestModel
{
    public sender from { get; set; }
    public Message message_body { get; set; }
    public string subject { get; set; }
    public List<Recepient> to { get; set; }
    public class sender{
        public string email { get; set; } //= "no-reply@bondo.com";
        public string name { get; set; } //= "Bondo";
    }
    public class Recepient{
        public string email { get; set; }
        public string name { get; set; }
    }
    public class Message {
        public string type { get; set; } // = "text/html";
        public string value { get; set; }
     }
}
