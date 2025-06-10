using AntiFraud.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiFraud.Application.Commands
{
    public record ValidateTransactionCommand(ValidateTransactionRequest request) : IRequest<bool> { }
}
