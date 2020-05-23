using course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course.Repository
{
    class UnitOfWork : IDisposable
    {
        private ModelDATABASE db = ModelDATABASE.getInstance();
        private DateRepository dateRepository;
        private taskRepository TaskRepository;
        private UserRepository userRepository;
        private РасписаниеRepository расписаниеRepository;
        private РасходыRepository расходыRepository;
        public DateRepository Dates
        {
            get
            {
                if (dateRepository == null)
                    dateRepository = new DateRepository(db);
                return dateRepository;
            }
        }

        public taskRepository Tasks
        {
            get
            {
                if (TaskRepository == null)
                    TaskRepository = new taskRepository(db);
                return TaskRepository;
            }
        }

        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public РасписаниеRepository Расписание
        {
            get
            {
                if (расписаниеRepository == null)
                    расписаниеRepository = new РасписаниеRepository(db);
                return расписаниеRepository;
            }
        }
        public РасходыRepository Расходы
        {
            get
            {
                if (расходыRepository == null)
                    расходыRepository = new РасходыRepository(db);
                return расходыRepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
