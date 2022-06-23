using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

//author: Phan Quân

namespace DataAccess
{
    public class MemberDAO
    {
        private static List<MemberObject> MemberList = new List<MemberObject>()
        {
            new MemberObject{MemberID=1, MemberName="Hoàng", City="Yên Bái",
                Country="Việt Nam",Email="huyhoang123@gmail.com",Password="123456"},
            new MemberObject{MemberID=2, MemberName="Quân", City="Hà Nội",
                Country="Mỹ",Email="quannp124@gmail.com",Password="quan123"},
            new MemberObject{MemberID=3, MemberName="Long", City="Hai Duong",
                Country="Singapo",Email="longtk12@gmail.com",Password="12345"},
            new MemberObject{MemberID=4, MemberName="Thăng", City="Hà Nội",
                Country="Trung Quốc",Email="tiktik22@gmail.com",Password="43214"},
            new MemberObject{MemberID=5, MemberName="Tiến", City="TP Hồ Chí Minh",
                Country="Bồ Đào Nha",Email="tiendp14@gmail.com",Password="123123"},
            new MemberObject{MemberID=6, MemberName="Anh Quân", City="Hà Giang",
                Country="Anh",Email="hoanglong42@gmail.com",Password="1234567"},
        };
        //using singleton pattern
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        //-----------------------------------------
        public List<MemberObject> GetMemberList => MemberList;

        //-------------------------------------------
        public MemberObject GetMemberByID(int memberID)
        {
            MemberObject member = MemberList.SingleOrDefault(x => x.MemberID == memberID);
            return member;
        }

        public List<MemberObject> GetMembersByID(int memberID)
        {
            var member = MemberList.Where(x => x.MemberID == memberID).ToList();
            return member;
        }

        public List<MemberObject> GetMembersByName(string memberName)
        {
            var member = MemberList.Where(x => x.MemberName.Contains(memberName)).ToList();
            return member;
        }

        public void AddNew(MemberObject member)
        {
            MemberObject mem = GetMemberByID(member.MemberID);
            if (mem == null)
            {
                MemberList.Add(member);
            }
            else
            {
                throw new Exception("Member is already exists.");
            }
        }

        public void Update(MemberObject member)
        {
            MemberObject m = GetMemberByID(member.MemberID);
            if (m != null)
            {
                var index = MemberList.IndexOf(m);
                MemberList[index] = member;
            }
            else
            {
                throw new Exception("Member does not already exists.");
            }
        }

        public void Remove(int memberID)
        {
            MemberObject m = GetMemberByID(memberID);
            if (m != null)
            {
                MemberList.Remove(m);
            }
            else
            {
                throw new Exception("Member does not already exists.");
            }
        }

        public List<MemberObject> DescSortListByName()
        {
            var mems = MemberList.OrderByDescending(x => x.MemberName).ToList();
            return mems;
            
        }

        public List<MemberObject> DescSortListByCity()
        {
            var mem = MemberList.OrderBy(x => x.City).ToList();
            return mem;

        }

        public List<MemberObject> DescSortListByCountry()
        {
            var mem = MemberList.OrderBy(x => x.Country).ToList();
            return mem;

        }

        public MemberObject GetMemberByAccount(string email,string password)
        {
            MemberObject member = MemberList.SingleOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
            return member;
        }

    }
}
