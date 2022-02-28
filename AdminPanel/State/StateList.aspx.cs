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

public partial class MultiUserAddressBook_AdminPanel_State_StateList : System.Web.UI.Page
{
    #region Page Lode 
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            FillStateData();
        }

    }
    #endregion Page Lode 

    #region gvState : RowCommand
    protected void gvState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                DeleteState(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
    }
    #endregion gvState : RowCommand

    #region Fill State Data
    private void FillStateData()
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
            objCmd.CommandText = "PR_State_SelectAllByUserID";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);

            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvState.DataSource = objSDR;
            gvState.DataBind();

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
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
    #endregion Fill State Data

    #region Delete State
    private void DeleteState(SqlInt32 StateID)
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
            objCmd.CommandText = "[dbo].[PR_State_DeleteByPK]";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.Parameters.AddWithValue("@StateID", StateID);
            objCmd.ExecuteNonQuery();

            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            FillStateData();
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
    #endregion Delete State
}