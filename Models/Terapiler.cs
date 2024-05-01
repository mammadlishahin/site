
namespace Models;

public class Terapiler
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Url { get; set; }
    public string AppName { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime StartDateTime { get; set; }
    public string IstegeBagli { get; set; }
    public bool TherapyStatus { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}