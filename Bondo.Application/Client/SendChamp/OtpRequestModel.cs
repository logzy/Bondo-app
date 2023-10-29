namespace Bondo.Application.Client.SendChamp;
public record OtpRequestModel
{
    public string channel { get; set; }
    public string sender { get; set; }
    public string token_type { get; set; }
    public int token_length { get; set; }
    public int expiration_time { get; set; }
    public string customer_email_address { get; set; }
    public string customer_mobile_number { get; set; }
}
