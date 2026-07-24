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
    public partial class Paiement : Form
    {
        public Paiement()
        {
            InitializeComponent();
        }
        public string Cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";

        private void Paiement_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Espèces");   
            comboBox1.Items.Add("Carte");     
            comboBox1.Items.Add("Virement");
            comboBox1.Items.Add("Chèque");
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IDP.Clear();
            IDV.Clear();
            dateTimePicker1.Value= DateTime.UtcNow;
            comboBox1.SelectedIndex = 0 ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Paiement";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

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

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                string Cmd = "insert into Paiement (id_paiement,date_paiement,mode_paiement,id_vente) values ( @idp, @dp ,@mp,@idv) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                insrt.Parameters.Add("@dp", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                insrt.Parameters.Add("@mp", SqlDbType.VarChar,50).Value = comboBox1.SelectedItem.ToString();
                insrt.Parameters.Add("@idv", SqlDbType.Int).Value = int.Parse(IDV.Text.Trim());
                
                insrt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_Paiement", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@id_paiement", int.Parse(IDP.Text.Trim()));
                cmd.Parameters.AddWithValue("@date_paiement", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@mode_paiement", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@id_vente", int.Parse(IDV.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);

                cmd.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Paiement";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

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
                MessageBox.Show("Paiement Bien Ajouter");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string srch = "SELECT * FROM Paiement WHERE id_paiement = @idp AND id_vente = @idv  ";
                SqlCommand cmd = new SqlCommand(srch, db);
                cmd.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text);
                cmd.Parameters.Add("@idv", SqlDbType.Int).Value = int.Parse(IDV.Text);
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
                if (string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDV.Text) )
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " UPDATE Paiement SET date_paiement = @dp, mode_paiement = @mp WHERE id_paiement = @idp AND id_vente = @idv ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                insrt.Parameters.Add("@idv", SqlDbType.Int).Value = int.Parse(IDV.Text.Trim());
                insrt.Parameters.Add("@dp", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                insrt.Parameters.Add("@mp", SqlDbType.VarChar, 50).Value = comboBox1.SelectedItem.ToString();

                insrt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_Paiement", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@id_paiement", int.Parse(IDP.Text.Trim()));
                cmd.Parameters.AddWithValue("@date_paiement", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@mode_paiement", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@id_vente", int.Parse(IDV.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);

                cmd.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Paiement";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

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
                MessageBox.Show("Paiement Bien Modifier");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDV.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = "DELETE FROM Paiement WHERE id_paiement = @idp AND id_vente = @idv ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                insrt.Parameters.Add("@idv", SqlDbType.Int).Value = int.Parse(IDV.Text.Trim());
                insrt.ExecuteNonQuery();


                SqlCommand cmd = new SqlCommand("prc_Audit_Paiement", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@id_paiement", int.Parse(IDP.Text.Trim()));
                cmd.Parameters.AddWithValue("@date_paiement", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@mode_paiement", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@id_vente", int.Parse(IDV.Text.Trim()));
                cmd.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);

                cmd.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Paiement";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

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
                MessageBox.Show("Paiement Bien Modifier");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
