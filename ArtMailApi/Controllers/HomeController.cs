using ArtMailApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtMailApi.Controllers
{
    public class HomeController : ApiController
    {
        private static string Constr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        [HttpGet]
        public string Index()
        {
            return "api working..";
        }
        [HttpPost]
        public ArtLoginRes Login(ArtLogin al)
        {
            ArtLoginRes Alr = new ArtLoginRes();
            var conn = new SqlConnection(Constr);
            try
            {

                var dt = new DataTable();

                using (var cmd = new SqlCommand("usp_validateLogin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId",SqlDbType.VarChar,50);
                    cmd.Parameters["@UserId"].Value = al.UserId;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50);
                    cmd.Parameters["@Password"].Value = al.Password;
                    //cmd.Parameters
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }


                if (dt.Rows.Count > 0)
                {
                    Alr.IsError = false;
                    Alr.Error = null;
                    Alr.Aid = Convert.ToInt32(dt.Rows[0]["AID"].ToString());
                    Alr.UserName= dt.Rows[0]["UserName"].ToString();


                }
            }
            catch (Exception Ex)
            {
                Alr.IsError = true;
                Alr.Error = Ex.Message;

            }
            finally
            {
                if (conn.State != ConnectionState.Closed || conn.State != ConnectionState.Broken)

                {
                    conn.Close();
                }
            }
            return Alr;
        }


    }
}
