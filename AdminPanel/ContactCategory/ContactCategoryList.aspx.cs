using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class MultiUserAddressBook_AdminPanel_ContactCategory_ContactCategoryList : System.Web.UI.Page
{
    #region Page Lode
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillContactCategoryData();
        }
    }
    #endregion Page Lode

    #region gvContactCategory : RowCommand
    protected void gvContactCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                DeleteContactCategory(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
    }
    #endregion gvContactCategory : RowCommand

    #region Fill ContactCategory Data
    private void FillContactCategoryData()
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        #endregion Local Variables
        try
        {
            #region Set Connection & Commend Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_ContactCategory_SelectAllByUserID";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvContactCategory.DataSource = objSDR;
            gvContactCategory.DataBind();
            
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            #endregion Set Connection & Commend Object
        }

         catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion Fill ContactCategory Data

    #region Delete ContactCategory
    private void DeleteContactCategory(SqlInt32 ContactCategoryID)
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        #endregion Local Variables

        try
        {
            #region Set Connection & Commend Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_ContactCategory_DeleteByPK]";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID);
            objCmd.ExecuteNonQuery();

            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            FillContactCategoryData();
            #endregion Set Connection & Commend Object
        }
        catch(Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion Delete ContactCategory
}