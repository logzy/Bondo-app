using Bondo.Application.DTOs.Otp;
using Bondo.Application.Interfaces;
using Bondo.Shared;

namespace Bondo.Infrastructure.Services;
public class OtpService : IOtpService
{
    public Task<Result<SendOtpResponseDto>> CreateEmailOtp(SendOtpRequestDto emailOtpRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result<SendOtpResponseDto>> CreatePhoneOtp(SendOtpRequestDto phoneOtpRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> VerifyEmailOtp(ConfirmOtpRequestDto confirmOtpRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> VerifyPhoneOtp(ConfirmOtpRequestDto confirmOtpRequest)
    {
        throw new NotImplementedException();
    }
}
