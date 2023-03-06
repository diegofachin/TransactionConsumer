using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using EasyNetQ;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;
using TransactionConsumer.Transaction;

namespace TransactionConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _bus;

        public Worker(ILogger<Worker> logger, IUnitOfWork unitOfWork, IBus bus)
        {
            _logger = logger;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queue = _bus.Advanced.QueueDeclare("transaction_purchase");

            _bus.Advanced.Consume(queue, (byte[] content, MessageProperties properties, MessageReceivedInfo info) =>
            {
                var json = Encoding.UTF8.GetString(content);
                var message = JsonConvert.DeserializeObject<TransactionMessage>(json);

                ProcessTransaction(message);
            });
            
        }

        private void ProcessTransaction(TransactionMessage transactionMessage)
        {
            //TODO: Aqui, antes de definir o status da transação, poderiamos efeturar o processamento real, validando o cartão. 
            //TODO: Em caso de sucesso, poderiamos atualizar a transação para Approved, caso contrário é necessário atualizar como Refused.

            var transaction = new TransactionEntity()
            {
                Id = transactionMessage.Id,
                NameOnCreditCard = transactionMessage.NameOnCreditCard,
                NumberCard = transactionMessage.NumberCard,
                Amount = transactionMessage.Amount,
                Validate = transactionMessage.Validate,
                Cvc = transactionMessage.Cvc,
                PaymentStatus = PaymentStatus.Approved,               
            };

            _unitOfWork.TransactionRepository.Update(transaction);
            _unitOfWork.Commit();
        }
    }
}