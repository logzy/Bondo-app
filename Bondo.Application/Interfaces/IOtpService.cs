using Bondo.Application.DTOs.Otp;
using Bondo.Shared;

namespace Bondo.Application.Interfaces;
public interface IOtpService
{
    public Task<Result<SendOtpResponseDto>> CreatePhoneOtp(SendOtpRequestDto phoneOtpRequest);
    public Task<Result<SendOtpResponseDto>> CreateEmailOtp(SendOtpRequestDto emailOtpRequest);

    public Task<Result<bool>> VerifyOtp(ConfirmOtpRequestDto confirmOtpRequest);
    // public Task<Result<bool>> VerifyEmailOtp(ConfirmOtpRequestDto confirmOtpRequest);
}
