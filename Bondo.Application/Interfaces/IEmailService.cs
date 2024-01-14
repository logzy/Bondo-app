using System;
using Bondo.Application.Client.SendChamp;
using Bondo.Application.DTOs.Email;
using Bondo.Shared;

namespace Bondo.Application.Interfaces
{
    public interface IEmailService
    {
        Task<Result<EmailResponseModel>> SendAsync(EmailRequestDto request);
    }
}

