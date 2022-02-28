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

public partial class MultiUserAddressBook_AdminPanel_Country_CountryList : System.Web.UI.Page
{
    #region Page Lode 
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            FillCountryData();
        }
    }
    #endregion Page Lode 

    #region gvCountry : RowCommand
    protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                DeleteCountry(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
    }
    #endregion gvCountry : RowCommand

    #region Fill Country Data
    private void FillCountryData()
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection();
        objConn.ConnectionString = ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString;        
        #endregion Local Variables

        try
        {
            #region Set Connection & Commend Object
            if(objConn.State !=ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Country_SelectAllByUserId";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvCountry.DataSource = objSDR;
            gvCountry.DataBind();

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
    #endregion Fill Country Data

    #region Delete Country
    private void DeleteCountry(SqlInt32 CountryID)
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
            objCmd.CommandText = "[dbo].[PR_Country_DeleteByPK]";
            objCmd.Parameters.AddWithValue("@CountryID", CountryID);
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.ExecuteNonQuery();

            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            FillCountryData();
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
    #endregion Delete Country
}