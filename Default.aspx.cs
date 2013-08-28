using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    //init vars
    DataTable user;
    //DataTable user2;
    public string displayName = String.Empty;
    protected string userRole = String.Empty;
    protected string password = String.Empty;
    protected string suggestedUserID = String.Empty;
    protected int errorCode = 000;
    //protected string userID_manual = String.Empty;
    //protected string password_manual = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //get UFAD
        string logon = Page.User.Identity.Name;
        //TRIM it
        string logonTrimmed = logon.Remove(0, 5);
        //add this for page
        suggestedUserID = logonTrimmed;
        //send/search db for user
        user = FootballDb_Gateway.SELECT_USER(logonTrimmed);
        //if user found...
        if ((user != null) && (user.Rows.Count > 0))
        {
            //only need to grab the first row because there should only be one....
            //? 2do: confirm above statement
            //get info from db
            displayName = user.Rows[0]["displayName"].ToString();
            userRole = user.Rows[0]["role"].ToString();
            //determine if user is admin
            if (userRole.Contains("admin"))
            {
                Response.Redirect("Dashboard.aspx");
            }
            //user must be user
            else
            {
                Response.Redirect("Dashboard.aspx");
            }
        }
        else //if no username match found 
        {
            //prompt options write out
            errorCode = 002;
        }
        //handling buttons when clicked
        if (IsPostBack)
        {
            //grab the js hidden form thing
            string action = Request.Form["item_action"];
            //send it away!
            switch (action)
            {
                case "action1":
                    login_manual_clicked();
                    break;
                case "action2":
                    register_UFAD_clicked();
                    Response.Redirect("Dashboard.aspx");
                    break;
                case "action3":
                    //register external login info
                    //not used..
                    //2do: make work
                    break;
            }
        }
    }

    public void optionsMessageWriter()
    {
        if (errorCode == 002)
        {
            Response.Write("<h2>No Username Match Found!</h2><br \\>");
            Response.Write("<a href=\"#\" onclick=\"show('registerUFAD');hide('manualLogin');hide('registration');\">Click here to register your UFAD.</a> <br /><br />");
            Response.Write("<a href=\"#\" onclick=\"show('manualLogin');hide('registerUFAD');hide('registration');\"> Click here to login manually.</a> <br /><br />");
            Response.Write("<a href=\"#\" onclick=\"show('registration');hide('registerUFAD');hide('manualLogin');\">Click here to register if do not have a UFAD.</a> <br /><br />");
        }
        else
        {
            errorCode = 001; //fatal error
            Response.Write("Error: 001 fatal");
        }
    }

    protected void role_redirect()
    {
        //2do place the redirect/role code here and call it from each function.
    }

    //this is button 1
    protected void login_manual_clicked()
    {
        //define and get stuff from user
        string userID_manual = Request.Form["userID_manual"];
        string password_manual = Request.Form["password_manual"];
        //send it over to the db to authenticate
        user = FootballDb_Gateway.AUTH_USER(userID_manual, password_manual);
        //if user found...
        if ((user != null) && (user.Rows.Count > 0))
        {
            //only should return one row if user is found
            userRole = user.Rows[0]["role"].ToString();
            password = user.Rows[0]["password"].ToString();
            if (password_manual == password)
            {
                //we found one! However, this page sould not display if properly authenticated.
                Response.Write("match!");
                //send to authed page...
                Response.Redirect("Dashboard.aspx");
            }
            //no username found in database
            else { Response.Write("no match!"); }

            //determine if user is admin
            if (userRole.Contains("admin"))
            {
                //redirect to authed page
                Response.Redirect("Dashboard.aspx");
            }
            //if not admin, then must be user
            else
            {
                //redirect to authed page
                Response.Redirect("Dashboard.aspx");
            }
        }
        else //if no username match found 
        {
            //this prompts an output of options...
            errorCode = 002;
        }
    }

    //this is button 2
    protected void register_UFAD_clicked()
    {
        //get ufad from user input
        //string userID_UFAD = Request.Form["userID_UFAD"];
        string userID_UFAD = suggestedUserID;
        //send to db (register)
        FootballDb_Gateway.REGISTER_UFAD(userID_UFAD);
        //redirect to authed page
        Response.Redirect("Dashboard.aspx");
    }
}