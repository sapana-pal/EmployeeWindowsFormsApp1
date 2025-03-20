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
using System.Xml.Linq;

namespace EmployeeWindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        }

        // Load Data
        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employee", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        // Add Employee
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "INSERT INTO Employee (Name, Email) VALUES (@Name, @Email)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                LoadData();
                MessageBox.Show("Record Added!");
            }
        }

        // Update Employee
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "UPDATE Employee SET Name = @Name, Email = @Email WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", dataGridView1.SelectedRows[0].Cells[0].Value);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                LoadData();
                MessageBox.Show("Record Updated!");
            }
        }

        // Delete Employee
        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "DELETE FROM Employee WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", dataGridView1.SelectedRows[0].Cells[0].Value);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                LoadData();
                MessageBox.Show("Record Deleted!");
            }
        }

        // Load Data on Form Load
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
