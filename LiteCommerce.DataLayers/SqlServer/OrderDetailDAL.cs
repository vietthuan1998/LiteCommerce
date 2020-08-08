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
    public class OrderDetailDAL: IOrderDetailDAL
    {
        private string connectionString;
        public OrderDetailDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool Add(OrderDetail data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO OrderDetails
                                          (
	                                          OrderID,
	                                          ProductID,
	                                          UnitPrice,
	                                          Quantity,
                                              Discount
                                          )
                                          VALUES
                                          (
	                                          @OrderID,
	                                          @ProductID,
	                                          @UnitPrice,
	                                          @Quantity,
                                              @Discount
                                          )";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = data.OrderID;
                cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = data.ProductID;
                cmd.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = data.UnitPrice;
                cmd.Parameters.Add("@Quantity", SqlDbType.SmallInt).Value = data.Quantity;
                cmd.Parameters.Add("@Discount", SqlDbType.Real).Value = data.Discount;
                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }
            return rowsAffected > 0;
        }

        public int Delete(string[] detailIDs)
        {
            int countDeleted = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM OrderDetails
                                            WHERE OrderID = @OrderID AND ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@OrderID", SqlDbType.Int);
                cmd.Parameters.Add("@ProductID", SqlDbType.Int);
                foreach (string detailID in detailIDs)
                {
                    string[] id = detailID.Split(',');
                    cmd.Parameters["@OrderID"].Value = Convert.ToInt32(id[0]);
                    cmd.Parameters["@ProductID"].Value = Convert.ToInt32(id[1]);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        countDeleted += 1;
                }

                connection.Close();
            }
            return countDeleted;
        }

        public OrderDetail Get(int orderID, int productID)
        {
            OrderDetail data = new OrderDetail();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM OrderDetails WHERE OrderID = @orderID AND ProductID = @productID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@orderID", orderID);
                cmd.Parameters.AddWithValue("@productID", productID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new OrderDetail()
                        {
                            OrderID = Convert.ToInt32(dbReader["OrderID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            UnitPrice = Convert.ToDouble(dbReader["UnitPrice"]),
                            Quantity = Convert.ToInt32(dbReader["Quantity"]),
                            Discount = Convert.ToDouble(dbReader["Discount"])
                        };
                    }
                }

                connection.Close();
            }
            return data;
        }

        public List<OrderDetail> List(int orderID)
        {
            List<OrderDetail> data = new List<OrderDetail>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //Tạo lệnh thực thi truy vấn dữ liệu
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *from OrderDetails where OrderID = @orderID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@orderID", orderID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new OrderDetail()
                        {
                            OrderID = Convert.ToInt32(dbReader["OrderID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            UnitPrice = Convert.ToDouble(dbReader["UnitPrice"]),
                            Quantity = Convert.ToInt32(dbReader["Quantity"]),
                            Discount = Convert.ToDouble(dbReader["Discount"])
                        });
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(OrderDetail data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE OrderDetails
                                           SET UnitPrice = @UnitPrice 
                                              ,Quantity = @Quantity
                                              ,Discount = @Discount
                                          WHERE OrderID = @OrderID AND ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@OrderID", data.OrderID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@UnitPrice", data.UnitPrice);
                cmd.Parameters.AddWithValue("@Quantity", data.Quantity);
                cmd.Parameters.AddWithValue("@Discount", data.Discount);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}
