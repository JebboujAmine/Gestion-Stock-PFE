using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Projet_Fin_Formation
{
    public partial class Utilisateur : Form
    {
        public Utilisateur()
        {
            InitializeComponent();
        }
        public string Chemin = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";

           
        private void button1_Click(object sender, EventArgs e)
        {
            idu.Clear();
            nu.Clear();
            mp.Clear();
            idu.Select();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Chemin))
            {

                db.Open();

                    string actualiser = "SELECT id_utilisateur, nom_utilisateur, mot_passe, id_role FROM Utilisateur WHERE is_active = 1";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        Fiche["id_utilisateur"],
                        Fiche["nom_utilisateur"],
                        Fiche["mot_passe"],
                        Fiche["id_role"]);
                }
                
                Fiche.Close();
            }
            
            }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Chemin))
            {
                db.Open();
                SqlCommand cast = new SqlCommand("SET CONTEXT_INFO @id_u;", db);
                cast.Parameters.Add("@id_u", SqlDbType.VarBinary, 128).Value = SecurityContext.IdBytes;
                cast.ExecuteNonQuery();

                string Cmd = "insert into Utilisateur(id_utilisateur,nom_utilisateur,mot_passe,id_role) values (@id, @n, @mdp, @ir)  ";

                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.AddWithValue("@id", idu.Text.Trim());
                insrt.Parameters.AddWithValue("@n", nu.Text.Trim());
                insrt.Parameters.AddWithValue("@mdp", mp.Text.Trim());
                insrt.Parameters.Add("@ir", SqlDbType.Int).Value = Convert.ToInt32(R.SelectedValue);
                

                insrt.ExecuteNonQuery();
                Utilisateur_Load(sender, e);

                MessageBox.Show(" Utilisateur Bien Ajouter !! .");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
    using (SqlConnection db = new SqlConnection(Chemin))
    {
        db.Open();

        string Q = "SELECT id_utilisateur, nom_utilisateur, mot_passe, id_role, is_active FROM Utilisateur WHERE id_utilisateur = @id";
        SqlCommand Cmd = new SqlCommand(Q, db);
        Cmd.Parameters.AddWithValue("@id", idu.Text.Trim());
        SqlDataReader Fiche = Cmd.ExecuteReader();
        
        dataGridView1.Rows.Clear();
        while (Fiche.Read())
        {
            dataGridView1.Rows.Add(
                Fiche["id_utilisateur"],
                Fiche["nom_utilisateur"],
                Fiche["mot_passe"],
                Fiche["id_role"],
                (Convert.ToInt32(Fiche["is_active"]) == 1 ? "Actif" : "Désactivé")
            );
        }

        Fiche.Close();
    }
}

        

        private void Utilisateur_Load(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Chemin))
            {

                db.Open();

                string actualiser = "SELECT id_utilisateur, nom_utilisateur, mot_passe, id_role FROM Utilisateur WHERE is_active = 1";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        Fiche["id_utilisateur"],
                        Fiche["nom_utilisateur"],
                        Fiche["mot_passe"],
                        Fiche["id_role"]                       
                        );
                }
                Fiche.Close();

                 SqlCommand ir = new SqlCommand( " SELECT id_role, libelle FROM Role",db);
                System.Data.DataTable dt = new System.Data.DataTable();
                try 
                {
                     dt.Load(ir.ExecuteReader());
                     R.DataSource = dt;
                     R.DisplayMember = "libelle";
                     R.ValueMember = "id_role";
                 } 
                            catch (Exception ex) 
                           { 
                            MessageBox.Show("Role n'est pas correcte!!.  " + ex.Message); 
                          }
            }
        }
           
        

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Chemin))
            {
                db.Open();
                SqlCommand cast = new SqlCommand("SET CONTEXT_INFO @id_u;", db);
                cast.Parameters.Add("@id_u", SqlDbType.VarBinary, 128).Value = SecurityContext.IdBytes;
                cast.ExecuteNonQuery();


                if (string.IsNullOrEmpty(idu.Text) ||
                    string.IsNullOrEmpty(nu.Text) ||
                    string.IsNullOrEmpty(mp.Text) ||
                    R.SelectedIndex == -1)
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                    return;
                }

                int idUtilisateur, idRole;
                if (!int.TryParse(idu.Text.Trim(), out idUtilisateur))
                {
                    MessageBox.Show("ID utilisateur invalide !");
                    return;
                }
                
                idRole = Convert.ToInt32(R.SelectedValue);

                string Q = "UPDATE Utilisateur SET nom_utilisateur = @n, mot_passe = @mdp, id_role = @ir WHERE id_utilisateur = @id";

                SqlCommand updt = new SqlCommand(Q, db);
                updt.Parameters.Add("@id", SqlDbType.Int).Value = idUtilisateur;
                updt.Parameters.Add("@n", SqlDbType.VarChar, 50).Value = nu.Text.Trim();
                updt.Parameters.Add("@mdp", SqlDbType.VarChar, 50).Value = mp.Text.Trim();
                updt.Parameters.Add("@ir", SqlDbType.Int).Value = idRole;

                updt.ExecuteNonQuery();
                Utilisateur_Load(sender, e);
            }
    MessageBox.Show("Modification bien effectuée !");
}


        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Chemin))
            {
                db.Open();

                SqlCommand cast = new SqlCommand("SET CONTEXT_INFO @id_u;", db);
                cast.Parameters.Add("@id_u", SqlDbType.VarBinary, 128).Value = SecurityContext.IdBytes;
                cast.ExecuteNonQuery();

                string Q = "UPDATE Utilisateur SET is_active = 0 WHERE id_utilisateur = @id";


                SqlCommand Cmd = new SqlCommand(Q, db);
                Cmd.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(idu.Text.Trim());

                Cmd.ExecuteNonQuery();
                Utilisateur_Load(sender, e);
                MessageBox.Show("Utilisateur bien supprimé !");
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Chemin))
            {
                db.Open();

                
                SqlCommand cast = new SqlCommand("SET CONTEXT_INFO @id_u;", db);
                cast.Parameters.Add("@id_u", SqlDbType.VarBinary, 128).Value = SecurityContext.IdBytes;
                cast.ExecuteNonQuery();

                
                string checkQ = "SELECT is_active FROM Utilisateur WHERE id_utilisateur = @id";
                SqlCommand checkCmd = new SqlCommand(checkQ, db);
                checkCmd.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(idu.Text.Trim());

                int currentState = Convert.ToInt32(checkCmd.ExecuteScalar());

                
                int newState = (currentState == 1) ? 0 : 1;

                string Q = "UPDATE Utilisateur SET is_active = @newState WHERE id_utilisateur = @id";
                SqlCommand Cmd = new SqlCommand(Q, db);
                Cmd.Parameters.Add("@newState", SqlDbType.Int).Value = newState;
                Cmd.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(idu.Text.Trim());

                Cmd.ExecuteNonQuery();

                Utilisateur_Load(sender, e);

                if (newState == 0)
                    MessageBox.Show("Utilisateur désactivé !");
                else
                    MessageBox.Show("Utilisateur activé !");
            }
        }

        private void R_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
