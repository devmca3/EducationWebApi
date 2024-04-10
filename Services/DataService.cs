using System;
using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.Services
{
    public static class DbContextExtensions
    {
        public static DataSet DbDataSet(this DbContext context,
           string sqlQuery, List<SqlParameter> parameters)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(context.Database.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(command))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = sqlQuery;
                            if (parameters != null)
                            {
                                foreach (var item in parameters)
                                {
                                    command.Parameters.Add(item);
                                }
                            }
                            ds = new DataSet();
                            sda.Fill(ds);
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }
            }
            
            return ds;
        }
    }
}

