using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Fetch_Data_form_Day_4
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=SampleDB;Integrated Security=True");
        public int StudentId;
        System.Random random = new System.Random();
        private void button5_Click(object sender, EventArgs e)
        {
            if(StudentId > 0)
            {
                SqlCommand cmd = new SqlCommand("Update Students SET Name =@Name, FatherName=@FatherName, RollNumber=@RollNo, Address=@Address, Mobile=@Mobile WHERE StudentId=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtS_Name.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtF_Name.Text);
                cmd.Parameters.AddWithValue("@RollNo", txtRollNo.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student info updated in db", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStuddentRecord();
                Reset_form();
            }
            else
            {
                MessageBox.Show("Please Select student to update info","Select!!");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            GetStuddentRecord();
        }

        private void GetStuddentRecord()
        {
            
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Students", con);
            DataTable dt = new DataTable();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            StudentRecordDataView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(isValid())
            {
                SqlCommand cmd = new SqlCommand("Insert into Students(StudentId,Name, FatherName,RollNumber,Address, Mobile) values (@Id,@Name, @FatherName, @RollNo,@Address,@Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtS_Name.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtF_Name.Text);
                cmd.Parameters.AddWithValue("@RollNo", txtRollNo.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@Id", random.Next());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New student is saved in db", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStuddentRecord();
                Reset_form();

            }
        }

        private bool isValid()
        {
            if(txtS_Name.Text == string.Empty)
            {
                MessageBox.Show("Student name is required", "Failed", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset_form();
        }

        private void Reset_form()
        {
            StudentId = 0;
            txtS_Name.Clear();
            txtF_Name.Clear();
            txtRollNo.Clear();
            txtAddress.Clear();
            txtMobile.Clear();

            txtS_Name.Focus();
        }

        private void StudentRecordDataView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentId = (int)StudentRecordDataView.SelectedRows[0].Cells[0].Value;
            txtS_Name.Text = StudentRecordDataView.SelectedRows[0].Cells[1].Value.ToString();
            txtF_Name.Text = StudentRecordDataView.SelectedRows[0].Cells[2].Value.ToString();
            txtRollNo.Text = StudentRecordDataView.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = StudentRecordDataView.SelectedRows[0].Cells[4].Value.ToString();
            txtMobile.Text = StudentRecordDataView.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(StudentId > 0)
            {
                SqlCommand cmd = new SqlCommand("Delete from Students WHERE StudentId=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudentId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student info deleted from db", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStuddentRecord();
                Reset_form();
            }
            else
            {
                MessageBox.Show("Please Select student to delete info", "Select!!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 obj = new Form3();
            obj.Show();
        }
    }
}
