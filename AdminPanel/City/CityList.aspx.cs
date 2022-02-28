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
public partial class MultiUserAddressBook_AdminPanel_City_CityList : System.Web.UI.Page
{
    #region Page Lode
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillCityData();
        }
    }
    #endregion Page Lode

    #region gvCity : RowCommand
    protected void gvCity_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                DeleteCity(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
    }
    #endregion gvCity : RowCommand

    #region Fill City Data
    private void FillCityData()
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
            objCmd.CommandText = "PR_City_SelectAllByUserID";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);

            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvCity.DataSource = objSDR;
            gvCity.DataBind();

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
    #endregion Fill City Data

    #region Delete City
    private void DeleteCity(SqlInt32 CityID)
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
            objCmd.CommandText = "[dbo].[PR_City_DeleteByPK]";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.Parameters.AddWithValue("@CityID", CityID);
            objCmd.ExecuteNonQuery();

            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            FillCityData();
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
    #endregion Delete City
}