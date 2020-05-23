using course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course.Repository
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
    class DateRepository:IRepository<Date>
    {
        private ModelDATABASE db;

        public DateRepository(ModelDATABASE context)
        {
            this.db = context;
        }

        public IEnumerable<Date> GetAll()
        {
            return db.Dates;
        }

        public Date Get(int id)
        {
            return db.Dates.Find(id);
        }

        public void Create(Date book)
        {
            db.Dates.Add(book);
        }

        public void Update(Date book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Date book = db.Dates.Find(id);
            if (book != null)
                db.Dates.Remove(book);
        }
    }
}
