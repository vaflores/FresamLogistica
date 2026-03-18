using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FresamLogistica.Helper
{
    internal class EstadosHelper
    {
        public static void GenerarEstadosActivoInactivo(ref ComboBox _cb)
        {
            var estados = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("A", "Activo"),
                new KeyValuePair<string, string>("I", "Inactivo")
            };
            _cb.DataSource = estados;
            _cb.DisplayMember = "Value";
            _cb.ValueMember = "Key";
        }

        public static void GenerarEstadosDesdeLista(ref ComboBox _cb, List<KeyValuePair<string, string>> _lb)
        {
            _cb.DataSource = _lb;
            _cb.DisplayMember = "Value";
            _cb.ValueMember = "Key";
        }

        public static void GenerarEstadosRecepciones(ref ComboBox _cb)
        {
            var estados = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("A", "Activo"),
                new KeyValuePair<string, string>("I", "Inactivo")
            };

            _cb.DataSource = estados;
            _cb.DisplayMember = "Value";
            _cb.ValueMember = "Key";
        }

        public static void GenerarEstadosCiclos(ref ComboBox _cb)
        {
            var estados = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("A", "Activo"),
                new KeyValuePair<string, string>("C", "Cerrado"),
                new KeyValuePair<string, string>("I", "Inactivo")
            };
            _cb.DataSource = estados;
            _cb.DisplayMember = "Value";
            _cb.ValueMember = "Key";
        }
    }
}
