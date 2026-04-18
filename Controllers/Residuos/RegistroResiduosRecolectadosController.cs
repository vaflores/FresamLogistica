using FresamLogistica.Controllers.General;
using FresamLogistica.Helper;
using FresamLogistica.Model;
using FresamLogistica.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace FresamLogistica.Controllers.Residuos
{
    internal class RegistroResiduosRecolectadosController
    {
        private static VehiculosController _repoVehiculos = new VehiculosController();
        private static FoliosController _repoFolios = new FoliosController();
        private static OperadoresResiduosController _repoOperadores = new OperadoresResiduosController();
        public RegistroResiduosRecolectadosController()
        {

        }

        public List<RegistroResiduoRecolectadoListViewModel> ConsultarRegistrosResiduosRecolectadosPorFecha(DateTime _fechaConsulta)
        {
            var fechaInicio = _fechaConsulta.Date;
            var fechaFin = fechaInicio.AddDays(1);
            using (var context = new Model.Model())
            {
                var _registros = (from registros in context.RegistrosResiduoRecolectado
                                  join operadores in context.OperadoresResiduos on registros.operador_residuo_id equals operadores.operador_residuo_id into operadorJoin
                                  join empresas in context.EmpresaGestoraDeResiduos on operadorJoin.FirstOrDefault().empresa_gestora_id equals empresas.empresa_gestora_id into empresaJoin
                                  join destinos in context.DestinosResiduo on registros.destino_residuo_id equals destinos.destino_residuo_id into destinoJoin
                                  join vehiculo in context.Vehiculos on registros.vehiculo_id equals vehiculo.vehiculo_id into vehiculoJoin
                                  join detRegistro in context.DetalleRegistroResiduoRecolectado on registros.registro_residuo_id equals detRegistro.registro_residuo_id into detalleJoin
                                  join tipoResiduo in context.TipoResiduo on detalleJoin.FirstOrDefault().tipo_residuo_id equals tipoResiduo.tipo_residuo_id into tipoResiduoJoin
                                  join frutas in context.Frutas on tipoResiduoJoin.FirstOrDefault().fruta_id equals frutas.fruta_id into frutaJoin
                                  join producciones in context.Producciones on new { fruta_id = frutaJoin.FirstOrDefault().fruta_id, fecha = registros.fecha } equals new { fruta_id = producciones.fruta_id, fecha = producciones.fecha } into produccionesJoin
                                  where registros.fecha >= fechaInicio && registros.fecha < fechaFin
                                  select new RegistroResiduoRecolectadoListViewModel
                                  {
                                      RegistroResiduoId = registros.registro_residuo_id,
                                      Fecha = registros.fecha,
                                      EmpresaResiduo = empresaJoin.FirstOrDefault().nombre,
                                      Operador = operadorJoin.FirstOrDefault().nombre,
                                      Destino = destinoJoin.FirstOrDefault().nombre,
                                      PlacasVehiculo = vehiculoJoin.FirstOrDefault().placas,
                                      CantidadTotal = registros.cantidad_total,
                                      ProduccionTotal = produccionesJoin.FirstOrDefault() == null ? 0 : produccionesJoin.FirstOrDefault().cantidad,
                                      PorcentajeMerma = produccionesJoin.FirstOrDefault() == null ? 0 : registros.cantidad_total * 100 / produccionesJoin.FirstOrDefault().cantidad,
                                      Observaciones = registros.observaciones,
                                      Estado = registros.estado
                                  }).ToList();


                return _registros;
            }
        }

        public int GuardarRegistroResiduoRecolectado(RegistroResiduosRecolectadoViewModel _registro)
        {
            int _registroId = 0;
            if (_registro.VehiculoId == 0)
            {
                _registro.VehiculoId = _repoVehiculos.CrearVehiculo(_registro.Vehiculo);
            }

            using (var context = new Model.Model())
            {
                var nuevoRegistro = new RegistrosResiduoRecolectado
                {
                    fecha = _registro.Fecha,
                    operador_residuo_id = _registro.OperadorResiduoId,
                    destino_residuo_id = _registro.DestinoResiduoId,
                    vehiculo_id = _registro.VehiculoId,
                    cantidad_total = _registro.CantidadTotal,
                    observaciones = _registro.Observaciones,
                    estado = _registro.Estado
                };
                SesionActualHelper.RegistrarSesionCrea(ref nuevoRegistro);
                context.RegistrosResiduoRecolectado.Add(nuevoRegistro);
                context.SaveChanges();
                _registroId = nuevoRegistro.registro_residuo_id;
            }

            _registro.RegistroResiduoId = _registroId;

            CrearDetalleRegistroRecolectado(_registro);
            CrearDetalleFoliosUtilizados(_registro);

            return _registroId;
        }

        public void ActualizarRegistroResiduoRecolectado(RegistroResiduosRecolectadoViewModel _registro)
        {
            using (var context = new Model.Model())
            {
                var registroExistente = context.RegistrosResiduoRecolectado.Find(_registro.RegistroResiduoId);
                if (registroExistente != null)
                {
                    registroExistente.fecha = _registro.Fecha;
                    registroExistente.operador_residuo_id = _registro.OperadorResiduoId;
                    registroExistente.destino_residuo_id = _registro.DestinoResiduoId;
                    registroExistente.vehiculo_id = _registro.VehiculoId;
                    registroExistente.cantidad_total = _registro.CantidadTotal;
                    registroExistente.observaciones = _registro.Observaciones;
                    registroExistente.estado = _registro.Estado;
                    SesionActualHelper.RegistrarUsuarioModifica(ref registroExistente);
                    context.Entry(registroExistente).CurrentValues.SetValues(registroExistente);
                    context.SaveChanges();
                    _registro.RegistroResiduoId = registroExistente.registro_residuo_id;
                }
            }

            EliminarDetalleRegistroRecolectado(_registro);
            CrearDetalleRegistroRecolectado(_registro);
            EliminarDetalleFoliosNoUtilizados(_registro);
            CrearDetalleFoliosUtilizados(_registro); 

        }

        public RegistroResiduosRecolectadoViewModel ConsultarRegistroResiduosRecolectado(int _registroId)
        {
            using (var context = new Model.Model())
            {
                var registro = context.RegistrosResiduoRecolectado.Find(_registroId);
                var empresaId = _repoOperadores.ObtenerPorId(registro.operador_residuo_id).empresa_gestora_id;

                if (registro != null)
                {
                    var detalleResiduos = (from detalle in context.DetalleRegistroResiduoRecolectado
                                            join tipoResiduo in context.TipoResiduo on detalle.tipo_residuo_id equals tipoResiduo.tipo_residuo_id
                                            join frutas in context.Frutas on tipoResiduo.fruta_id equals frutas.fruta_id
                                           where detalle.registro_residuo_id == _registroId
                                            select new DetalleRegistroResiduoRecolectadoViewModel
                                            {
                                                DetalleRegistroId = detalle.detalle_registro_residuo_id,
                                                FrutaId = frutas.fruta_id,
                                                NombreFruta = frutas.nombre,
                                                TipoResiduoId = tipoResiduo.tipo_residuo_id,
                                                NombreTipoResiduo = tipoResiduo.nombre,
                                                Cantidad = detalle.cantidad
                                            }).ToList();

                    var foliosResiduos = (from folios in context.FoliosRegistroResiduoRecolectado
                                          where folios.registro_residuo_id == _registroId
                                          select new FoliosRegistroResiduoRecolectadoViewModel
                                          {
                                              FolioId = folios.folio_id,
                                              numero = folios.Folios.numero
                                          }).ToList();
                    var vehiculo = context.Vehiculos.Find(registro.vehiculo_id);

                    return new RegistroResiduosRecolectadoViewModel
                    {
                        RegistroResiduoId = registro.registro_residuo_id,
                        Fecha = registro.fecha,
                        EmpresaResiduoId = empresaId,
                        OperadorResiduoId = registro.operador_residuo_id,
                        DestinoResiduoId = registro.destino_residuo_id,
                        VehiculoId = registro.vehiculo_id,
                        CantidadTotal = registro.cantidad_total,
                        Observaciones = registro.observaciones,
                        Estado = registro.estado,
                        DetalleResiduosRecolectados = detalleResiduos,
                        FoliosRegistroResiduosRecolectados = foliosResiduos,
                        Vehiculo = vehiculo
                    };
                }
            }
            return null;
        }


        //Detalle Registro Residuo Recolectado
        public void CrearDetalleRegistroRecolectado(RegistroResiduosRecolectadoViewModel _registro)
        {
            foreach (var detalle in _registro.DetalleResiduosRecolectados)
            {
                var nuevoDetalle = new DetalleRegistroResiduoRecolectado
                {
                    detalle_registro_residuo_id = 0,
                    registro_residuo_id = _registro.RegistroResiduoId,
                    tipo_residuo_id = detalle.TipoResiduoId,
                    cantidad = detalle.Cantidad
                };

                SesionActualHelper.RegistrarSesionCrea(ref nuevoDetalle);
                using (var context = new Model.Model())
                {
                    context.DetalleRegistroResiduoRecolectado.Add(nuevoDetalle);
                    context.SaveChanges();
                }
            }
        }
        
        public void EliminarDetalleRegistroRecolectado(RegistroResiduosRecolectadoViewModel _registro)
        {
            using (var context = new Model.Model())
            {
                var detallesExistentes = context.DetalleRegistroResiduoRecolectado.Where(d => d.registro_residuo_id == _registro.RegistroResiduoId).ToList();
                context.DetalleRegistroResiduoRecolectado.RemoveRange(detallesExistentes);
                context.SaveChanges();
            }
        }

        public void CrearDetalleFoliosUtilizados(RegistroResiduosRecolectadoViewModel _registro)
        {
            foreach (var folio in _registro.FoliosRegistroResiduosRecolectados)
            {
                //Verificar si el folio ya existe en la recepcion para evitar duplicarlo
                FoliosRegistroResiduoRecolectado _f = _repoFolios.ObtenerFolioRecepcionPorId(_registro.RegistroResiduoId,  folio.FolioId);

                //Si existe actualizar usuario y fecha de modificación
                if (_f != null)
                {
                    using (var context = new Model.Model())
                    {
                        var folioExistente = context.FoliosRegistroResiduoRecolectado.Find(_f.folios_registro_residuo_id);
                        if (folioExistente != null)
                        {
                            SesionActualHelper.RegistrarUsuarioModifica(ref folioExistente);
                            context.Entry(folioExistente).CurrentValues.SetValues(folioExistente);
                            context.SaveChanges();
                        }
                    }
                }
                //Si no existe crear nuevo registro
                else
                {
                    using (var context = new Model.Model())
                    {
                        var nuevoFolio = new FoliosRegistroResiduoRecolectado
                        {
                            registro_residuo_id = _registro.RegistroResiduoId,
                            folio_id = folio.FolioId
                        };
                        SesionActualHelper.RegistrarSesionCrea(ref nuevoFolio);
                        context.FoliosRegistroResiduoRecolectado.Add(nuevoFolio);
                        context.SaveChanges();
                    }
                        _repoFolios.MarcarFolioUtilizado(folio.FolioId, _registro.Fecha);
                }

            }
        }

        public void EliminarDetalleFoliosNoUtilizados(RegistroResiduosRecolectadoViewModel _recoleccion)
        {
            List<FoliosRegistroResiduoRecolectado> foliosAEliminar;
            using (var context = new Model.Model())
            {
                List<FoliosRegistroResiduoRecolectado> foliosExistentes = context.FoliosRegistroResiduoRecolectado.Where(f => f.registro_residuo_id == _recoleccion.RegistroResiduoId).ToList();
                foliosAEliminar = foliosExistentes.Where(f => !_recoleccion.FoliosRegistroResiduosRecolectados.Any(u => u.FolioId == f.folio_id)).ToList();
            }

            if(foliosAEliminar.Count > 0)
            {
                foreach (var folio in foliosAEliminar)
                {
                    using (var context = new Model.Model())
                    {
                        var folioExistente = context.FoliosRegistroResiduoRecolectado.Find(folio.folios_registro_residuo_id);
                        if (folioExistente != null)
                        {
                            context.FoliosRegistroResiduoRecolectado.Remove(folioExistente);
                        }
                        context.SaveChanges();
                    }
                    _repoFolios.MarcarFolioNoUtilizado(folio.folio_id);
                }
            }

                
        }
    }


}
