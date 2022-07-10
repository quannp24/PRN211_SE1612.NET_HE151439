using BusinessObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO : DBContext
    {
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

        public IEnumerable<MemberObject> GetMemberList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "SELECT [MemberId],[Email],[CompanyName],[City],[Country],[Password] FROM [dbo].[Member]";
            var mems = new List<MemberObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    mems.Add(new MemberObject
                    {
                        MemberID = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        Company = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        Country = dataReader.GetString(4),
                        Password = dataReader.GetString(5)
                    });
                }
            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return mems;
        }

      

        public MemberObject GetMemberByID(int memberID)
        {
            MemberObject mem = null;
            IDataReader dataReader = null;
            string SQLSelect = "SELECT [MemberId],[Email],[CompanyName],[City],[Country],[Password] FROM [dbo].[Member]" +
                " where MemberId=@MemberId";
            try
            {
                var param = dataProvider.CreateParameter("@MemberId", 4, memberID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    mem = new MemberObject
                    {
                        MemberID = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        Company = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        Country = dataReader.GetString(4),
                        Password = dataReader.GetString(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return mem;
        }

        public MemberObject GetMemberByAccount(string email,string password)
        {
            MemberObject mem = null;
            IDataReader dataReader = null;
            string SQLSelect = "SELECT [MemberId],[Email],[CompanyName],[City],[Country],[Password] FROM [dbo].[Member]" +
                " where Email=@email and Password=@password";
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add( dataProvider.CreateParameter("@email", 70, email, DbType.String));
                parameters.Add( dataProvider.CreateParameter("@password", 50, password, DbType.String));
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, parameters.ToArray());
                if (dataReader.Read())
                {
                    mem = new MemberObject
                    {
                        MemberID = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        Company = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        Country = dataReader.GetString(4),
                        Password = dataReader.GetString(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return mem;
        }

        public void AddNew(MemberObject member)
        {
            try
            {
                MemberObject mem = GetMemberByID(member.MemberID);
                if (mem == null)
                {
                    string SQLInsert = "INSERT INTO [dbo].[Member] ([Email],[CompanyName],[City],[Country],[Password]) VALUES " +
                        "(@Email,@CompanyName,@City,@Country,@Password)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter(("@CompanyName"), 50, member.Company, DbType.String));
                    parameters.Add(dataProvider.CreateParameter(("@City"), 50, member.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter(("@Country"), 50, member.Country, DbType.String));
                    parameters.Add(dataProvider.CreateParameter(("@Password"), 50, member.Password, DbType.String));
                    parameters.Add(dataProvider.CreateParameter(("@Email"), 70, member.Email, DbType.String));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());

                }
                else
                {
                    throw new Exception("The mem is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(MemberObject mem)
        {
            try
            {
                MemberObject member = GetMemberByID(mem.MemberID);
                if (member != null)
                {
                    string SQLUpdate = "Update Member set Email=@Email,CompanyName=@CompanyName," +
                        "City=@City,Country=@Country,Password=@Password where MemberID=@MemberID";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter(("@MemberID"), 4, mem.MemberID, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter(("@Email"), 70, mem.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter(("@CompanyName"), 50, mem.Company, DbType.String));
                    parameters.Add(dataProvider.CreateParameter(("@City"), 50, mem.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter(("@Country"), 50, mem.Country, DbType.String));
                    parameters.Add(dataProvider.CreateParameter(("@Password"), 50, mem.Password, DbType.String));
                    dataProvider.Update(SQLUpdate, CommandType.Text, parameters.ToArray());

                }
                else
                {
                    throw new Exception("The mem is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Remove(int memberID)
        {
            try
            {
                MemberObject mem = GetMemberByID(memberID);
                if (mem != null)
                {
                    string SQLDelete = "Delete Member where MemberID=@MemberID";
                    var param = dataProvider.CreateParameter("@MemberID", 4, memberID, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("The mem does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
