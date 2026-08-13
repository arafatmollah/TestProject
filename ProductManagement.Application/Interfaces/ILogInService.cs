using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.Interfaces
{
    public interface ILogInService
    {
        Task<LogInResponseDto> LoginAsync(LoginDto dto);
    }
}
