using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Application.Commands;
using Transaction.Application.DTOs;
using Transaction.Domain.Interfaces;
using Transaction.Domain.Services;

namespace Transaction.Application.Handlers
{
    public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, ApiResponse>
    {
        private readonly ITransactionRepository _transactionRepository;

        public UpdateStatusHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<ApiResponse> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            var req = request.request;

            if (req is null)
                return ApiResponse.BadRequest("La solicitud está vacía.");

            await _transactionRepository.UpdateStatusTransaction(req.transactionId, req.status);

            return ApiResponse.Ok(true, "Transaccion registrada exitosamente!"); ;
        }

    }
}
