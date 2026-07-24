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
    public partial class Emplacement : Form
    {
        public Emplacement()
        {
            InitializeComponent();
        }
        public string cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            IDE.Clear();
            CE.Clear();
            IE.Clear();
            IDE.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();

                string actualiser = "SELECT id_emplacement,code_emplacement,id_entrpot FROM Emplacement";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_emplacement"],
                    fiche["code_emplacement"],
                    fiche["id_entrpot"]);
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {
                db.Open();
                if (string.IsNullOrWhiteSpace(IDE.Text) ||
                    string.IsNullOrWhiteSpace(CE.Text) ||
                    string.IsNullOrWhiteSpace(IE.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = "insert into Emplacement(id_emplacement,code_emplacement,id_entrpot) values ( @ide, @ce,@ie) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@ide", SqlDbType.Int).Value = int.Parse(IDE.Text.Trim());
                insrt.Parameters.Add("@ce", SqlDbType.VarChar, 100).Value = CE.Text.Trim();
                insrt.Parameters.Add("@ie", SqlDbType.Int).Value = int.Parse(IE.Text.Trim());

                insrt.ExecuteNonQuery();
                MessageBox.Show("Emplacement Bien Ajouter");

                string actualiser = "SELECT id_emplacement,code_emplacement,id_entrpot FROM Emplacement";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_emplacement"],
                        fiche["code_emplacement"],
                         fiche["id_entrpot"]);
                }
                fiche.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();

                string actualiser = "select * from Emplacement where id_emplacement = @ide ";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                Cmd.Parameters.AddWithValue("@ide", IDE.Text.Trim());
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        Fiche["id_emplacement"],
                        Fiche["code_emplacement"],
                        Fiche["id_entrpot"]
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
                if (string.IsNullOrWhiteSpace(IDE.Text) ||
                    string.IsNullOrWhiteSpace(CE.Text) ||
                    string.IsNullOrWhiteSpace(IE.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " update Emplacement set code_emplacement = @ce where id_emplacement = @ide and id_entrpot=@ie ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@ide", SqlDbType.VarChar, 80).Value = IDE.Text.Trim();
                updt.Parameters.Add("@ie", SqlDbType.VarChar, 80).Value = IE.Text.Trim();
                updt.Parameters.Add("@ce", SqlDbType.VarChar, 20).Value = CE.Text.Trim();

                updt.ExecuteNonQuery();
                MessageBox.Show("Emplacement Bien Modifier");

                string actualiser = "SELECT id_emplacement,code_emplacement,id_entrpot FROM Emplacement";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_emplacement"],
                        fiche["code_emplacement"],
                         fiche["id_entrpot"]);
                }
                fiche.Close();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {
                db.Open();
                string deleteCmd = "DELETE FROM Emplacement WHERE id_emplacement=@ide AND id_entrpot=@ie";
                SqlCommand Cmd = new SqlCommand(deleteCmd, db);

                int idEmplacement, idEntrepot;
                if (!int.TryParse(IDE.Text.Trim(), out idEmplacement) ||
                    !int.TryParse(IE.Text.Trim(), out idEntrepot))
                {
                    MessageBox.Show("Valeurs invalides !");
                    return;
                }

                Cmd.Parameters.Add("@ide", SqlDbType.Int).Value = idEmplacement;
                Cmd.Parameters.Add("@ie", SqlDbType.Int).Value = idEntrepot;

                int rows = Cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Emplacement bien supprimé");

                    string actualiser = "SELECT id_emplacement, code_emplacement, id_entrpot FROM Emplacement";
                    SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                    SqlDataReader fiche = refreshCmd.ExecuteReader();

                    dataGridView1.Rows.Clear();
                    while (fiche.Read())
                    {
                        dataGridView1.Rows.Add(
                            fiche["id_emplacement"],
                            fiche["code_emplacement"],
                            fiche["id_entrpot"]);
                    }
                    fiche.Close();
                }
                else
                {
                    MessageBox.Show("Aucun emplacement trouvé à supprimer !");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
