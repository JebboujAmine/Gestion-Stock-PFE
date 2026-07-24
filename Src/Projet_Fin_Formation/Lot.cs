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
    public partial class Lot : Form
    {
        public Lot()
        {
            InitializeComponent();
        }
        public string Cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";
        public int idutilisateur;
        public int idlot;

        private void Lot_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm";

            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Lot";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_lot"],
                    fiche["date_fabrication"],
                    fiche["date_peremption"],
                    fiche["id_produit"]);
                }
                fiche.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IDL.Clear();
            IDP.Clear();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm";
            IDP.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Lot";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_lot"],
                    fiche["date_fabrication"],
                    fiche["date_peremption"],
                    fiche["id_produit"]);
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                string Cmd = "insert into Lot (id_lot, date_fabrication , date_peremption, id_produit) values ( @idl, @df ,@dp,@idp) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@idl", SqlDbType.Int).Value = int.Parse(IDL.Text.Trim());
                insrt.Parameters.Add("@df", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                insrt.Parameters.Add("@dp", SqlDbType.DateTime).Value = dateTimePicker2.Value;
                insrt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());
                insrt.ExecuteNonQuery();

                SqlCommand cmdAudit = new SqlCommand("prc_Audit_Lot", db);
                cmdAudit.CommandType = CommandType.StoredProcedure;
                cmdAudit.Parameters.AddWithValue("@Action", "INSERT");
                cmdAudit.Parameters.AddWithValue("@id_lot",int.Parse(IDL.Text));
                cmdAudit.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text));
                cmdAudit.Parameters.AddWithValue("@date_fabrication", dateTimePicker1.Value);
                cmdAudit.Parameters.AddWithValue("@date_peremption", dateTimePicker2.Value);
                cmdAudit.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmdAudit.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Lot";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_lot"],
                    fiche["date_fabrication"],
                    fiche["date_peremption"],
                    fiche["id_produit"]);
                }
                fiche.Close();

                MessageBox.Show("Lot Bien Ajouter");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDL.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }

                if (!int.TryParse(IDL.Text.Trim(), out idlot))
                {
                    MessageBox.Show("ID Lot invalide !");
                    return;
                }
                string Cmd = " update Lot set date_fabrication = @DF , date_peremption = @DP where id_lot = @idl and id_produit = @idp ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@idl", SqlDbType.Int).Value = int.Parse(IDL.Text.Trim());
                updt.Parameters.Add("@DF", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                updt.Parameters.Add("@DP", SqlDbType.DateTime).Value = dateTimePicker2.Value;
                updt.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text.Trim());

                updt.ExecuteNonQuery();

                SqlCommand cmdAudit = new SqlCommand("prc_Audit_Lot", db);
                cmdAudit.CommandType = CommandType.StoredProcedure;
                cmdAudit.Parameters.AddWithValue("@Action", "UPDATE");
                cmdAudit.Parameters.AddWithValue("@id_lot", int.Parse(IDL.Text));
                cmdAudit.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text));
                cmdAudit.Parameters.AddWithValue("@date_fabrication", dateTimePicker1.Value);
                cmdAudit.Parameters.AddWithValue("@date_peremption", dateTimePicker2.Value);
                cmdAudit.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmdAudit.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Lot";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_lot"],
                    fiche["date_fabrication"],
                    fiche["date_peremption"],
                    fiche["id_produit"]);
                }
                fiche.Close();

                MessageBox.Show("Lot Bien Modifier");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrEmpty(IDP.Text) || string.IsNullOrEmpty(IDL.Text))
                {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                return;
                }

            if (!int.TryParse(IDL.Text.Trim(), out idlot))
             {
                MessageBox.Show("ID Lot invalide !");
                return;
             }
                string deleteCmd = "DELETE FROM Lot WHERE id_lot = @idl and id_produit = @idp  ";

                SqlCommand Cmd = new SqlCommand(deleteCmd, db);
                Cmd.Parameters.Add("@idl", SqlDbType.Int).Value = idlot;
                Cmd.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text);

                int rows = Cmd.ExecuteNonQuery();
            if (rows > 0)
            {

                SqlCommand cmdAudit = new SqlCommand("prc_Audit_Lot", db);
                cmdAudit.CommandType = CommandType.StoredProcedure;
                cmdAudit.Parameters.AddWithValue("@Action", "DELETE");
                cmdAudit.Parameters.AddWithValue("@id_lot", int.Parse(IDL.Text));
                cmdAudit.Parameters.AddWithValue("@id_produit", int.Parse(IDP.Text));
                cmdAudit.Parameters.AddWithValue("@date_fabrication", dateTimePicker1.Value);
                cmdAudit.Parameters.AddWithValue("@date_peremption", dateTimePicker2.Value);
                cmdAudit.Parameters.AddWithValue("@id_utilisateur", SecurityContext.IdUtilisateur);
                cmdAudit.ExecuteNonQuery();
                    MessageBox.Show("Lot Bien supprimme");

                string actualiser = "SELECT * FROM Lot";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_lot"],
                    fiche["date_fabrication"],
                    fiche["date_peremption"],
                    fiche["id_produit"]);
                }
                fiche.Close();
            }
            else
            {
                MessageBox.Show("Aucun Lot trouvé avec cet ID");
            }
        }
    }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string srch = "SELECT * FROM Lot where id_lot =@idl and id_produit = @idp";
                SqlCommand cmd = new SqlCommand(srch, db);
                cmd.Parameters.Add("@idl", SqlDbType.Int).Value = int.Parse(IDL.Text);
                cmd.Parameters.Add("@idp", SqlDbType.Int).Value = int.Parse(IDP.Text);
                SqlDataReader fiche = cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_lot"],
                    fiche["date_fabrication"],
                    fiche["date_peremption"],
                    fiche["id_produit"]);
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
