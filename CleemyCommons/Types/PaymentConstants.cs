namespace CleemyCommons.Types
{
    public class PaymentConstants
    {
        public const string CST_AMOUNT = "Amount";
        public const string CST_DATE = "Date";

        public const string CST_ERROR_INCOHERENT_CURRENCY = "Payment currency and user currency must be the same";
        public const string CST_ERROR_DUPLICATE_PAYMENT = "Duplicate payment";
        public const string CST_ERROR_CURRENCY_NOT_EXIST = "Currency does not exist";
        public const string CST_ERROR_USER_NOT_EXIST = "User does not exist";
    }
}