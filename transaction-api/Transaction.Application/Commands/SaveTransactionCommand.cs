using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Transaction.Application.DTOs;

namespace Transaction.Application.Commands
{
    public record SaveTransactionCommand(SaveTransactionRequest request) : IRequest<ApiResponse> { }
}
