using course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course.Repository
{
    class РасходыRepository : IRepository<Расходы>
    {
        private ModelDATABASE db;

        public РасходыRepository(ModelDATABASE context)
        {
            this.db = context;
        }

        public IEnumerable<Расходы> GetAll()
        {
            return db.Расходы;
        }

        public Расходы Get(int id)
        {
            return db.Расходы.Find(id);
        }

        public void Create(Расходы book)
        {
            db.Расходы.Add(book);
        }

        public void Update(Расходы book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Расходы book = db.Расходы.Find(id);
            if (book != null)
                db.Расходы.Remove(book);
        }
    }
}
