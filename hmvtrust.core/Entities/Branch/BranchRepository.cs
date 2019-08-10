using hmvtrust.core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class BranchRepository : IBranchRepository
    {
        List<Branch> branches = null;

        public BranchRepository()
        {
            branches = this.GetBranches();
        }

        public List<Branch> GetBranches()
        {
            if (branches == null)
            {
                var expression = PredicateBuilder.Create<Branch>(d => d.DeletedDate == null);
                branches = DbStore.ExecuteeExpressionList<Branch>(expression);
            }

            branches.Sort();
            return branches;
        }

        public Branch Add(Branch t)
        {
            if (!IsDuplicate(t))
            {
                t.Id = 0;
                var ret = DbStore.Save<Branch>(t);

                if (ret != null)
                    branches.Add(ret);

                return ret;
            }
            else
                throw new DuplicateItemException("Branchname / Branchcode already exists");
        }

        public void Delete(long id)
        {
            Branch r = Get(id);
            DbStore.Delete<Branch>(r);
            branches.Remove(r);
        }

        public Branch Get(long id)
        {
            return branches.Find(d => d.Id == id);
        }

        public List<Branch> Get()
        {
            return branches;
        }

        public Branch Get(Expression<Func<Branch, bool>> expression)
        {
            return branches.Find(expression.Compile().Invoke);
        }

        public List<Branch> GetList(Expression<Func<Branch, bool>> expression)
        {
            return branches.FindAll(expression.Compile().Invoke);
        }

        public Branch Update(Branch t)
        {
            Branch f = Get(t.Id);

            if (f == null)
                throw new ArgumentNullException($"Branch with id {t.Id} does not exist or its deleted");

            branches.Remove(t);

            if (!IsDuplicate(t))
            {
                if (t.Id <= 0)
                    throw new ArgumentException("Invalid Branch Id");

                f.BranchName = t.BranchName;
                f.BranchCode = t.BranchCode;
                f.Location = t.Location;
                f.PinCode = t.PinCode;

                f = DbStore.Save<Branch>(f);

                if (f != null)
                {
                    branches.Remove(t);
                    branches.Add(f);
                }

                return f;
            }
            else
            {
                branches.Add(f);
                throw new DuplicateItemException("Branchname / Branchcode already exists");
            }
        }

        bool IsDuplicate(Branch r)
        {
            return branches.Exists(d => (d.BranchName == r.BranchName || d.BranchCode == r.BranchCode));
        }
    }
}
