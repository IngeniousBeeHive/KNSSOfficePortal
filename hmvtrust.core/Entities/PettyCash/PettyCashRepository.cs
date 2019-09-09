using hmvtrust.core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class PettyCashRepository : IPettyCashRepository
    {
        List<PettyCash> pettycashes = null ;

        public PettyCashRepository()
        {
            pettycashes = this.GetPettyCash();
        }
        public PettyCash Add(PettyCash t)
        {
            if (!IsDuplicate(t))
            {
                t.Id = 0;
                var ret = DbStore.Save<PettyCash>(t);

                if (ret != null)
                    pettycashes.Add(ret);

                return ret;
            }
            else
                throw new DuplicateItemException("Member  already exists");
        }
        bool IsDuplicate(PettyCash r)
        {
            return pettycashes.Exists(d => (d.Description == r.Description && d.Amount == r.Amount));
        }

        public void Delete(long id)
        {
            PettyCash r = Get(id);
            DbStore.Delete<PettyCash>(r);
            pettycashes.Remove(r);
        }

        public PettyCash Get(long id)
        {
            return pettycashes.Find(d => d.Id == id);
        }

        public List<PettyCash> Get()
        {
            return pettycashes;
        }

        public PettyCash Get(Expression<Func<PettyCash, bool>> expression)
        {
            return pettycashes.Find(expression.Compile().Invoke);
        }

        public List<PettyCash> GetList(Expression<Func<PettyCash, bool>> expression)
        {
            return pettycashes.FindAll(expression.Compile().Invoke);
        }

        public List<PettyCash> GetPettyCash()
        {
            if (pettycashes == null)
            {
                var expression = PredicateBuilder.Create<PettyCash>(d => d.DeletedDate == null);
                pettycashes = DbStore.ExecuteeExpressionList<PettyCash>(expression);
            }
            return pettycashes;
        }

        public PettyCash Update(PettyCash t)
        {
            if (t.Id <= 0)
                throw new ArgumentException("Invalid PettyCash Id");

            PettyCash f = Get(t.Id);

            if (f == null)
                throw new ArgumentNullException($"PettyCash with id {t.Id} does not exist or its deleted");

           
            f.Description = t.Description;
            f.Amount = t.Amount;
            f.ReceivedDate = t.ReceivedDate;
            f.AttachedFiles = t.AttachedFiles;

            f = DbStore.Save<PettyCash>(f);

            return f;
        }
    }
}
