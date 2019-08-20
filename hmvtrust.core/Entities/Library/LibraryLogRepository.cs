using hmvtrust.core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class LibraryLogRepository : ILibraryLogRepository
    {
        List<LibraryLog> libraryLogs = null;

        public LibraryLogRepository()
        {
            libraryLogs = this.GetLibraryLog();
        }
        public LibraryLog Add(LibraryLog t)
        {
            if (!IsDuplicate(t))
            {
                t.Id = 0;
                var ret = DbStore.Save<LibraryLog>(t);

                if (ret != null)
                    libraryLogs.Add(ret);

                return ret;
            }
            else
                throw new DuplicateItemException("Log details already exists");
        }
        bool IsDuplicate(LibraryLog r)
        {
            return libraryLogs.Exists(d => (d.BookNo == r.BookNo && d.Id == r.Id && d.CheckedOutDate == r.CheckedOutDate));
        }
        public void Delete(long id)
        {
            LibraryLog r = Get(id);
            DbStore.Delete<LibraryLog>(r);
            libraryLogs.Remove(r);
        }

        public LibraryLog Get(long id)
        {
            return libraryLogs.Find(d => d.Id == id);
        }

        public List<LibraryLog> Get()
        {
            return libraryLogs;
        }

        public LibraryLog Get(Expression<Func<LibraryLog, bool>> expression)
        {
            return libraryLogs.Find(expression.Compile().Invoke);
        }

        public List<LibraryLog> GetLibraryLog()
        {
            if (libraryLogs == null)
            {
                var expression = PredicateBuilder.Create<LibraryLog>(d => d.DeletedDate == null);
                libraryLogs = DbStore.ExecuteeExpressionList<LibraryLog>(expression);
            }
            libraryLogs.Distinct();
            return libraryLogs;
        }

        public List<LibraryLog> GetList(Expression<Func<LibraryLog, bool>> expression)
        {
            return libraryLogs.FindAll(expression.Compile().Invoke);
        }

        public LibraryLog Update(LibraryLog t)
        {
            LibraryLog f = Get(t.Id);

            if (f == null)
                throw new ArgumentNullException($"Log Details with id {t.Id} does not exist or its deleted");

            libraryLogs.Remove(t);

            
                if (t.Id <= 0)
                    throw new ArgumentException("Invalid Log Details Item Id");

                f.BookNo = t.BookNo;
                f.BookName = t.BookName;
                f.MemberName = t.MemberName;
                f.MobileNo = t.MobileNo;
                f.Address = t.Address;
                f.CheckedOutDate = t.CheckedOutDate;
                f.ReturnedDate = t.ReturnedDate;



            f = DbStore.Save<LibraryLog>(f);
           
            return f;
            
        }
    }
}
