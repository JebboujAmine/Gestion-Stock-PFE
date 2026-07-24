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
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }
        public string Cnx = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";
        public int idClient;
        
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            idc.Clear();
            nc.Clear();
            m.Clear();
            t.Clear();
            a.Clear();
            idc.Select();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
               db.Open();
                string Cmd = "insert into Client(id_client,nom_complet,telephone,email,adresse) values ( @IDC, @NC ,@T ,@M , @A) ";
                SqlCommand insrt = new SqlCommand(Cmd, db);
                insrt.Parameters.Add("@IDC", SqlDbType.Int).Value = int.Parse(idc.Text.Trim());
                insrt.Parameters.Add("@NC", SqlDbType.VarChar, 80).Value = nc.Text.Trim();
                insrt.Parameters.Add("@T", SqlDbType.VarChar, 20).Value = t.Text.Trim();
                insrt.Parameters.Add("@M", SqlDbType.VarChar, 100).Value = m.Text.Trim();
                insrt.Parameters.Add("@A", SqlDbType.VarChar, 200).Value = a.Text.Trim();
                
                insrt.ExecuteNonQuery();
                
                MessageBox.Show("Client Bien Ajouter");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
            db.Open();
            if (string.IsNullOrEmpty(idc.Text) ||
                string.IsNullOrEmpty(nc.Text) ||
                string.IsNullOrEmpty(t.Text) ||
                string.IsNullOrEmpty(m.Text) ||
                string.IsNullOrEmpty(a.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                return;
            }
            
            if (!int.TryParse(idc.Text.Trim(), out idClient))
            {
                MessageBox.Show("ID client invalide !");
                return;
            }
            string Cmd = " update Client set nom_complet = @NC ,telephone = @T ,email =@ML , adresse = @AD where id_client = @IDC " ;
            SqlCommand updt = new SqlCommand(Cmd,db );
            updt.Parameters.Add("@IDC", SqlDbType.Int).Value = idClient;
            updt.Parameters.Add("@NC", SqlDbType.VarChar, 80).Value = nc.Text.Trim();
            updt.Parameters.Add("@T", SqlDbType.VarChar, 20).Value = t.Text.Trim();
            updt.Parameters.Add("@ML", SqlDbType.VarChar, 100).Value = m.Text.Trim();
            updt.Parameters.Add("@AD", SqlDbType.VarChar, 200).Value = a.Text.Trim(); 
            updt.ExecuteNonQuery();
            MessageBox.Show("Client Bien Modifier");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

               string actualiser = "SELECT * FROM Client";
               SqlCommand refreshCmd = new SqlCommand(actualiser, db);
               SqlDataReader fiche = refreshCmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (fiche.Read())
                    {
                  dataGridView1.Rows.Add(
                  fiche["id_client"],
                  fiche["nom_complet"],
                  fiche["telephone"],
                  fiche["email"],
                  fiche["adresse"]);
                   }
                  fiche.Close();
            }
        }

        private void Client_Load(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "select id_client,nom_complet,telephone,email,adresse from Client ";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        Fiche["id_client"],
                        Fiche["nom_complet"],
                        Fiche["telephone"],
                        Fiche["email"],
                        Fiche["adresse"]);

                }
                Fiche.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {

                db.Open();

                string actualiser = "select * from Client where id_client = @id ";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                Cmd.Parameters.AddWithValue("@id",idc.Text.Trim());
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        Fiche["id_client"],
                        Fiche["nom_complet"],
                        Fiche["telephone"],
                        Fiche["email"],
                        Fiche["adresse"]);

                }
                
                Fiche.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Cnx))
            {
             db.Open();

             int idClient;
                if (!int.TryParse(idc.Text.Trim(), out idClient))
                {
                    MessageBox.Show("ID client invalide !");
               return;
                 }

                 string deleteCmd = "DELETE FROM Client WHERE id_client = @id";
             SqlCommand Cmd = new SqlCommand(deleteCmd, db);
             Cmd.Parameters.Add("@id", SqlDbType.Int).Value = idClient;

         int rows = Cmd.ExecuteNonQuery();
         if (rows > 0)
         {
            MessageBox.Show("Client Bien Supprimé");
          string actualiser = "SELECT id_client, nom_complet, telephone, email, adresse FROM Client";
          SqlCommand refreshCmd = new SqlCommand(actualiser, db);
          SqlDataReader fiche = refreshCmd.ExecuteReader();

           dataGridView1.Rows.Clear();
       while (fiche.Read())
           {
              dataGridView1.Rows.Add(
                  fiche["id_client"],
                   fiche["nom_complet"],
                   fiche["telephone"],
                   fiche["email"],
                   fiche["adresse"]);
    }
    
            fiche.Close();
        }
        else
        {
            MessageBox.Show("Aucun client trouvé avec cet ID");
        }
            }
        }
    }
}
