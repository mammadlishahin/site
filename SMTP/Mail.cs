using System.Net;
using System.Net.Mail;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Models;

namespace SMTP;




public class Mail
{
    public static async Task Auth(string? userMailAdres, string? verificationUrl)
    {
        verificationUrl=verificationUrl ?? "";
        // Amazon SES istemcisini yapılandır
        var region = RegionEndpoint.USEast1; // US East (N. Virginia) bölgesi
        var client = new AmazonSimpleEmailServiceClient("**************", "**************", region);

        // E-posta gövdesi ve başlığı
        string subject = "Novaturient'e Hoşgeldiniz";
        string body = css.HTML(verificationUrl);

        // Alıcı ve gönderici e-posta adresleri
        string gonderici = "novaturient.com@novaturient.com";

        // E-posta gönderme isteği oluştur
        var sendRequest = new SendEmailRequest
        {
            Source = gonderici,
            Destination = new Destination { ToAddresses = { userMailAdres } },
            Message = new Message
            {
                Subject = new Content(subject),
                Body = new Body { Html = new Content(body) }
            }

        };
        try
        {
            var response = await client.SendEmailAsync(sendRequest);
            Console.WriteLine(response.HttpStatusCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static async Task SoruyaYanit(string email, string message)
    {

        // Amazon SES istemcisini yapılandır
        var region = RegionEndpoint.USEast1; // US East (N. Virginia) bölgesi
        var client = new AmazonSimpleEmailServiceClient("**************", "**************", region);

        // E-posta gövdesi ve başlığı
        string subject = "Novaturient'e Hoşgeldiniz";
        string body = css.HTML(email);

        // Alıcı ve gönderici e-posta adresleri
        string gonderici = "novaturient@novaturient.com";
        // string senderEmail = "info@naciyem.awsapps.com";

        // E-posta gönderme isteği oluştur
        var sendRequest = new SendEmailRequest
        {
            Source = gonderici,
            Destination = new Destination { ToAddresses = { email } },
            Message = new Message
            {
                Subject = new Content(subject),
                Body = new Body { Html = new Content(message) }
                // Body = new Body { Html = new Content("<html><head></head><body><h1>HTML Email Body</h1><p>This is a test HTML email body.</p></body></html>") }
            }

        };
        try
        {
            var response = await client.SendEmailAsync(sendRequest);
            Console.WriteLine(response.HttpStatusCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static async Task ResetPassword(string email, string verificationCode){
        var region = RegionEndpoint.USEast1; // US East (N. Virginia) bölgesi
        var client = new AmazonSimpleEmailServiceClient("**************", "****************************", region);

        // E-posta gövdesi ve başlığı
        string subject = "Tekrar Hoşgeldiniz";
        string body = css.HTMLresetPassword(verificationCode);

        // Alıcı ve gönderici e-posta adresleri
        string gonderici = "novaturient@novaturient.com";

        // E-posta gönderme isteği oluştur
        var sendRequest = new SendEmailRequest
        {
            Source = gonderici,
            Destination = new Destination { ToAddresses = { email } },
            Message = new Message
            {
                Subject = new Content(subject),
                Body = new Body { Html = new Content(body) }
            }

        };
        try
        {
            var response = await client.SendEmailAsync(sendRequest);
            Console.WriteLine(response.HttpStatusCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
































