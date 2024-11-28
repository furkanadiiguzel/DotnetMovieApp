using System;
using System.Linq;
using BLL.DAL;

namespace BLL.Models
{
    public class MovieModel
    {
        public movie Record { get; set; }

        public string Name => Record?.name ?? string.Empty;

        public string ReleaseDate => Record?.releasedate?.ToString("dd/MM/yyyy") ?? string.Empty;
        
        public string TotalRevenue => Record?.totalrevenue?.ToString("C2") ?? string.Empty;

        public string MovieDirector => Record.director?.name + " " + Record.director?.surname;

    }
}