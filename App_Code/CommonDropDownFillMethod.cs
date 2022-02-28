using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public static class CommonDropDownFillMethod
{
    #region Fill Country DropDown List
    public static void FillDropDownCountry(DropDownList  ddl , SqlInt32 UserId)
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
            objCmd.CommandText = "PR_Country_SelectForDropDownList";
            if (UserId.ToString().Trim() != null)
                objCmd.Parameters.AddWithValue("@UserID", UserId);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            #endregion Set Connection & Commend Object

            #region Fill Data Value In DropDownList
            if (objSDR.HasRows == true)
            {
                ddl.DataSource = objSDR;
                ddl.DataValueField = "CountryID";
                ddl.DataTextField = "CountryName";
                ddl.DataBind();
            }

            ddl.Items.Insert(0, new ListItem("Select Country", "-1"));
            #endregion Fill Data Value In DropDownList

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }

        catch (Exception ex)
        {
            
        }

        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        
    }

    #endregion Fill Country  DropDown List

    #region Fill State DropDown List
    public static void FillDropDownState(DropDownList  ddl , SqlInt32 UserId)
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
            objCmd.CommandText = "PR_State_SelectForDropDownList";
            if (UserId.ToString().Trim() != null)
                objCmd.Parameters.AddWithValue("@UserID", UserId);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            #endregion Set Connection & Commend Object

            #region Fill Data Value In DropDownList
            if (objSDR.HasRows == true)
            {
                ddl.DataSource = objSDR;
                ddl.DataValueField = "StateID";
                ddl.DataTextField = "StateName";
                ddl.DataBind();
            }

            ddl.Items.Insert(0, new ListItem("Select State", "-1"));
            #endregion Fill Data Value In DropDownList

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }

        catch (Exception ex)
        {
           
        }

        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }

    #endregion Fill State DropDown List

    #region Fill State DropDown List By CountryID
    public static void FillStateDropDownListByCountryID(DropDownList ddl, SqlInt32 UserId ,SqlInt32 CountryId)
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
            objCmd.CommandText = "PR_State_SelectForDropDownListByCountryID";
            objCmd.Parameters.Add("CountryId", SqlDbType.Int).Value = CountryId;
            if (UserId.ToString().Trim() != null)
                objCmd.Parameters.AddWithValue("@UserID", UserId);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            #endregion Set Connection & Commend Object

            #region Fill Data Value In DropDownList
            if (objSDR.HasRows == true)
            {
                ddl.DataSource = objSDR;
                ddl.DataValueField = "StateID";
                ddl.DataTextField = "StateName";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select State", "-1"));
            }
            else
            {
                ddl.Items.Clear();
                ddl.Items.Insert(0, new ListItem("Select State", "-1"));

                ddl.Items.Clear();
                ddl.Items.Insert(0, new ListItem("Select City", "-1"));
            }

            #endregion Fill Data Value In DropDownList

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }

        catch (Exception ex)
        {
            
        }

        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }

    #endregion Fill State DropDown List By CountryID

    #region Fill City DropDown List By StateID
    public static void FillCityDropDownListByStateID(DropDownList ddl, SqlInt32 UserId, SqlInt32 StateID)
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
            objCmd.CommandText = "PR_City_SelectForDropDownListByStateID";
            objCmd.Parameters.Add("StateID", SqlDbType.Int).Value = StateID;
            if (UserId.ToString().Trim() != null)
                objCmd.Parameters.AddWithValue("@UserID", UserId);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            #endregion Set Connection & Commend Object

            #region Fill Data Value In DropDownList
            if (objSDR.HasRows == true)
            {
                ddl.DataSource = objSDR;
                ddl.DataValueField = "CityID";
                ddl.DataTextField = "CityName";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select City", "-1"));
            }
            else
            {
                ddl.Items.Clear();
                ddl.Items.Insert(0, new ListItem("Select City", "-1"));
            }

            #endregion Fill Data Value In DropDownList

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }

        catch (Exception ex)
        {
            
        }

        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }
    #endregion Fill City DropDown List By StateID
}