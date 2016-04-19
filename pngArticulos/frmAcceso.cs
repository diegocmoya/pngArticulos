using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//llamado de las referencias propias del proyecto
using System.Data.SqlClient;
using Modelo;
using Controlador;

namespace pngArticulos
{
    public partial class frmAcceso : Form
    {

        #region Atributos
        clsConexionSQL conexion;
        clsEntidadUsuario pEntidadUsuario;
        clsUsuario usuario;
        SqlDataReader dtrUsuario;//retorno de las tuplas
        int contador = 0;
        #endregion

        // inicializamos los atributos que utilizaremos en toda la ventana 
        public frmAcceso()
        {
            conexion = new clsConexionSQL();
            pEntidadUsuario = new clsEntidadUsuario();
            usuario = new clsUsuario();
            InitializeComponent();
        }

        private void frmAcceso_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Accion para salir del sistema
            Application.Exit();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                this.txtClave.Focus();
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (mValidarDatos()==true)
                {
                    this.btnIngresar.Enabled = true;
                }
            }
        }



        #region Metodos
        private Boolean mValidarDatos()
        {
            if (contador <= 2)
            {
                //llenado de los atributos para el servidor
                conexion.setCodigo("admEstudiante");
                conexion.setCodigo("123");
                //llenado de los atributos de la clase entidadUsuario
                pEntidadUsuario.setCodigo(this.txtUsuario.Text.Trim());
                pEntidadUsuario.setClave(this.txtClave.Text.Trim());
                //consultamos si el usuario existe
                dtrUsuario = usuario.mConsultarUsuario(conexion, pEntidadUsuario);

                //evaluo si retorna tuplas o datos
                if (dtrUsuario != null)
                {
                    if (dtrUsuario.Read())
                    {
                        pEntidadUsuario.setPerfil(dtrUsuario.GetString(2));
                        pEntidadUsuario.setEstado(dtrUsuario.GetInt32(3));
                        if (pEntidadUsuario.getEstado() == 0)
                        {
                            this.btnIngresar.Enabled = true;
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("El usuario esta bloqueado", "Atencion", MessageBoxButtons.OK);
                            return false;
                        }//fin pEntidadUsuario
                    }
                    else
                    {
                        MessageBox.Show("El usuario no existe", "Atencion", MessageBoxButtons.OK);
                        return false;
                    }//fin del if read
                } else {
                    MessageBox.Show("El usuario no existe", "Atencion", MessageBoxButtons.OK);
                    return false;
                }// fin del null

            }else {
                MessageBox.Show("Usted digito el 3 veces su usuario de forma erronea :( ", "Usuario Bloqueado", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }//fin if contador

            return true;
        }//fin del mValidar
        #endregion

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            this.SetVisibleCore(false);
            mdiMenu menu = new mdiMenu();
            menu.Show();
        }
    }
}
