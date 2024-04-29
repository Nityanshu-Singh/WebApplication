using Question2.Models.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Question2.Models.Repo
{

    public class MovRepo<T> : IMovRepo<T> where T : class
    {
        MoviesDB db;
        DbSet<T> dbset;


        public void MovieRepository()
        {
            db = new MoviesDB();
            dbset = db.Set<T>();
        }
        public void Delete(object Id)
        {
            T getmodel = dbset.Find(Id);
            dbset.Remove(getmodel);
        }

        public IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public T GetById(object Id)
        {
            return dbset.Find(Id);
        }

        public void Insert(T obj)
        {
            dbset.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}