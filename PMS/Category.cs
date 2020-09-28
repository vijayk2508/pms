using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS
{
    public class Category
    {
        int mCategory_ID=-1;
        string mCategory_Name = String.Empty;
        string mCategory_Description = String.Empty;


        public int Category_ID { get => mCategory_ID; set => mCategory_ID = value; }
        public string Category_Name{ get => mCategory_Name; set => mCategory_Name = value; }
        public string Category_Description { get => mCategory_Description; set => mCategory_Description = value; }
    }
}