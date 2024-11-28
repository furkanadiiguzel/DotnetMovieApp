using BLL.DAL;

namespace BLL.Models;

public class GenreModel
{
    public genre Record { get; set; }
    
    public string Name => Record.name;
    

}