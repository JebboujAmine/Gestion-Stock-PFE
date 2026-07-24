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
    public partial class Ligne_Stock : Form
    {
        public Ligne_Stock()
        {
            InitializeComponent();
        }
        public string Cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            IDE.Clear();
            IDP.Clear();
            Q.Clear();
            IDE.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Ligne_Stock";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_produit"],
                    fiche["id_emplacement"],
                    fiche["Quantite"]);
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                string Cmd = " INSERT into Ligne_Stock (id_produit, id_emplacement,Quantite) " + "values ( @idp, @ide,@q) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                insrt.Parameters.Add("@ide", SqlDbType.Int).Value = int.Parse(IDE.Text.Trim());
                insrt.Parameters.Add("@q", SqlDbType.Int).Value = int.Parse(Q.Text.Trim());
                insrt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_LigneStock", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_emplacement", int.Parse(IDE.Text.Trim()));
                cmd.Parameters.AddWithValue("@Quantite", int.Parse(Q.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmd.ExecuteNonQuery();



                string actualiser = "SELECT * FROM Ligne_Stock";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                     fiche["id_produit"],
                     fiche["id_emplacement"],
                     fiche["Quantite"]);
                }
                fiche.Close();
                MessageBox.Show("Ligne d'achat Bien Ajouter");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string srch = "SELECT * FROM Ligne_Stock WHERE id_produit = @idp AND id_emplacement = @ide";
                SqlCommand cmd = new SqlCommand(srch, db);
                cmd.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text);
                cmd.Parameters.Add("@ide", SqlDbType.Int).Value = int.Parse(IDE.Text);
                SqlDataReader fiche = cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                         fiche["id_produit"],
                         fiche["id_emplacement"],
                         fiche["Quantite"]);
                }
                fiche.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDE.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " UPDATE Ligne_Stock SET Quantite = @q WHERE id_produit = @idp AND id_emplacement = @ide ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                updt.Parameters.Add("@ide", SqlDbType.Int).Value = int.Parse(IDE.Text.Trim());
                updt.Parameters.Add("@q", SqlDbType.Int).Value = int.Parse(Q.Text.Trim());

                updt.ExecuteNonQuery();
                int oldQ;
                if (!int.TryParse(Q.Text.Trim(), out oldQ))
                {
                    MessageBox.Show("Quantité de stockage invalide !");
                    return;
                }
                int qte = oldQ;
                SqlCommand cmd = new SqlCommand("prc_Audit_LigneStock", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_emplacement", int.Parse(IDE.Text.Trim()));
                cmd.Parameters.AddWithValue("@Quantite", int.Parse(Q.Text.Trim()));
                cmd.Parameters.AddWithValue("@Ancien_Quantite", qte);
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmd.ExecuteNonQuery();


                string actualiser = "SELECT * FROM Ligne_Stock";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                       fiche["id_produit"],
                         fiche["id_emplacement"],
                         fiche["Quantite"]);
                }
                fiche.Close();
                MessageBox.Show("Ligne de stock bien modifié");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

                using (SqlConnection db = new SqlConnection(Cnx))
                {
                    db.Open();

                    if (string.IsNullOrEmpty(IDE.Text) || string.IsNullOrEmpty(IDP.Text))
                    {
                        MessageBox.Show("Champs obligatoires manquants !");
                        return;
                    }

                    
                    int oldQte = 0;
                    using (SqlCommand sel = new SqlCommand( "SELECT Quantite FROM Ligne_Stock WHERE id_emplacement = @ide AND id_produit= @idp", db))
                    {
                        sel.Parameters.AddWithValue("@ide", int.Parse(IDE.Text.Trim()));
                        sel.Parameters.AddWithValue("@idp", int.Parse(IDP.Text.Trim()));
                        var r = sel.ExecuteReader();
                        if (r.Read()) oldQte = (int)r["Quantite"];
                        r.Close();
                    }

                    
                    using (SqlCommand dlt = new SqlCommand(
                        "DELETE FROM Ligne_Stock WHERE id_emplacement=@ide AND id_produit=@idp", db))
                    {
                        dlt.Parameters.AddWithValue("@ide", int.Parse(IDE.Text.Trim()));
                        dlt.Parameters.AddWithValue("@idp", int.Parse(IDP.Text.Trim()));
                        dlt.ExecuteNonQuery();
                    }

                    // Audit
                    using (SqlCommand cmd = new SqlCommand("prc_Audit_LigneStock", db))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "DELETE");
                        cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text.Trim()));
                        cmd.Parameters.AddWithValue("@id_emplacement", int.Parse(IDE.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Quantite", oldQte);
                        cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                        cmd.ExecuteNonQuery();
                    }

                    
                    SqlCommand refreshCmd = new SqlCommand("SELECT * FROM Ligne_Stock", db);
                    SqlDataReader fiche = refreshCmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (fiche.Read())
                        dataGridView1.Rows.Add(
                            fiche["id_produit"],
                            fiche["id_emplacement"],
                            fiche["Quantite"]);
                    fiche.Close();

                    MessageBox.Show("Ligne de stock bien supprimée");
                }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
