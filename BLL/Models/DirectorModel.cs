using BLL.DAL;

namespace BLL.Models;

public class DirectorModel
{
    public Director Record { get; set; }

    public string Name => Record.name;
    public string Surname => Record.surname;
    public string isRetired => Record.isretired ? "Yes" : "No";
    public string Movies => string.Join("<br>", Record.movie?.Select(m => m.name));

}