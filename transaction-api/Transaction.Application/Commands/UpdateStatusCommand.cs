using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Application.DTOs;

namespace Transaction.Application.Commands
{
    public record UpdateStatusCommand(UpdateStatusRequest request) : IRequest<ApiResponse> { }
}
