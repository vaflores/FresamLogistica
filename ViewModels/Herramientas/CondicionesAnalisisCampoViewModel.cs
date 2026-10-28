using FresamLogistica.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels.Herramientas
{
    internal class CondicionesAnalisisCampoViewModel
{
        public int analisis_campo_detalle_id { get; set; }

        public int analisis_campo_id { get; set; }

        public int tipo_analisis_campo_id { get; set; }

        public string codigo_analisis { get; set; }

        public string analisis_campo { get; set; }

        public bool cumple { get; set; }

    }
}
