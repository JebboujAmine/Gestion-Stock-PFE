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
    public partial class Role : Form
    {
        public Role()
        {
            InitializeComponent();
        }
        public string cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            IDR.Clear();
            LB.Clear();
            IDR.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();

                string actualiser = "SELECT id_role,libelle FROM Role";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_role"],
                    fiche["libelle"]);
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {
                db.Open();
                if (string.IsNullOrWhiteSpace(IDR.Text) ||
                    string.IsNullOrWhiteSpace(LB.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = "insert into Role(id_role,libelle) values ( @IDR, @LB) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@IDR", SqlDbType.Int).Value = int.Parse(IDR.Text.Trim());
                insrt.Parameters.Add("@LB", SqlDbType.VarChar, 100).Value = LB.Text.Trim();

                insrt.ExecuteNonQuery();
                MessageBox.Show("Role Bien Ajouter");

                string actualiser = "SELECT id_role,libelle FROM Role";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_role"],
                         fiche["libelle"]);
                }
                fiche.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();

                string actualiser = "select * from Role where id_role = @ID ";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                Cmd.Parameters.AddWithValue("@ID", IDR.Text.Trim());
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        Fiche["id_role"],
                        Fiche["libelle"]
                        );

                }

                Fiche.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {
                db.Open();
                if (string.IsNullOrWhiteSpace(IDR.Text) ||
                    string.IsNullOrWhiteSpace(LB.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " update Role set libelle = @lb where id_role = @idr ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@idr", SqlDbType.VarChar, 80).Value = IDR.Text.Trim();
                updt.Parameters.Add("@lb", SqlDbType.VarChar, 20).Value = LB.Text.Trim();

                updt.ExecuteNonQuery();
                MessageBox.Show("Role Bien Modifier");

                string actualiser = "SELECT id_role,libelle FROM Role";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_role"],
                         fiche["libelle"]);
                }
                fiche.Close();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();
                string deleteCmd = "DELETE FROM Role WHERE id_role = @id";
                SqlCommand Cmd = new SqlCommand(deleteCmd, db);
                Cmd.Parameters.Add("@id", SqlDbType.Int).Value = IDR.Text;

                int rows = Cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Role Bien Supprimé");

                    string actualiser = "SELECT id_role,libelle FROM Role";
                    SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                    SqlDataReader fiche = refreshCmd.ExecuteReader();

                    dataGridView1.Rows.Clear();
                    while (fiche.Read())
                    {
                        dataGridView1.Rows.Add(
                            fiche["id_role"],
                             fiche["libelle"]);
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
