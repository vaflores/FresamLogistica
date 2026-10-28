using FresamLogistica.Helper;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FresamLogistica.Helper
{
    public static class HelperPdf 
    {
        #region Convertir archivo a bytes

        /// <summary>
        /// Convierte un archivo PDF seleccionado por el usuario
        /// en un arreglo de bytes para almacenarlo en la BD.
        /// </summary>
        /// <param name="owner">Formulario propietario del diálogo.</param>
        /// <param name="nombreArchivo">Nombre del archivo seleccionado.</param>
        /// <returns>Contenido del PDF en bytes. Null si se cancela.</returns>
        public static byte[] ConvertirABytes(
            IWin32Window owner,
            out string nombreArchivo)
        {
            nombreArchivo = null;

            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Title = "Seleccionar documento PDF";
                dialogo.Filter = "Archivo PDF (*.pdf)|*.pdf";
                dialogo.Multiselect = false;
                dialogo.CheckFileExists = true;

                if (dialogo.ShowDialog(owner) != DialogResult.OK)
                    return null;

                byte[] contenido =
                    File.ReadAllBytes(dialogo.FileName);

                ValidarPdf(contenido);

                nombreArchivo =
                    ObtenerNombreSeguro(
                        Path.GetFileName(dialogo.FileName));

                return contenido;
            }
        }

        /// <summary>
        /// Convierte directamente una ruta de archivo PDF a bytes.
        /// </summary>
        public static byte[] ConvertirABytes(
            string rutaArchivo)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo))
                throw new ArgumentException(
                    "La ruta del archivo es obligatoria.");

            if (!File.Exists(rutaArchivo))
                throw new FileNotFoundException(
                    "No se encontró el archivo PDF.",
                    rutaArchivo);

            byte[] contenido =
                File.ReadAllBytes(rutaArchivo);

            ValidarPdf(contenido);

            return contenido;
        }

        #endregion


        #region Visualizar

        /// <summary>
        /// Visualiza un PDF que fue obtenido de la base de datos.
        /// </summary>
        public static void Visualizar(
            byte[] contenido,
            string nombreArchivo = "documento.pdf",
            string titulo = "Visor de PDF")
        {
            ValidarPdf(contenido);

            string rutaTemporal =
                CrearArchivoTemporal(
                    contenido,
                    nombreArchivo);

            try
            {
                using (FrmVisorPdf visor =
                    new FrmVisorPdf(
                        rutaTemporal,
                        titulo))
                {
                    visor.ShowDialog();
                }
            }
            finally
            {
                EliminarArchivoTemporal(
                    rutaTemporal);
            }
        }

        #endregion


        #region Descargar

        /// <summary>
        /// Permite descargar/guardar localmente un PDF
        /// obtenido de la base de datos.
        /// </summary>
        /// <returns>
        /// True si el usuario guardó el archivo.
        /// False si canceló.
        /// </returns>
        public static bool Descargar(
            byte[] contenido,
            string nombreArchivo = "documento.pdf",
            IWin32Window owner = null)
        {
            ValidarPdf(contenido);

            nombreArchivo =
                ObtenerNombreSeguro(nombreArchivo);

            using (SaveFileDialog dialogo =
                new SaveFileDialog())
            {
                dialogo.Title =
                    "Guardar documento PDF";

                dialogo.Filter =
                    "Archivo PDF (*.pdf)|*.pdf";

                dialogo.DefaultExt = "pdf";

                dialogo.AddExtension = true;

                dialogo.FileName =
                    nombreArchivo;

                dialogo.OverwritePrompt = true;

                if (dialogo.ShowDialog(owner) !=
                    DialogResult.OK)
                {
                    return false;
                }

                File.WriteAllBytes(
                    dialogo.FileName,
                    contenido);

                return true;
            }
        }

        #endregion


        #region Visor externo

        /// <summary>
        /// Abre el PDF utilizando el visor PDF
        /// predeterminado de Windows.
        /// </summary>
        public static void AbrirConVisorPredeterminado(
            byte[] contenido,
            string nombreArchivo = "documento.pdf")
        {
            ValidarPdf(contenido);

            string rutaTemporal =
                CrearArchivoTemporal(
                    contenido,
                    nombreArchivo);

            try
            {
                Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = rutaTemporal,
                        UseShellExecute = true
                    });
            }
            catch
            {
                EliminarArchivoTemporal(
                    rutaTemporal);

                throw;
            }
        }

        #endregion


        #region Archivo temporal

        private static string CrearArchivoTemporal(
            byte[] contenido,
            string nombreArchivo)
        {
            string carpeta =
                Path.Combine(
                    Path.GetTempPath(),
                    "Fresam",
                    "Pdf");

            Directory.CreateDirectory(
                carpeta);

            nombreArchivo =
                ObtenerNombreSeguro(
                    nombreArchivo);

            string archivoTemporal =
                $"{Guid.NewGuid()}_{nombreArchivo}";

            string ruta =
                Path.Combine(
                    carpeta,
                    archivoTemporal);

            File.WriteAllBytes(
                ruta,
                contenido);

            return ruta;
        }

        private static void EliminarArchivoTemporal(
            string rutaArchivo)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(
                    rutaArchivo) &&
                    File.Exists(rutaArchivo))
                {
                    File.Delete(rutaArchivo);
                }
            }
            catch
            {
                // No interrumpir la aplicación
                // por un error al eliminar el temporal.
            }
        }

        #endregion


        #region Validación

        /// <summary>
        /// Valida que el contenido corresponda
        /// a un archivo PDF.
        /// </summary>
        public static void ValidarPdf(
            byte[] contenido)
        {
            if (contenido == null ||
                contenido.Length == 0)
            {
                throw new ArgumentException(
                    "El contenido del PDF está vacío.");
            }

            // Encabezado PDF: %PDF-
            if (contenido.Length < 5 ||
                contenido[0] != 0x25 ||
                contenido[1] != 0x50 ||
                contenido[2] != 0x44 ||
                contenido[3] != 0x46 ||
                contenido[4] != 0x2D)
            {
                throw new ArgumentException(
                    "El archivo no contiene un PDF válido.");
            }
        }

        #endregion


        #region Información

        public static long ObtenerTamano(
            byte[] contenido)
        {
            return contenido?.LongLength ?? 0;
        }

        public static string ObtenerTamanoLegible(
            byte[] contenido)
        {
            if (contenido == null ||
                contenido.Length == 0)
            {
                return "0 KB";
            }

            double bytes =
                contenido.Length;

            if (bytes < 1024)
                return $"{bytes:0} bytes";

            if (bytes < 1024 * 1024)
                return $"{bytes / 1024:0.##} KB";

            return $"{bytes / (1024 * 1024):0.##} MB";
        }

        public static string ObtenerContentType()
        {
            return "application/pdf";
        }

        #endregion


        #region Nombre

        public static string ObtenerNombreSeguro(
            string nombreArchivo)
        {
            if (string.IsNullOrWhiteSpace(
                nombreArchivo))
            {
                nombreArchivo =
                    "documento.pdf";
            }

            nombreArchivo =
                Path.GetFileName(
                    nombreArchivo);

            foreach (char caracter in
                Path.GetInvalidFileNameChars())
            {
                nombreArchivo =
                    nombreArchivo.Replace(
                        caracter.ToString(),
                        string.Empty);
            }

            if (!nombreArchivo.EndsWith(
                ".pdf",
                StringComparison.OrdinalIgnoreCase))
            {
                nombreArchivo += ".pdf";
            }

            return nombreArchivo;
        }

        #endregion
    }
}

