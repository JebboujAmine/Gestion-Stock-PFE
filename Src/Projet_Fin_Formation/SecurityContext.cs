using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Projet_Fin_Formation
{
    public static class SecurityContext
{
            public static int IdUtilisateur { get; set; }

            public static byte[] IdBytes
            {
                get { return BitConverter.GetBytes(IdUtilisateur); }
            }
            public static string RoleConnecte { get; set; }

        //    public static void SetContextInfo(SqlConnection db)
        //    {

        //    if (SecurityContext.IdUtilisateur <= 0)
        //    {
        //        MessageBox.Show("Utilisateur non valide !");
        //        return;
        //    }
        //    SqlCommand cast = new SqlCommand("SET CONTEXT_INFO @id_u;", db);
        //    //cast.Parameters.Add("@id_u", SqlDbType.Binary, 4).Value = BitConverter.GetBytes(SecurityContext.IdUtilisateur);
        //    cast.Parameters.Add("@id_u", SqlDbType.VarBinary, 128).Value = BitConverter.GetBytes(SecurityContext.IdUtilisateur);
        //    cast.ExecuteNonQuery();
        //    SqlCommand test = new SqlCommand("SELECT CAST(CONTEXT_INFO() AS INT)", db);
        //    MessageBox.Show("SQL ID = " + test.ExecuteScalar());
        //}
    }
}
