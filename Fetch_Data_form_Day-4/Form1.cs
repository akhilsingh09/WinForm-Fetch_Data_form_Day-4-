using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Fetch_Data_form_Day_4
{
    public partial class Form1 : Form
    {
        private SqlCommand GetCommand(string commandText)
        {
            string connectionString = string.Empty;

            connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(commandText, connection);
            return command;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            string query = ConfigurationManager.AppSettings["Fetch_Query"];

            SqlCommand command = GetCommand(query);
            SqlDataReader reader = command.ExecuteReader();

            BindingSource binding = new BindingSource();
            binding.DataSource = reader;

            dgvDetails.DataSource = binding;

        }

        private void btnNavigate_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 obj = new Form2();
            obj.Show();
            
        }

        
    }
}
