using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Application.DTOs;
using Transaction.Application.Query;
using Transaction.Domain.Interfaces;

namespace Transaction.Application.Handlers
{
    public class GetTransactionByIdAndDateHandler : IRequestHandler<GetTransactionByIdAndDateQuery, ApiResponse>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionByIdAndDateHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<ApiResponse> Handle(GetTransactionByIdAndDateQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAndDate(request.TransactionId, request.CreatedAt);

            if (transaction == null)
                return ApiResponse.BadRequest("No se encontró la transacción.");

            return ApiResponse.Ok(transaction);
        }
    }
}
