namespace Bondo.Application.Client.SendChamp;
public class OtpResponseModel
{

    public int code { get; set; }
    public string message { get; set; }
    public string status { get; set; }
    public OtpResponseModelData data { get; set; }
    public class OtpResponseModelData {
        public string business_uid { get; set; }
        public string reference { get; set; }
        public string token { get; set; }
        public string status { get; set; }
    }
}
