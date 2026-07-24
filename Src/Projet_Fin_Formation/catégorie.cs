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
    public partial class catégorie : Form
    {
        public catégorie()
        {
            InitializeComponent();
        }
        public string cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {
            IDC.Clear();
            NC.Clear();
            IDC.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();

                string actualiser = "SELECT id_categorie,nom_categorie FROM Categorie";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_categorie"],
                    fiche["nom_categorie"]);
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
                    string.IsNullOrWhiteSpace(NC.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = "insert into Categorie(id_categorie,nom_categorie) values ( @IDC, @NC) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@IDC", SqlDbType.Int).Value = int.Parse(IDC.Text.Trim());
                insrt.Parameters.Add("@NC", SqlDbType.VarChar, 100).Value = NC.Text.Trim();

                insrt.ExecuteNonQuery();
                MessageBox.Show("Categorie Bien Ajouter");

                string actualiser = "SELECT id_categorie,nom_categorie FROM Categorie";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_categorie"],
                         fiche["nom_categorie"]);
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
                    string.IsNullOrWhiteSpace(NC.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " update Categorie set nom_categorie = @NC where id_categorie = @IDC ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@IDC", SqlDbType.VarChar, 80).Value = IDC.Text.Trim();
                updt.Parameters.Add("@NC", SqlDbType.VarChar, 20).Value = NC.Text.Trim();

                updt.ExecuteNonQuery();
                MessageBox.Show("Catégorie Bien Modifier");

                string actualiser = "SELECT id_categorie,nom_categorie FROM Categorie";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_categorie"],
                         fiche["nom_categorie"]);
                }
                fiche.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();

                string actualiser = "select * from Categorie where id_categorie = @idc ";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                Cmd.Parameters.AddWithValue("@idc", IDC.Text.Trim());
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        Fiche["id_categorie"],
                        Fiche["nom_categorie"]
                        );

                }

                Fiche.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();
                string deleteCmd = "DELETE FROM Categorie WHERE id_categorie = @id";
                SqlCommand Cmd = new SqlCommand(deleteCmd, db);
                Cmd.Parameters.Add("@id", SqlDbType.Int).Value = IDC.Text;

                int rows = Cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Catégorie Bien Supprimé");

                    string actualiser = "SELECT id_categorie,nom_categorie FROM Categorie";
                    SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                    SqlDataReader fiche = refreshCmd.ExecuteReader();

                    dataGridView1.Rows.Clear();
                    while (fiche.Read())
                    {
                        dataGridView1.Rows.Add(
                            fiche["id_categorie"],
                             fiche["nom_categorie"]);
                    }

                    fiche.Close();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void catégorie_Load(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();

                string actualiser = "SELECT id_categorie,nom_categorie FROM Categorie";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_categorie"],
                    fiche["nom_categorie"]);
                }
                fiche.Close();
            }
        }
    }
}
