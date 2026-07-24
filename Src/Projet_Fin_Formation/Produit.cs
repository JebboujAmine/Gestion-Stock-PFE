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
    public partial class Produit : Form
    {
        public Produit()
        {
            InitializeComponent();
        }
        public string Cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";
        public int idClient;

        private void button1_Click(object sender, EventArgs e)
        {
            idp.Clear();
            idc.Clear();
            d.Clear();
            p.Clear();
            desc.Clear();
            q.Clear();
            idp.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Produit";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_produit"],
                    fiche["designation"],
                    fiche["prix_unitaire"],
                    fiche["seuil_alerte"],
                    fiche["description_p"],
                    fiche["id_categorie"],
                    fiche["quantité"]);
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                string Cmd = "insert into Produit(id_produit, designation, prix_unitaire, description_p, id_categorie, quantité) values ( @IDP, @DESG ,@PU , @DESC,@IDC,@Q) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@IDP", SqlDbType.Int).Value = int.Parse(idp.Text.Trim());
                insrt.Parameters.Add("@DESG", SqlDbType.VarChar, 80).Value = d.Text.Trim();
                insrt.Parameters.Add("@PU", SqlDbType.Decimal).Value =decimal.Parse( p.Text.Trim());
                insrt.Parameters.Add("@DESC", SqlDbType.VarChar, 200).Value = desc.Text.Trim();
                insrt.Parameters.Add("@IDC", SqlDbType.VarChar, 200).Value = int.Parse(idc.Text.Trim());
                insrt.Parameters.Add("@Q", SqlDbType.Int).Value = int.Parse(q.Text.Trim());

                insrt.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Produit";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_produit"],
                    fiche["designation"],
                    fiche["prix_unitaire"],
                    fiche["seuil_alerte"],
                    fiche["description_p"],
                    fiche["id_categorie"],
                    fiche["quantité"]);
                }
                fiche.Close();

                MessageBox.Show("produit Bien Ajouter");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "select * from PRODUIT where id_produit = @IDP and id_categorie = @IDC  ";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                Cmd.Parameters.AddWithValue("@IDP", idp.Text.Trim());
                Cmd.Parameters.AddWithValue("@IDC", idc.Text.Trim());
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                     Fiche["id_produit"],
                    Fiche["designation"],
                    Fiche["prix_unitaire"],
                    Fiche["seuil_alerte"],
                    Fiche["description_p"],
                    Fiche["id_categorie"],
                    Fiche["quantité"]);
                }

                Fiche.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();
                if (string.IsNullOrEmpty(idp.Text) ||
                    string.IsNullOrEmpty(idc.Text) ||
                    string.IsNullOrEmpty(d.Text) ||
                    string.IsNullOrEmpty(p.Text) ||
                    string.IsNullOrEmpty(desc.Text) ||
                    string.IsNullOrEmpty(q.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }

                if (!int.TryParse(idc.Text.Trim(), out idClient))
                {
                    MessageBox.Show("ID client invalide !");
                    return;
                }
                string Cmd = " update Produit set designation = @D ,prix_unitaire = @PU , description_p = @DESC ,quantité = @Q  where id_produit = @IDP AND id_categorie = @IDC ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@IDP", SqlDbType.Int).Value = int.Parse(idp.Text.Trim());
                updt.Parameters.Add("@IDC", SqlDbType.VarChar, 200).Value = int.Parse(idc.Text.Trim());
                updt.Parameters.Add("@D", SqlDbType.VarChar, 80).Value = d.Text.Trim();
                updt.Parameters.Add("@PU", SqlDbType.Decimal).Value = decimal.Parse(p.Text.Trim());
                updt.Parameters.Add("@DESC", SqlDbType.VarChar, 200).Value = desc.Text.Trim();
                updt.Parameters.Add("@Q", SqlDbType.Int).Value = int.Parse(q.Text.Trim());

                updt.ExecuteNonQuery();

                string actualiser = "SELECT * FROM Produit";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_produit"],
                    fiche["designation"],
                    fiche["prix_unitaire"],
                    fiche["seuil_alerte"],
                    fiche["description_p"],
                    fiche["id_categorie"],
                    fiche["quantité"]);
                }
                fiche.Close();
                MessageBox.Show("produit Bien Modifier");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
                db.Open();

                int idproduit;
                int idcategorie;
                if (!int.TryParse(idc.Text.Trim(), out idproduit) || !int.TryParse(idc.Text.Trim(), out idcategorie))
                {
                    MessageBox.Show("ID produit ou de catégorie invalide !");
                    return;
                }

                string deleteCmd = "DELETE FROM Produit WHERE id_produit = @IDP AND id_categorie = @IDC";

                SqlCommand Cmd = new SqlCommand(deleteCmd, db);
                Cmd.Parameters.Add("@IDP", SqlDbType.Int).Value = idproduit;
                Cmd.Parameters.Add("@IDC", SqlDbType.Int).Value = idcategorie;

                int rows = Cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("produit Bien Supprimé");
                    string actualiser = "SELECT * FROM produit";
                    SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                    SqlDataReader fiche = refreshCmd.ExecuteReader();

                    dataGridView1.Rows.Clear();
                    while (fiche.Read())
                    {
                        dataGridView1.Rows.Add(
                            fiche["id_produit"],
                        fiche["designation"],
                        fiche["prix_unitaire"],
                        fiche["seuil_alerte"],
                        fiche["description_p"],
                        fiche["id_categorie"],
                        fiche["quantité"]);
                    }

                    fiche.Close();
                }
                else
                {
                    MessageBox.Show("Aucun produit trouvé avec cet ID");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
