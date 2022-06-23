using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//author: Phan Quân

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> GetMemberObjects();
        MemberObject GetMemberByID(int memberID);
        MemberObject GetMemberByAccount(string email,string password);
        void InsertMember(MemberObject member);
        void DeleteMember(int memberID);
        void UpdateMember(MemberObject member);
        IEnumerable<MemberObject> DescSortListByName();
        IEnumerable<MemberObject> DescSortListByCity();
        IEnumerable<MemberObject> DescSortListByCountry();
        IEnumerable<MemberObject> GetMembersByID(int memberID);
        IEnumerable<MemberObject> GetMembersByName(string memberName);
    }
}
