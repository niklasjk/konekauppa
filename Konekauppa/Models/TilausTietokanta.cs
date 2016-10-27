using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Web.Configuration;

namespace Konekauppa.Models
{
    public class TilausTietokanta
    {
        public TilausTietokanta()
        {
        }

        public int TeeTilaus(
          string AsiakasID, int TuoteID, int Lkm, string Maksuehto)
        {
            Int32 TilausID = -1;

            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            string sqlCmd1 = null;

            if (!OnkoRekisteroitynyt(AsiakasID))
            {
                System.Diagnostics.Debug.WriteLine(AsiakasID);
                sqlCmd1 = "INSERT INTO ASIAKAS (ASIAKASID, SUKUNIMI, ETUNIMI) VALUES (@AsiakasID, 'petterila', 'petteri'); ";

                SqlCommand cmd1 = new SqlCommand(sqlCmd1, conn);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@AsiakasID";
                param.Value = AsiakasID;

                // 3. add new parameter to command object
                cmd1.Parameters.Add(param);
                try
                {
                    conn.Open();
                    Int32 rowsAffected = cmd1.ExecuteNonQuery();
                    Console.WriteLine("RowsAffected: {0}", rowsAffected);

                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    conn.Close();
                }

            }

            string sqlCmd2 = "INSERT INTO TILAUS (ASIAKASID, PVM, MAKSUEHTO) VALUES(@AsiakasID, GETDATE(), @Maksuehto); ";
            string sqlCmd3 = "SELECT @@IDENTITY AS [@@IDENTITY];";

            SqlTransaction tran1 = null;
            SqlCommand cmd2 = new SqlCommand(sqlCmd2, conn);
            SqlCommand cmd3 = new SqlCommand(sqlCmd3, conn);

            cmd2.Parameters.AddWithValue("@AsiakasID", AsiakasID);
            cmd2.Parameters.AddWithValue("@Maksuehto", Maksuehto);

            try
            {
                conn.Open();

                tran1 = conn.BeginTransaction();
                cmd2.Transaction = tran1;
                cmd3.Transaction = tran1;

                int onnistuiko = cmd2.ExecuteNonQuery();
                SqlDataReader reader = cmd3.ExecuteReader();

                while (reader.Read())
                {
                    TilausID = Convert.ToInt32(reader[0]);
                }
                reader.Close();
                tran1.Commit();
                return TilausID;

            }
            catch (SqlException ex)
            {
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public Boolean LisaaTilausRivi(int TilausID, int TuoteID, int Lkm)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            string sqlCmd1 = "UPDATE VARASTO SET VAPAANA_LKM = VAPAANA_LKM - @Lkm, VARATTUJEN_LKM = VARATTUJEN_LKM + @Lkm WHERE TuoteID = @TuoteID; ";
            string sqlCmd2 = "INSERT INTO TILAUSRIVI (TILAUSID, TUOTEID, LUKUMAARA) VALUES(@TilausID, @TuoteID, @Lukumaara); ";

            SqlCommand cmd1 = new SqlCommand(sqlCmd1, conn);
            SqlCommand cmd2 = new SqlCommand(sqlCmd2, conn);

            cmd1.Parameters.AddWithValue("@TuoteID", TuoteID);
            cmd1.Parameters.AddWithValue("@Lkm", Lkm);

            cmd2.Parameters.AddWithValue("@TilausID", TilausID);
            cmd2.Parameters.AddWithValue("@TuoteID", TuoteID);
            cmd2.Parameters.AddWithValue("@Lukumaara", Lkm);

            SqlTransaction tran1 = null;

            try
            {
                conn.Open();

                tran1 = conn.BeginTransaction();
                cmd1.Transaction = tran1;
                cmd2.Transaction = tran1;

                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                tran1.Commit();

                return true;
            }
            catch (SqlException ex)
            {
                tran1.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }

        }


        public Boolean OnkoRekisteroitynyt(string aktiivi_tunnus)
        {
            string strConn = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);

            string sqlCmd = "SELECT ASIAKASID FROM ASIAKAS WHERE AsiakasID = @AsiakasID; ";
            SqlCommand cmd1 = new SqlCommand(sqlCmd, conn);
            cmd1.Parameters.AddWithValue("@AsiakasID", aktiivi_tunnus);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd1.ExecuteReader();

                if (reader.HasRows)
                    return true;
                else
                    return false;

            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error: Unable to connect to the database!");
            }
            finally
            {
                conn.Close();
            }
        }



        public string CastToSqlServerDateTime(DateTime Pvm)
        {
            string vvvv = Pvm.Year.ToString();
            string mm = Pvm.Month.ToString();
            string dd = Pvm.Day.ToString();
            return "" + vvvv + "-" + mm + "-" + dd;
        }

    }
}