using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum;

public enum PaymentStatus
{
    AwaitingProcessing = 0,
    Approved = 1,
    Refused = 2,
    Canceled = 3,
}
