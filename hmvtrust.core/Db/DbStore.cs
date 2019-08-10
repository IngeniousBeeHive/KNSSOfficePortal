using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace hmvtrust.core
{
    public class DbStore
    {
        public static T ExecuteExpressionFirst<T>(Expression<Func<T, bool>> expression) where T : AppBase
        {
            T t = default(T);

            var criteria = DetachedCriteria.For<T>().Add(NHibernate.Criterion.Expression.Where<T>(expression));
            using (ISession session = ConnectionManager.GetSessionFactory().OpenSession())
            {
                t = criteria.GetExecutableCriteria(session).SetFetchMode(typeof(T).Name, FetchMode.Join).UniqueResult<T>();
            }

            return t;
        }

        public static bool Any<T>(Expression<Func<T, bool>> expression) where T : AppBase
        {
            bool anyItem = false;
            using (ISession session = ConnectionManager.GetSessionFactory().OpenSession())
            {
                anyItem             = session.Query<T>().Any(expression);
            }

            return anyItem;
        }

        public static List<T> ExecuteeExpressionList<T>(Expression<Func<T, bool>> expression) where T : AppBase
        {
            List<T> t = null;

            var criteria = DetachedCriteria.For<T>().Add(NHibernate.Criterion.Expression.Where<T>(expression)).SetResultTransformer(CriteriaSpecification.DistinctRootEntity);
            using (ISession session = ConnectionManager.GetSessionFactory().OpenSession())
            {
                t = (List<T>)criteria.GetExecutableCriteria(session).List<T>();
            }

            return t;
        }

        public static T Save<T>(T t) where T : AppBase
        {
            T newObj = default(T);

            try
            {
                if (t.IsValid)
                {

                    using (ISession session = ConnectionManager.GetSessionFactory().OpenSession())
                    {
                        if (t.Id > 0)
                        {
                            t.ModifiedDate = DateTime.UtcNow;
                        }
                        else
                        {
                            t.CreatedDate = DateTime.UtcNow;
                        }

                        using (ITransaction trans = session.BeginTransaction())
                        {
                            session.SaveOrUpdate(t);
                            trans.Commit();

                            newObj = t;
                        }
                    }
                }
                else
                {
                    throw new core.ValidationException("Validation Error ", t.Errors());
                }

            }
            catch (Exception excp)
            {
                //Logger.Current.Fatal($"Error while saving {typeof(T)}", excp);
                throw;
            }

            return newObj;
        }

        public static void Delete<T>(T t) where T : AppBase
        {
            using (ISession session = ConnectionManager.GetSessionFactory().OpenSession())
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    t.DeletedDate = DateTime.UtcNow;

                    session.SaveOrUpdate(t);

                    trans.Commit();
                }
            }
        }

        public static T Get<T>(long id) where T : AppBase
        {
            T t = null;

            using (ISession session = ConnectionManager.GetSessionFactory().OpenSession())
            {
                t  = session.Get<T>(id);
            }

            return t;
        }
    }
}
