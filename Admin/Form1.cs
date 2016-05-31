using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;

namespace Admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         public void AddAdmin()
        {
            Admin admin = new Admin();
            string salt = PasswordHelper.GenerateRandomSalt();
            admin.PasswordHash = PasswordHelper.HashPassword(tbxPassword.Text, salt);
            admin.Salt = salt;
            
            admin.FirstName = tbxFirstName.Text;
            admin.LastName = txtbxLastName.Text;

            using (var connection = new SqlConnection(@"Data Source=YS\SQLEXPRESS1;Initial Catalog=Library;Integrated Security=True"))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText ="INSERT INTO Admin ( FirstName, LastName, PasswordHash, Salt) VALUES ( @firstName,@lastName, @password, @salt)";
            
                cmd.Parameters.AddWithValue("@password", admin.PasswordHash);
                cmd.Parameters.AddWithValue("@salt", admin.Salt);
                cmd.Parameters.AddWithValue("@firstName", admin.FirstName);
                cmd.Parameters.AddWithValue("@lastName", admin.LastName);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public class Admin
        {
            public int Id { get; set; }
            public string PasswordHash { get; set; }
            public string Salt { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

       

        private void button1_Click_1(object sender, EventArgs e)
        {
            AddAdmin();
        }

       
    
    }
}
