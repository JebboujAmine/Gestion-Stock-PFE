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
    public partial class Audit : Form
    {
        public Audit()
        {
            InitializeComponent();
        }

        public string Chemin = "Data Source=AMINEJB\\SQLEXPRESS;Initial Catalog=Gestion_Stock;Integrated Security=True";

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Audit_Load(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Chemin))
            {

                db.Open();

                string actualiser = "select * from Audit ";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                        Fiche["id_audit"],
                        Fiche["id_cible"],
                        Fiche["nom_table"],
                        Fiche["type_op"],
                        Fiche["Date_op"],
                        Fiche["nom_utilisateur"],
                        Fiche["ancienne_valeur"],
                        Fiche["nouvelle_valeur"],
                        Fiche["id_utilisateur"],
                        Fiche["id_utilisateur_connecte"]);
                }
                Fiche.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(Chemin))
            {

                db.Open();

                string actualiser = "select * from Audit ";
                SqlCommand Cmd = new SqlCommand(actualiser, db);
                SqlDataReader Fiche = Cmd.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (Fiche.Read())
                {
                    dataGridView1.Rows.Add(
                      Fiche["id_audit"],
                        Fiche["id_cible"],
                        Fiche["nom_table"],
                        Fiche["type_op"],
                        Fiche["Date_op"],
                        Fiche["nom_utilisateur"],
                        Fiche["ancienne_valeur"],
                        Fiche["nouvelle_valeur"],
                        Fiche["id_utilisateur"],
                        Fiche["id_utilisateur_connecte"]);
                }
                Fiche.Close();
            }
        }
    }
}
