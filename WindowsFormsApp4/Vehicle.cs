using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Vehicle : Form
    {
        public Vehicle()
        {
            InitializeComponent();
        }
        Database database = new Database();
        private void wyswietl()
        {
            database.OpenConnection();
            string query = "select * from Auto";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, database.myconn);
            SQLiteCommandBuilder builder = new SQLiteCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            database.CloseConnection();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string raddio = "";
            if (rdbYes.Checked)
            {
                raddio = rdbYes.Text;
            }
            else if (rdbNo.Checked)
            {
                raddio = rdbNo.Text;
            }

            if (txtVin.Text == "" || txtBrand.Text == "" || txtModel.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Brakuje informacji");
            }
            else
            {
                try
                {
                    database.OpenConnection();
                    var zapytanie = "Insert into Auto values('" + txtVin.Text + "','" + txtBrand.Text + "','" + txtModel.Text + "','" + txtPrice.Text + "','" + raddio + "')";
                    SQLiteCommand cmd = new SQLiteCommand(zapytanie, database.myconn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Auto dodane pomyslnie");
                    database.CloseConnection();
                    wyswietl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Vehicle_Load(object sender, EventArgs e)
        {
            wyswietl();
            //Test();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string raddio = "";
            if (rdbYes.Checked)
            {
                raddio = rdbYes.Text;
            }
            else if (rdbNo.Checked)
            {
                raddio = rdbNo.Text;
            }

            if (txtVin.Text == "" || txtBrand.Text == "" || txtModel.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Brakuje informacji");
            }
            else
            {
                try
                {
                    database.OpenConnection();
                    string zapytanie = "UPDATE Auto set Marka='" + txtBrand.Text + "', Model='" + txtModel.Text + "', Cena='" + txtPrice.Text + "', Dostepny='" + raddio + "'where Vin='" + txtVin.Text + "';";
                    SQLiteCommand cmd = new SQLiteCommand(zapytanie, database.myconn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Auto zmienione pomyslnie");
                    database.CloseConnection();
                    wyswietl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtVin.Text == "")
            {
                MessageBox.Show("Brakuje numeru VIN");
            }
            else
            {
                try
                {
                    database.OpenConnection();
                    string zapytanie = "DELETE from Auto where Vin='" + txtVin.Text + "';";
                    SQLiteCommand cmd = new SQLiteCommand(zapytanie, database.myconn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Usunieto pomyslnie");
                    database.CloseConnection();
                    wyswietl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtVin.Text = dataGridView1.CurrentRow.Cells["Vin"].Value.ToString();
            txtBrand.Text = dataGridView1.CurrentRow.Cells["Marka"].Value.ToString();
            txtModel.Text = dataGridView1.CurrentRow.Cells["Model"].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells["Cena"].Value.ToString();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            Clients clients = new Clients();
            clients.Show();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rented rented = new Rented();
            rented.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }
    }
}
