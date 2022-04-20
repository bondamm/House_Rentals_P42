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
    public partial class Landforms : Form
    {
        public Landforms()
        {
            InitializeComponent();
            ShowLLords();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bondamm\Documents\HouseRent.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowLLords()
        {
            Con.Open();
            string Query = "Select * from LandLordTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            landLordsDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void ResetData()
        {
            PhoneTb.Text = "";
            GenCb.SelectedIndex = -1;
            LLNameTb.Text = "";
        }
        private void Save_Click(object sender, EventArgs e)
        {
            if (LLNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into LandLordTbl(LLName,LLPhone,LLGen)values(@LLN,@LLP,@LLG)", Con);
                    cmd.Parameters.AddWithValue("@LLN", LLNameTb.Text);
                    cmd.Parameters.AddWithValue("@LLP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@LLG", GenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("LandLord Added!!!");
                    Con.Close();
                    ResetData();
                    ShowLLords();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void landLordsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LLNameTb.Text = landLordsDGV.SelectedRows[0].Cells[1].Value.ToString();
            PhoneTb.Text = landLordsDGV.SelectedRows[0].Cells[2].Value.ToString();
            GenCb.Text = landLordsDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (LLNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(landLordsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a LandLord!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from LandLordTbl where LLID-@LLKey", Con);
                    cmd.Parameters.AddWithValue("@LLKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Landlord Deleted!!!");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (LLNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update LandlordTbl set LLName=@LLN,LLPhone=@LLP,LLgen=@LLG where LLID=@LLKey", Con);
                    cmd.Parameters.AddWithValue("@LLN", LLNameTb.Text);
                    cmd.Parameters.AddWithValue("@LLP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@LLG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LLKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("LandLord Updated!!!");
                    Con.Close();
                    ResetData();
                    ShowLLords();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Apartments Obj = new Apartments();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
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
