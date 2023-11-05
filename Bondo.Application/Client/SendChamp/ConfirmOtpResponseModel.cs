namespace Bondo.Application.Client.SendChamp;
public class ConfirmOtpResponseModel
{
    public int code { get; set; }
    public string message { get; set; }
    public string status { get; set; }
    // public ConfirmOtpResponseModelData data { get; set; }
    // public class ConfirmOtpResponseModelData {
    //     public string business_uid { get; set; }
    //     public string reference { get; set; }
    //     public string token { get; set; }
    //     public string status { get; set; }
    // }
}
