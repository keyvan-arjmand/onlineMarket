using System.ComponentModel;

namespace Domain.Enums;

public enum OrderStatus
{
    [Description("در انتظار پرداخت")]
    PendingPayment = 1,

    [Description("پرداخت شده")]
    Paid = 2,

    [Description("در حال بررسی")]
    Processing = 3,

    [Description("در حال آماده‌سازی")]
    Preparing = 4,

    [Description("آماده ارسال")]
    ReadyToShip = 5,

    [Description("ارسال شده")]
    Shipped = 6,

    [Description("تحویل شده")]
    Delivered = 7,

    [Description("لغو شده")]
    Cancelled = 8,

    [Description("مرجوع شده")]
    Returned = 9,

    [Description("بازگشت وجه انجام شد")]
    Refunded = 10,

    [Description("ناموفق")]
    Failed = 11
}