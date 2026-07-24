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
    public partial class Inventaire : Form
    {
        public Inventaire()
        {
            InitializeComponent();
        }
        public string Cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";
        private void Inventaire_Load(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Inventaire";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_inventaire"],
                    fiche["date_inventaire"],
                    fiche["remarque"],
                    fiche["id_entrpot"],
                    fiche["id_produit"],
                    fiche["id_utilisateur"]);
                }
                fiche.Close();
        }   }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IDI.Clear();
            IDE.Clear();
            IDP.Clear();
            dateTimePicker1.Value = DateTime.UtcNow;
            IDI.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Inventaire";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_inventaire"],
                    fiche["date_inventaire"],
                    fiche["remarque"],
                    fiche["id_entrpot"],
                    fiche["id_produit"],
                    fiche["id_utilisateur"]);
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrEmpty(IDI.Text) || string.IsNullOrEmpty(IDE.Text) || string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDU.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = "insert into Inventaire (id_inventaire,date_inventaire,remarque,id_entrpot,id_produit,id_utilisateur) values ( @idi, @di ,@r,@ide,@idp,@idu) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@idi", SqlDbType.Int).Value = int.Parse(IDI.Text.Trim());
                insrt.Parameters.Add("@di", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                insrt.Parameters.Add("@r", SqlDbType.VarChar , 50).Value = R.Text.Trim();
                insrt.Parameters.Add("@ide", SqlDbType.Int).Value = int.Parse(IDE.Text.Trim());
                insrt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                insrt.Parameters.Add("@idu", SqlDbType.Int).Value = int.Parse(IDU.Text.Trim());

                insrt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_Inventaire", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@id_inventaire", int.Parse(IDI.Text));
                cmd.Parameters.AddWithValue("@date_inventaire", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@remarque", R.Text.Trim());
                cmd.Parameters.AddWithValue("@id_entrepot", int.Parse(IDE.Text));
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmd.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Inventaire";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_inventaire"],
                    fiche["date_inventaire"],
                    fiche["remarque"],
                    fiche["id_entrpot"],
                    fiche["id_produit"],
                    fiche["id_utilisateur"]);
                }
                fiche.Close();
                MessageBox.Show("Inventaire Bien Ajouter");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();


                string srch = "SELECT * FROM Inventaire WHERE id_inventaire=@idi and id_produit = @idp and id_entrpot=@ide and id_produit=@idp and id_utilisateur =@idu ";
                SqlCommand cmd = new SqlCommand(srch, db);
                cmd.Parameters.Add("@idi", SqlDbType.Int).Value = int.Parse(IDI.Text);
                cmd.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text);
                cmd.Parameters.Add("@ide", SqlDbType.Int).Value = int.Parse(IDE.Text);
                cmd.Parameters.Add("@idu", SqlDbType.Int).Value = int.Parse(IDU.Text);
                SqlDataReader fiche = cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_paiement"],
                    fiche["date_paiement"],
                    fiche["mode_paiement"],
                    fiche["id_vente"]);
                }
                fiche.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrEmpty(IDI.Text) || string.IsNullOrEmpty(IDE.Text) || string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDU.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = "UPDATE Inventaire SET date_inventaire=@di, remarque=@r WHERE id_inventaire=@idi and id_entrpot=@ide and id_produit=@idp";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@idi", SqlDbType.Int).Value = int.Parse(IDI.Text);
                updt.Parameters.Add("@di", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                updt.Parameters.Add("@r", SqlDbType.VarChar, 50).Value = R.Text.Trim();
                updt.Parameters.Add("@ide", SqlDbType.Int).Value = int.Parse(IDE.Text);
                updt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text);
                updt.Parameters.Add("@idu", SqlDbType.Int).Value = int.Parse(IDU.Text);
                updt.ExecuteNonQuery();

                updt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_Inventaire", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@id_inventaire", int.Parse(IDI.Text));
                cmd.Parameters.AddWithValue("@date_inventaire", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@remarque", R.Text.Trim());
                cmd.Parameters.AddWithValue("@id_entrepot", int.Parse(IDE.Text));
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmd.ExecuteNonQuery();
                
                string actualiser = "SELECT * FROM Inventaire";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_inventaire"],
                    fiche["date_inventaire"],
                    fiche["remarque"],
                    fiche["id_entrpot"],
                    fiche["id_produit"],
                    fiche["id_utilisateur"]);
                }
                fiche.Close();
                MessageBox.Show("Inventaire Bien Ajouter");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrEmpty(IDI.Text) || string.IsNullOrEmpty(IDE.Text) || string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDU.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = "DELETE FROM Inventaire WHERE id_inventaire = @idi and id_entrpot = @ide and id_produit = @idp and id_utilisateur = @idu";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@idi", SqlDbType.Int).Value = int.Parse(IDI.Text);
                updt.Parameters.Add("@ide", SqlDbType.Int).Value = int.Parse(IDE.Text);
                updt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text);
                updt.Parameters.Add("@idu", SqlDbType.Int).Value = int.Parse(IDU.Text);
                updt.ExecuteNonQuery();

                updt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_Inventaire", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@id_inventaire", int.Parse(IDI.Text));
                cmd.Parameters.AddWithValue("@date_inventaire", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@remarque", R.Text.Trim());
                cmd.Parameters.AddWithValue("@id_entrepot", int.Parse(IDE.Text));
                cmd.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmd.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Inventaire";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_inventaire"],
                    fiche["date_inventaire"],
                    fiche["remarque"],
                    fiche["id_entrpot"],
                    fiche["id_produit"],
                    fiche["id_utilisateur"]);
                }
                fiche.Close();
                MessageBox.Show("Inventaire Bien Supprimer");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }   
}
