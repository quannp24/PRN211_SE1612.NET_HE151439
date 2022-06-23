using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//author: Phan Quân

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {

        public void DeleteMember(int memberID) => MemberDAO.Instance.Remove(memberID);

        public MemberObject GetMemberByID(int memberID) => MemberDAO.Instance.GetMemberByID(memberID);
        public MemberObject GetMemberByAccount(string email,string password) => MemberDAO.Instance.GetMemberByAccount(email,password);


        public IEnumerable<MemberObject> GetMemberObjects() => MemberDAO.Instance.GetMemberList;

        public void InsertMember(MemberObject member) => MemberDAO.Instance.AddNew(member);

        public void UpdateMember(MemberObject member) => MemberDAO.Instance.Update(member);

        public IEnumerable<MemberObject> DescSortListByName() => MemberDAO.Instance.DescSortListByName();
        public IEnumerable<MemberObject> DescSortListByCity() => MemberDAO.Instance.DescSortListByCity();
        public IEnumerable<MemberObject> DescSortListByCountry() => MemberDAO.Instance.DescSortListByCountry();
        public IEnumerable<MemberObject> GetMembersByID(int memberID) => MemberDAO.Instance.GetMembersByID(memberID);
        public IEnumerable<MemberObject> GetMembersByName(string memberName) => MemberDAO.Instance.GetMembersByName(memberName);
    }
}
