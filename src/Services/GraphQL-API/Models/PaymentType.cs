using System.ComponentModel;

namespace GraphQL_API.Models;

public enum PaymentType
{
    [Description("Free Course")]
    FREE = 0,

    [Description("Paid Course")]
    PAID = 1
}