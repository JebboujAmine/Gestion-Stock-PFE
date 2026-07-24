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
    public partial class Vente : Form
    {
        public Vente()
        {
            InitializeComponent();
        }
        public string cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";
        private void Vente_Load(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Vente";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_vente"],
                    fiche["date_vente"],
                    fiche["id_utilisateur"],
                    fiche["id_bon_livraison"],
                    fiche["id_client"]);
                }
                fiche.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IDV.Clear();
            IDU.Clear();
            IDBL.Clear();
            IDC.Clear();
            IDV.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Vente";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_vente"],
                    fiche["date_vente"],
                    fiche["id_utilisateur"],
                    fiche["id_bon_livraison"],
                    fiche["id_client"]);
                }
                fiche.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {
                db.Open();
                if (string.IsNullOrWhiteSpace(IDV.Text) ||
                    string.IsNullOrWhiteSpace(IDU.Text) ||
                    string.IsNullOrWhiteSpace(IDBL.Text) ||
                    string.IsNullOrWhiteSpace(IDC.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = "INSERT INTO Vente (id_vente, id_utilisateur, id_client, id_bon_Livraison) VALUES (@idv, @idu, @idc, @idb)";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@idv", SqlDbType.Int).Value = int.Parse(IDV.Text.Trim());
                insrt.Parameters.Add("@idu", SqlDbType.Int).Value = int.Parse(IDU.Text.Trim());
                insrt.Parameters.Add("@idc", SqlDbType.Int).Value = int.Parse(IDC.Text.Trim());
                insrt.Parameters.Add("@idb", SqlDbType.Int).Value = int.Parse(IDBL.Text.Trim());
                insrt.ExecuteNonQuery();
                MessageBox.Show("Vente Bien Ajouter");

                string actualiser = "SELECT * FROM Vente";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_vente"],
                    fiche["date_vente"],
                    fiche["id_utilisateur"],
                    fiche["id_bon_livraison"],
                    fiche["id_client"]);
                }
                fiche.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {

                db.Open();

                string actualiser = "SELECT * FROM Vente WHERE id_vente = @idv  ";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                Cmd.Parameters.Add("@idv", SqlDbType.Int).Value = int.Parse(IDV.Text.Trim());
                
                
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                   while (Fiche.Read())
                    {
                        dataGridView1.Rows.Add(
                        Fiche["id_vente"],
                        Fiche["date_vente"],
                        Fiche["id_utilisateur"],
                        Fiche["id_bon_livraison"],
                        Fiche["id_client"]);
                    }

                Fiche.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {
                db.Open();
                if (string.IsNullOrWhiteSpace(IDV.Text) ||
                    string.IsNullOrWhiteSpace(IDU.Text) ||
                    string.IsNullOrWhiteSpace(IDBL.Text) ||
                    string.IsNullOrWhiteSpace(IDC.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }
                string Cmd = " update Vente set  id_utilisateur= @idu , id_client= @idc ,id_bon_livraison = @idb  where id_vente = @idu ";
                SqlCommand updt = new SqlCommand(Cmd, db);
                updt.Parameters.Add("@idv", SqlDbType.Int).Value = int.Parse(IDV.Text.Trim());
                updt.Parameters.Add("@idu", SqlDbType.Int).Value = int.Parse(IDU.Text.Trim());
                updt.Parameters.Add("@idc", SqlDbType.Int).Value = int.Parse(IDC.Text.Trim());
                updt.Parameters.Add("@idb", SqlDbType.Int).Value = int.Parse(IDBL.Text.Trim());
                updt.ExecuteNonQuery();
                MessageBox.Show("Vente Bien Modifier");

                string actualiser = "SELECT * FROM Vente";
                SqlCommand refreshCmd = new SqlCommand(actualiser, db);
                SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                {
                    dataGridView1.Rows.Add(
                    fiche["id_vente"],
                    fiche["date_vente"],
                    fiche["id_utilisateur"],
                    fiche["id_bon_livraison"],
                    fiche["id_client"]);
                }
                fiche.Close();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(cnx))
            {
                db.Open();

                if (!int.TryParse(IDV.Text.Trim(), out int idVente))
                {
                    MessageBox.Show("ID Vente invalide !");
                    return;
                }

                string deleteCmd = "DELETE FROM Vente WHERE id_vente=@id";
                SqlCommand Cmd = new SqlCommand(deleteCmd, db);
                Cmd.Parameters.Add("@id", SqlDbType.Int).Value = idVente;

                int rows = Cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Vente bien supprimée");

                    SqlCommand refreshCmd = new SqlCommand("SELECT * FROM Vente", db);
                    SqlDataReader fiche = refreshCmd.ExecuteReader();

                    dataGridView1.Rows.Clear();
                    while (fiche.Read())
                    {
                        dataGridView1.Rows.Add(
                            fiche["id_vente"],
                            fiche["date_vente"],
                            fiche["id_utilisateur"],
                            fiche["id_bon_livraison"],
                            fiche["id_client"]);
                    }
                    fiche.Close();
                }
                else
                {
                    MessageBox.Show("Aucune vente trouvée à supprimer !");
                }
                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
