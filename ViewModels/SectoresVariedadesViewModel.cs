using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.ViewModels
{
    internal class SectoresVariedadesViewModel
    {
        public int sector_id { get; set; }
        public int superficie { get; set; }
        public string nombre { get; set; }
        public int fruta_id { get; set; }
        public string nombre_fruta { get; set; }
        public int variedad_id { get; set; }
        public string nombre_variedad { get; set; }
        public string estado { get; set; }
        public int rancho_id { get; set; }
        public int ciclo_id { get; set; }
    }
}
