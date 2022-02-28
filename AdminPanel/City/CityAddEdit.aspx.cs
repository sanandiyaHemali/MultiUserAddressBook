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

public partial class MultiUserAddressBook_AdminPanel_City_CityAddEdit : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillStateDropDownList();
            if (Page.RouteData.Values["CityID"] != null)
            {
                lblHeader.Text = "Edit City | CityID = " + Page.RouteData.Values["CityID"].ToString();
                FillControls(Convert.ToInt32(Page.RouteData.Values["CityID"].ToString()));
            }
            else
            {
                lblHeader.Text = "Add City";
            }
        }
    }
    #endregion Page Load

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        addCityData();
    }
    #endregion Button : Save

    #region Fill State DropDown List
    private void FillStateDropDownList()
    {
        CommonDropDownFillMethod.FillDropDownState(ddlState, Convert.ToInt32(Session["UserId"].ToString().Trim()));
    }
    #endregion Fill State DropDown List

    #region Add City Data
    private void addCityData()
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        SqlInt32 StrStateId = SqlInt32.Null;
        SqlString StrCityName = SqlString.Null;
        SqlString StrSTDCode = SqlString.Null;
        SqlString StrPinCode = SqlString.Null;
        #endregion Local Variables

        try
        {
            #region Server Side Validation
            string StrErrorMessage = "";

            if (ddlState.SelectedIndex == 0)
            {
                StrErrorMessage += "- Select State <br /><br />";
            }
            if (txtCityName.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter City Name <br /><br />";
            }
            if (StrErrorMessage != "")
            {
                lblMessage.Text = StrErrorMessage;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            if (ddlState.SelectedIndex > 0)
                StrStateId = Convert.ToInt32(ddlState.SelectedValue);
            if (txtCityName.Text.Trim() != "")
                StrCityName = txtCityName.Text.Trim();
            if (txtSTDCode.Text.Trim() != "")
                StrSTDCode = txtSTDCode.Text.Trim();
            if (txtPinCode.Text.Trim() != "")
                StrPinCode = txtPinCode.Text.Trim();
            #endregion Gather Information

            #region Set Connection & Commend Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.Parameters.AddWithValue("@StateID", StrStateId);
            objCmd.Parameters.AddWithValue("@CityName", StrCityName);
            objCmd.Parameters.AddWithValue("@STDCode", StrSTDCode);
            objCmd.Parameters.AddWithValue("@PinCode", StrPinCode);
            #endregion Set Connection & Commend Object

            if (Page.RouteData.Values["CityID"] != null)
            {
                #region Update Record
                objCmd.Parameters.AddWithValue("CityID", Request.QueryString["CityID"].ToString().Trim());
                objCmd.CommandText = "PR_City_UpdateByPK";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/City/CityList.aspx");
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                objCmd.CommandText = "PR_City_Insert";
                objCmd.ExecuteNonQuery();
                lblMessage.Text = "Data Inserted Successfully <br /><br />";
                ddlState.SelectedIndex = 0;
                txtCityName.Text = "";
                txtSTDCode.Text = "";
                txtPinCode.Text = "";
                ddlState.Focus();
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
    #endregion Add City Data

    #region Fill Controls
    private void FillControls(SqlInt32 CityID)
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
            objCmd.CommandText = "PR_City_SelectByPK";

            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.Parameters.AddWithValue("@CityID", CityID.ToString().Trim());
            #endregion Set Connection & Commend Object

            #region Read the value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        ddlState.SelectedValue = objSDR["StateID"].ToString().Trim();
                    }
                    if (!objSDR["CityName"].Equals(DBNull.Value))
                    {
                        txtCityName.Text = objSDR["CityName"].ToString().Trim();
                    }
                    if (!objSDR["STDCode"].Equals(DBNull.Value))
                    {
                        txtSTDCode.Text = objSDR["STDCode"].ToString().Trim();
                    }
                    if (!objSDR["PinCode"].Equals(DBNull.Value))
                    {
                        txtPinCode.Text = objSDR["PinCode"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No data available for the CityID = " + CityID.ToString();
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