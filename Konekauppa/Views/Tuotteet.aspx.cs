using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Konekauppa.Views
{
    public partial class Tuotteet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 TuoteID;
            TuoteID = (Int32)GridView1.SelectedValue;
            this.Session["tuoteid"] = TuoteID;
            Response.Redirect("Tuote.aspx");

        }
    }
}