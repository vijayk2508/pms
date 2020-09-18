using System;

namespace PMS.Web
{
    public class Product
    {
        private Int32 mProduct_Id = -1;
        private string mProduct_Name = string.Empty;
        private string mProduct_Description = string.Empty;
        private Int32 mProduct_Category = -1;
        private double mProduct_MRP = 0;
        public int Product_Id { get => mProduct_Id; set => mProduct_Id = value; }
        public string Product_Name { get => mProduct_Name; set => mProduct_Name = value; }
        public string Product_Description { get => mProduct_Description; set => mProduct_Description = value; }
        public Int32 Product_Category { get => mProduct_Category; set => mProduct_Category = value; }
        public double Product_MRP { get => mProduct_MRP; set => mProduct_MRP = value; }

    }
}