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
    public partial class Apartments : Form
    {
        public Apartments()
        {
            InitializeComponent();
            GetCategories();
            GetOwner();
            ShowAparts();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bondamm\Documents\HouseRent.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetCategories() 
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select LLID from LandLordTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("LLID", typeof(int));
            dt.Load(Rdr);
            LLcb.ValueMember = "LLID";
            LLcb.DataSource = dt;
            Con.Close();
        }
        private void GetOwner()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CNum from categoryTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CNum", typeof(int));
            dt.Load(Rdr);
            TypeCb.ValueMember = "CNum";
            TypeCb.DataSource = dt;
            Con.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
            private void ShowAparts()
            {
                Con.Open();
                string Query = "Select * from ApartTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                ApartmentsDGV.DataSource = ds.Tables[0];
                Con.Close();

            }
            private void ResetData()
            {
                ApNameTb.Text = "";
                LLcb.SelectedIndex = -1;
                CostTb.Text = "";
                TypeCb.SelectedIndex = -1;
                AddressTb.Text = "";
            }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ApNameTb.Text == "" || LLcb.SelectedIndex == -1 || CostTb.Text == "" || TypeCb.SelectedIndex == -1 || AddressTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ApartTbl(AName,AAddress,AType,ACost,Owner)values(@AN,@AAdd,@AT,@AC,@AO)", Con);
                    cmd.Parameters.AddWithValue("@AN", ApNameTb.Text);
                    cmd.Parameters.AddWithValue("@AAdd", AddressTb.Text);
                    cmd.Parameters.AddWithValue("@AT", TypeCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@AC", CostTb.Text);
                    cmd.Parameters.AddWithValue("@Ao", LLcb.SelectedValue.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartement Added!!!");
                    Con.Close();
                    ResetData();
                    ShowAparts();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void ApartmentsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ApNameTb.Text = ApartmentsDGV.SelectedRows[0].Cells[1].Value.ToString();
            AddressTb.Text = ApartmentsDGV.SelectedRows[0].Cells[2].Value.ToString();
            TypeCb.Text = ApartmentsDGV.SelectedRows[0].Cells[3].Value.ToString();
            CostTb.Text = ApartmentsDGV.SelectedRows[0].Cells[4].Value.ToString();
            LLcb.Text = ApartmentsDGV.SelectedRows[0].Cells[5].Value.ToString();

            if (ApNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ApartmentsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ApNameTb.Text == "" || LLcb.SelectedIndex == -1 || CostTb.Text == "" || TypeCb.SelectedIndex == -1 || AddressTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update ApartTbl set AName=@AN,AAddress=@AAdd,AType=@AT,ACost=@AC,Owner=@AO where ANum = @AKey", Con);
                    cmd.Parameters.AddWithValue("@AN", ApNameTb.Text);
                    cmd.Parameters.AddWithValue("@AAdd", AddressTb.Text);
                    cmd.Parameters.AddWithValue("@AT", TypeCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@AC", CostTb.Text);
                    cmd.Parameters.AddWithValue("@Ao", LLcb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@AKey", Key);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartement Updated!!!");
                    Con.Close();
                    ResetData();
                    ShowAparts();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select an Apartment!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ApartTbl where ANum-@AKey", Con);
                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Deleted!!!");
                    Con.Close() ;
                    ResetData();
                    ShowAparts();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Landforms Obj = new Landforms();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            //this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
