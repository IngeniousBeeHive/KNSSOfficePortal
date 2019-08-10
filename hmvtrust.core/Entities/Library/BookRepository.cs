using hmvtrust.core.Entities;
using hmvtrust.core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class BookRepository : IBookRepository
    {
        List<Book> bookdetails = null;

        public BookRepository()
        {
            bookdetails = this.GetBookDetails();
        }
        public Book Add(Book t)
        {
            if (!IsDuplicate(t))
            {
                t.Id = 0;
                var ret = DbStore.Save<Book>(t);

                if (ret != null)
                    bookdetails.Add(ret);

                return ret;
            }
            else
                throw new DuplicateItemException("Book Category already exists");
        }
        bool IsDuplicate(Book r)
        {
            return bookdetails.Exists(d => (d.BookName == r.BookName && d.BookNo == r.BookNo));
        }

        public void Delete(long id)
        {
            Book r = Get(id);
            DbStore.Delete<Book>(r);
            bookdetails.Remove(r);
        }

        public Book Get(long id)
        {
            return bookdetails.Find(d => d.Id == id);
        }

        public List<Book> Get()
        {
            return bookdetails;
        }

        public Book Get(Expression<Func<Book, bool>> expression)
        {
            return bookdetails.Find(expression.Compile().Invoke);
        }

        public List<Book> GetBookDetails()
        {
            if (bookdetails == null)
            {
                var expression = PredicateBuilder.Create<Book>(d => d.DeletedDate == null);
                bookdetails = DbStore.ExecuteeExpressionList<Book>(expression);
            }
            return bookdetails;
        }

        public List<Book> GetList(Expression<Func<Book, bool>> expression)
        {
            return bookdetails.FindAll(expression.Compile().Invoke);
        }

        public Book Update(Book t)
        {
            string cName = t.CategoryId.CategoryName;

            if (t.Id <= 0)
                throw new ArgumentException("Invalid Book Item Id");

            Book f = Get(t.Id);

            if (f == null)
                throw new ArgumentNullException($"Book Item with id {t.Id} does not exist or its deleted");


                f.BookNo = t.BookNo;
                f.BookName = t.BookName;
                f.AuthorName = t.AuthorName;
                f.CategoryId = t.CategoryId;
           
                f = DbStore.Save<Book>(f);              

                return f;
            
        }
    }
}
