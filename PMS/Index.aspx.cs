using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace PMS
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlProductCategory_Bind();
                GridProduct_Bind();
            }
        }

        #region DropDown
        private void ddlProductCategory_Bind()
        {
            ddlProductCategory.DataSource = new CategoryDataManager().GetAll();
            ddlProductCategory.DataTextField = "Category_Name";
            ddlProductCategory.DataValueField = "Category_ID";
            ddlProductCategory.DataBind();
            ddlProductCategory.Items.Insert(0, new ListItem("Select Category", "0"));
        }

        #endregion

        #region Grid 

        #region BindGrid
        private void GridProduct_Bind()
        {
            gridProduct.DataSource = new ProductDataManager().GetAll();
            gridProduct.DataBind();
        }
        #endregion

        #region Grid
        protected void gridProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Product objProduct = (Product)e.Row.DataItem;

                Label lblProductID = (Label)e.Row.FindControl("lblProductID");
                Label lblProductName = (Label)e.Row.FindControl("lblProductName");
                Label lblProductDescription = (Label)e.Row.FindControl("lblProductDescription");
                Label lblProductCategory = (Label)e.Row.FindControl("lblProductCategory");
                Label lblProductMRP = (Label)e.Row.FindControl("lblProductMRP");

                LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                LinkButton lnkbtnDelete = (LinkButton)e.Row.FindControl("lnkbtnDelete");

                lblProductID.Text = objProduct.Product_Id.ToString();
                lblProductName.Text = objProduct.Product_Name;

                lblProductDescription.Text = objProduct.Product_Description.Length > 25 ? objProduct.Product_Description.Substring(0, 25) + "..." : objProduct.Product_Description.Length != 0 ? objProduct.Product_Description : " ";

                lblProductCategory.Text = new CategoryDataManager().GetByID(objProduct.Product_Category).Category_Name;

                lblProductMRP.Text = objProduct.Product_MRP.ToString();

                lnkbtnEdit.CommandArgument = objProduct.Product_Id.ToString();
                lnkbtnDelete.CommandArgument = objProduct.Product_Id.ToString();
            }
        }
        #endregion

        #endregion



        #region click event 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Validation();
            if (Validation() == false)
            {
                Int32.TryParse(ddlProductCategory.SelectedValue, out Int32 nProduct_Category);
                double.TryParse(txtProductMRP.Text, out double Product_MRP);
                Int32.TryParse(hdnProductID.Value.ToString(), out int nProduct_ID);

                Product objProduct = new Product();
                objProduct.Product_Id = nProduct_ID;
                objProduct.Product_Name = txtProductName.Text.Trim();
                objProduct.Product_Description = txtProductDescription.Text.Trim();
                objProduct.Product_Category = nProduct_Category;
                objProduct.Product_MRP = Product_MRP;

                int i = new ProductDataManager(objProduct).Save();

                if (i > 0)
                {
                    clearfield();
                }
            }

            GridProduct_Bind();
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            clearfield();
            int.TryParse(e.CommandArgument.ToString(), out int nProduct_Id);
            Product objProduct = new ProductDataManager().GetByID(nProduct_Id);

            hdnProductID.Value = objProduct.Product_Id.ToString();
            txtProductName.Text = objProduct.Product_Name;
            txtProductDescription.Text = objProduct.Product_Description;
            ddlProductCategory.SelectedValue = ddlProductCategory.Items.FindByValue(objProduct.Product_Category.ToString()).Value;
            txtProductMRP.Text = objProduct.Product_MRP.ToString();
            btnSave.Text = "Update";
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            clearfield();
            int Id = -1;
            int.TryParse(e.CommandArgument.ToString(), out Id);

            hdnID.Value = Id.ToString();

            ScriptManager.RegisterStartupScript(updatePnlDeleteProduct, updatePnlDeleteProduct.GetType(), "show", "$(function () { $('#" + pnlDeleteProduct.ClientID + "').modal('show'); });", true);
            updatePnlDeleteProduct.Update();

        }
        #endregion

        bool bStatus = false;
        public bool Validation()
        {
            if (String.IsNullOrEmpty(txtProductName.Text))
            {
                lblProductName.Text = "Product Name is empty.";
                bStatus = true;
            }
            else
            {
                lblProductName.Text = String.Empty;
            }

            if (ddlProductCategory.SelectedIndex <= 0)
            {
                lblProductCategory.Text = "Please select product category.";
                bStatus = true;
            }
            else
            {
                lblProductCategory.Text = String.Empty;
            }

            Int64 nProduct_MRP = -1;
            Int64.TryParse(txtProductMRP.Text, out nProduct_MRP);
            
            if (!String.IsNullOrEmpty(txtProductMRP.Text))
            {
                if (txtProductMRP.Text=="0")
                {
                    lblProductMRP.Text = "Product MRP should not be 0.";
                    bStatus = true;
                }
                else
                {
                    lblProductMRP.Text = String.Empty;
                }
            }
            else
            {
                lblProductMRP.Text = "Product MRP should not be empty.";
                bStatus = true;
            }




            return bStatus;
        }

        public void clearfield()
        {
            hdnProductID.Value = String.Empty;
            txtProductName.Text = String.Empty;
            txtProductDescription.Text = String.Empty;
            ddlProductCategory.SelectedIndex = 0;
            txtProductMRP.Text = String.Empty;
            lblProductName.Text = String.Empty;
            lblProductDescription.Text = String.Empty;
            lblProductCategory.Text = String.Empty;
            lblProductMRP.Text = String.Empty;
            btnSave.Text = "Save";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int32.TryParse(hdnID.Value.ToString(), out int nProduct_ID);
            Product objProduct = new Product();
            objProduct.Product_Id = nProduct_ID;
            int iResult = new ProductDataManager(objProduct).Delete();
            ScriptManager.RegisterStartupScript(updatePnlDeleteProduct, updatePnlDeleteProduct.GetType(), "hide", "$(function () { $('#" + pnlDeleteProduct.ClientID + "').modal('hide'); });", true);

            GridProduct_Bind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearfield();
        }

        protected void gridProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridProduct.PageIndex = e.NewPageIndex;
            GridProduct_Bind();
        }
    }
}