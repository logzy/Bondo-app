using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bondo.Application;
using Bondo.Application.Client.SendChamp;
using Bondo.Application.DTOs.Email;
using Bondo.Application.Interfaces;
using Bondo.Shared;
using Microsoft.Extensions.Configuration;

namespace Bondo.Infrastructure;
public class EmailService : IEmailService
{
    private static readonly HttpClient httpClient = new HttpClient();
    private readonly IConfiguration _config;
    private readonly string _baseAddress = "https://api.sendchamp.com/api/v1/";
    public EmailService(IConfiguration config)
    {
        _config = config;
        // httpClient.BaseAddress = new Uri("https://api.sendchamp.com/api/v1/");
        // httpClient.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", _config["SendChampAPiKey"]);
    }
    public async Task<Result<EmailResponseModel>> SendAsync(EmailRequestDto request)
    {
        var emailResponse = new EmailResponseModel();
        SendEmailRequestModel emailReq = new SendEmailRequestModel{
                from = new SendEmailRequestModel.sender{
                    email = _config["SendChamSenderEmail"] ?? "no-reply@bondo.com",
                    name = "Bondo"
                },
                message_body = new SendEmailRequestModel.Message{
                    type = "text/html",
                    value = request.body
                },
                subject = request.subject,
                to = new List<SendEmailRequestModel.Recepient>{}
            };
            foreach(var recepient in request.to)
                emailReq.to.Add(new SendEmailRequestModel.Recepient{
                    name = recepient.name,
                    email = recepient.email
                });

        using(var httpClient = new HttpClient()){
            httpClient.BaseAddress = new Uri(_baseAddress);
            httpClient.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", _config["SendChampAPiKey"]);
            var res = await httpClient.PostAsJsonAsync("email/send", emailReq); 
            emailResponse = await res.Content.ReadFromJsonAsync<EmailResponseModel>();
        }
        
        if(emailResponse.status != "success")
            return Result<EmailResponseModel>.Failure("Email not sent");
        return Result<EmailResponseModel>.Success("Email sent");
    }
}
