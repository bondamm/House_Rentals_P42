using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace House_Rental
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Reset()
        {
            UNameTb.Text = "";
            PasswordTb.Text = "";
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter Both UserName and Password!!!");
                Reset();
            } else if (UNameTb.Text == "Admin" && PasswordTb.Text == "Admin")
{
                    Tenants Obj = new Tenants();
                    Obj.Show();
                    this.Hide();
                }else
                {
                MessageBox.Show("Wrong UserName Or Password!!!");
                Reset();
                }
            }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UNameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
