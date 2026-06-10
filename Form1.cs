using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ContactManagementSystem
{
    public class Form1 : Form
    {
        private TextBox txtId;
        private TextBox txtName;
        private TextBox txtEmail;
        private DataGridView dgv;
        private void LoadContacts()
        {
            dgv.Rows.Clear();

            string connStr = "server=localhost;database=contactdb;uid=root;pwd=;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT id,name,email FROM contacts";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dgv.Rows.Add(
                        reader["id"].ToString(),
                        reader["name"].ToString(),
                        reader["email"].ToString()
                    );
                }
            }
        }


        string connectionString =
             
            "server=localhost;database=ContactDB;uid=root;pwd=;";

        public Form1()
        {
            Text = "CONTACT MANAGEMENT SYSTEM";
            WindowState = FormWindowState.Maximized;
            BackColor = Color.White;

            //  central panal
            Panel panel = new Panel();
            panel.Size = new Size(900, 650);
            panel.Left = (Screen.PrimaryScreen.WorkingArea.Width - panel.Width) / 2;
            panel.Top = 20;

            // title for app
            Label title = new Label();
            title.Text = "CONTACT MANAGEMENT SYSTEM";
            title.Font = new Font("Arial", 22, FontStyle.Bold);
            title.AutoSize = true;
            title.Left = 180;
            title.Top = 20;

            // ID for app
            Label lblId = new Label();
            lblId.Text = "ID";
            lblId.Font = new Font("Arial", 12);
            lblId.Left = 120;
            lblId.Top = 100;

            txtId = new TextBox();
            txtId.Size = new Size(350, 30);
            txtId.Left = 250;
            txtId.Top = 95;

            // label for name
            Label lblName = new Label();
            lblName.Text = "Name";
            lblName.Font = new Font("Arial", 12);
            lblName.Left = 120;
            lblName.Top = 150;

            txtName = new TextBox();
            txtName.Size = new Size(350, 30);
            txtName.Left = 250;
            txtName.Top = 145;

            // label for Email
            Label lblEmail = new Label();
            lblEmail.Text = "Email";
            lblEmail.Font = new Font("Arial", 12);
            lblEmail.Left = 120;
            lblEmail.Top = 200;

            txtEmail = new TextBox();
            txtEmail.Size = new Size(350, 30);
            txtEmail.Left = 250;
            txtEmail.Top = 195;

            //  Add button  for giving or adding data
            Button btnAdd = new Button();
            btnAdd.Text = "Add Contact";
            btnAdd.Size = new Size(150, 45);
            btnAdd.Left = 250;
            btnAdd.Top = 260;
            btnAdd.Click += AddContact;

            //  Delete button for remove data 
            Button btnDelete = new Button();
            btnDelete.Text = "Delete Contact";
            btnDelete.Size = new Size(150, 45);
            btnDelete.Left = 450;
            btnDelete.Top = 260;
            btnDelete.Click += DeleteContact;

            // dia gram or jadwal  for database
            dgv = new DataGridView();
            dgv.Left = 50;
            dgv.Top = 340;
            dgv.Width = 800;
            dgv.Height = 250;

            dgv.ColumnCount = 3;
            dgv.Columns[0].Name = "ID";
            dgv.Columns[1].Name = "Name";
            dgv.Columns[2].Name = "Email";

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // افزودن به پنل
            panel.Controls.Add(title);
            panel.Controls.Add(lblId);
            panel.Controls.Add(txtId);
            panel.Controls.Add(lblName);
            panel.Controls.Add(txtName);
            panel.Controls.Add(lblEmail);
            panel.Controls.Add(txtEmail);
            panel.Controls.Add(btnAdd);
            panel.Controls.Add(btnDelete);
            panel.Controls.Add(dgv);

            Controls.Add(panel);
            LoadContacts();
        }

        private void AddContact(object sender, EventArgs e)
        {
            using (MySqlConnection conn =
                   new MySqlConnection(connectionString))
            {
                conn.Open();

                string query =
                    "INSERT INTO Contacts(Id,Name,Email) VALUES(@id,@name,@email)";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", txtId.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                cmd.ExecuteNonQuery();
                LoadContacts();
            }

            dgv.Rows.Add(txtId.Text, txtName.Text, txtEmail.Text);

            txtId.Clear();
            txtName.Clear();
            txtEmail.Clear();
        }

        private void DeleteContact(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                string id =
                    dgv.SelectedRows[0].Cells[0].Value.ToString();

                using (MySqlConnection conn =
                       new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query =
                        "DELETE FROM Contacts WHERE Id=@id";

                    MySqlCommand cmd =
                        new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

                dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);
            }
        }
    }
}