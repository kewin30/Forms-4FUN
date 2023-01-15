using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Database databaseObj = new Database();
            //string query = "INSERT INTO Wypozycz (`Id`, `Vin`, `Imie`,`DataOdKiedy`,`DataDoKiedy`,`Nalezne`) VALUES (@Id, @Vin, @Imie, @DataOdKiedy, @DataDoKiedy, @Nalezne)";
            //SQLiteCommand mycommand = new SQLiteCommand(query, databaseObj.myconn);
            //databaseObj.OpenConnection();
            //mycommand.Parameters.AddWithValue("@Id", 1);
            //mycommand.Parameters.AddWithValue("@Vin", "AAg6691yai");
            //mycommand.Parameters.AddWithValue("@Imie", "Kewin");
            //mycommand.Parameters.AddWithValue("@DataOdKiedy", "20.04.2022");
            //mycommand.Parameters.AddWithValue("@DataDoKiedy","30.04.2022");
            //mycommand.Parameters.AddWithValue("@Nalezne", 3000);
            ////var result = mycommand.ExecuteNonQuery();
            //databaseObj.CloseConnection();

            string zapytanieAuto = "SELECT COUNT(*) AS IloscAut FROM Auto";
            string zapytanieKlienci = "SELECT COUNT(*) AS IloscKlientow FROM Tabela";
            string zapytanieDostepny = "SELECT COUNT(*) AS Dostepnosc FROM  Auto WHERE Dostepny =\"TAK\"";
            SQLiteCommand zapytanieA = new SQLiteCommand(zapytanieAuto, databaseObj.myconn);
            SQLiteCommand zapytanieK = new SQLiteCommand(zapytanieKlienci, databaseObj.myconn);
            SQLiteCommand zapytanieD = new SQLiteCommand(zapytanieDostepny, databaseObj.myconn);
            databaseObj.OpenConnection();
            SQLiteDataReader wynikA = zapytanieA.ExecuteReader();
            SQLiteDataReader wynikK = zapytanieK.ExecuteReader();
            SQLiteDataReader wynikD = zapytanieD.ExecuteReader();
            if(wynikA.HasRows)
            {
                wynikA.Read();
                int count = Convert.ToInt32(wynikA["IloscAut"].ToString());
                lblAuta.Text = "Ilosc aut w systemie: "+count.ToString();
            }
            wynikA.Close();
            if (wynikK.HasRows)
            {
                wynikK.Read();
                int count = Convert.ToInt32(wynikK["IloscKlientow"].ToString());
                lblKlienci.Text = "Ilosc klientów w systemie: " + count.ToString();
            }
            wynikK.Close();
            if (wynikD.HasRows)
            {
                wynikD.Read();
                int count = Convert.ToInt32(wynikD["Dostepnosc"].ToString());
                lblDostepnosc.Text = "Ilosc dostepnych aut: " + count.ToString();
            }
            wynikD.Close();

            databaseObj.CloseConnection();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rented rented = new Rented();
            rented.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            Clients clients = new Clients();
            clients.Show();
        }

        private void btnVehicles_Click(object sender, EventArgs e)
        {
            this.Hide();
            Vehicle vehicle = new Vehicle();
            vehicle.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void backgroundPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
