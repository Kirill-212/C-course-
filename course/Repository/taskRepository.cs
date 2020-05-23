using course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course.Repository
{

    class taskRepository : IRepository<task>
    {
        private ModelDATABASE db;

        public taskRepository(ModelDATABASE context)
        {
            this.db = context;
        }

        public IEnumerable<task> GetAll()
        {
            return db.tasks;
        }

        public task Get(int id)
        {
            return db.tasks.Find(id);
        }

        public void Create(task book)
        {
            db.tasks.Add(book);
        }

        public void Update(task book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            task book = db.tasks.Find(id);
            if (book != null)
                db.tasks.Remove(book);
        }
    }
}
