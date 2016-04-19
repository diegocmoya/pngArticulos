using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;

namespace pngArticulos
{
    public partial class frmUsuarios : Form
    {
        clsConexionSQL conexion;
        public frmUsuarios(clsConexionSQL conexion)
        {
            this.conexion = conexion;
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {

        }
    }
}
