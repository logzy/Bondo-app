using System.Net.Http.Json;
using Bondo.Application.Client.SendChamp;
using Bondo.Application.DTOs.Otp;
using Bondo.Application.Interfaces;
using Bondo.Shared;
using Bondo.Domain.Constants;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace Bondo.Infrastructure.Services;
public class OtpService : IOtpService
{
    private static readonly HttpClient httpClient = new HttpClient();
    private readonly IConfiguration _config;
    private readonly string _baseAddress = "https://api.sendchamp.com/api/v1/";
    public OtpService(IConfiguration config)
    {
        _config = config;
        // httpClient.BaseAddress = new Uri("https://api.sendchamp.com/api/v1/");
        // httpClient.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", _config["SendChampAPiKey"]);
    }
    public async Task<Result<SendOtpResponseDto>> CreateEmailOtp(SendOtpRequestDto emailOtpRequest)
    {
        OtpRequestModel req = new OtpRequestModel{
            channel = SendChampClientConstants.EMAIL,
            sender  = SendChampClientConstants.SENDER,
            token_type = SendChampClientConstants.NUMERIC_TOKEN,
            token_length = SendChampClientConstants.TOKEN_LENGTH,
            expiration_time = SendChampClientConstants.EXPIRATION_TIME,
            customer_email_address = emailOtpRequest.Recepient,
        };
        OtpResponseModel createdOtp = new OtpResponseModel();
        using(var httpClient = new HttpClient()){
            httpClient.BaseAddress = new Uri(_baseAddress);
            httpClient.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", _config["SendChampAPiKey"]);
            var res = await httpClient.PostAsJsonAsync("verification/create", req); 
            createdOtp = await res.Content.ReadFromJsonAsync<OtpResponseModel>();
        }
        
        
       
        if(createdOtp!= null && createdOtp.code == 200){
            SendOtpResponseDto otpResponseDto = new SendOtpResponseDto{
                Reference = createdOtp.data.reference
            };
            return Result<SendOtpResponseDto>.Success(otpResponseDto);
        }else{
            return Result<SendOtpResponseDto>.Failure(createdOtp.message);
        }
            
    }
    public async Task<Result<SendOtpResponseDto>> CreatePhoneOtp(SendOtpRequestDto phoneOtpRequest)
    {
         OtpRequestModel req = new OtpRequestModel{
            channel = SendChampClientConstants.SMS,
            sender  = SendChampClientConstants.SENDER,
            token_type = SendChampClientConstants.NUMERIC_TOKEN,
            token_length = SendChampClientConstants.TOKEN_LENGTH,
            expiration_time = SendChampClientConstants.EXPIRATION_TIME,
            customer_mobile_number = phoneOtpRequest.Recepient
        };
        OtpResponseModel createdOtp = new OtpResponseModel();
        using(var httpClient = new HttpClient()){
            httpClient.BaseAddress = new Uri(_baseAddress);
            httpClient.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", _config["SendChampAPiKey"]);
            var res = await httpClient.PostAsJsonAsync("verification/create", req);
            createdOtp = await res.Content.ReadFromJsonAsync<OtpResponseModel>();
        }
       
        if(createdOtp!= null && createdOtp.code == 200){
            SendOtpResponseDto otpResponseDto = new SendOtpResponseDto{
                Reference = createdOtp.data.reference
            };
            return Result<SendOtpResponseDto>.Success(otpResponseDto);
        }else{
            return Result<SendOtpResponseDto>.Failure(createdOtp.message);
        }
            
    }

    public async Task<Result<bool>> VerifyOtp(ConfirmOtpRequestDto confirmOtpRequest)
    {
        ConfirmOtpRequestModel req = new ConfirmOtpRequestModel{
            verification_reference = confirmOtpRequest.Reference,
            verification_code  = confirmOtpRequest.Code,
        };
        OtpResponseModel confirmedOtp = new OtpResponseModel();
        using(var httpClient = new HttpClient()){
            httpClient.BaseAddress = new Uri(_baseAddress);
            httpClient.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", _config["SendChampAPiKey"]);
            var res = await httpClient.PostAsJsonAsync("verification/confirm", req);
            confirmedOtp = await res.Content.ReadFromJsonAsync<OtpResponseModel>();
        }
       
        if(confirmedOtp != null && confirmedOtp.code == 200){
            // ConfirmOtpResponseDto otpResponseDto = new SendOtpResponseDto{
            //     Reference = createdOtp.data.reference
            // };
            return Result<bool>.Success(confirmedOtp.message);
        }else{
            return Result<bool>.Failure(confirmedOtp.message);
        }
    }

    // public async Task<Result<bool>> VerifyPhoneOtp(ConfirmOtpRequestDto confirmOtpRequest)
    // {
    //     throw new NotImplementedException();
    // }
}
