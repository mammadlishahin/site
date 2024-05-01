using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;

namespace Namespace_Iyzipay;

public class My_Iyzipay
{
    public Payment Ode(PaymentCard paymentCard,string email)
    {
        paymentCard.RegisterCard=0;
        Options options = new Options();
        options.ApiKey = "sandbox-*************************";
        options.SecretKey = "sandbox-*******************";
        options.BaseUrl = "https://sandbox-api.iyzipay.com";

        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = "123456789";
        request.Price = "10";
        request.PaidPrice = "10";
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();


        request.PaymentCard = paymentCard;

        Buyer buyer = new Buyer();
        buyer.Id = "BY789";
        buyer.Name = "John";
        buyer.Surname = "Doe";
        buyer.Email = "email@email.com";
        buyer.IdentityNumber = "74300864791";
        buyer.RegistrationAddress = email;
        buyer.City = "Istanbul";
        buyer.Country = "Turkey";
        request.Buyer = buyer;

        Address shippingAddress = new Address(); // Teslimat Adresi
        shippingAddress.ContactName = "Jane Doe";
        shippingAddress.City = "Istanbul";
        shippingAddress.Country = "Turkey";
        shippingAddress.Description = email;
        request.ShippingAddress = shippingAddress;

        Address billingAddress = new Address(); // Fatura Adresi (buyer alanı ile ayı olmak zorunda)
        billingAddress.ContactName = "Jane Doe";
        billingAddress.City = "Istanbul";
        billingAddress.Country = "Turkey";
        billingAddress.Description = email;
        request.BillingAddress = billingAddress;

        List<BasketItem> basketItems = new List<BasketItem>();


        BasketItem thirdBasketItem = new BasketItem();
        thirdBasketItem.Id = "BI103";
        thirdBasketItem.Name = "Usb";
        thirdBasketItem.Category1 = "Goruntulu";
        thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
        thirdBasketItem.Price = "10";
        basketItems.Add(thirdBasketItem);
        request.BasketItems = basketItems;

        Payment payment = Payment.Create(request, options);
        
        
        return payment;
    }
}