using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Projet_Fin_Formation
{
    public partial class Bon_Livraison : Form
    {
        public Bon_Livraison()
        {
            InitializeComponent();
        }
        public string Cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";
        public int idbon;
        private void button1_Click(object sender, EventArgs e)
        {
            idb.Clear();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            a.Clear();
            idb.Select();


        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Bon_Livraison";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_bon_Livraison"],
                    fiche["date_livraison"],
                    fiche["adresse"]);
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                string Cmd = "insert into Bon_Livraison(id_bon_Livraison, date_livraison, adresse) values ( @IDBL, @DL ,@AD) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@IDBL", SqlDbType.Int).Value = int.Parse(idb.Text.Trim());
                insrt.Parameters.Add("@DL", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                insrt.Parameters.Add("@AD", SqlDbType.VarChar , 50).Value = a.Text.Trim();
                insrt.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Bon_Livraison";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_bon_Livraison"],
                    fiche["date_livraison"],
                    fiche["adresse"]);
                }
                fiche.Close();

                MessageBox.Show("Bon Livraison Bien Ajouter");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "select * from Bon_Livraison where id_bon_Livraison = @IDB ";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                Cmd.Parameters.AddWithValue("@IDB", idb.Text.Trim());
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                     Fiche["id_bon_Livraison"],
                    Fiche["date_livraison"],
                    Fiche["adresse"]);
                }

                Fiche.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrEmpty(idb.Text) ||
                    dateTimePicker1.Value < DateTime.Now ||
                    string.IsNullOrEmpty(a.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }

                if (!int.TryParse(idb.Text.Trim(), out idbon))
                {
                    MessageBox.Show("ID Bon livraison invalide !");
                    return;
                }
                string Cmd = " update Bon_Livraison set date_livraison = @D ,adresse = @AD  where id_bon_Livraison = @IDB ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@IDB", SqlDbType.Int).Value = int.Parse(idb.Text.Trim());
                updt.Parameters.Add("@D", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                updt.Parameters.Add("@AD", SqlDbType.VarChar, 50).Value = a.Text.Trim();
                
                updt.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Bon_Livraison";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                     fiche["id_bon_Livraison"],
                     fiche["date_livraison"],
                    fiche["adresse"]);
                }
                fiche.Close();
                MessageBox.Show("Bon livraison Bien Modifier");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();

                
                int idbon;
                if (!int.TryParse(idb.Text.Trim(), out idbon))
                {
                    MessageBox.Show("ID produit ou de catégorie invalide !");
                    return;
                }

                string deleteCmd = "DELETE FROM Bon_Livraison WHERE id_bon_Livraison = @IDB ";

                SqlCommand Cmd = new SqlCommand(deleteCmd, db);
                Cmd.Parameters.Add("@IDB", SqlDbType.Int).Value = idbon;

                int rows = Cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Bon livraison Bien Supprimé");
                    string actualiser = "SELECT * FROM Bon_Livraison";
                    SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                    SqlDataReader Fiche = refreshCmd.ExecuteReader();

                    dataGridView1.Rows.Clear();
                    while (Fiche.Read())
                    {
                        dataGridView1.Rows.Add(
                    Fiche["id_bon_Livraison"],
                   Fiche["date_livraison"],
                   Fiche["adresse"]);
                    }

                    Fiche.Close();
                }
                else
                {
                    MessageBox.Show("Aucun produit trouvé avec cet ID");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Bon_Livraison_Load(object sender, EventArgs e)
        {

        }
    }
}
