using course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course.Repository
{
    class UserRepository : IRepository<User>
    {
        private ModelDATABASE db;

        public UserRepository(ModelDATABASE context)
        {
            this.db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User book)
        {
            db.Users.Add(book);
        }

        public void Update(User book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User book = db.Users.Find(id);
            if (book != null)
                db.Users.Remove(book);
        }
    }
}
