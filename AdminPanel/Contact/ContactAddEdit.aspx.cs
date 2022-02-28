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
public partial class MultiUserAddressBook_AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
{
    #region Page Lode
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillCountryDropDownList();
            FillContactCategoryDropDownList();
            if (Page.RouteData.Values["ContactID"] != null)
            {
                lblHeader.Text = "Edit Contact | ContactID = " + Page.RouteData.Values["ContactID"].ToString();
                FillControls(Convert.ToInt32(Page.RouteData.Values["ContactID"]));
            }
            else
            {
                lblHeader.Text = "Add Contact";
            }
        }
    }
    #endregion Page Lode

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        addContactData();
    }
    #endregion Button : Save

    #region ddlCountry - Selected Index Changed
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex > 0)
        {
            FillStateDropDownListByCountryID(Convert.ToInt32(ddlCountry.SelectedValue));
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
        }
        else
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select State", "-1"));

            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
        }
    }

    #endregion ddlCountry - Selected Index Changed

    #region ddlState - Selected Index Changed
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex > 0)
        {
            FillCityDropDownListByStateID(Convert.ToInt32(ddlState.SelectedValue));
        }
        else
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
        }
    }

    #endregion ddlState - Selected Index Changed

    #region Fill Country DropDown List
    private void FillCountryDropDownList()
    {
        CommonDropDownFillMethod.FillDropDownCountry(ddlCountry, Convert.ToInt32(Session["UserId"].ToString().Trim()));
    }
    #endregion Fill Country DropDown List

    #region Fill State DropDown List By CountryID
    private void FillStateDropDownListByCountryID(SqlInt32 CountryId)
    {
        CommonDropDownFillMethod.FillStateDropDownListByCountryID(ddlState, Convert.ToInt32(Session["UserId"].ToString().Trim()), CountryId);
    }
    #endregion Fill State DropDown List ByCountry ID

    #region Fill City DropDown List By StateID
    private void FillCityDropDownListByStateID(SqlInt32 StateID)
    {
        CommonDropDownFillMethod.FillCityDropDownListByStateID(ddlCity, Convert.ToInt32(Session["UserId"].ToString().Trim()), StateID);
    }
    #endregion Fill City DropDown List By StateID

    #region Fill ContactCategory CheckBox List
    private void FillContactCategoryDropDownList()
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
            objCmd.CommandText = "PR_ContactCategory_SelectForDropDownList";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            #endregion Set Connection & Commend Object

            #region Fill Data Value In DropDownList
            if (objSDR.HasRows == true)
            {
                chkblContactCategory.DataSource = objSDR;
                chkblContactCategory.DataValueField = "ContactCategoryID";
                chkblContactCategory.DataTextField = "ContactCategoryName";
                chkblContactCategory.DataBind();
            }

            #endregion Fill Data Value In DropDownList

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
    #endregion Fill ContactCategory CheckBox List

    #region Add Contact Data
    private void addContactData()
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiUserAddressBookConnectionString"].ConnectionString);
        SqlInt32 StrContactId = SqlInt32.Null;
        SqlInt32 StrCountryId = SqlInt32.Null;
        SqlInt32 StrStateId = SqlInt32.Null;
        SqlInt32 StrCityId = SqlInt32.Null;
        SqlInt32 StrContactCategoryId = SqlInt32.Null;
        String ContactPhotoPath = "";
        String Attribute = "";
        SqlString StrContactName = SqlString.Null;
        SqlString StrContactNo = SqlString.Null;
        SqlString StrWhatsAppNo = SqlString.Null;
        SqlDateTime StrBirthDate = SqlDateTime.Null;
        SqlString StrEmail = SqlString.Null;
        SqlInt32 StrAge = SqlInt32.Null;
        SqlString StrAddress = SqlString.Null;
        SqlString StrBloodGroup = SqlString.Null;
        SqlString StrFacebookID = SqlString.Null;
        SqlString StrLinkedINID = SqlString.Null;
        #endregion Local Variables

        try
        {
            #region Server Side Validation
            string StrErrorMessage = "";

            if (ddlCountry.SelectedIndex == 0)
            {
                StrErrorMessage += "- Select Country <br /><br />";
            }
            if (ddlState.SelectedIndex == 0)
            {
                StrErrorMessage += "- Select State <br /><br />";
            }
            if (ddlCity.SelectedIndex == 0)
            {
                StrErrorMessage += "- Select City <br /><br />";
            }
            if (chkblContactCategory.SelectedItem == null)
            {
                StrErrorMessage += "- Select Contact Category <br /><br />";
            }
            if (txtContactName.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter Contact Name <br /><br />";
            }
            //if (!fuContactPhotoPath.HasFile)
            //{
            //    StrErrorMessage += "- Select file <br /><br />";
            //}
            //if (fucontactphotopath.hasfile)
            //{
            //    string fileext = system.io.path.getextension(fucontactphotopath.filename);

            //    if (fileext.tolower() == ".jpeg" || fileext.tolower() == ".jpg")
            //    {

            //    }
            //    else
            //    {
            //        strerrormessage += "- only .jpeg files allowed! <br /><br />";
            //    }

            //    httppostedfile file = fucontactphotopath.postedfile;
            //    int ifilesize = file.contentlength;
            //    if (ifilesize > 1048576)
            //    {
            //        strerrormessage += "- file size should be less than 1mb <br /><br />";
            //        return;
            //    }
            //}
            if (txtContactNo.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter Contact No. <br /><br />";
            }
            if (txtBirthDate.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter Birthdate <br /><br />";
            }
            if (txtEmail.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter Email ID <br /><br />";
            }
            if (txtAddress.Text.Trim() == "")
            {
                StrErrorMessage += "- Enter Address <br /><br />";
            }
            if (StrErrorMessage != "")
            {
                lblMessage.Text = StrErrorMessage;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            if (ddlCountry.SelectedIndex > 0)
                StrCountryId = Convert.ToInt32(ddlCountry.SelectedValue);
            if (ddlState.SelectedIndex > 0)
                StrStateId = Convert.ToInt32(ddlState.SelectedValue);
            if (ddlCity.SelectedIndex > 0)
                StrCityId = Convert.ToInt32(ddlCity.SelectedValue);
            if (txtContactName.Text.Trim() != "")
                StrContactName = txtContactName.Text.Trim();
            if (fuContactPhotoPath.HasFile)
            {

                System.Drawing.Image img = System.Drawing.Image.FromStream(fuContactPhotoPath.PostedFile.InputStream);
                decimal size = Math.Round(((decimal)fuContactPhotoPath.PostedFile.ContentLength / (decimal)1024), 2);
                string FileExtn = System.IO.Path.GetExtension(fuContactPhotoPath.PostedFile.FileName);
                string ext = Path.GetExtension(FileExtn);
                //int sizeRestricted = fuFileContactPhotoPath.PostedFile.ContentLength;
                int ActualWidth = img.Width;
                int ActualHeight = img.Height;
                Attribute = "File Size - " + size + "KB "
                            + " Height - " + ActualHeight + " px "
                            + " Width - " + ActualWidth + " px "
                           + " File Type - " + ext + " File ";

                String AbsolutePhotoPath = "~/Content/Images/";

                if (!Directory.Exists(Server.MapPath(AbsolutePhotoPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(AbsolutePhotoPath));
                }

                ContactPhotoPath = "~/Content/Images/" + fuContactPhotoPath.FileName.ToString().Trim();
                //  ContactPhotoPath = "~/UserContent/" + DateTime.Now.ToString("ddMMyyyyhhmmssffftt") + fuContactPhotoPath.FileName.ToString().Trim();

                fuContactPhotoPath.SaveAs(Server.MapPath(ContactPhotoPath));

            }
            if (txtContactNo.Text.Trim() != "")
                StrContactNo = txtContactNo.Text.Trim();
            if (txtWhatsAppNo.Text.Trim() != "")
                StrWhatsAppNo = txtWhatsAppNo.Text.Trim();
            if (txtBirthDate.Text.Trim() != "")
                StrBirthDate = Convert.ToDateTime(txtBirthDate.Text.Trim());
            if (txtEmail.Text.Trim() != "")
                StrEmail = txtEmail.Text.Trim();
            if (txtAge.Text.Trim() != "")
                StrAge = Convert.ToInt32(txtAge.Text.Trim());
            if (txtAddress.Text.Trim() != "")
                StrAddress = txtAddress.Text.Trim();
            if (txtBloodGroup.Text.Trim() != "")
                StrBloodGroup = txtBloodGroup.Text.Trim();
            if (txtFacebookID.Text.Trim() != "")
                StrFacebookID = txtFacebookID.Text.Trim();
            if (txtLinkedINID.Text.Trim() != "")
                StrLinkedINID = txtLinkedINID.Text.Trim();
            #endregion Gather Information

            #region Set Connection & Commend Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.Parameters.AddWithValue("@CountryID", StrCountryId);
            objCmd.Parameters.AddWithValue("@StateID", StrStateId);
            objCmd.Parameters.AddWithValue("@CityID", StrCityId);
            objCmd.Parameters.AddWithValue("@ContactName", StrContactName);
            //objCmd.Parameters.AddWithValue("@ContactPhotoPath", ContactPhotoPath);
            //objCmd.Parameters.AddWithValue("@PhotoAttributes", Attribute);
            objCmd.Parameters.AddWithValue("@ContactNo", StrContactNo);
            objCmd.Parameters.AddWithValue("@WhatsAppNo", StrWhatsAppNo);
            objCmd.Parameters.AddWithValue("@BirthDate", StrBirthDate);
            objCmd.Parameters.AddWithValue("@Email", StrEmail);
            objCmd.Parameters.AddWithValue("@Age", StrAge);
            objCmd.Parameters.AddWithValue("@Address", StrAddress);
            objCmd.Parameters.AddWithValue("@BloodGroup", StrBloodGroup);
            objCmd.Parameters.AddWithValue("@FacebookID", StrFacebookID);
            objCmd.Parameters.AddWithValue("@LinkedINID", StrLinkedINID);

            #endregion Set Connection & Commend Object

            if (Request.QueryString["ContactID"] != null)
            {
                #region Update Record

                #region Hidden Field Information
                if (fuContactPhotoPath.HasFile)
                {

                    FileInfo file = new FileInfo(Server.MapPath(hfContactPhotoPath.Value.ToString()));
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    objCmd.Parameters.AddWithValue("@ContactPhotoPath", ContactPhotoPath);
                    objCmd.Parameters.AddWithValue("@PhotoAttributes", Attribute);
                }
                else
                {
                    objCmd.Parameters.AddWithValue("ContactPhotoPath", hfContactPhotoPath.Value.ToString());
                    objCmd.Parameters.AddWithValue("@PhotoAttributes", hfAttribute.Value.ToString());
                }
                #endregion Hidden Field Information

                objCmd.CommandText = "PR_Contact_UpdateByPK";
                objCmd.Parameters.AddWithValue("ContactID", Request.QueryString["ContactID"].ToString().Trim());
                objCmd.ExecuteNonQuery();

                #region Gather Information
                SqlCommand objCmdContactCategoryDelete = objConn.CreateCommand();
                objCmdContactCategoryDelete.CommandType = CommandType.StoredProcedure;
                if (Session["UserId"] != null)
                    objCmdContactCategoryDelete.Parameters.AddWithValue("@UserID", Session["UserId"]);
                objCmdContactCategoryDelete.Parameters.AddWithValue("ContactID", Request.QueryString["ContactID"].ToString().Trim());
                #endregion Gather Information

                objCmdContactCategoryDelete.CommandText = "PR_ContactWiseContactCategory_DeleteByPk";
                objCmdContactCategoryDelete.ExecuteNonQuery();

                foreach (ListItem li in chkblContactCategory.Items)
                {
                    if (li.Selected)
                    {
                        #region Gather Information
                        SqlCommand objCmdContactCategory = objConn.CreateCommand();
                        objCmdContactCategory.CommandType = CommandType.StoredProcedure;

                        if (Session["UserId"] != null)
                            objCmdContactCategory.Parameters.AddWithValue("@UserID", Session["UserId"]);
                        objCmdContactCategory.Parameters.AddWithValue("ContactID", Request.QueryString["ContactID"].ToString().Trim());
                        StrContactCategoryId = Convert.ToInt32(li.Value.ToString().Trim());
                        objCmdContactCategory.Parameters.AddWithValue("@ContactCategoryID", StrContactCategoryId);
                        #endregion Gather Information

                        objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_UpdateByPk";
                        objCmdContactCategory.ExecuteNonQuery();
                    }
                }

                Response.Redirect("~/AdminPanel/Contact/ContactList.aspx");
                #endregion Update Record
            }
            else
            {
                #region Insert Record
                objCmd.Parameters.AddWithValue("@ContactPhotoPath", ContactPhotoPath);
                objCmd.Parameters.AddWithValue("@PhotoAttributes", Attribute);
                objCmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                objCmd.CommandText = "PR_Contact_Insert";
                objCmd.ExecuteNonQuery();
                StrContactId = Convert.ToInt32(objCmd.Parameters["@ContactID"].Value);
                lblMessage.Text = "Data Inserted Successfully <br /><br />" + StrContactId;

                foreach (ListItem li in chkblContactCategory.Items)
                {
                    if (li.Selected)
                    {
                        #region Gather Information
                        SqlCommand objCmdContactCategory = objConn.CreateCommand();
                        objCmdContactCategory.CommandType = CommandType.StoredProcedure;

                        if (Session["UserId"] != null)
                            objCmdContactCategory.Parameters.AddWithValue("@UserID", Session["UserId"]);
                        objCmdContactCategory.Parameters.AddWithValue("@ContactID", StrContactId);
                        StrContactCategoryId = Convert.ToInt32(li.Value.ToString().Trim());
                        objCmdContactCategory.Parameters.AddWithValue("@ContactCategoryID", StrContactCategoryId);
                        #endregion Gather Information

                        objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_Insert";
                        objCmdContactCategory.ExecuteNonQuery();
                    }
                }


                ddlCountry.SelectedIndex = 0;
                ddlState.SelectedIndex = 0;
                ddlCity.SelectedIndex = 0;
                chkblContactCategory.SelectedIndex = -1;
                txtContactName.Text = "";
                txtContactNo.Text = "";
                txtWhatsAppNo.Text = "";
                txtBirthDate.Text = "";
                txtEmail.Text = "";
                txtAge.Text = "";
                txtAddress.Text = "";
                txtBloodGroup.Text = "";
                txtFacebookID.Text = "";
                txtLinkedINID.Text = "";
                ddlCountry.Focus();
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
    #endregion Add Contact Data

    #region Fill Controls
    private void FillControls(SqlInt32 ContactID)
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
            objCmd.CommandText = "PR_Contact_SelectByPK";
            if (Session["UserId"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
            objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
            #endregion Set Connection & Commend Object

            #region Read the value and set the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                    {
                        ddlCountry.SelectedValue = objSDR["CountryID"].ToString().Trim();
                        FillStateDropDownListByCountryID(Convert.ToInt32(ddlCountry.SelectedValue));
                    }
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        ddlState.SelectedValue = objSDR["StateID"].ToString().Trim();
                        FillCityDropDownListByStateID(Convert.ToInt32(ddlState.SelectedValue));
                    }
                    if (!objSDR["CityID"].Equals(DBNull.Value))
                    {
                        ddlCity.SelectedValue = objSDR["CityID"].ToString().Trim();
                    }
                    if (!objSDR["ContactPhotoPath"].Equals(DBNull.Value))
                    {
                        hfContactPhotoPath.Value = objSDR["ContactPhotoPath"].ToString().Trim();
                    }
                    if (objSDR["PhotoAttributes"].Equals(DBNull.Value) != true)
                    {
                        String Attribute = objSDR["PhotoAttributes"].ToString().Trim();
                        hfAttribute.Value = objSDR["PhotoAttributes"].ToString().Trim();
                    }
                    if (!objSDR["ContactName"].Equals(DBNull.Value))
                    {
                        txtContactName.Text = objSDR["ContactName"].ToString().Trim();
                    }
                    if (!objSDR["ContactNo"].Equals(DBNull.Value))
                    {
                        txtContactNo.Text = objSDR["ContactNo"].ToString().Trim();
                    }
                    if (!objSDR["ContactNo"].Equals(DBNull.Value))
                    {
                        txtContactNo.Text = objSDR["ContactNo"].ToString().Trim();
                    }
                    if (!objSDR["WhatsAppNo"].Equals(DBNull.Value))
                    {
                        txtWhatsAppNo.Text = objSDR["WhatsAppNo"].ToString().Trim();
                    }
                    if (!objSDR["BirthDate"].Equals(DBNull.Value))
                    {
                        txtBirthDate.Text = objSDR["BirthDate"].ToString().Trim();
                    }
                    if (!objSDR["Email"].Equals(DBNull.Value))
                    {
                        txtEmail.Text = objSDR["Email"].ToString().Trim();
                    }
                    if (!objSDR["Age"].Equals(DBNull.Value))
                    {
                        txtAge.Text = objSDR["Age"].ToString().Trim();
                    }
                    if (!objSDR["Address"].Equals(DBNull.Value))
                    {
                        txtAddress.Text = objSDR["Address"].ToString().Trim();
                    }
                    if (!objSDR["BloodGroup"].Equals(DBNull.Value))
                    {
                        txtBloodGroup.Text = objSDR["BloodGroup"].ToString().Trim();
                    }
                    if (!objSDR["FacebookID"].Equals(DBNull.Value))
                    {
                        txtFacebookID.Text = objSDR["FacebookID"].ToString().Trim();
                    }
                    if (!objSDR["LinkedINID"].Equals(DBNull.Value))
                    {
                        txtLinkedINID.Text = objSDR["LinkedINID"].ToString().Trim();
                    }
                    break;
                }
            }

            objSDR.Close();

            #region FillContactCategory
            DataTable dt = new DataTable();
            SqlCommand objCmdContactCategory = objConn.CreateCommand();
            objCmdContactCategory.CommandType = CommandType.StoredProcedure;
            objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_CheckboxList";
            objCmdContactCategory.Parameters.AddWithValue("@ContactID", ContactID);
            SqlDataReader objSDRContactCategory = objCmdContactCategory.ExecuteReader();
            dt.Load(objSDRContactCategory);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][2].ToString() == "SELECTED")
                    {
                        chkblContactCategory.Items[i].Selected = true;
                    }
                }
            }
            #endregion FillContactCategory
            

            else
            {
                lblMessage.Text = "No data available for the ContactID = " + ContactID.ToString();
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