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

public partial class AdminPanel_Default : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillUserData();
        }
    }

    #endregion Page Load

    

    #region Fill User Data
    private void FillUserData()
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection();
        objConn.ConnectionString = ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString;
        #endregion Local Variables

        try
        {
            #region Set Connection & Commend Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_SelectByPK";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvUser.DataSource = objSDR;
            gvUser.DataBind();

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
    #endregion Fill User Data

    
}