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
    public partial class Ligne_Vente : Form
    {
        public Ligne_Vente()
        {
            InitializeComponent();
        }
        public string Cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";
        private void Ligne_Vente_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IDV.Clear();
            IDP.Clear();
            IDL.Clear();
            PV.Clear();
            Q.Clear();
            IDV.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Ligne_Vente";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_vente"],
                    fiche["id_produit"],
                    fiche["id_lot"],
                    fiche["prix_Vente"],
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
                string Cmd = "insert into Ligne_Vente (id_vente, id_produit,id_lot,prix_Vente,Quantite) values ( @idv, @idp ,@idl,@pv,@q) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@idv", SqlDbType.Int).Value = int.Parse(IDV.Text.Trim());
                insrt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                insrt.Parameters.Add("@idl", SqlDbType.Int).Value = int.Parse(IDL.Text.Trim());
                insrt.Parameters.Add("@pv", SqlDbType.Money).Value = decimal.Parse(PV.Text.Trim());
                insrt.Parameters.Add("@q", SqlDbType.Int).Value = int.Parse(Q.Text.Trim());
                insrt.ExecuteNonQuery();

                
                SqlCommand cmd = new SqlCommand("prc_Audit_LigneVente", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@id_vente", int.Parse(IDV.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_lot", int.Parse(IDL.Text.Trim()));                
                cmd.Parameters.AddWithValue("@Prix_Vente", decimal.Parse(PV.Text.Trim()));
                cmd.Parameters.AddWithValue("@Quantite", int.Parse(Q.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                
                cmd.ExecuteNonQuery();
                
                string actualiser = "SELECT * FROM Ligne_Vente";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_vente"],
                    fiche["id_produit"],
                    fiche["id_lot"],
                    fiche["prix_Vente"],
                    fiche["Quantite"]);
                }
                fiche.Close();
                MessageBox.Show("Ligne de vente Bien Ajouter");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDV.Text) || string.IsNullOrEmpty(IDL.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " UPDATE Ligne_Vente SET prix_Vente = @pv, Quantite = @q WHERE id_vente = @idv AND id_produit = @idp AND id_lot = @idl ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@idv", SqlDbType.Int).Value = int.Parse(IDV.Text.Trim());
                updt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                updt.Parameters.Add("@idl", SqlDbType.Int).Value = int.Parse(IDL.Text.Trim());
                updt.Parameters.Add("@pv", SqlDbType.Money).Value = decimal.Parse(PV.Text.Trim());
                updt.Parameters.Add("@q", SqlDbType.Int).Value = int.Parse(Q.Text.Trim());
                updt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_LigneVente", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@id_vente", int.Parse(IDV.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_lot", int.Parse(IDL.Text.Trim()));
                cmd.Parameters.AddWithValue("@prix_Vente", decimal.Parse(PV.Text.Trim()));
                cmd.Parameters.AddWithValue("@Quantite", int.Parse(Q.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmd.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Ligne_Vente";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_vente"],
                    fiche["id_produit"],
                    fiche["id_lot"],
                    fiche["prix_Vente"],
                    fiche["Quantite"]);
                }
                fiche.Close();

                MessageBox.Show("Ligne de vente Bien Modifier");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();

                if (string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDV.Text) || string.IsNullOrEmpty(IDL.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }

                int oldQuantite = 0;
                decimal oldPrix = 0;
                using (SqlCommand sel = new SqlCommand(
                    "SELECT Quantite, Prix_Vente FROM Ligne_Vente WHERE id_vente=@idv AND id_produit=@idp AND id_lot=@idl", db))
                {
                    sel.Parameters.AddWithValue("@idv", int.Parse(IDV.Text.Trim()));
                    sel.Parameters.AddWithValue("@idp", int.Parse(IDP.Text.Trim()));
                    sel.Parameters.AddWithValue("@idl", int.Parse(IDL.Text.Trim()));
                    using (var r = sel.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            oldQuantite = Convert.ToInt32(r["Quantite"]);
                            oldPrix = Convert.ToDecimal(r["Prix_Vente"]);
                        }
                    }
                }

                using (SqlCommand dlt = new SqlCommand(
                    "DELETE FROM Ligne_Vente WHERE id_vente=@idv AND id_produit=@idp AND id_lot=@idl", db))
                {
                    dlt.Parameters.AddWithValue("@idv", int.Parse(IDV.Text.Trim()));
                    dlt.Parameters.AddWithValue("@idp", int.Parse(IDP.Text.Trim()));
                    dlt.Parameters.AddWithValue("@idl", int.Parse(IDL.Text.Trim()));
                    int rows = dlt.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        MessageBox.Show("Aucune ligne trouvée à supprimer !");
                        return;
                    }
                }

                using (SqlCommand cmd = new SqlCommand("prc_Audit_LigneVente", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "DELETE");
                    cmd.Parameters.AddWithValue("@id_vente", int.Parse(IDV.Text.Trim()));
                    cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text.Trim()));
                    cmd.Parameters.AddWithValue("@id_lot", int.Parse(IDL.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Prix_Vente", oldPrix);
                    cmd.Parameters.AddWithValue("@Quantite", oldQuantite);
                    cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                    cmd.ExecuteNonQuery();
                }

                SqlCommand refreshCmd = new SqlCommand("SELECT * FROM Ligne_Vente", db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_vente"],
                        fiche["id_produit"],
                        fiche["id_lot"],
                        fiche["Prix_Vente"],
                        fiche["Quantite"]);
                }
                fiche.Close();

                MessageBox.Show("Ligne de vente bien supprimée");
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string srch = "SELECT * FROM Ligne_Vente WHERE id_vente = @idv AND id_produit = @idp AND id_lot = @idl";
                SqlCommand cmd = new SqlCommand(srch, db);
                cmd.Parameters.Add("@idv", SqlDbType.Int).Value = int.Parse(IDV.Text);
                cmd.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text);
                cmd.Parameters.Add("@idl", SqlDbType.Int).Value = int.Parse(IDL.Text);
                SqlDataReader fiche = cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                fiche["id_vente"],
                fiche["id_produit"],
                fiche["id_lot"],
                fiche["prix_Vente"],
                fiche["Quantite"]);
                }
                fiche.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
