using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace PMS
{
    public class ProductDataManager
    {
        string constr = ConfigurationManager.ConnectionStrings["DbPMS"].ConnectionString;
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader objReader;
        Product objCurrentProduct = new Product();
        int iResult = 0;
        int result = 0;
        public ProductDataManager(Product objProduct)
        {
            this.objCurrentProduct = objProduct;
        }

        public ProductDataManager()
        {

        }
        public int Save()
        {

            string InsertQuery = "INSERT INTO pms_product (Product_Name,Product_Description,Product_Category,Product_MRP) VALUES (@Product_Name, @Product_Description,@Product_Category,@Product_MRP)";

            string UpdateQuery = "update pms_product set  " +
                "Product_Name=@Product_Name," +
                "Product_Description=@Product_Description," +
                "Product_Category=@Product_Category," +
                "Product_MRP=@Product_MRP" +
                " where Product_ID=@Product_ID";

            string sQuery = objCurrentProduct.Product_Id > 0 ? UpdateQuery : InsertQuery;

            iResult = ExecuteQuery(sQuery);

            return iResult > 0 ? iResult : 0;
        }

        public int Delete()
        {
            string sQuery = "Delete from pms_product where Product_Id='" + objCurrentProduct.Product_Id + "'";
            iResult = ExecuteQuery(sQuery);
            return iResult > 0 ? iResult : 0;
        }

        public Product GetByID(int nID)
        {
            Product curentObjProduct = new Product();

            try
            {
                con = new MySqlConnection(constr);


                string sqlQuery = "select * from pms_product where Product_Id=" + nID;
                cmd = new MySqlCommand(sqlQuery);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                con.Open();
                objReader = cmd.ExecuteReader();
                if (objReader.Read())
                {
                    curentObjProduct.Product_Id = Convert.ToInt32(objReader.GetValue(0).ToString());
                    curentObjProduct.Product_Name = objReader.GetValue(1).ToString();
                    curentObjProduct.Product_Description = objReader.GetValue(2).ToString();
                    curentObjProduct.Product_Category = Convert.ToInt32(objReader.GetValue(3).ToString());
                    curentObjProduct.Product_MRP = Convert.ToDouble(objReader.GetValue(4).ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return curentObjProduct;
        }

        public List<Product> GetAll()
        {
            List<Product> lstProducts = new List<Product>();
            try
            {
                con = new MySqlConnection(constr);
                cmd = new MySqlCommand("SELECT * FROM pms_product order by Product_Id desc");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                con.Open();
                objReader = cmd.ExecuteReader();
                while (objReader.Read())
                {
                    Product objProduct = new Product();
                    objProduct.Product_Id = Convert.ToInt32(objReader.GetValue(0));
                    objProduct.Product_Name = objReader.GetValue(1).ToString();
                    objProduct.Product_Description = objReader.GetValue(2).ToString();
                    objProduct.Product_Category = Convert.ToInt32(objReader.GetValue(3));
                    objProduct.Product_MRP = Convert.ToDouble(objReader.GetValue(4));
                    lstProducts.Add(objProduct);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return lstProducts;
        }

        public int ExecuteQuery(string sQuery)
        {
            try
            {
                con = new MySqlConnection(constr);

                cmd = new MySqlCommand(sQuery);

                MySqlDataAdapter sda = new MySqlDataAdapter();

                cmd.Parameters.AddWithValue("@Product_ID", objCurrentProduct.Product_Id);
                cmd.Parameters.AddWithValue("@Product_Name", objCurrentProduct.Product_Name);
                cmd.Parameters.AddWithValue("@Product_Description", objCurrentProduct.Product_Description);
                cmd.Parameters.AddWithValue("@Product_Category", objCurrentProduct.Product_Category);
                cmd.Parameters.AddWithValue("@Product_MRP", objCurrentProduct.Product_MRP);

                cmd.Connection = con;
                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }


            return result;
        }
    }
}