using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Fetch_Data_form_Day_4
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private SqlCommand GetCommand(string commandText)
        {
            string connectionString = string.Empty;

            connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(commandText, connection);
            return command;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = ConfigurationManager.AppSettings["Fetch_Query"];
            string query1 = ConfigurationManager.AppSettings["Fetch_Query_S"];

            SqlCommand command = GetCommand(query);
            SqlCommand command1 = GetCommand(query1);
            DataSet ds = new DataSet();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //adapter.SelectCommand = command;
            //DataSet ds = new DataSet("Persons");
            //adapter.Fill(ds);

            adapter.Fill(ds,"Persons");
            dgvForDataset.DataSource = ds.Tables["Persons"];
            
            ds = new DataSet();
            adapter1.Fill(ds, "Students");
            dgvStudents.DataSource = ds.Tables["Students"];




            //SqlDataReader reader = command.ExecuteReader();

            //BindingSource binding = new BindingSource();
            //binding.DataSource = reader;

            //dgvDetails.DataSource = binding;

        }

    }
}
