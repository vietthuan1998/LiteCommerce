using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class CountryDAL: ICountryDAL   
    {
        private string connectionString;
        public CountryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(Country data)
        {
            int countryID = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Countries
                                          (
	                                          CountryName,
                                              Code
                                          )
                                          VALUES
                                          (
	                                          @CountryName,
                                              @Code
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CountryName", data.CountryName);
                cmd.Parameters.AddWithValue("@Code", data.Code);

                countryID = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return countryID;
        }

        public int Count(string searchValue)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"select count(*) from Countries 
                                    where @searchValue = N'' or CountryName like @searchValue
                                            or Code like @searchValue";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return count;
        }

        public int Delete(int[] countryIDs)
        {
            int countDeleted = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Countries
                                            WHERE(CountryID = @countryID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@countryID", SqlDbType.Int);
                foreach (int shipperID in countryIDs)
                {
                    cmd.Parameters["@countryID"].Value = shipperID;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        countDeleted += 1;
                }

                connection.Close();
            }
            return countDeleted;
        }

        public Country Get(int countryID)
        {
            Country data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Countries WHERE CountryID = @CountryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CountryID", countryID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Country()
                        {
                            CountryID = Convert.ToInt32(dbReader["CountryID"]),
                            CountryName = Convert.ToString(dbReader["CountryName"]),
                            Code = Convert.ToString(dbReader["Code"])
                        };
                    }
                }

                connection.Close();
            }
            return data;
        }

        public List<Country> List()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // tạo câu lệnh query
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from Countries order by CountryName";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Country()
                        {
                            CountryID = Convert.ToInt32(dbReader["CountryID"]),
                            CountryName = Convert.ToString(dbReader["CountryName"]),
                            Code = Convert.ToString(dbReader["Code"])
                        });
                    }
                }

                connection.Close();
            }
            return data;
        }

        public List<Country> List(int page, int pageSize, string searchValue)
        {
            List<Country> data = new List<Country>();
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                //Tạo lệnh thực thi truy vấn
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from
                                    (
                                        select ROW_NUMBER() over(order by CountryName) as RowNumber,
                                                Countries.*
                                        from Countries
                                        where @searchValue = N'' or CountryName like @searchValue or Code like @searchValue
                                    ) as t
                                    where t.RowNumber between(@page -1)*@pageSize + 1 and @page *@pageSize
                                    order by t.RowNumber";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);


                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Country()
                        {
                            CountryID = Convert.ToInt32(dbReader["CountryID"]),
                            CountryName = Convert.ToString(dbReader["CountryName"]),
                            Code = Convert.ToString(dbReader["Code"])
                        });
                    }
                }

                connection.Close();
            }

            return data;
        }

        public bool Update(Country data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Countries
                                           SET CountryName = @CountryName,
                                               Code = @Code 
                                          WHERE CountryID = @CountryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CountryID", data.CountryID);
                cmd.Parameters.AddWithValue("@CountryName", data.CountryName);
                cmd.Parameters.AddWithValue("@Code", data.Code);


                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}
