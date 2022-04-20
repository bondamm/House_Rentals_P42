using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace House_Rental
{
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();
            Showcategories();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bondamm\Documents\HouseRent.mdf;Integrated Security=True;Connect Timeout=30");
        private void Showcategories()
        {
            Con.Open();
            string Query = "Select * from categoryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CategoriesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ResetData()
        {
            CategoryTb.Text = "";
            RemarksTb.Text = "";
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (CategoryTb.Text =="" || RemarksTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                { 
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into CategoryTbl (Category, Remarks)values(@Cat,@Rem)", Con);
                cmd.Parameters.AddWithValue("@Cat",CategoryTb.Text);
                cmd.Parameters.AddWithValue("@Rem",RemarksTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Added!!!");
                    Con.Close();
                    ResetData();
                    Showcategories();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CategoriesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
