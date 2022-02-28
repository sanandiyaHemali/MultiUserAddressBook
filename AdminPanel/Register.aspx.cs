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

public partial class AdminPanel_Register : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion Page Load

    #region Button : Register
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        #region Add User Data

        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        SqlString StrUserName = SqlString.Null;
        SqlString StrPassword = SqlString.Null;
        SqlString StrDiaplayName = SqlString.Null;
        SqlString StrMobileNo = SqlString.Null;
        SqlString StrEmail = SqlString.Null;
        #endregion Local Variables

        try
        {
            #region Server Side Validation
            string StrErrorMessage = "";

            if (txtUserName.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter User Name <br /><br />";
            }
            if (txtPassword.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter Password <br /><br />";
            }
            if (txtDisplayName.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter Display Name <br /><br />";
            }
            if (txtMobileNo.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter Mobile NO. <br /><br />";
            }
            if (txtEmail.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter EmailID <br /><br />";
            }

            if (StrErrorMessage != "")
            {
                lblRegisterMassage.Text = StrErrorMessage;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            if (txtUserName.Text.Trim() != "")
                StrUserName = txtUserName.Text.Trim();
            if (txtPassword.Text.Trim() != "")
                StrPassword = txtPassword.Text.Trim();
            if (txtDisplayName.Text.Trim() != "")
                StrDiaplayName = txtDisplayName.Text.Trim();
            if (txtMobileNo.Text.Trim() != "")
                StrMobileNo = txtMobileNo.Text.Trim();
            if (txtEmail.Text.Trim() != "")
                StrEmail = txtEmail.Text.Trim();
            #endregion Gather Information

            #region Set Connection & Commend Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@UserName", StrUserName);
            objCmd.Parameters.AddWithValue("@Password", StrPassword);
            objCmd.Parameters.AddWithValue("@DisplayName", StrDiaplayName);
            objCmd.Parameters.AddWithValue("@MobileNo", StrMobileNo);
            objCmd.Parameters.AddWithValue("@Email", StrEmail);
            #endregion Set Connection & Commend Object

            #region Insert Record
            objCmd.CommandText = "PR_User_Insert";
            objCmd.ExecuteNonQuery();
            lblRegisterMassage.Text = "Data Inserted Successfully <br /><br />";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtDisplayName.Text = "";
            txtMobileNo.Text = "";
            txtEmail.Text = "";
            txtUserName.Focus();
            #endregion Insert Record

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            Response.Redirect("LogIn.aspx");
        }

        catch (Exception ex)
        {
            lblRegisterMassage.Text = ex.Message;
        }

        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        #endregion Add Country Data

    }
    #endregion Button : Register
}