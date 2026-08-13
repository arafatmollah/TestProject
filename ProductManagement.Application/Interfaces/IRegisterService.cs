using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.Interfaces
{
    public interface IRegisterService
    {
        Task<RegisterResponseDto> RegisterAsync(
        RegisterDto dto,
        CancellationToken cancellationToken = default);
    }
}
