using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;// retornar de system de windows de la maquina 
using System.Data.SqlClient;//acceso a la BD(IMEC)

namespace Modelo
{
    public class clsConexionSQL
    {
        //Area de declaracion de variables
        #region Atributos
        private string codigo;
        private string clave;
        private string perfil;
        private string baseDatos;
        private string nombreServidor;

        private SqlConnection conexion; //Guardar la cadena de conexion del usuario
        private SqlCommand comando; // permite ejecutar IMEC
        #endregion

        // Establecemos el metodo inicial
        #region Constructor
        public clsConexionSQL()
        {
            this.codigo = "";
            this.clave = "";
            this.baseDatos = "BDEstudiantes";
            this.perfil = "";

        }
        #endregion


        // Propiedades de lectura y escritura
        #region Gety Set
        public void setCodigo(string codigo) { this.codigo = codigo.Trim(); }
        public void setClave(string clave) { this.clave = clave.Trim(); }
        public void setPerfil(string perfil) { this.perfil = perfil.Trim(); }

        public string getCodigo() { return codigo; }
        public string getClave() { return clave; }
        public string getPerfil() { return perfil; }
        public string getBaseDatos() { return baseDatos; }
        #endregion

        //Metodo para la conexion con la BD
        #region Metodos
        // este metodo permite ejecutar los select

        public SqlDataReader mSeleccionar(string strSentencia, clsConexionSQL cone)
        {
            try
            {
                if (mConectar(cone))
                {
                    comando = new SqlCommand(strSentencia, conexion);
                    comando.CommandType = System.Data.CommandType.Text;
                    return comando.ExecuteReader();// El ExecuteReader ejecuta solo select

                }
                else
                    return null;
            }
            catch 
            {
                return null;
            }
        }


        //Este metodo permitira ejecutar los Insert,Update, Delete
        public Boolean mEjecutar(string strSentencia, clsConexionSQL cone)
        {
            try
            {
                if (mConectar(cone))
                {
                    comando = new SqlCommand(strSentencia, conexion);
                    comando.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }


        //este permite conectar
        public Boolean mConectar(clsConexionSQL cone)
        {
            try
            {
                conexion = new SqlConnection();
                conexion.ConnectionString = "user id='" + cone.getCodigo() + "'; password='" + cone.getClave() + "'; Data Source='" + mNomServidor() + "'; Initial Catalog='" + this.getBaseDatos() + "'";
                conexion.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string mNomServidor()
        {
            return Dns.GetHostName();
        }

        #endregion

    }
}
