using System.Text;
using Models;
using SMTP;
namespace EfCore;

public class DB
{
    private DateTime SuAnkiTurkiyeZamani;
    public DB()
    {
        DateTime turkiyeSaati = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
        SuAnkiTurkiyeZamani = new DateTime(turkiyeSaati.Year, turkiyeSaati.Month, turkiyeSaati.Day, turkiyeSaati.Hour, turkiyeSaati.Minute, 0);
    }

    public void UpdatePassword(string password,int userId){
        using (DatabaseContext database = new DatabaseContext())
        {
                User? user = database.Users.FirstOrDefault(u => u.Id == userId && u.EmailVerification);
                if (user != null)
                {
                    user.Password = password;
                    database.Users.Update(user);
                    database.SaveChanges();
                }
        } // using
    }
    public User? PasswordResetVerification(string verificationCode)
    {
        User? user = null;
        using (DatabaseContext database = new DatabaseContext())
        {
            List<User>  users= database.Users.Where(u =>u.EmailVerification&& u.VerificationCode == verificationCode).ToList();
            if (users.Count > 0 && users.Count < 2)
            {
                user = users[0];
            }
            if (user != null)
            {
                user.VerificationCode = null;
                database.Users.Update(user);
                database.SaveChanges();
            }
        } // using
        return user;
    }
    public void UpdateUser(User user)
    {
        using (DatabaseContext database = new DatabaseContext())
        {
                database.Users.Update(user);
                database.SaveChanges();
        } // using
    }

    public User? GetUserEmail(string email)
    {
        User? user = null;
        using (DatabaseContext database = new DatabaseContext())
        {
            user = database.Users.FirstOrDefault(u => u.Email == email && u.EmailVerification == true);
        } // using
        return user;
    }

    public async Task<sbyte> AuthUser(User user)
    {
        // Eğer Veri tabanında Belirli Kullanıcı Adı ve Email'i varsa geriye 7 döner.
        // Eğer Veri tabanında Belirli Kullanıcı adı varsa geriye 4 döner.
        // Eğer Veri tabanında Belirli Email adresi varsa geriye 3 döner.
        // Eğer Belirli Değerler yoksa Yeni bir Kullanıcı oluşturur ve geriye 1 döner.
        int StatusCode = 0;
        using (DatabaseContext database = new DatabaseContext())
        {
            int email = database.Users.FirstOrDefault(d => d.Email == user.Email && d.EmailVerification == true) == null ? 0 : 3;// Eğer Veri Tabaninda bu email  Yoxdursa 0 donur
            int username = database.Users.FirstOrDefault(d => d.UserName == user.UserName && d.EmailVerification == true) == null ? 0 : 4;// Eğer Veri Tabaninda bu UserName  Yoxdursa 0 donur

            StatusCode = email + username;
            if (StatusCode == 0 && user != null)
            {
                bool kapi = false;
                do
                {
                    string urlCode = PasswordGenerator.GeneratePassword(100, 150);
                    //false                          //urlCode burda var. tapdi  // VAR   no null
                    kapi = database.Users.FirstOrDefault(d => d.VerificationCode == urlCode) == null; // false
                    if (kapi) { user.VerificationCode = urlCode; }

                } while (!kapi);

                user.UserType = 1; // Hər ehtimala qarşı user tipini yenədə 1 edirəmki birdən admin yetkisi ilə kayıt olmasın.
                user.Id = 0; // Forum Id de atanan Id Değeri burada Sıfırlanır ki Veri Tabanında 'auto increment'de problem Yaşatmasın. 
                var existingUser = database.Users.FirstOrDefault(d => (d.Email == user.Email && d.EmailVerification == false) || (d.UserName == user.UserName && d.EmailVerification == false));// Eğer Veri Tabaninda bu email  Yoxdursa 0 donur
                if (existingUser == null)
                {
                    database.Users.Add(user);
                }
                else
                {
                    existingUser.Password = user.Password;
                    existingUser.UserName = user.UserName;
                    existingUser.Email = user.Email;
                    existingUser.VerificationCode = user.VerificationCode;
                    database.Users.Update(existingUser);
                }
                await Mail.Auth(user.Email, user.VerificationCode);
                StatusCode = 1;
            }
            else
            {
                Console.WriteLine($"Öğe mevcut. Durum Kodu: {StatusCode}");

            } // else
            database.SaveChanges();
        } // using
        return (sbyte)StatusCode;
    }

    public User? EmailVerification(string verificationCode)
    {
        User? user = null;
        using (DatabaseContext database = new DatabaseContext())
        {
            user = database.Users.FirstOrDefault(u => u.VerificationCode == verificationCode);
            if (user != null)
            {
                user.VerificationCode = null;
                user.EmailVerification = true;
                database.Users.Update(user);
                database.SaveChanges();
            }
        } // using
        return user;
    }

    public User? GetUser(string username)
    {
        User? user = null;
        using (DatabaseContext database = new DatabaseContext())
        {
            user = database.Users.FirstOrDefault(u => u.UserName == username && u.EmailVerification == true);
        } // using
        return user;
    }

    public sbyte AddUser(User user)
    {
        // Eğer Veri tabanında Belirli Kullanıcı Adı ve Email'i varsa geriye 7 döner.
        // Eğer Veri tabanında Belirli Kullanıcı adı varsa geriye 4 döner.
        // Eğer Veri tabanında Belirli Email adresi varsa geriye 3 döner.
        // Eğer Belirli Değerler yoksa Yeni bir Kullanıcı oluşturur ve geriye 1 döner.
        int StatusCode = 0;
        using (DatabaseContext database = new DatabaseContext())
        {
            int email = database.Users.FirstOrDefault(d => d.Email == user.Email) == null ? 0 : 3;// Eğer Veri Tabaninda bu email  Yoxdursa 0 donur
            int username = database.Users.FirstOrDefault(d => d.UserName == user.UserName) == null ? 0 : 4;// Eğer Veri Tabaninda bu UserName  Yoxdursa 0 donur

            StatusCode = email + username;
            if (StatusCode == 0)
            {

                user.UserType = 1; // Hər ehtimala qarşı user tipini yenədə 1 edirəmki birdən admin yetkisi ilə kayıt olmasın.
                user.Id = 0; // Forum Id de atanan Id Değeri burada Sıfırlanır ki Veri Tabanında 'auto increment'de problem Yaşatmasın. 
                database.Users.Add(user);
                StatusCode = 1;
                Console.WriteLine("Kayıt işlemi başarılı.");

            }
            else
            {
                Console.WriteLine($"Öğe mevcut. Durum Kodu: {StatusCode}");

            } // else
            database.SaveChanges();
        } // using
        return (sbyte)StatusCode;
    } // UserAdd()

    public User? SingIn(User user)
    {
        User? data = null;

        if (user != null)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                data = database.Users.FirstOrDefault(d => (d.Email == user.UserName || d.UserName == user.UserName) && d.Password == user.Password && d.EmailVerification == true);
            }
        }
        return data;
    }

    public void UpdateTherapyDateTime(int id, DateTime date)
    {
        using (DatabaseContext database = new DatabaseContext())
        {

            var terapi = database.Terapiler.FirstOrDefault(t => t.Id == id);
            if (terapi != null)
            {
                terapi.StartDateTime = date;
                database.Terapiler.Update(terapi);
                database.SaveChanges();
                Console.WriteLine("Terapi Tarihi Değiştirildi");
            }
            else
            {
                Console.WriteLine("Terapi bulunamadi");
            }

        } // using

    }

    public void FinishTherapy(int id)
    {
        using (DatabaseContext database = new DatabaseContext())
        {

            var terapi = database.Terapiler.FirstOrDefault(t => t.Id == id);
            if (terapi != null)
            {
                terapi.TherapyStatus = true;
                database.Terapiler.Update(terapi);
                database.SaveChanges();
                Console.WriteLine("Terapi Sonlandırıldı");
            }
            else
            {
                Console.WriteLine("Terapi bulunamadi");
            }

        } // using

    }

    public void AddTherapyUrl(int id, string url)
    {
        using (DatabaseContext database = new DatabaseContext())
        {

            var terapi = database.Terapiler.FirstOrDefault(t => t.Id == id);
            if (terapi != null)
            {
                terapi.Url = url;
                database.Terapiler.Update(terapi);
                database.SaveChanges();
            }
            else
            {
                Console.WriteLine("Terapi bulunamadi");
            }

        } // using
    }

    public void AddTherapy(Terapiler terapi)
    {
        using (DatabaseContext database = new DatabaseContext())
        {
            if (terapi != null)
            {
                database.Terapiler.Add(terapi);
                database.SaveChanges();
            }
            else
            {
                Console.WriteLine("Randevu Veri Tabanına eklenemedi.");
            }
        }
    }

    public List<Terapiler>? ResultTherapy(User user)
    {
        List<Terapiler>? terapiler = null;
        using (DatabaseContext database = new DatabaseContext())
        {
            if (user != null)
            {
                terapiler = database.Terapiler.Where(t => t.UserId == user.Id).ToList();
            }
        }
        return terapiler;
    }

    public List<UserAndTherapy> AllTherapy()
    {
        List<UserAndTherapy>? terapilerVeKullanicilar = null;
        using (DatabaseContext database = new DatabaseContext())
        {
            var terapiler = database.Terapiler.ToList();
            var users = database.Users.Where(u => u.EmailVerification).ToList();

            terapilerVeKullanicilar = (from t in terapiler
                                       join u in users on t.UserId equals u.Id
                                       select new UserAndTherapy
                                       {
                                           User = u,
                                           Therapy = t
                                       }).ToList();

        }
        return terapilerVeKullanicilar;
    }

    public bool MusaitTerapiVakiti(DateTime UserSelectDateTime)
    {
        using (DatabaseContext database = new DatabaseContext())
        {
            List<DateTime> TarihListesi = new List<DateTime>();

            // Burada Terapi vakti yetişmiş seanslar ve tamamlanmış seanslar listeye eklenmiyor. ,,, ve OrderBy ile küçükten büyüğe doğru sıralanıyor
            var terapiler = database.Terapiler.Where(t => t.TherapyStatus == false && t.StartDateTime > SuAnkiTurkiyeZamani).OrderBy(t => t.StartDateTime).ToList();

            if (terapiler.Count > 0)
            {
                // Kullanıcı mevcut tarihten En az 1 saat sonrası için seans oluşturabilir
                TarihListesi.Add(SuAnkiTurkiyeZamani.AddHours(1));
                foreach (var item in terapiler)
                {
                    TarihListesi.Add(item.StartDateTime);
                }
                // Son seans tarihinden sonra exta 1 yıl eklenir. BU MUHAKKAK EKLENMELİ!
                TarihListesi.Add(SuAnkiTurkiyeZamani.AddYears(1));
            }
            else
            {    // Kullanıcı mevcut tarihten En az 1 saat sonrası için seans oluşturabilir
                TarihListesi.Add(SuAnkiTurkiyeZamani.AddHours(1));
                // Son seans tarihinden sonra exta 1 yıl eklenir. BU MUHAKKAK EKLENMELİ!
                TarihListesi.Add(SuAnkiTurkiyeZamani.AddYears(1));
            }

            for (int i = 0; i < TarihListesi.Count; i++)
            {
                // İki tarih arasındakı zaman farkını alır ve AddHours ile ilk tarihe 1 saat eklenirki çünkü seans müddədi 1 saat çəkir
                TimeSpan zamanFarki = TarihListesi[i + 1].Subtract(TarihListesi[i].AddHours(i == 0 ? 0 : 1));// burada eğer listenin 0-ıncı elemanı gelire extra 1 saat atanmasın. çünkü 'SuAnkiTurkiyeZamani' listeye eklendiği zaman zaten 1 saat ilave ediliyor.
                // Eğer belirli iki randevu arasındakı fark double türünde 1 saateden çok veya 1 saat ise
                if (zamanFarki.TotalHours >= 1)
                {   // Kullanıcının girdiği tarih, bu iki tarihin ilkinden sonra ve ikincisinden öncemi,,,adHours ile 1 ssat elave edilirki seans müddedi 1 saatdir
                    if (TarihListesi[i].AddHours(i == 0 ? 0 : 1) <= UserSelectDateTime && UserSelectDateTime.AddHours(1) <= TarihListesi[i + 1])
                    {                           // Burası.^: eğer listenin 0-ıncı elemanı gelire extra 1 saat atanmasın. çünkü 'SuAnkiTurkiyeZamani' listeye eklendiği zaman zaten 1 saat ilave ediliyor.
                        return true;
                    }
                }
                if (TarihListesi.Count - 2 == i)
                {
                    break;
                }
            }

        } // using
        return false;
    }

    public void AddSoru(Soru soru)
    {
        using (DatabaseContext database = new DatabaseContext())
        {
            var data = database.Sorular.Add(soru);
            database.SaveChanges();

        } // using
    }

    public List<Soru> AllSoru()
    {
        List<Soru>? soruList = null;
        using (DatabaseContext database = new DatabaseContext())
        {
            soruList = database.Sorular.ToList();

        } // using

        return soruList;
    }

    public void SoruFinsh(int id)
    {
        using (DatabaseContext database = new DatabaseContext())
        {
            var NewSoru = database.Sorular.Where(soru => soru.Id == id).FirstOrDefault();
            if (NewSoru != null)
            {
                NewSoru.Okundu = true;
                database.Sorular.Update(NewSoru);
                database.SaveChanges();
            }
            else
            {
                Console.WriteLine("EfCore.DB.SoruFinsh() Metodu aranan Suruyu veri tabanindan bulamadı");
            }
        }
    }

}
















public class PasswordGenerator
{
    private static Random random = new Random();

    public static string GeneratePassword(int minLength, int maxLength)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        int size = random.Next(minLength, maxLength + 1);
        StringBuilder password = new StringBuilder();

        for (int i = 0; i < size; i++)
        {
            password.Append(chars[random.Next(chars.Length)]);
        }

        return password.ToString();
    }
}
