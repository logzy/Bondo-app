using System;
using Bondo.Application.DTOs.Email;

namespace Bondo.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequestDto request);
    }
}

