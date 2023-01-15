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
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }
        Database database = new Database();
        private void wyswietl()
        {
            database.OpenConnection();
            string query = "select * from Tabela";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, database.myconn);
            SQLiteCommandBuilder builder = new SQLiteCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            database.CloseConnection();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtImie.Text == "" || txtNazwisko.Text == "" || txtEmail.Text == "" || txtTelefon.Text == "" || txtAddress.Text == "" || txtPesel.Text == "")
            {
                MessageBox.Show("Brakuje informacji");
            }
            else
            {
                try
                {
                    database.OpenConnection();
                    string zapytanie = "INSERT INTO Tabela (`Pesel`, `Imie`,`Nazwisko`,`Email`,`Telefon`,`Adres`) VALUES (@Pesel, @Imie, @Nazwisko, @Email, @Telefon,@Adres)";
                    SQLiteCommand cmd = new SQLiteCommand(zapytanie, database.myconn);
                    Int64 pesel = Int64.Parse(txtPesel.Text);
                    int telefon = int.Parse(txtTelefon.Text);
                    cmd.Parameters.AddWithValue("@Pesel", pesel);
                    cmd.Parameters.AddWithValue("@Imie", txtImie.Text);
                    cmd.Parameters.AddWithValue("@Nazwisko", txtNazwisko.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Telefon", telefon);
                    cmd.Parameters.AddWithValue("@Adres", txtAddress.Text);
                    var result = cmd.ExecuteNonQuery();
                    database.CloseConnection();
                    MessageBox.Show("Uzytkownik dodany pomyslnie");
                    database.CloseConnection();
                    wyswietl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            wyswietl();
            //Test
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtPesel.Text == "")
            {
                MessageBox.Show("Brakuje PESEL'u");
            }
            else
            {
                try
                {
                    database.OpenConnection();
                    string query = "delete from Tabela where Pesel=" + txtPesel.Text + ";";
                    SQLiteCommand cmd = new SQLiteCommand(query, database.myconn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Uzytkownik skasowany.");
                    database.CloseConnection();
                    wyswietl();
                }
                catch (Exception myex)
                {
                    MessageBox.Show(myex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtImie.Text == "" || txtNazwisko.Text == "" || txtEmail.Text == "" || txtPesel.Text == "" || txtTelefon.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Brakuje informacji");
            }
            else
            {
                try
                {
                    database.OpenConnection();
                    string zapytanie = "UPDATE Tabela set Imie='" + txtImie.Text + "', Nazwisko='" + txtNazwisko.Text + "', Email='" + txtEmail.Text + "', telefon='" + txtTelefon.Text + "', Adres='" + txtAddress.Text + "'where Pesel=" + txtPesel.Text + ";";
                    SQLiteCommand cmd = new SQLiteCommand(zapytanie, database.myconn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Uzytkownik zmieniony pomyslnie");
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

        private void btnVehicles_Click(object sender, EventArgs e)
        {
            this.Hide();
            Vehicle vehicle = new Vehicle();
            vehicle.Show();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rented rented = new Rented();
            rented.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtImie.Text = dataGridView1.CurrentRow.Cells["Imie"].Value.ToString();
            txtNazwisko.Text = dataGridView1.CurrentRow.Cells["Nazwisko"].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells["Telefon"].Value.ToString();
            txtAddress.Text = dataGridView1.CurrentRow.Cells["Adres"].Value.ToString();
            txtPesel.Text = dataGridView1.CurrentRow.Cells["Pesel"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }
    }
}
