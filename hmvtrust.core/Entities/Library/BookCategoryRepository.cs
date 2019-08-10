using hmvtrust.core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class BookCategoryRepository : IBookCategoryRepository
    {
        List<BookCategory> bookCategory = null;

        public BookCategoryRepository()
        {
            bookCategory = this.GetBookCategory();
        }

        public BookCategory Add(BookCategory t)
        {
            if (!IsDuplicate(t))
            {
                t.Id = 0;
                var ret = DbStore.Save<BookCategory>(t);

                if (ret != null)
                    bookCategory.Add(ret);

                return ret;
            }
            else
                throw new DuplicateItemException("Book Category already exists");
        }

        bool IsDuplicate(BookCategory r)
        {
            return bookCategory.Exists(d => (d.CategoryName == r.CategoryName && d.Quantity == r.Quantity));
        }

        public void Delete(long id)
        {
            BookCategory r = Get(id);
            DbStore.Delete<BookCategory>(r);
            bookCategory.Remove(r);
        }

        public BookCategory Get(long id)
        {
            return bookCategory.Find(d => d.Id == id);
        }

        public List<BookCategory> Get()
        {
            bookCategory.Distinct();
            return bookCategory;
        }

        public List<BookCategory> GetBookCategory()
        {
            if (bookCategory == null)
            {
                var expression = PredicateBuilder.Create<BookCategory>(d => d.DeletedDate == null);
                bookCategory = DbStore.ExecuteeExpressionList<BookCategory>(expression);
            }
            bookCategory.Distinct();
            return bookCategory;
        }

        public BookCategory Get(Expression<Func<BookCategory, bool>> expression)
        {
            return bookCategory.Find(expression.Compile().Invoke);
        }



        public List<BookCategory> GetList(Expression<Func<BookCategory, bool>> expression)
        {
            return bookCategory.FindAll(expression.Compile().Invoke);
        }

        public BookCategory Update(BookCategory t)
        {
            BookCategory f = Get(t.Id);

            if (f == null)
                throw new ArgumentNullException($"BookCategory Item with id {t.Id} does not exist or its deleted");

            bookCategory.Remove(t);

            if (!IsDuplicate(t))
            {
                if (t.Id <= 0)
                    throw new ArgumentException("Invalid BookCategory Item Id");

                f.CategoryName = t.CategoryName;
                f.Quantity = t.Quantity;


                f = DbStore.Save<BookCategory>(f);
               
                return f;
            }
            else
            {
                bookCategory.Add(f);
                throw new DuplicateItemException("BookCategory Item already exists");
            }
        }       
    }
}
