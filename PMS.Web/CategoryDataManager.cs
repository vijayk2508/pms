using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace PMS.Web
{
    public class CategoryDataManager
    {
        string constr = ConfigurationManager.ConnectionStrings["DbPMS"].ConnectionString;
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader objReader;
        Category objCurrentCategory = new Category();
        int iResult = 0;
        int result = 0;
        public CategoryDataManager(Category objCategory)
        {
            this.objCurrentCategory = objCategory;
        }

        public CategoryDataManager()
        {
            
        }
        public int Save()
        {

            string InsertQuery = "INSERT INTO pms_Category (Category_Name,Category_Description) VALUES (@Category_Name, @Category_Description)";

            string UpdateQuery = "update pms_Category set  " +
                "Category_Name=@Category_Name," +
                "Category_Description=@Category_Description," +
                " where Category_ID=@Category_ID";

            string sQuery = objCurrentCategory.Category_ID > 0 ? UpdateQuery : InsertQuery;

            iResult = ExecuteQuery(sQuery);

            return iResult > 0 ? iResult : 0;
        }

        public int Delete()
        {
            string sQuery = "Delete from pms_Category where Category_Id='" + objCurrentCategory.Category_ID + "'";
            iResult = ExecuteQuery(sQuery);
            return iResult > 0 ? iResult : 0;
        }

        public Category GetByID(int nID)
        {
            con = new MySqlConnection(constr);

            Category curentObjCategory = new Category();

            string sqlQuery = "select * from pms_Category where Category_Id=" + nID;
            cmd = new MySqlCommand(sqlQuery);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            objReader = cmd.ExecuteReader();
            if (objReader.Read())
            {
                curentObjCategory.Category_ID= Convert.ToInt32(objReader.GetValue(0).ToString());
                curentObjCategory.Category_Name= objReader.GetValue(1).ToString();
                curentObjCategory.Category_Description= objReader.GetValue(2).ToString();
            }
            return curentObjCategory;
        }

        public List<Category> GetAll()
        {
            List<Category> lstCategorys = new List<Category>();
            con = new MySqlConnection(constr);
            cmd = new MySqlCommand("SELECT * FROM pms_Category");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            objReader = cmd.ExecuteReader();
            while (objReader.Read())
            {
                Category objCategory = new Category();
                objCategory.Category_ID = Convert.ToInt32(objReader.GetValue(0));
                objCategory.Category_Name = objReader.GetValue(1).ToString();
                objCategory.Category_Description = objReader.GetValue(2).ToString();
                lstCategorys.Add(objCategory);
            }
            con.Close();
            return lstCategorys;
        }

        public int ExecuteQuery(string sQuery)
        {
            con = new MySqlConnection(constr);

            cmd = new MySqlCommand(sQuery);

            MySqlDataAdapter sda = new MySqlDataAdapter();

            cmd.Parameters.AddWithValue("@Category_ID", objCurrentCategory.Category_ID);
            cmd.Parameters.AddWithValue("@Category_Name", objCurrentCategory.Category_Name);
            cmd.Parameters.AddWithValue("@Category_Description", objCurrentCategory.Category_Description);
         
            cmd.Connection = con;
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();

            return result;
        }
    }
}