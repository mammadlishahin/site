
namespace SMTP;
public class css
{


    public static string authBody
    {
        get
        {
            return @"
            padding: 10px;
            margin-top: 10px;
            font-family: inherit;
            user-select: none;
            border: 1px solid transparent;
            font-size: 1rem;
            line-height: 1.5;
            font-weight: 400;
            color: #fff;
            background-color: #00501bcf;
            font-size: 20px;
            text-decoration: none;";
        }
    }

    public static string VerifyEmailBTN
    {
        get
        {
            return @"
            font-family: 'Inter', sans-serif;
            background-color: #4e48e0;
            border-radius: 3px;
            color: #ffffff;
            display: inline-block;
            font-size: 14px;
            font-weight: normal;
            font-style: normal;
            text-decoration: none;
            line-height: 20px;
            padding: 15px;
            text-align: center;";
        }

    }

    public static string HTML(string verificationUrl)
    {
        return $@"
            <html>
                <head></head>
                <body>
                    <div style=""{authBody}"" >
                        <h1 style=""text-align:center;padding:10px;margin:10px;color:rgb(108 117 125)"">Novaturiente Hoş Geldiniz</h1>
                        <a href=""https://novaturientpsikoloji.xyz/Auth/Auth?Change={verificationUrl}"" style=""{VerifyEmailBTN}"">Email adresinizi doğrulayın</a>
                    </div>
                </body>
            </html>
        ";
    }

    public static string HTMLresetPassword(string verificationUrl)
    {
        return $@"
            <html>
                <head></head>
                <body>
                    <div style=""{authBody}"" >
                        <h1 style=""text-align:center;padding:10px;margin:10px;color:rgb(108 117 125)"">Parolanızımı unuttunuz?</h1>
                        <p>
                            Eğer parolanızı unuttuysanız ve böyle bir talepte bulunduysanız lütfen aşağıdakı buttonu tıklayınız. Eğer bu siz değilsenin bu e-posta'yı görmezden gelebilirsinz
                        </p>
                        <a href=""https://novaturientpsikoloji.xyz/Auth/PasswordResetVerification?urlCode={verificationUrl}"" style=""{VerifyEmailBTN}"">Şifremi sıfırla</a>
                    </div>
                </body>
            </html>
        ";
    }


}

