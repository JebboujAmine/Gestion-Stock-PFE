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
    public partial class Ligne_Achat : Form
    {
        public Ligne_Achat()
        {
            InitializeComponent();
        }
        public string Cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {
            IDC.Clear();
            IDP.Clear();
            IDL.Clear();
            PA.Clear();
            Q.Clear();
            IDC.Select();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Ligne_Achat";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_produit"],
                    fiche["id_commande"],
                    fiche["id_lot"],
                    fiche["Quantite"],
                    fiche["Prix_achat"]);
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
              
                string Cmd = " INSERT into Ligne_Achat (id_produit, id_commande,id_lot,Quantite,Prix_achat) " + "values ( @idp, @idc,@idl,@q,@pa) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                insrt.Parameters.Add("@idc", SqlDbType.Int).Value = int.Parse(IDC.Text.Trim());
                insrt.Parameters.Add("@idl", SqlDbType.Int).Value = int.Parse(IDL.Text.Trim());
                insrt.Parameters.Add("@pa", SqlDbType.Int).Value = int.Parse(Q.Text.Trim());
                insrt.Parameters.Add("@q", SqlDbType.Int).Value = decimal.Parse(PA.Text.Trim());
                insrt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_LigneAchat", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_commande", int.Parse(IDC.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_lot", int.Parse(IDL.Text.Trim()));
                cmd.Parameters.AddWithValue("@Quantite", int.Parse(Q.Text.Trim()));
                cmd.Parameters.AddWithValue("@Prix_achat", decimal.Parse(PA.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmd.ExecuteNonQuery();


                string actualiser = "SELECT * FROM Ligne_Achat";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                     fiche["id_produit"],
                     fiche["id_commande"],
                     fiche["id_lot"],
                     fiche["Quantite"],
                     fiche["Prix_achat"]);
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

                string srch = "SELECT * FROM Ligne_Achat WHERE id_commande = @idc AND id_produit = @idp AND id_lot = @idl";
                SqlCommand cmd = new SqlCommand(srch, db);
                cmd.Parameters.Add("@idc", SqlDbType.Int).Value = int.Parse(IDC.Text);
                cmd.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text);
                cmd.Parameters.Add("@idl", SqlDbType.Int).Value = int.Parse(IDL.Text);
                SqlDataReader fiche = cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                         fiche["id_produit"],
                         fiche["id_commande"],
                         fiche["id_lot"],
                         fiche["Quantite"],
                         fiche["Prix_achat"]);
                }
                fiche.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDC.Text) || string.IsNullOrEmpty(IDL.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " UPDATE Ligne_Achat SET Prix_achat = @pa, Quantite = @q WHERE id_commande = @idc AND id_produit = @idp AND id_lot = @idl ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@idc", SqlDbType.Int).Value = int.Parse(IDC.Text.Trim());
                updt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                updt.Parameters.Add("@idl", SqlDbType.Int).Value = int.Parse(IDL.Text.Trim());
                updt.Parameters.Add("@pa", SqlDbType.Money).Value = decimal.Parse(PA.Text.Trim());
                updt.Parameters.Add("@q", SqlDbType.Int).Value = int.Parse(Q.Text.Trim());

                updt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_LigneAchat", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@id_commande", int.Parse(IDC.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_lot", int.Parse(IDL.Text.Trim()));
                cmd.Parameters.AddWithValue("@Prix_achat", decimal.Parse(PA.Text.Trim()));
                cmd.Parameters.AddWithValue("@Quantite", int.Parse(Q.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmd.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Ligne_Achat";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                       fiche["id_produit"],
                         fiche["id_commande"],
                         fiche["id_lot"],
                         fiche["Quantite"],
                         fiche["Prix_achat"]);
                }
                fiche.Close();
                MessageBox.Show("Ligne d'achat bien modifié");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();

                if (string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDC.Text) || string.IsNullOrEmpty(IDL.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = "DELETE FROM Ligne_Achat WHERE id_commande = @idc AND id_produit = @idp AND id_lot = @idl ";
                SqlCommand dlt = new SqlCommand(Cmd, db);
                decimal montant;
                int quantite;
                if (!decimal.TryParse(PA.Text.Trim(), out montant) || !int.TryParse(Q.Text.Trim(), out quantite))
                {
                    montant = 0 ;
                    quantite = 0 ;
                }
                
                dlt.Parameters.Add("@idc", SqlDbType.Int).Value = int.Parse(IDC.Text.Trim());
                dlt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                dlt.Parameters.Add("@idl", SqlDbType.Int).Value = int.Parse(IDL.Text.Trim());

                dlt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_LigneAchat", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@id_commande", int.Parse(IDC.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_lot", int.Parse(IDL.Text.Trim()));
                cmd.Parameters.AddWithValue("@Prix_achat",montant);
                cmd.Parameters.AddWithValue("@Quantite", quantite);
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmd.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Ligne_Achat";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                       fiche["id_produit"],
                         fiche["id_commande"],
                         fiche["id_lot"],
                         fiche["Quantite"],
                         fiche["Prix_achat"]);
                }
                fiche.Close();

                MessageBox.Show("Ligne d'achat Bien Surimer");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}