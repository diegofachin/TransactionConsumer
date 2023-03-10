using Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class TransactionEntity : BaseEntity
{
    public string NumberCard { get; set; }

    public string NameOnCreditCard { get; set; }

    public string Validate { get; set; }

    public string Cvc { get; set; }

    public decimal Amount { get; set; }

    public PaymentStatus PaymentStatus { get; set; }
}
