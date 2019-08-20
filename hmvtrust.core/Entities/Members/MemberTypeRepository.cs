using hmvtrust.core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class MemberTypeRepository : IMemberTypeRepository
    {
        List<MemberType> memberType = null;

        public MemberTypeRepository()
        {
            memberType = this.GetMemberType();
        }
        public MemberType Add(MemberType t)
        {
            if (!IsDuplicate(t))
            {
                t.Id = 0;
                var ret = DbStore.Save<MemberType>(t);

                if (ret != null)
                    memberType.Add(ret);

                return ret;
            }
            else
                throw new DuplicateItemException("Book  already exists");
        }
        bool IsDuplicate(MemberType r)
        {
            return memberType.Exists(d => (d.MemberTypeName == r.MemberTypeName));
        }

        public void Delete(long id)
        {
            MemberType r = Get(id);
            DbStore.Delete<MemberType>(r);
            memberType.Remove(r);
        }

        public MemberType Get(long id)
        {
            return memberType.Find(d => d.Id == id);
        }

        public List<MemberType> Get()
        {           
            return memberType;
        }

        public MemberType Get(Expression<Func<MemberType, bool>> expression)
        {
            return memberType.Find(expression.Compile().Invoke);
        }

        public List<MemberType> GetList(Expression<Func<MemberType, bool>> expression)
        {
            return memberType.FindAll(expression.Compile().Invoke);
        }

        public List<MemberType> GetMemberType()
        {
            if (memberType == null)
            {
                var expression = PredicateBuilder.Create<MemberType>(d => d.DeletedDate == null);
                memberType = DbStore.ExecuteeExpressionList<MemberType>(expression);
            }
            return memberType;
        }

        public MemberType Update(MemberType t)
        {
           
            if (t.Id <= 0)
                throw new ArgumentException("Invalid Member Type Id");

            MemberType f = Get(t.Id);

            if (f == null)
                throw new ArgumentNullException($"Member Type with id {t.Id} does not exist or its deleted");


            f.MemberTypeName = t.MemberTypeName;
            f.AgeLimit = t.AgeLimit;

            f = DbStore.Save<MemberType>(f);

            return f;
        }
    }
}
