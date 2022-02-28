using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class MultiUserAddressBook_AdminPanel_Contact_ContactList : System.Web.UI.Page
{
    #region Page Lode
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillContactData();
        }
    }
    #endregion Page Lode

    #region gvContact : RowCommand
    protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                String[] commandArgument = e.CommandArgument.ToString().Split(new char[] { ',' });
                String ContactID = commandArgument[0];
                String ContactPhotoPath = commandArgument[1];

                DeleteContact(Convert.ToInt32(ContactID.ToString().Trim()), ContactPhotoPath);
            }
        }
    }
    #endregion gvContact : RowCommand

    #region Fill Contact Data
    private void FillContactData()
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
            objCmd.CommandText = "PR_Contact_SelectAllByUserID";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            gvContact.DataSource = objSDR;
            gvContact.DataBind();

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
    #endregion Fill Contact Data

    #region Delete Contact
    private void DeleteContact(SqlInt32 ContactID, String ContactPhotoPath)
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
            objCmd.CommandText = "[dbo].[PR_Contact_DeleteByPK]";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.Parameters.AddWithValue("@ContactID", ContactID);
            objCmd.ExecuteNonQuery();

            if (objConn.State == ConnectionState.Open)
                objConn.Close();

            FileInfo file = new FileInfo(Server.MapPath(ContactPhotoPath));
            if (file.Exists)
            {
                file.Delete();
            }

            FillContactData();
            #endregion Set Connection & Commend Object
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            objConn.Close();
        }
    }
    #endregion Delete Contact

    
}