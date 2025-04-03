
namespace CCP.Service.Vnpay
{
    public interface Ivnpay
    {
        bool ValidateSignature(string queryString, string vnp_HashSecret);
        string CreatePaymentUrl1(decimal amount, string returnUrl);
    }
}
