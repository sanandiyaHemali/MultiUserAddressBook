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

public partial class AdminPanel_LogIn : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion Page Load

    #region Button : LogIn
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        #region Check LogIn

        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        SqlString StrUName = SqlString.Null;
        SqlString StrPass = SqlString.Null;
        #endregion Local Variables

        try
        {
            #region Server Side Validation

            string strErrorMessage = "";
            if (txtUName.Text.Trim() == "")
            {
                strErrorMessage += "-Enter UserName</br>";
            }
            if (txtPass.Text.Trim() == "")
            {
                strErrorMessage += "-Enter Password</br>";
            }
            if (strErrorMessage != "")
            {
                lblLogInMassage.Text = strErrorMessage;
                return;
            }

            #endregion Server Side Validation

            #region Assign the Value
            if (txtUName.Text.Trim() != "")
                StrUName = txtUName.Text.Trim();

            if (txtPass.Text.Trim() != "")
                StrPass = txtPass.Text.Trim();
            #endregion Assign the Value

            #region Set Connection & Commend Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_SelectUserNamePassword";

            objCmd.Parameters.AddWithValue("@UserName", StrUName);
            objCmd.Parameters.AddWithValue("@Password", StrPass);

            #endregion Set Connection & Commend Object

            #region Read the value
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                lblLogInMassage.Text = "Valid User";

                while (objSDR.Read())
                {
                    if (!objSDR["UserID"].Equals(DBNull.Value))
                    {
                        Session["UserID"] = objSDR["UserID"].ToString().Trim();
                    }
                    if (!objSDR["DisplayName"].Equals(DBNull.Value))
                    {
                        Session["DisplayName"] = objSDR["DisplayName"].ToString().Trim();
                    }
                    Response.Redirect("Default.aspx");
                    break;
                }
            }
            else
            {
                lblLogInMassage.Text = "Either Username or Password Is not valid, Try again with different UserName and Password";
            }
            #endregion Read the value 
            
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }

        catch(Exception ex)
        {
            lblLogInMassage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        #endregion Check LogIn
    }
    #endregion Button : LogIn
}