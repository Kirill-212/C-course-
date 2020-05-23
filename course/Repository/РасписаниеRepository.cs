using course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course.Repository
{
    class РасписаниеRepository : IRepository<Расписание>
    {
        private ModelDATABASE db;

        public РасписаниеRepository(ModelDATABASE context)
        {
            this.db = context;
        }

        public IEnumerable<Расписание> GetAll()
        {
            return db.Расписание;
        }

        public Расписание Get(int id)
        {
            return db.Расписание.Find(id);
        }

        public void Create(Расписание book)
        {
            db.Расписание.Add(book);
        }

        public void Update(Расписание book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Расписание book = db.Расписание.Find(id);
            if (book != null)
                db.Расписание.Remove(book);
        }
    }
}
