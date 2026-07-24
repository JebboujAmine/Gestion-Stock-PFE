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
    public partial class Fournisseur : Form
    {
        public Fournisseur()
        {
            InitializeComponent();
        }
        public string Cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            idf.Clear();
            nc.Clear();
            m.Clear();
            a.Clear();
            t.Clear();
            idf.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Fournisseur";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_Fournisseur"],
                    fiche["nom_complet"],
                    fiche["mail"],
                    fiche["adresse"],
                    fiche["telephone"]);
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                string Cmd = "insert into Fournisseur(id_Fournisseur,nom_complet,mail,adresse,telephone) values ( @IDF, @NC ,@M ,@A , @T) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@IDF", SqlDbType.Int).Value = int.Parse(idf.Text.Trim());
                insrt.Parameters.Add("@NC", SqlDbType.VarChar, 80).Value = nc.Text.Trim();
                insrt.Parameters.Add("@M", SqlDbType.VarChar, 20).Value = m.Text.Trim();
                insrt.Parameters.Add("@A", SqlDbType.VarChar, 200).Value = a.Text.Trim();
                insrt.Parameters.Add("@T", SqlDbType.VarChar, 20).Value = t.Text.Trim();

                insrt.ExecuteNonQuery();
                MessageBox.Show("Fournisseur Bien Ajouter");
            }
        }

        private void Fournisseur_Load(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Fournisseur";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_Fournisseur"],
                    fiche["nom_complet"],
                    fiche["mail"],
                    fiche["adresse"],
                    fiche["telephone"]);
                }
                fiche.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string search = "select * from Fournisseur where id_Fournisseur = @id ";
                SqlCommand Cmd = new SqlCommand(search, db);
                Cmd.Parameters.AddWithValue("@id", idf.Text.Trim());
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                   Fiche["id_Fournisseur"],
                   Fiche["nom_complet"],
                   Fiche["mail"],
                   Fiche["adresse"],
                   Fiche["telephone"]);

                }

                Fiche.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrWhiteSpace(idf.Text) ||
                    string.IsNullOrWhiteSpace(nc.Text) ||
                    string.IsNullOrWhiteSpace(m.Text) ||
                    string.IsNullOrWhiteSpace(a.Text) ||
                    string.IsNullOrWhiteSpace(t.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " update Fournisseur set nom_complet = @NC, mail = @M, adresse = @AD , telephone = @T where id_Fournisseur = @IDF ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@IDF", SqlDbType.Int).Value = int.Parse(idf.Text.Trim());
                updt.Parameters.Add("@NC", SqlDbType.VarChar, 80).Value = nc.Text.Trim();
                updt.Parameters.Add("@M", SqlDbType.VarChar, 100).Value = m.Text.Trim();
                updt.Parameters.Add("@AD", SqlDbType.VarChar, 200).Value = a.Text.Trim();
                updt.Parameters.Add("@T", SqlDbType.VarChar, 20).Value = t.Text.Trim();
                updt.ExecuteNonQuery();
                MessageBox.Show("Fournisseur Bien Modifier");

                string actualiser = "SELECT * from Fournisseur";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_Fournisseur"],
                        fiche["nom_complet"],
                        fiche["mail"],
                        fiche["adresse"],
                         fiche["telephone"]);
                }
                fiche.Close();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                                
                int idfournisseur;
                if (!int.TryParse(idf.Text.Trim(), out idfournisseur))
                {
                    MessageBox.Show("ID client invalide !");
                    return;
                }

                string deleteCmd = "DELETE FROM Fournisseur WHERE id_Fournisseur = @id";
                SqlCommand Cmd = new SqlCommand(deleteCmd, db);
                Cmd.Parameters.Add("@id", SqlDbType.Int).Value = idfournisseur;
                int rows = Cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Fournisseur Bien Supprimé");
                    string actualiser = "SELECT * FROM Fournisseur";
                    SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                    SqlDataReader fiche = refreshCmd.ExecuteReader();

                    dataGridView1.Rows.Clear();
                    while (fiche.Read())
                    {
                        dataGridView1.Rows.Add(
                            fiche["id_fournisseur"],
                             fiche["nom_complet"],
                             fiche["telephone"],
                             fiche["mail"],
                             fiche["adresse"]);
                    }

                    fiche.Close();
                }
            }
            }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
