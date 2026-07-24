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
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();

        }
       public string conn = @"Data Source=AMINEJB\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";
                        
        private void Form_Login_Load(object sender, EventArgs e)
        {
             using (SqlConnection cnx = new SqlConnection (conn) )
             {
                SqlCommand cmd = new SqlCommand( " SELECT id_role, libelle FROM Role",cnx);
                System.Data.DataTable dt = new System.Data.DataTable();
                try 
                {
                    cnx.Open();
                     dt.Load(cmd.ExecuteReader());
                     R.DataSource = dt;
                     R.DisplayMember = "libelle";
                     R.ValueMember = "id_role";
                 } 
                            catch (Exception ex) 
                           { 
                            MessageBox.Show("Erreur Rôles: " + ex.Message); 
                          }

             }
         }

        private void button1_Click(object sender, EventArgs e)
        {

            using (SqlConnection cnx = new SqlConnection(conn))
            {
                string Q = "SELECT id_utilisateur FROM Utilisateur WHERE nom_utilisateur = @u  AND mot_passe = @mdp AND id_role = @r AND is_active = '1' ";

                SqlCommand cmd = new SqlCommand(Q, cnx);
                cmd.Parameters.Add("@u", SqlDbType.VarChar, 50).Value = nu.Text.Trim();
                cmd.Parameters.Add("@mdp", SqlDbType.VarChar, 50).Value = mp.Text.Trim();
                cmd.Parameters.Add("@r", SqlDbType.Int).Value = Convert.ToInt32(R.SelectedValue);

                try
                {
                    cnx.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            SecurityContext.IdUtilisateur = Convert.ToInt32(dr["id_utilisateur"]);
                            SecurityContext.RoleConnecte = R.Text;
                            new Menu_Principale().Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("NOM D’UTILISATEUR OU MOT DE PASSE OU ROLE EST INCORRECT !!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
             {
             this.Close();
             }

        private void R_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
            
    }
}
