using Microsoft.Data.SqlClient;
using System;

namespace Procedure
{
    class Program
    {
        static(int OutputValue,int ReturnValue) CountProductsByCategoryID(int CategoryID)
        {
            (int OutputValue, int ReturnValue) result;
            string ConnectionString = "Server=(local);uid=sa;pwd=12345678;database=MyStore";
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            CountProductsUsingOutputValue();
            CountProductsUsingReturnValue();
            void CountProductsUsingOutputValue()
            {
                command.CommandText = "spCountProductsUsingOutputValue";
                command.Parameters.AddWithValue("@CategoryID", CategoryID);
                command.Parameters.Add("@NumberOfProducts", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
                command.ExecuteNonQuery();
                result.OutputValue = (int)command.Parameters["@NumberOfProducts"].Value;

            }
            void CountProductsUsingReturnValue()
            {
                command.CommandText = "spCountProductsUsingReturnValue";
                command.Parameters["@NumberOfProducts"].Direction = System.Data.ParameterDirection.ReturnValue;
                command.ExecuteNonQuery();
                result.ReturnValue = (int)command.Parameters["@NumberOfProducts"].Value;
            }
            connection.Close();
            return result;
        }
        static void Main(string[] args)
        {
            int CategoryID = 1;
            var result = CountProductsByCategoryID(CategoryID);
            Console.WriteLine($"Number of products by CategoryID:{CategoryID}");
            Console.WriteLine($"-->OutputValue:{result.OutputValue}, ReturnValue;{result.ReturnValue}");
            CategoryID = 3;
            Console.WriteLine($"Number of products by CategoryID:{CategoryID}");
            result = CountProductsByCategoryID(CategoryID);
            Console.WriteLine($"-->OutputValue:{result.OutputValue},ReturnValue{result.ReturnValue}");
            Console.ReadLine();
        }
    }
}
