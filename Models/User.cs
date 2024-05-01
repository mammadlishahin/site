using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace Models;
public class User
{
    public int Id { get; set; }=1;



    [Required(ErrorMessage = "Kullanıcı Adı zorunludur.")]
    [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "5 ila 30 karakter aralığında değer girmelisiniz.")]
    public string UserName { get; set; }



    [Required(ErrorMessage = "Email alanı zorunludur.")]
    [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "6 ila 50 karakter aralığında değer girmelisiniz.")]
    public string Email { get; set; }



    [Required(ErrorMessage = "Şifre alanı zorunludur.")]
    [StringLength(maximumLength: 100, MinimumLength = 8, ErrorMessage = "8 ila 100 karakter aralığında parola girmelisiniz.")]
    public string Password { get; set; }

    public bool EmailVerification { get; set; }
    [StringLength(maximumLength:150)]
    public string VerificationCode { get; set; }

    [Range(0,3)]
     public sbyte UserType { get; set; }

    [BindNever]
     public List<Terapiler> Terapiler { get; set; }
}



