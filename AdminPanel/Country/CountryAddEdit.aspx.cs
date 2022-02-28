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

public partial class MultiUserAddressBook_AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
{
    #region Page Lode 
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            if (Page.RouteData.Values["CountryID"] != null)
            {
                lblHeader.Text = "Edit Country | CountryID = " + Page.RouteData.Values["CountryID"].ToString();
                FillControls(Convert.ToInt32(Page.RouteData.Values["CountryID"].ToString()));
            }
            else
            {
                lblHeader.Text = "Add Country";
            }
        }
    }
    #endregion Page Lode 

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        addCountryData();
    }
    #endregion Button : Save

    #region Add Country Data
    private void addCountryData()
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        SqlString StrCountryName = SqlString.Null;
        SqlString StrCountryCode = SqlString.Null;
        #endregion Local Variables

        try
        {
            #region Server Side Validation
            string StrErrorMessage = "";

            if (txtCountryName.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter Country Name <br /><br />";
            }
            if (StrErrorMessage != "")
            {
                lblMessage.Text = StrErrorMessage;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            if(txtCountryName.Text.Trim() != "")
                StrCountryName = txtCountryName.Text.Trim();

            if (txtCountryCode.Text.Trim() != "")
                StrCountryCode = txtCountryCode.Text.Trim();
            #endregion Gather Information

            #region Set Connection & Commend Object

            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.Parameters.AddWithValue("@CountryName", StrCountryName);
            objCmd.Parameters.AddWithValue("@CountryCode", StrCountryCode);
            #endregion Set Connection & Commend Object

            if (Request.QueryString["CountryID"] != null)
            {
                #region Update Record
                objCmd.Parameters.AddWithValue("CountryId", Request.QueryString["CountryID"].ToString().Trim());
                objCmd.CommandText = "PR_Country_UpdateByPK";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/Country/CountryList.aspx");
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                objCmd.CommandText = "PR_Country_Insert";
                objCmd.ExecuteNonQuery();
                lblMessage.Text = "Data Inserted Successfully <br /><br />";
                txtCountryName.Text = "";
                txtCountryCode.Text = "";
                txtCountryName.Focus();
                #endregion Insert Record
            }
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
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
    #endregion Add Country Data

    #region Fill Controls
    private void FillControls(SqlInt32 CountryID)
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
            objCmd.CommandText = "PR_Country_SelectByPK";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString().Trim());
            #endregion Set Connection & Commend Object

            #region Read the value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if(objSDR.HasRows)
            {
                while(objSDR.Read())
                {
                    if (!objSDR["CountryName"].Equals(DBNull.Value))
                    {
                        txtCountryName.Text = objSDR["CountryName"].ToString().Trim();
                    }
                    if (!objSDR["CountryCode"].Equals(DBNull.Value))
                    {
                        txtCountryCode.Text = objSDR["CountryCode"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No data available for the CountryID = " + CountryID.ToString();
            }
            #endregion Read the value and set the controls

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
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
    #endregion Fill Controls
}