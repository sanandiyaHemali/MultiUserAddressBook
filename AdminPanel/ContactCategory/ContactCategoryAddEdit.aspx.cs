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

public partial class MultiUserAddressBook_AdminPanel_ContactCategory_ContactCategoryAddEdit : System.Web.UI.Page
{
    #region Page Lode
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Page.RouteData.Values["ContactCategoryID"] != null)
            {
                lblHeader.Text = "Edit ContactCategory | ContactCategoryID = " + Page.RouteData.Values["ContactCategoryID"].ToString();
                FillControls(Convert.ToInt32(Page.RouteData.Values["ContactCategoryID"].ToString()));
            }
            else
            {
                lblHeader.Text = "Add ContactCategory";
            }
        }
    }
    #endregion Page Lode

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        addContactCategoryData();
    }
    #endregion Button : Save

    #region Add ContactCategory Data
    private void addContactCategoryData()
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        SqlString StrContactCategoryName = SqlString.Null;
        #endregion Local Variables

        try
        {
            #region Server Side Validation
            string StrErrorMessage = "";

            if (txtContactCategoryName.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter Contact Category Name <br /><br />";
            }
            if (StrErrorMessage != "")
            {
                lblMessage.Text = StrErrorMessage;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            if (txtContactCategoryName.Text.Trim() != "")
                StrContactCategoryName = txtContactCategoryName.Text.Trim();
            #endregion Gather Information

            #region Set Connection & Commend Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.Parameters.AddWithValue("@ContactCategoryName", StrContactCategoryName);
            #endregion Set Connection & Commend Object

            if (Request.QueryString["ContactCategoryID"] != null)
            {
                #region Update Record
                objCmd.Parameters.AddWithValue("ContactCategoryID", Request.QueryString["ContactCategoryID"].ToString().Trim());
                objCmd.CommandText = "PR_ContactCategory_UpdateByPK";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/ContactCategory/ContactCategoryList.aspx");
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                objCmd.CommandText = "PR_ContactCategory_Insert";
                objCmd.ExecuteNonQuery();
                lblMessage.Text = "Data Inserted Successfully <br /><br />";
                txtContactCategoryName.Text = "";
                txtContactCategoryName.Focus();
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
    #endregion Add ContactCategory Data

    #region Fill Controls
    private void FillControls(SqlInt32 ContactCategoryID)
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
            objCmd.CommandText = "PR_ContactCategory_SelectByPK";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID.ToString().Trim());
            #endregion Set Connection & Commend Object

            #region Read the value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["ContactCategoryName"].Equals(DBNull.Value))
                    {
                        txtContactCategoryName.Text = objSDR["ContactCategoryName"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No data available for the ContactCategoryID = " + ContactCategoryID.ToString();
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