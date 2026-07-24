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
    public partial class Ligne_Inventaire : Form
    {
        public Ligne_Inventaire()
        {
            InitializeComponent();
        }
        public string Cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";

        private void IDL_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void IDP_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();

                if (string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDI.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }

                string selectCmd = "SELECT stock_theorique, stock_reel FROM Ligne_Inventaire WHERE id_inventaire=@idi AND id_produit=@idp";
                SqlCommand sel = new SqlCommand(selectCmd, db);
                sel.Parameters.Add("@idi", SqlDbType.Int).Value = int.Parse(IDI.Text.Trim());
                sel.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());

                int stockTheo = 0, stockReel = 0;
                SqlDataReader r = sel.ExecuteReader();
                if (r.Read())
                {
                    stockTheo = (int)r["stock_theorique"];
                    stockReel = (int)r["stock_reel"];
                }
                r.Close();
                                
                string Cmd = "DELETE FROM Ligne_Inventaire WHERE id_inventaire=@idi AND id_produit=@idp";
                SqlCommand dlt = new SqlCommand(Cmd, db);
                dlt.Parameters.Add("@idi", SqlDbType.Int).Value = int.Parse(IDI.Text.Trim());
                dlt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                dlt.ExecuteNonQuery();

                SqlCommand cmd = new SqlCommand("prc_Audit_LigneInventaire", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_inventaire", int.Parse(IDI.Text.Trim()));
                cmd.Parameters.AddWithValue("@stock_theorique", stockTheo);
                cmd.Parameters.AddWithValue("@stock_reel", stockReel);
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmd.ExecuteNonQuery();

                
                string actualiser = "SELECT * FROM Ligne_Inventaire";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_produit"],
                        fiche["id_inventaire"],
                        fiche["stock_theorique"],
                        fiche["stock_reel"]);
                }
                fiche.Close();

                // 6. Message
                MessageBox.Show("Ligne d'inventaire bien supprimée");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrEmpty(IDI.Text) || string.IsNullOrEmpty(IDP.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " UPDATE Ligne_Inventaire SET stock_theorique = @st, stock_reel = @sr WHERE id_inventaire = @idi AND id_produit = @idp";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@idi", SqlDbType.Int).Value = int.Parse(IDI.Text.Trim());
                updt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                updt.Parameters.Add("@st", SqlDbType.Int).Value = int.Parse(ST.Text.Trim());
                updt.Parameters.Add("@sr", SqlDbType.Money).Value = int.Parse(SR.Text.Trim());
                updt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_LigneInventaire", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text));
                cmd.Parameters.AddWithValue("@id_inventaire", int.Parse(IDI.Text));
                cmd.Parameters.AddWithValue("@stock_theorique", int.Parse(ST.Text));
                cmd.Parameters.AddWithValue("@stock_reel", int.Parse(SR.Text));
                cmd.Parameters.AddWithValue("@Ancien_stock_theorique", int.Parse(ST.Text.Trim()));
                cmd.Parameters.AddWithValue("@Ancien_stock_reel", int.Parse(SR.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmd.ExecuteNonQuery();


                string actualiser = "SELECT * FROM Ligne_Inventaire";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_produit"],
                     fiche["id_inventaire"],
                    fiche["stock_theorique"],
                    fiche["stock_reel"]);
                }
                fiche.Close();

                MessageBox.Show("Ligne d'inventaire Bien Modifier");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string srch = "SELECT * FROM Ligne_Inventaire WHERE id_inventaire = @idi AND id_produit = @idp";
                SqlCommand cmd = new SqlCommand(srch, db);
                cmd.Parameters.Add("@idi", SqlDbType.Int).Value = int.Parse(IDI.Text);
                cmd.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text);
                SqlDataReader fiche = cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_inventaire"],
                    fiche["id_produit"],
                    fiche["stock_theorique"],
                    fiche["stock_reel"]);
                }
                fiche.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Ligne_Inventaire";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_inventaire"],
                    fiche["id_produit"],
                    fiche["stock_theorique"],
                    fiche["stock_reel"]);
                }
                fiche.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IDI.Clear();
            IDP.Clear();
            ST.Clear();
            SR.Clear();
            IDI.Select();

        }

        private void PV_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void IDV_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                string Cmd = "insert into Ligne_Inventaire (id_inventaire, id_produit,stock_theorique,stock_reel) values (@idi ,@idp,@st,@sr) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@idi", SqlDbType.Int).Value = int.Parse(IDI.Text.Trim());
                insrt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                insrt.Parameters.Add("@st", SqlDbType.Int).Value = int.Parse(ST.Text.Trim());
                insrt.Parameters.Add("@sr", SqlDbType.Money).Value = decimal.Parse(SR.Text.Trim());
                insrt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_LigneInventaire", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text));
                cmd.Parameters.AddWithValue("@id_inventaire", int.Parse(IDI.Text));
                cmd.Parameters.AddWithValue("@stock_theorique", int.Parse(ST.Text));
                cmd.Parameters.AddWithValue("@stock_reel", int.Parse(SR.Text));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmd.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Ligne_Inventaire";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_inventaire"],
                    fiche["id_produit"],
                    fiche["stock_theorique"],
                    fiche["stock_reel"]);
                }
                fiche.Close();
                MessageBox.Show("Ligne d'inventaire Bien Ajouter");
            }
        }
    }
}
