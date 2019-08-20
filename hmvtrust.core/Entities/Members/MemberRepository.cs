using hmvtrust.core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public class MemberRepository : IMemberRepository
    {
        List<Member> member = null;

        public MemberRepository()
        {
            member = this.GetMember();
        }
        public Member Add(Member t)
        {
            if (!IsDuplicate(t))
            {
                t.Id = 0;
                var ret = DbStore.Save<Member>(t);

                if (ret != null)
                    member.Add(ret);

                return ret;
            }
            else
                throw new DuplicateItemException("Member  already exists");
        }
        bool IsDuplicate(Member r)
        {
            return member.Exists(d => (d.FamilyNo == r.FamilyNo && d.MemberName == r.MemberName));
        }

        public void Delete(long id)
        {
            Member r = Get(id);
            DbStore.Delete<Member>(r);
            member.Remove(r);
        }

        public Member Get(long id)
        {
            return member.Find(d => d.Id == id);
        }

        public List<Member> Get()
        {           
            return member;
        }

        public Member Get(Expression<Func<Member, bool>> expression)
        {
            return member.Find(expression.Compile().Invoke);
        }

        public List<Member> GetList(Expression<Func<Member, bool>> expression)
        {
            return member.FindAll(expression.Compile().Invoke);
        }

        public List<Member> GetMember()
        {
            if (member == null)
            {
                var expression = PredicateBuilder.Create<Member>(d => d.DeletedDate == null);
                member = DbStore.ExecuteeExpressionList<Member>(expression);
            }
            return member;
        }

        public Member Update(Member t)
        {
            if (t.Id <= 0)
                throw new ArgumentException("Invalid Member Id");

            Member f = Get(t.Id);

            if (f == null)
                throw new ArgumentNullException($"Member with id {t.Id} does not exist or its deleted");         

            if((f.MemberName != t.MemberName) || (t.Photo is null))
            {
                f.Photo = f.Photo;
            }
            else if(f.Photo != t.Photo)
            {
                f.Photo = f.FamilyNo + "_" + f.MemberName +  ".jpg";
            }

            f.BranchId = t.BranchId;
            f.FamilyNo = t.FamilyNo;
            f.MemberTypeId = t.MemberTypeId;
            f.MemberName = t.MemberName;
            f.Gender = t.Gender;
            f.DateofBirth = t.DateofBirth;
            f.Age = t.Age;
            f.MobileNo = t.MobileNo;
            f.AlternateMobileNo = t.AlternateMobileNo;
            f.Email = t.Email;
            f.Status = t.Status;
            f.Address = t.Address;
            f.BloodGroup = t.BloodGroup;
            f.MemberTypeId.MemberTypeName = t.MemberTypeId.MemberTypeName;



            f = DbStore.Save<Member>(f);

            return f;
        }
    }
}
