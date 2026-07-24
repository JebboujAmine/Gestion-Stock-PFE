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
    public partial class Fournir : Form
    {
        public Fournir()
        {
            InitializeComponent();
        }
        public string Cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            IDP.Clear();
            IDF.Clear();
            Q.Clear();
            IDP.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Fournir";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_produit"],
                    fiche["id_fournisseur"],
                    fiche["quantite"]);
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrWhiteSpace(IDP.Text) ||
                    string.IsNullOrWhiteSpace(IDF.Text) ||
                    string.IsNullOrWhiteSpace(Q.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = "insert into Fournir(id_produit,id_fournisseur,quantite) values ( @idp, @idf,@q) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                insrt.Parameters.Add("@idf", SqlDbType.Int).Value = int.Parse(IDF.Text.Trim());
                insrt.Parameters.Add("@q", SqlDbType.Int).Value = int.Parse(Q.Text.Trim());

                insrt.ExecuteNonQuery();
                MessageBox.Show("Domande de Fourniture Bien Ajouter");

                string actualiser = "SELECT * FROM Fournir";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_produit"],
                        fiche["id_fournisseur"],
                         fiche["quantite"]);
                }
                fiche.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "select * from Fournir where id_produit = @idp and id_fournisseur = @idf ";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                Cmd.Parameters.Add("@idf", SqlDbType.Int).Value = int.Parse(IDF.Text.Trim());
                Cmd.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                SqlDataReader fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_produit"],
                        fiche["id_fournisseur"],
                         fiche["quantite"]);
                }

                fiche.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrWhiteSpace(IDF.Text) ||
                    string.IsNullOrWhiteSpace(IDP.Text) ||
                    string.IsNullOrWhiteSpace(Q.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " update Fournir set quantite = @q where id_fournisseur = @idf and id_produit = @idp ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@idf", SqlDbType.VarChar, 80).Value = int.Parse(IDF.Text.Trim());
                updt.Parameters.Add("@idp", SqlDbType.VarChar, 20).Value = int.Parse(IDP.Text.Trim());
                updt.Parameters.Add("@q", SqlDbType.VarChar, 20).Value = int.Parse(Q.Text.Trim());


                updt.ExecuteNonQuery();
                MessageBox.Show("Domande de Fourniture Bien Modifier");

                string actualiser = "SELECT * FROM Fournir";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_produit"],
                        fiche["id_fournisseur"],
                         fiche["quantite"]);
                }
                fiche.Close();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();
                string deleteCmd = "DELETE FROM Fournir WHERE id_fournisseur = @idf and  id_produit = @idp";
                SqlCommand Cmd = new SqlCommand(deleteCmd, db);
                Cmd.Parameters.Add("@idf", SqlDbType.VarChar, 80).Value = int.Parse(IDF.Text.Trim());
                Cmd.Parameters.Add("@idp", SqlDbType.VarChar, 20).Value = int.Parse(IDP.Text.Trim());

                int rows = Cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Domande de Fourniture Bien Supprimé");

                    string actualiser = "SELECT * FROM Fournir";
                    SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                    SqlDataReader fiche = refreshCmd.ExecuteReader();

                    dataGridView1.Rows.Clear();
                    while (fiche.Read())
                    {
                        dataGridView1.Rows.Add(
                            fiche["id_produit"],
                            fiche["id_fournisseur"],
                             fiche["quantite"]);
                    }

                    fiche.Close();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Fournir_Load(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Fournir";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_produit"],
                    fiche["id_fournisseur"],
                    fiche["quantite"]);
                }
                fiche.Close();
            }
        }
    }
}
