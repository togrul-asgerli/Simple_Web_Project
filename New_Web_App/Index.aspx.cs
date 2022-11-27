using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace New_Web_App
{
    public partial class Index : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-SQ9BA76\\SQLEXPRESS;Integrated Security=True; Database=Northwind;");
        SqlCommand cmd;
        DataTable dt=new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("Search", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name",txt_input.Text);
            cmd.Parameters.Add(new SqlParameter()
            {
                Direction = ParameterDirection.Output,
                ParameterName = "@result",
                SqlDbType = SqlDbType.Int
            });
            cmd.ExecuteNonQuery();
            if(cmd.Parameters["@result"].Value.ToString()=="1")
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                datagrid.DataSource = dt;
                datagrid.DataBind();
                lbl_mes.Text = "";
            }
            else
            {
                
                lbl_mes.Text = "Data not found!";
                datagrid.DataSource = null;
                datagrid.DataBind();
            }
            con.Close();
        }

        protected void grd_result_SelectedIndexChanged(object sender, EventArgs e)
        {
             
        }
    }
}