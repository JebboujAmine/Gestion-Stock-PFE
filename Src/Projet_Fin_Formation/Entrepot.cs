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
    public partial class Entrepot : Form
    {
        public Entrepot()
        {
            InitializeComponent();
        }
        public string cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            ide.Clear();
            nm.Clear();
            adr.Clear();
            ide.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();

                string actualiser = "SELECT id_entrpot,nom_magasin,adresse FROM Entrepot";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_entrpot"],
                    fiche["nom_magasin"],
                    fiche["adresse"]
                    );
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {
                db.Open();
                if (string.IsNullOrWhiteSpace(ide.Text) ||
                    string.IsNullOrWhiteSpace(nm.Text) ||
                        string.IsNullOrWhiteSpace(adr.Text))

                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = "insert into Entrepot(id_entrpot,nom_magasin,adresse) values ( @IDE, @NM,@A) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@IDE", SqlDbType.Int).Value = int.Parse(ide.Text.Trim());
                insrt.Parameters.Add("@NM", SqlDbType.VarChar, 100).Value = nm.Text.Trim();
                insrt.Parameters.Add("@A", SqlDbType.VarChar, 100).Value = adr.Text.Trim();

                insrt.ExecuteNonQuery();
                MessageBox.Show("Entrepot Bien Ajouter");

                string actualiser = "SELECT id_entrpot,nom_magasin,adresse FROM Entrepot";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_entrpot"],
                        fiche["nom_magasin"],
                         fiche["adresse"]);
                }
                fiche.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();

                string actualiser = "select * from entrepot where id_entrpot = @ide ";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                Cmd.Parameters.AddWithValue("@ide", ide.Text.Trim());
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        Fiche["id_entrpot"],
                        Fiche["nom_magasin"],
                        Fiche["adresse"]
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
                if (string.IsNullOrWhiteSpace(ide.Text) ||
                    string.IsNullOrWhiteSpace(nm.Text) ||
                    string.IsNullOrWhiteSpace(adr.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " update Entrepot set nom_magasin = @NM , adresse = @ADR where id_entrpot = @IDE ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@IDE", SqlDbType.Int).Value = int.Parse(ide.Text.Trim());
                updt.Parameters.Add("@NM", SqlDbType.VarChar, 100).Value = nm.Text.Trim();
                updt.Parameters.Add("@ADR", SqlDbType.VarChar, 100).Value = adr.Text.Trim();

                updt.ExecuteNonQuery();
                MessageBox.Show("Entrepot Bien Modifier");

                string actualiser = "SELECT id_entrpot,nom_magasin,adresse FROM Entrepot";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        fiche["id_entrpot"],
                        fiche["nom_magasin"],
                         fiche["adresse"]);
                }
                fiche.Close();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();
                string deleteCmd = "DELETE FROM Entrepot WHERE id_entrpot = @id";
                SqlCommand Cmd = new SqlCommand(deleteCmd, db);
                Cmd.Parameters.Add("@id", SqlDbType.Int).Value = ide.Text;

                int rows = Cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Catégorie Bien Supprimé");

                    string actualiser = "SELECT id_entrpot,nom_magasin,adresse FROM Entrepot";
                    SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                    SqlDataReader fiche = refreshCmd.ExecuteReader();

                    dataGridView1.Rows.Clear();
                    while (fiche.Read())
                    {
                        dataGridView1.Rows.Add(
                            fiche["id_entrpot"],
                             fiche["nom_magasin"],
                             fiche["adresse"]);
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