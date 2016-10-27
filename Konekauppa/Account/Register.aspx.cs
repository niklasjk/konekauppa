using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Konekauppa.Models;
using Konekauppa.Migrations;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Konekauppa.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text, etunimi = etunimi.Text, sukunimi = sukunimi.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            manager.AddToRole(user.Id, "asiakas");

            if (result.Succeeded)
            {

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}