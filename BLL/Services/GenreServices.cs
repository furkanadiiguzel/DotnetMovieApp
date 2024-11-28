using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface Igenreervices
    {
        public IQueryable<GenreModel> Query();
        public ServiceBase Create(genre record);
        public ServiceBase Update(genre record);
        public ServiceBase Delete(int id);
    }

    public class genreservice: ServiceBase, Igenreervices
    {
        public genreservice(Db db) : base(db)
        {
            
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.genre.OrderBy(s => s.name).Select(s => new GenreModel() {Record = s});
        }

        public ServiceBase Create(genre record)
        {
            if (_db.genre.Any(s => s.name.ToUpper() == record.name.ToUpper().Trim()))
            {
                return Error("A Genre already exists.");
            }

            record.name = record.name?.Trim();
            _db.genre.Add(record);
            _db.SaveChanges();
            return Success("Genre Created");
        }

        public ServiceBase Update(genre record)
        {
            if (_db.genre.Any(s => s.id != record.id && s.name.ToUpper() == record.name.ToUpper().Trim()))
                return Error("A Genre with same name already exists.");
            var entity = _db.genre.SingleOrDefault(s => s.id != record.id);
            if (entity == null)
                return Error("A Genre does not exist.");
            entity.name = record.name?.Trim();
            _db.genre.Update(entity);
            _db.SaveChanges();
            return Success("Genre Updated");
            
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.genre.Include(s => s.id).SingleOrDefault(s => s.id == id);
            if (entity == null)
                return Error("A Genre does not exist.");
            if (entity.moviegenre.Any())
                return Error("A Genre has relational movies!");
            _db.genre.Remove(entity);
            _db.SaveChanges();
            return Success("Genre Deleted");
        }
    }
}