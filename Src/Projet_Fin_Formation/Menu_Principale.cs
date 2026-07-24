using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Projet_Fin_Formation
{
    public partial class Menu_Principale : Form
    {
        public Menu_Principale()
        {
            InitializeComponent();
        }



        public static String RoleConnecte = "";
        private void button2_Click(object sender, EventArgs e)
        {
            new Vente().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Paiement().Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Client().Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //if (RoleConnecte.Trim() != "1") 
            //{
            //    button1.Enabled = false;
            //    button2.Enabled = false;
            //}
            // if (RoleConnecte.Trim() != "2")
            //{
            //    button1.Enabled = true;
            //    button2.Enabled = true;
            //}
            //else if (RoleConnecte.Trim() != "3")
            //{
            //    button1.Enabled = true;
            //    button2.Enabled = true;
            //}

            

        }

        private void button16_Click(object sender, EventArgs e)
        {
            new Utilisateur().Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            new Audit().Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new catégorie().Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new Entrepot().Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new Emplacement().Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            new Role().Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new Commande_Achat().Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            new Fournisseur().Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            new Produit().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Bon_Livraison().Show();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            new Lot().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Ligne_Vente().Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            new Fournir().Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            new Inventaire().Show();
        }

        private void Ligne_Iinventaire_Click(object sender, EventArgs e)
        {
            new Ligne_Inventaire().Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new Ligne_Achat().Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new Ligne_Stock().Show();
        }
    }
}
