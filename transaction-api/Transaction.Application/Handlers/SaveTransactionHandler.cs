using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Application.Commands;
using Transaction.Application.Common.Messaging;
using Transaction.Application.DTOs;
using Transaction.Domain.Events;
using Transaction.Domain.Interfaces;
using Transaction.Domain.Services;

namespace Transaction.Application.Handlers
{
    public class SaveTransactionHandler : IRequestHandler<SaveTransactionCommand, ApiResponse>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IKafkaProducer _kafkaProducer;

        public SaveTransactionHandler(ITransactionRepository transactionRepository, IKafkaProducer kafkaProducer)
        {
            _transactionRepository = transactionRepository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<ApiResponse> Handle(SaveTransactionCommand request, CancellationToken cancellationToken)
        {
            var req = request.request;

            if (req is null)
                return ApiResponse.BadRequest("La solicitud está vacía.");

            var transaction = TransactionFactory.CreateNew(req.sourceAccountId, req.targetAccountId, req.transferTypeId, req.value);

            await _transactionRepository.SaveTransaction(transaction);

            var evento = new TransactionCreatedEvent
            {
                TransactionId = transaction.Id,
                SourceAccountId = transaction.sourceAccountId,
                TargetAccountId = transaction.targetAccountId,
                TransferTypeId = transaction.transferTypeId,
                Amount = transaction.amount,
                CreatedAt = transaction.createAt
            };

            await _kafkaProducer.PublishAsync("transaction-created", evento);

            return ApiResponse.Ok(transaction, "Transaccion registrada exitosamente!");
        }
    }
}
