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
    public partial class Commande_Achat : Form
    {
        public Commande_Achat()
        {
            InitializeComponent();
        }
        public string cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            IDC.Clear();
            ST.Clear();
            IDU.Clear();
            IDF.Clear();
            IDC.Select();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {
                db.Open();
                string actualise = "select * from Commande_Achat";
                SqlCommand cmd = new SqlCommand(actualise, db);
                SqlDataReader fiche = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_commande"],
                    fiche["date_commande"],
                    fiche["statut"],
                    fiche["id_utilisateur"],
                    fiche["id_fournisseur"]);
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {
                db.Open();
                if (string.IsNullOrWhiteSpace(IDC.Text) ||
                    string.IsNullOrWhiteSpace(ST.Text) ||
                    string.IsNullOrWhiteSpace(IDU.Text) ||
                    string.IsNullOrWhiteSpace(IDF.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = "insert into Commande_Achat(id_commande,Statut,id_utilisateur,id_fournisseur) values ( @idc, @s,@idu,@idf) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@idc", SqlDbType.Int).Value = int.Parse(IDC.Text.Trim());
                insrt.Parameters.Add("@s", SqlDbType.VarChar, 100).Value = ST.Text.Trim();
                insrt.Parameters.Add("@idu", SqlDbType.Int).Value = int.Parse(IDU.Text.Trim());
                insrt.Parameters.Add("@idf", SqlDbType.Int).Value = int.Parse(IDF.Text.Trim());
                insrt.ExecuteNonQuery();
                MessageBox.Show("Commande Bien Ajouter");

                string actualiser = "SELECT * FROM Commande_Achat";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                       fiche["id_commande"],
                       fiche["date_commande"],
                       fiche["statut"],
                       fiche["id_utilisateur"],
                       fiche["id_fournisseur"]);
                }
                fiche.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {
                db.Open();
                string recherche = "select * from Commande_Achat where id_commande = @id";
                SqlCommand cmd = new SqlCommand(recherche, db);
                cmd.Parameters.AddWithValue("@id", IDC.Text.Trim());
                SqlDataReader fiche = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_commande"],
                    fiche["date_commande"],
                    fiche["statut"],
                    fiche["id_utilisateur"],
                    fiche["id_fournisseur"]);
                }
                fiche.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {
                db.Open();
                if (string.IsNullOrWhiteSpace(IDC.Text) ||
                    string.IsNullOrWhiteSpace(ST.Text) ||
                    string.IsNullOrWhiteSpace(IDU.Text) ||
                    string.IsNullOrWhiteSpace(IDF.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " update Commande_Achat set statut = @s,id_utilisateur = @idc,id_fournisseur = @idf where id_commande = @idc ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@idc", SqlDbType.Int).Value = int.Parse(IDC.Text.Trim());
                updt.Parameters.Add("@s", SqlDbType.VarChar, 100).Value = ST.Text.Trim();
                updt.Parameters.Add("@idu", SqlDbType.Int).Value = int.Parse(IDU.Text.Trim());
                updt.Parameters.Add("@idf", SqlDbType.Int).Value = int.Parse(IDF.Text.Trim());
                updt.ExecuteNonQuery();
                MessageBox.Show("Commande Bien Modifier");

                string recherche = "select * from Commande_Achat where id_commande = @id";
                SqlCommand cmd = new SqlCommand(recherche, db);
                cmd.Parameters.AddWithValue("@id", IDC.Text.Trim());
                SqlDataReader fiche = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_commande"],
                    fiche["date_commande"],
                    fiche["statut"],
                    fiche["id_utilisateur"],
                    fiche["id_fournisseur"]);
                }
                fiche.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();
                string deleteCmd = "DELETE FROM Commande_Achat WHERE id_commande = @id";
                SqlCommand Cmd = new SqlCommand(deleteCmd, db);
                Cmd.Parameters.Add("@id", SqlDbType.Int).Value = IDC.Text;

                int rows = Cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Commande D'achat Bien Supprimé");

                    string recherche = "select * from Commande_Achat where id_commande = @id";
                    SqlCommand cmd = new SqlCommand(recherche, db);
                    cmd.Parameters.AddWithValue("@id", IDC.Text.Trim());
                    SqlDataReader fiche = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (fiche.Read())
                    {
                        dataGridView1.Rows.Add(
                        fiche["id_commande"],
                        fiche["date_commande"],
                        fiche["statut"],
                        fiche["id_utilisateur"],
                        fiche["id_fournisseur"]);
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
