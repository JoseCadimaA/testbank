using AntiFraud.Application.Commands;
using AntiFraud.Application.Common.Messaging;
using AntiFraud.Domain.Entities;
using AntiFraud.Domain.Enums;
using AntiFraud.Domain.Events;
using AntiFraud.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TransactionStatus = AntiFraud.Domain.Enums.TransactionStatus;

namespace AntiFraud.Application.Handlers
{
    public class ValidateTransactionHandler : IRequestHandler<ValidateTransactionCommand, bool>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IKafkaProducer _kafkaProducer;

        public ValidateTransactionHandler(ITransactionRepository transactionRepository, IKafkaProducer kafkaProducer)
        {
            _transactionRepository = transactionRepository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<bool> Handle(ValidateTransactionCommand request, CancellationToken cancellationToken)
        {
            var req = request.request;

            if (req is null)
                return false;

            var evento = new UpdateStatusEvent();
            evento.transactionId = req.TransactionId;

            var ordenACH = new OrdenACH
            {
                TransactionId = req.TransactionId,
                Amount = req.Amount,
                CreatedAt = req.CreatedAt,
                SourceAccountId = req.SourceAccountId,
                TargetAccountId = req.TargetAccountId,
                TransferTypeId = req.TransferTypeId,
            };

            var validation = await _transactionRepository.ValidateAndRegisterAsync(
                ordenACH,
                cancellationToken);

            evento.status = validation.IsApproved
                ? TransactionStatus.Approved
                : TransactionStatus.Rejected;

            await _kafkaProducer.PublishAsync("transactions-update-status", evento);

            return true;
        }
    }
}
