using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intermediate_agency.BD_Classes
{
    public enum PostEnum
    {
        Chief,
        Manager
    };

    public enum ClientLevelEnum
    {
        A, B, C, D, E, F
    };

    public enum SellerReliabilityEnum
    {
        Reliable,
        Unreliable,
        Unknown
    };

    public enum OrderStatusEnum
    {
        NotAccepted,
        InProgress,
        WaitingPayment,
        Fulfilled,
        Canceled,
        Failed
    }
}
