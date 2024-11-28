using BLL.DAL;
using BLL.Models;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface ImovieService
    {
        public IQueryable<MovieModel> Query();
        public ServiceBase Create(movie record);
        public ServiceBase Update(movie record);
        public ServiceBase Delete(int id);
    }

    public class movieservices: ServiceBase, ImovieService
    {
        public movieservices(Db db) : base(db)
        {
            
        }

        public IQueryable<MovieModel> Query()
        {
            return _db.movie
                .Include(m => m.director) // Include the director entity
                .OrderBy(s => s.name)
                .Select(s => new MovieModel
                {
                    Record = s
                });
        }


        public ServiceBase Create(movie entity)
        {
            if (_db.movie.Any(m => m.name.ToLower() == entity.name.ToLower()))
                return Error("movie already exists");
            _db.movie.Add(entity);
            _db.SaveChanges();
            return Success("movie created");
        }

        public ServiceBase Update(movie record)
        {
            var existingMovie = _db.movie.SingleOrDefault(m => m.id == record.id);

            if (existingMovie == null)
            {
                return Error("A movie does not exist.");
            }

            // Update logic
            existingMovie.name = record.name;
            existingMovie.releasedate = record.releasedate;
            existingMovie.totalrevenue = record.totalrevenue;
            existingMovie.directorid = record.directorid;

            _db.SaveChanges();
            return Success("Movie updated successfully.");
        }


        public ServiceBase Delete(int id)
        {
            var movie = _db.movie.Include(m => m.director).SingleOrDefault(m => m.id == id);

            if (movie == null)
                return Error("Movie not found.");

            _db.movie.Remove(movie);
            _db.SaveChanges();

            return Success("Movie deleted successfully.");
        }




    }
}