using System.ComponentModel.DataAnnotations;

namespace Models;


public class Soru
{
    public int Id { get; set; }
    [StringLength(maximumLength: 50)]
    public string Ad { get; set; }
    [Required]
    [StringLength(maximumLength: 50, MinimumLength = 5)]
    public string Email { get; set; }
    [Required]
    [StringLength(maximumLength: 500, MinimumLength = 2)]
    public string Mesaj { get; set; }

    public DateTime Date { get; set; }
    public bool Okundu { get; set; }
    public string User { get; set; }

}

