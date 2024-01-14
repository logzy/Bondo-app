namespace Bondo.Application.Client.SendChamp;
public class EmailResponseModel
{
    public int code { get; set; }
    public string message { get; set; }
    public string status { get; set; }
    public EmailResponseModelData data { get; set; }
    public class EmailResponseModelData {
        public string id { get; set; }
        public string subject { get; set; }
        public string email { get; set; }
        public string status { get; set; }
    }
}
