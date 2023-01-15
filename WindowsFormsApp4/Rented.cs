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
    public partial class Rented : Form
    {
        public Rented()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Database database = new Database();
            if (txtId.Text == "" || txtImie.Text == "" || txtNalezne.Text == "")
            {
                MessageBox.Show("Brakuje informacji");
            }
            else
            {
                try
                {
                    string query = "INSERT INTO Wypozycz (`Id`, `Vin`,`Imie`,`DataOdKiedy`,`DataDoKiedy`,`Nalezne`) VALUES (@Id, @Vin, @Imie, @DataOdKiedy, @DataDoKiedy,@Nalezne)";
                    SQLiteCommand mycommand = new SQLiteCommand(query, database.myconn);
                    database.OpenConnection();
                    int nalezne = int.Parse(txtNalezne.Text);
                    int id = int.Parse(txtId.Text);
                    mycommand.Parameters.AddWithValue("@Id", id);
                    mycommand.Parameters.AddWithValue("@Vin", cbVin.SelectedValue.ToString());
                    mycommand.Parameters.AddWithValue("@Imie", txtImie.Text);
                    mycommand.Parameters.AddWithValue("@DataOdKiedy", DataOd.Text);
                    mycommand.Parameters.AddWithValue("@DataDoKiedy", DataDo.Text);
                    mycommand.Parameters.AddWithValue("@Nalezne", nalezne);
                    var result = mycommand.ExecuteNonQuery();
                    database.CloseConnection();
                    MessageBox.Show("Pomyslnie dodano");
                    ZmienDostepnosc();
                    wyswietl();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void wyswietl()
        {
            Database database = new Database();
            database.OpenConnection();
            string zapytanie = "SELECT * FROM Wypozycz";
            SQLiteDataAdapter myadapter = new SQLiteDataAdapter(zapytanie, database.myconn);
            SQLiteCommandBuilder builder = new SQLiteCommandBuilder(myadapter);
            var ds = new DataSet();
            myadapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            database.CloseConnection();

        }
        private void UzupelnijVin()
        {
            Database database = new Database();
            database.OpenConnection();
            string zapytanie = "SELECT Vin FROM Auto WHERE Dostepny='TAK'";
            SQLiteCommand cmd = new SQLiteCommand(zapytanie, database.myconn);
            SQLiteDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Vin", typeof(string));
            dt.Load(dr);
            cbVin.ValueMember = "Vin";
            cbVin.DataSource = dt;
            database.CloseConnection();
        }
        private void ZmienDostepnosc()
        {

            Database database = new Database();
            database.OpenConnection();
            string zapytanie = "UPDATE Auto set Dostepny='" + "NIE" + "' where Vin='" + cbVin.Text.ToString() + "';";
            SQLiteCommand cmd = new SQLiteCommand(zapytanie, database.myconn);
            cmd.ExecuteNonQuery();
            database.CloseConnection();
        }
        private void ZmienDostepnoscNaTak()
        {
            Database database = new Database();
            database.OpenConnection();
            string zapytanie = "UPDATE Auto set Dostepny='" + "TAK" + "' where Vin='" + cbVin.Text.ToString() + "';";
            SQLiteCommand cmd = new SQLiteCommand(zapytanie, database.myconn);
            cmd.ExecuteNonQuery();
            database.CloseConnection();
        }
        private void UzupelnijPesel()
        {
            Database database = new Database();
            database.OpenConnection();
            string zapytanie = "SELECT Pesel FROM Tabela";
            SQLiteCommand cmd = new SQLiteCommand(zapytanie, database.myconn);
            SQLiteDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Pesel", typeof(Int64));
            dt.Load(rdr);
            cbPesel.ValueMember = "Pesel";
            cbPesel.DataSource = dt;
            database.CloseConnection();
        }
        private void znajdzImie()
        {
            Database database = new Database();
            database.OpenConnection();
            string zapytanie = "SELECT * FROM Tabela WHERE Pesel=" + cbPesel.SelectedValue.ToString() + "";
            SQLiteCommand cmd = new SQLiteCommand(zapytanie, database.myconn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                txtImie.Text = item["Imie"].ToString();
            }
            database.CloseConnection();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rented rented = new Rented();
            rented.Show();
        }

        private void Rented_Load(object sender, EventArgs e)
        {
            UzupelnijVin();
            UzupelnijPesel();
            wyswietl();
            //test();
        }

        private void cbPesel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            znajdzImie();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Database database = new Database();

            if (txtId.Text == "")
            {
                MessageBox.Show("Brakuje numeru ID");
            }
            else
            {
                try
                {
                    database.OpenConnection();
                    string szTablename = "Wypozycz";
                    string name = txtId.Text;
                    string zapytanie = "DELETE FROM " + szTablename + " WHERE Id='" + name + "';";
                    SQLiteCommand cmd = new SQLiteCommand(zapytanie, database.myconn);
                    cmd.ExecuteNonQuery();
                    database.CloseConnection();
                    MessageBox.Show("Usunieto pomyslnie");
                    ZmienDostepnoscNaTak();
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
            txtId.Text = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            cbVin.Text = dataGridView1.CurrentRow.Cells["Vin"].Value.ToString();
            txtImie.Text = dataGridView1.CurrentRow.Cells["Imie"].Value.ToString();
            DataOd.Text = dataGridView1.CurrentRow.Cells["DataOdKiedy"].Value.ToString();
            DataDo.Text = dataGridView1.CurrentRow.Cells["DataDoKiedy"].Value.ToString();
            txtNalezne.Text = dataGridView1.CurrentRow.Cells["Nalezne"].Value.ToString();
        }

        private void btnVehicles_Click(object sender, EventArgs e)
        {
            this.Hide();
            Vehicle vehicle = new Vehicle();
            vehicle.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            Clients clients = new Clients();
            clients.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }
    }
}
