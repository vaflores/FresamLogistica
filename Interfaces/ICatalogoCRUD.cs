using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Interfaces
{
    // Se agrega la restricción 'class' para indicar que T es un tipo de referencia.
    public interface ICatalogoCRUD<T, Tid> where T : class
    {
        void Crear(T entidad);
        void Actualizar(T entidad);
        void Eliminar(Tid id);
        T ObtenerPorId(Tid id); 
        List<T> Listar();
    }
}
