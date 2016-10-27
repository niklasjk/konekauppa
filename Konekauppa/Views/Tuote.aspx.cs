using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

using Konekauppa.Models;
using Microsoft.AspNet.Identity;

namespace Konekauppa.Views
{
    public partial class Tuote : System.Web.UI.Page
    {
        Int32 TuoteID;
        Int32 Lkm;
        string Maksuehto;
        string current_username;
        string etunimi;
        string sukunimi;

        protected void Page_Load(object sender, EventArgs e)
        {

            TuoteID = (Int32)this.Session["tuoteid"];

            current_username =  User.Identity.GetUserId();
            
            TilausTietokanta til = new TilausTietokanta();

            String connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            String sqlQuery = "SELECT TuoteID, Nimi, Hinta, Veroprosentti FROM Tuote WHERE TuoteID = @TuoteID";
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, conn);
            sqlCmd.Parameters.AddWithValue("@TuoteID", TuoteID);

            try
            {
                conn.Open();
                SqlDataReader reader = sqlCmd.ExecuteReader();
                FormView1.DataSource = reader;
                FormView1.DataBind();
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error: Unable create database connection!");
            }
            finally
            {
                conn.Close();
            }
        }

        protected void ButtonTilaa_Click(object sender, EventArgs e)
        {
            Lkm = Int32.Parse(TextBoxLukumaara.Text);
            Maksuehto = DropDownListMaksuehto.SelectedValue.ToString();
            
            
            // etunimi = ((TextBox)LoginView1.FindControl("TextBoxEtunimi")).Text;


            Models.TilausTietokanta til = new Models.TilausTietokanta();
            int TilausID = til.TeeTilaus(current_username, TuoteID, Lkm, Maksuehto);

            if (TilausID > 0)
            {
                Boolean onnistuiko = til.LisaaTilausRivi(TilausID, TuoteID, Lkm);

                if (onnistuiko)
                {
                    LabelOnnistuikoTilaus.Text = "Kiitos tilauksestanne! Voit seurata tilausta seuraa tilausta linkistä.";
                    LabelOnnistuikoTilaus.Visible = true;
                    Server.Transfer("KiitosTilauksesta.aspx");
                }
                else
                {
                    LabelOnnistuikoTilaus.Text = "Tilauksen tallennus ei onnistunut. Yritä uudelleen!";
                    LabelOnnistuikoTilaus.Visible = true;
                }
            }
            else
            {
                LabelOnnistuikoTilaus.Text = "Tilauksen tallennus ei onnistunut. Yritä uudelleen!";
                LabelOnnistuikoTilaus.Visible = true;
            }


        }
    }
}