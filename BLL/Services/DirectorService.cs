using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;
public interface IDirectorService
{
    public IQueryable<DirectorModel> Query();
    public ServiceBase Create(Director record);
    public ServiceBase Update(Director record);
    public ServiceBase Delete(int id);
}

public class DirectorService: ServiceBase, IService<Director, DirectorModel>, IDirectorService
{
    
    public DirectorService(Db db) : base(db)
    {
    }

    public IQueryable<DirectorModel> Query()
    {
        return _db.director.OrderByDescending(d => d.isretired).ThenBy(d => d.name).ThenBy(d => d.surname)
            .Include(d => d.movie).Select(d => new DirectorModel() {Record = d});
    }

    // public Service Create(DirectorModel model)
    // {
    //     if(_db.director.Any(d => d.isretired == model.Record.isretired && d.name.ToLower() == model.Record.name.ToLower().Trim()))
    //         return Error("This Director already exists.");
    //     _db.director.Add(model.Record);
    //     _db.SaveChanges();
    //     return Success("Director added by " + model.Developer);
    // }
    
    public ServiceBase Create(Director entity)
    {
        if(_db.director.Any(d => d.name.ToLower() == entity.name.ToLower().Trim() && d.surname.ToLower().Trim() == entity.surname.ToLower().Trim()))
            return Error("This Director already exists.");
        _db.director.Add(entity);
        _db.SaveChanges();
        return Success("Director added.");
    }

    public ServiceBase Update(Director entity)
    {
        if(_db.director.Any(d => d.id != entity.id && d.name.ToLower() == entity.name.ToLower().Trim()))
            return Error("This Director already exists.");
        var rec = _db.director.SingleOrDefault(d=>d.id == entity.id );
        if(rec is null)
            return Error("Director not found.");
        rec.name = entity.name.Trim();
        rec.surname = entity.surname.Trim();
        rec.isretired = entity.isretired;
        _db.director.Update(rec);
        _db.SaveChanges();
        return Success("Director updated.");
    }

    public ServiceBase Delete(int id)
    {
        var rec = _db.director.Include(d=>d.movie).SingleOrDefault(d=> d.id == id);
        if(rec == null)
            return Error("Director not found.");
        if(rec.movie.Any())
            return Error("Director has movie records.");
        _db.director.Remove(rec);
        _db.SaveChanges();
        return Success("Director deleted.");
    }
}