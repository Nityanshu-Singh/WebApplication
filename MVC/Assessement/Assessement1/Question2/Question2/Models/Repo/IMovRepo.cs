using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Question2.Models.Repository
{
    public interface IMovRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object Id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(Object Id);
        void Save();
    }
}