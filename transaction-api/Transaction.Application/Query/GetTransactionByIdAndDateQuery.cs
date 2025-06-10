using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Application.DTOs;

namespace Transaction.Application.Query
{
    public class GetTransactionByIdAndDateQuery : IRequest<ApiResponse>
    {
        public Guid TransactionId { get; set; }
        public DateTime CreatedAt { get; set; }

        public GetTransactionByIdAndDateQuery(GetByIdAndDateRequest request)
        {
            TransactionId = request.transactionId;
            CreatedAt = request.createdAt;
        }
    }
}
