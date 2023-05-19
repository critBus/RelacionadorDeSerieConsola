/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 9/8/2022
 * Hora: 22:04
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

using System.Linq;
using ReneUtiles;
using System.Collections.Generic;
using ReneUtiles.Clases;
//using System.IO;
using RelacionadorDeSerie.BD.Modelos;
using RelacionadorDeSerie.Privado;

using ReneUtiles.Clases.Multimedia;
using ReneUtiles.Clases.Multimedia.Relacionadores;
using ReneUtiles.Clases.Multimedia.Series;
using ReneUtiles.Clases.Multimedia.Series.Contextos;
using ReneUtiles.Clases.Multimedia.Series.Anime;
using ReneUtiles.Clases.Multimedia.Series.SeriesPersona;
using ReneUtiles.Clases.Multimedia.Series.Representaciones;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
using ReneUtiles.Clases.Multimedia.Series.Recorredores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos; 
using RelacionadorDeSerie.Representaciones;
using ReneUtiles.Clases.BD;
using ReneUtiles.Clases.BD.SesionEstorage;

using RelacionadorDeSerie.Privado.Consolas;

using ReneUtiles.Clases.Multimedia.Paquetes.Representaciones;
using ReneUtiles.Clases.Multimedia.Paquetes;
using ReneUtiles.Clases.Subprocesos;
using RelacionadorDeSerie.BD;

using Delimon.Win32.IO;
using System.Threading.Tasks;



namespace RelacionadorDeSerie
{

    public enum TipoDeFiltroPropio
    {
        FALTANTES_TODO,
        FALTANTES_ULTIMO_TRAMO,
        ULTIMOS_CAPITULOS

    }

    /// <summary>
    /// Description of ManagerRelacionadorDeSerie.
    /// </summary>
    public class ManagerRelacionadorDeSerie : ConsolaBasica
    {
        //		//List<>
        private BDRelacionador bd;
        private EjecutorDeSubprosesos ejs;

        public ManagerDeSeries animes;
        public ManagerDeSeries seriesPersona;

        private ManagerDePaquetes mngPaquete;

        private TemporalStorageBD ts;
        private BDSesionStorage sts;

        private Predicate<DirectoryInfo> usarCarpeta_Series;



        public ManagerRelacionadorDeSerie(EjecutorDeSubprosesos eje = null)
        {
            this.bd = new BDRelacionador();
            this.bd.crearTodasLasTablasSiNoExisten();

            this.ejs = (eje == null) ? new EjecutorDeSubprosesos() : eje;
            this.ts = new TemporalStorageBD();
            this.sts = new BDSesionStorage();

            ProcesadorDeRelacionesDeNombresClaveSeries proR = new ProcesadorDeRelacionesDeNombresClaveSeries();
            RecursosDePatronesDeSeriesGenerales reg = new RecursosDePatronesDeSeriesGenerales();
            //			
            ConfiguracionDeSeries cnfPersona = ConfiguracionDeSeries.getConfiguracionParaSeriesPersona(reg);

            this.seriesPersona = new ManagerDeSeries(proR, cnfPersona, TipoDeSeccion.SERIES);

            this.animes = new ManagerDeSeries(proR, ConfiguracionDeSeries.getConfiguracionParaAnime(reg), TipoDeSeccion.ANIME);

            this.mngPaquete = new ManagerDePaquetes(
                animes: this.animes
                , seriesPersona: this.seriesPersona);

            usarCarpeta_Series = v => (!v.Name.StartsWith("_")) && (!this.animes.cf.re.Re_NombresCarpetaSubtitulos.ReInicialFinal.Match(v.Name).Success);
            //RecursosDePatronesDeSeries rps=new RecursosDePatronesDeSeries();
        }


        public TemporalStorageBD TS()
        {
            return this.ts;
        }
        public BDSesionStorage STS()
        {
            return this.sts;
        }

        public void supr(EventosEnSubproceso ev,Action subproceso) {
            this.ejs.ejecutar(ev, subproceso);
        }

        public void guardarDireccionesPropias(
            TipoDeSeccion seccion
            , TipoDeCategoriaPropias categoria
        , List<DireccionDeActualizadorPropia> direcciones)
        {
            List<Direccion_MD> dsEnBD = this.bd.getDireccion_MD_All_Tipo_de_ubicacion_de_seccion_Seccion_Categoria(
                                            tipo_de_ubicacion_de_seccion: TipoDeUbicacionDeSeccion.PROPIAS.ToString()
                , seccion: seccion.ToString()
                , categoria: categoria.ToString()
                                        );
            foreach (Direccion_MD dm in dsEnBD)
            {
                //				bool laContiene=false;
                DireccionDeActualizadorPropia coincidente = null;
                foreach (DireccionDeActualizadorPropia d in direcciones)
                {
                    if (dm.idkey == d.id)
                    {
                        //						laContiene=true;
                        coincidente = d;
                        break;
                    }
                }
                if (coincidente != null)
                {
                    dm.url = coincidente.url;
                    dm.seleccionada = coincidente.seleccioniada;
                    dm.s();
                }
                else
                {
                    dm.d();
                }
            }

            foreach (DireccionDeActualizadorPropia d in direcciones)
            {
                if (d.id == null)
                {
                    Direccion_MD dm = new Direccion_MD(this.bd, url: d.url, seleccionada: d.seleccioniada
                                 , tipo_de_destino: TipoDeDestino.getTipoDeDestino_url(d.url).ToString()
                                 , tipo_de_ubicacion_de_seccion: TipoDeUbicacionDeSeccion.PROPIAS.ToString()
                                 , seccion: seccion.ToString()
                                 , categoria: categoria.ToString()
                                      ).s();
                    d.id = dm.idkey;
                }
            }

        }

        public DireccionDeActualizadorPropia agregarDireccionPropia(
            TipoDeSeccion seccion
            , TipoDeCategoriaPropias categoria
            , string url
            , bool estaSeleccionada = true
        )
        {
            DireccionDeActualizadorPropia d = new DireccionDeActualizadorPropia(
                                                  url: url,
                                                  seleccioniada: estaSeleccionada,
                                                  seccion: seccion,
                                                  categoria: categoria

                                              );


            Direccion_MD dm = new Direccion_MD(this.bd, url: url, seleccionada: d.seleccioniada
                                 , tipo_de_destino: TipoDeDestino.getTipoDeDestino_url(d.url).ToString()
                                 , tipo_de_ubicacion_de_seccion: TipoDeUbicacionDeSeccion.PROPIAS.ToString()
                                 , seccion: seccion.ToString()
                                 , categoria: categoria.ToString()
                              ).s();
            d.id = dm.idkey;
            return d;
        }


        public void eliminarDireccionPropia(DireccionDeActualizadorPropia d)
        {
            if (d.id != null && this.bd.existeDireccion_MD_id((int)d.id))
            {
                this.bd.deleteDireccion_MD_ForId((int)d.id);
            }
        }
        public void updateDireccionesPropias(DireccionDeActualizadorPropia d)
        {
            if (d.id != null && this.bd.existeDireccion_MD_id((int)d.id))
            {
                Direccion_MD dm = this.bd.getDireccion_MD_id((int)d.id);
                dm.categoria = d.categoria.ToString();
                dm.seccion = d.seccion.ToString();
                dm.seleccionada = d.seleccioniada;
                dm.url = d.url;
                dm.s();
            }
            else
            {

                Direccion_MD dm = new Direccion_MD(this.bd
                                                 , url: d.url
                                                 , seleccionada: d.seleccioniada
                                 , tipo_de_destino: TipoDeDestino.getTipoDeDestino_url(d.url).ToString()
                                 , tipo_de_ubicacion_de_seccion: TipoDeUbicacionDeSeccion.PROPIAS.ToString()
                                 , seccion: d.seccion.ToString()
                                 , categoria: d.categoria.ToString()
                                  ).s();
                d.id = dm.idkey;

            }
        }

        public List<DireccionDeActualizadorPropia> getDireccionesPropias(
            TipoDeSeccion seccion
            , TipoDeCategoriaPropias categoria)
        {

            List<Direccion_MD> dsEnBD = this.bd.getDireccion_MD_All_Tipo_de_ubicacion_de_seccion_Seccion_Categoria(
                                            tipo_de_ubicacion_de_seccion: TipoDeUbicacionDeSeccion.PROPIAS.ToString()
                , seccion: seccion.ToString()
                , categoria: categoria.ToString()
                                        );
            List<DireccionDeActualizadorPropia> ld = new List<DireccionDeActualizadorPropia>();
            foreach (Direccion_MD dm in dsEnBD)
            {
                ld.Add(new DireccionDeActualizadorPropia(
                    url: dm.url,
                    seleccioniada: dm.seleccionada??false,
                    seccion: seccion,
                    categoria: categoria,

                    id: dm.idkey

                ));
            }
            return ld;
        }


        public List<DireccionDeActualizadorPropia> getDireccionesPropias(
            TipoDeSeccion seccion
        )
        {
            List<Direccion_MD> dsEnBD = this.bd.getDireccion_MD_All_Tipo_de_ubicacion_de_seccion_Seccion(
                                            tipo_de_ubicacion_de_seccion: TipoDeUbicacionDeSeccion.PROPIAS.ToString()
                , seccion: seccion.ToString()
                                        );
            List<DireccionDeActualizadorPropia> ld = new List<DireccionDeActualizadorPropia>();
            foreach (Direccion_MD dm in dsEnBD)
            {
                ld.Add(new DireccionDeActualizadorPropia(
                    url: dm.url,
                    seleccioniada:dm.seleccionada??false,
                    categoria: TipoDeCategoriaPropias.get(dm.categoria),
                    seccion: seccion,//TipoDeCategoriaPropias.get(dm.tipo_de_seccion),
                    id: dm.idkey

                ));
            }
            return ld;
        }

        public List<DireccionDeActualizadorPropia> getDireccionesPropias(
            TipoDeCategoriaPropias categoria
        )
        {
            List<Direccion_MD> dsEnBD = this.bd.getDireccion_MD_All_Tipo_de_ubicacion_de_seccion_Categoria(
                                            tipo_de_ubicacion_de_seccion: TipoDeUbicacionDeSeccion.PROPIAS.ToString()
                , categoria: categoria.ToString()
                                        );
            List<DireccionDeActualizadorPropia> ld = new List<DireccionDeActualizadorPropia>();
            foreach (Direccion_MD dm in dsEnBD)
            {
                ld.Add(new DireccionDeActualizadorPropia(
                    url: dm.url,
                    seleccioniada:dm.seleccionada??false,
                    categoria: TipoDeCategoriaPropias.get(dm.categoria),
                    seccion: TipoDeSeccion.get(dm.seccion),//TipoDeCategoriaPropias.get(dm.tipo_de_seccion),
                    id: dm.idkey

                ));
            }
            return ld;
        }

        public List<DireccionDeActualizadorPropia> getDireccionesPropias(
            )
        {
            List<Direccion_MD> dsEnBD = this.bd.getDireccion_MD_All_Tipo_de_ubicacion_de_seccion(
                                            tipo_de_ubicacion_de_seccion: TipoDeUbicacionDeSeccion.PROPIAS.ToString()

                                        );
            List<DireccionDeActualizadorPropia> ld = new List<DireccionDeActualizadorPropia>();
            foreach (Direccion_MD dm in dsEnBD)
            {
                ld.Add(new DireccionDeActualizadorPropia(
                    url: dm.url,
                    seleccioniada:dm.seleccionada??false,
                    seccion: TipoDeSeccion.get(dm.seccion),
                    categoria: TipoDeCategoriaPropias.get(dm.categoria),

                    id: dm.idkey

                ));
            }
            return ld;
        }

        public void exportarDireccionesPropias_Subproseso(FileInfo f, EventosEnSubproceso eventos)
        {
            this.ejs.ejecutar(
                eventos,
                () =>
                {
                    BDAdmin newBD = new BDAdmin(f.ToString());
                    newBD.crearTodasLasTablas();
                    List<Direccion_MD> dsEnBD = this.bd.getDireccion_MD_All_Tipo_de_ubicacion_de_seccion(
                                                    tipo_de_ubicacion_de_seccion: TipoDeUbicacionDeSeccion.PROPIAS.ToString()

                                                );
                    foreach (Direccion_MD dm in dsEnBD)
                    {
                        newBD.insertarDireccion_MD(dm);
                    }
                }
            );


        }
        public void importarDireccionesPropias_Subproseso(FileInfo f, EventosEnSubproceso eventos)
        {
            this.ejs.ejecutar(
                eventos,
                () =>
                {
                    BDAdmin newBD = new BDAdmin(f.ToString());
                    List<Direccion_MD> dsEnNewBD = newBD.getDireccion_MD_All_Tipo_de_ubicacion_de_seccion(
                                                       tipo_de_ubicacion_de_seccion: TipoDeUbicacionDeSeccion.PROPIAS.ToString()

                                                   );
                    foreach (Direccion_MD dm in dsEnNewBD)
                    {
                        if (this.bd.existeDireccion_MD(
                                url: dm.url
                        , tipo_de_ubicacion_de_seccion: dm.tipo_de_ubicacion_de_seccion
                        , seccion: dm.seccion
                        , categoria: dm.categoria
                            ))
                        {
                            Direccion_MD dmPropia = this.bd.getDireccion_MD_For_Url_Tipo_de_ubicacion_de_seccion_Seccion_Categoria(
                                                        url: dm.url
                        , tipo_de_ubicacion_de_seccion: dm.tipo_de_ubicacion_de_seccion
                        , seccion: dm.seccion
                        , categoria: dm.categoria
                                                    );
                            dmPropia.seleccionada = dm.seleccionada;
                            dmPropia.s();
                        }
                        else
                        {
                            dm.idkey = -1;
                            this.bd.insertarDireccion_MD(dm);
                        }

                    }
                }
            );
        }

        public List<TipoDeCategoriaPropias> getCategoriasDeSeccionPropia(TipoDeSeccion seccion)
        {
            return new List<TipoDeCategoriaPropias>(TipoDeCategoriaPropias.VALUES);
        }


        private void actualizar_Propias(TipoDeSeccion seccion)
        {
            IEnumerable<Direccion_MD> ld = this.bd.getDireccion_MD_All_Tipo_de_ubicacion_de_seccion_Seccion(
                                            tipo_de_ubicacion_de_seccion: TipoDeUbicacionDeSeccion.PROPIAS.ToString()


                , seccion: seccion.ToString()
                                        );

            //cwl("cantidad de direcciones="+ld.Count);

            ld = from e in ld where e.seleccionada??false select e;


            if (seccion == TipoDeSeccion.ANIME)
            {
                this.animes.usarCarpeta = this.usarCarpeta_Series;

                this.animes.actualizar(ld);

                this.mngPaquete.animes.actualizar_Convinaciones(null);
            }
            else if (seccion == TipoDeSeccion.SERIES)
            {
                this.seriesPersona.usarCarpeta = this.usarCarpeta_Series;

                this.seriesPersona.actualizar(ld);

                this.mngPaquete.seriesPersona.actualizar_Convinaciones(null);
            }
            //actualizarSeries_EnPaquete(seccion);

        }

        public void actualizar_CategoriasPropias(TipoDeSeccion seccion
            , IEnumerable<TipoDeCategoriaPropias> categorias
            , EventosEnSubproceso eventos)
        {
            //cwl("aqui");
            this.ejs.ejecutar(eventos, () =>
            {
                List<Direccion_MD> ld = new List<Direccion_MD>();
                foreach (TipoDeCategoriaPropias cat in categorias)
                {
                    ld.AddRange(
                        this.bd.getDireccion_MD_All_Tipo_de_ubicacion_de_seccion_Seccion_Categoria(
                                            tipo_de_ubicacion_de_seccion: TipoDeUbicacionDeSeccion.PROPIAS.ToString()
                            , categoria: cat.ToString()

                        , seccion: seccion.ToString()
                                        )
                        );
                }



                //cwl("cantidad de direcciones="+ld.Count);

                ld = (from e in ld where e.seleccionada??false select e).ToList();


                if (seccion == TipoDeSeccion.ANIME)
                {
                    this.animes.usarCarpeta = this.usarCarpeta_Series;

                    this.animes.actualizarSolo(categorias,ld);

                    this.mngPaquete.animes.actualizar_Convinaciones(null, categorias.ToArray());
                }
                else if (seccion == TipoDeSeccion.SERIES)
                {
                    this.seriesPersona.usarCarpeta = this.usarCarpeta_Series;

                    this.seriesPersona.actualizarSolo(categorias, ld);

                    this.mngPaquete.seriesPersona.actualizar_Convinaciones(null, categorias.ToArray());
                }
                //actualizarSeries_EnPaquete(seccion);

            });

        }


        public void actualizar(TipoDeSeccion seccion, EventosEnSubproceso eventos)
        {
            //cwl("aqui");
            this.ejs.ejecutar(eventos, () =>
            {
                actualizar_Propias(seccion);

            });

        }
        public void actualizarSeries_EnPaquete_Y_Propias(TipoDeSeccion seccion
            , EventosEnSubproceso eventos

           , params TipoDeEtiquetaDeSerie[] etiquetas)
        {
            this.ejs.ejecutar(eventos, () =>
            {
                actualizar_Propias(seccion);
                actualizarSeries_EnPaquete(seccion, etiquetas);
            });
        }

        public void actualizarSeries_EnPaquete(
             TipoDeSeccion seccion
            , EventosEnSubproceso eventos

           , params TipoDeEtiquetaDeSerie[] etiquetas)
        {
            ejs.ejecutar(eventos, () =>
            {
                actualizarSeries_EnPaquete(seccion, etiquetas);
            });
        }


        

        public List<Capitulo_R> getCapitulos_Detalles_Propios(
            TipoDeSeccion seccion
            , TipoDeCategoriaPropias categoria
            , TipoDeFiltroPropio? filtro
        )
        {
            ManagerDeSeries mng = seccion == TipoDeSeccion.SERIES ? seriesPersona : animes;
            ConjuntoDeSeries cn = mng.getSeriesPropias(categoria);
            return getCapitulos_Detalles_Propios(cn, filtro);
        }
        public List<Capitulo_R> getCapitulos_Detalles_Propios(
            TipoDeSeccion seccion
        , TipoDeFiltroPropio? filtro)
        {
            ManagerDeSeries mng = seccion == TipoDeSeccion.SERIES ? seriesPersona : animes;
            return getCapitulos_Detalles_Propios(mng.todasLasSeries, filtro);
        }
        private List<Capitulo_R> getCapitulos_Detalles_Propios(
            ConjuntoDeSeries cn
                                                            , TipoDeFiltroPropio? filtro)
        {
            //if (!cn.isEmpty()) {
            //    cwl("vamos a ver 2");
            //}
            if (filtro != null)
            {
                if (filtro == TipoDeFiltroPropio.FALTANTES_TODO)
                {
                    cn = cn.getCapitulosFaltantes();
                }
                else if (filtro == TipoDeFiltroPropio.FALTANTES_ULTIMO_TRAMO)
                {
                    cn = cn.getCapitulosFaltantesSuperiores();
                }
                else if (filtro == TipoDeFiltroPropio.ULTIMOS_CAPITULOS)
                {
                    return UtilesSeriesR.getCapitulosR(cn.getUltimosCapitulos());
                }
            }

            return UtilesSeriesR.getCapitulosR(cn);
        }

        public List<Serie_R> getSeries_General_Propios(
            TipoDeSeccion seccion
            , TipoDeCategoriaPropias categoria)
        {
            ManagerDeSeries mng = seccion == TipoDeSeccion.SERIES ? seriesPersona : animes;
            ConjuntoDeSeries cn = mng.getSeriesPropias(categoria);
            return UtilesSeriesR.getSeriesR(cn);
        }
        public List<Serie_R> getSeries_General_Propios(
            TipoDeSeccion seccion
            )
        {
            ManagerDeSeries mng = seccion == TipoDeSeccion.SERIES ? seriesPersona : animes;
            return UtilesSeriesR.getSeriesR(mng.todasLasSeries);
        }

        public void cargarPaquete(DirectoryInfo carpeta, EventosEnSubproceso eventos)
        {
            ejs.ejecutar(eventos, () =>
            {
                Paquete p = mngPaquete.cargarPaquete(carpeta);

                SeccionSeriesPaquete[] secciones = { mngPaquete.seriesPersona, mngPaquete.animes };
                
                foreach (SeccionSeriesPaquete seccion in secciones)
                {

                    string seccionStr = seccion == mngPaquete.seriesPersona ? TipoDeSeccion.SERIES.ToString() : TipoDeSeccion.ANIME.ToString();

                    Dictionary<ConjuntoDeEtiquetasDeSerie, SeriesEnSeccionDelPaquete>[] conjuntos_en_seccion = { seccion.seriesEnEstaSeccion_tx, seccion.seriesEnEstaSeccion_finalizadas};
                    foreach (var conjunto in conjuntos_en_seccion)
                    {

                        foreach (KeyValuePair<ConjuntoDeEtiquetasDeSerie, SeriesEnSeccionDelPaquete> seriesEnSeccion in conjunto)
                        {
                            foreach (DirectorioDeSeriesDelPaquete directorio in seriesEnSeccion.Value.directorios)
                            {
                                DireccionDePaquete_MD d = new DireccionDePaquete_MD(bd
                                    , url: directorio.carpeta.ToString()
                                    , seleccionada: true
                                    , tipo_de_destino: TipoDeDestino.CARPETA.ToString()
                                    , seccion: seccionStr
                                    ).s();
                                foreach (TipoDeEtiquetaDeSerie tag in directorio.etiquetas.etiquetas)
                                {
                                    d.addEtiquetaDeDireccionPaquete_MD(new EtiquetaDeDireccionPaquete_MD(bd
                                        , nombre: tag.nombreTag
                                        , direccion_de_paquete: d
                                        ));
                                }
                            }

                        }

                    }

                    



                }
            });
        }

        private List<Url_Tipo> parseYSeleccionar(List<DireccionDePaquete_MD> ld) {
            List<Url_Tipo> lurls = (from u in ld where u.seleccionada??false select new Url_Tipo { url = u.url, esCarpeta = TipoDeDestino.get(u.tipo_de_destino) == TipoDeDestino.CARPETA }).ToList();
            return lurls;
        }

        private void actualizarSeries_EnPaquete(
            TipoDeSeccion seccion
            , params TipoDeEtiquetaDeSerie[] etiquetas)
        {
            List<DireccionDePaquete_MD> ld = bd.getDireccionDePaquete_All(
                seccion: seccion
                , etiquetas: etiquetas
                );

            SeccionSeriesPaquete seccionDePaquete = seccion == TipoDeSeccion.ANIME ? this.mngPaquete.animes : this.mngPaquete.seriesPersona;
            if (etiquetas.Length > 0)
            {
                //List<Url_Tipo> lurls = (from u in ld where u.seleccionada select new Url_Tipo { url = u.url, esCarpeta = TipoDeDestino.get(u.tipo_de_destino) == TipoDeDestino.CARPETA }).ToList();
                seccionDePaquete.actualizar(parseYSeleccionar(ld), etiquetas);
            }
            else
            {
                Dictionary<ConjuntoDeEtiquetasDeSerie, List<Url_Tipo>> dTags = ConjuntoDeEtiquetasDeSerie.getNewDictionary<List<Url_Tipo>>();
                foreach (DireccionDePaquete_MD d in ld)
                {
                    if (d.seleccionada??false)
                    {
                        List<EtiquetaDeDireccionPaquete_MD> le = d.getListaDe_EtiquetaDeDireccionPaquete_MD();
                        ConjuntoDeEtiquetasDeSerie c = new ConjuntoDeEtiquetasDeSerie(from t in le select TipoDeEtiquetaDeSerie.get(t.nombre));
                        Url_Tipo u = new Url_Tipo { url = d.url, esCarpeta = TipoDeDestino.get(d.tipo_de_destino) == TipoDeDestino.CARPETA };
                        if (dTags.ContainsKey(c))
                        {
                            dTags[c].Add(u);
                        }
                        else
                        {
                            dTags.Add(c, new Url_Tipo[] { u }.ToList());
                        }
                    }

                }
                seccionDePaquete.set(dTags);
                //foreach (KeyValuePair<ConjuntoDeEtiquetasDeSerie, List<Url_Tipo>> item in dTags)
                //{
                //    seccionDePaquete.actualizar(item.Value, item.Key.etiquetas);
                //}


            }


        }


        public void actualizarSeries_EnPaquete(
            TipoDeSeccion seccion
            , ConjuntoDeEtiquetasDeSerie[] conjuntoDeEtiquetas
            , EventosEnSubproceso eventos)
        {
            this.ejs.ejecutar(eventos, () => {
                Dictionary<ConjuntoDeEtiquetasDeSerie, List<Url_Tipo>> direcciones = ConjuntoDeEtiquetasDeSerie.getNewDictionary<List<Url_Tipo>>();

                foreach (ConjuntoDeEtiquetasDeSerie cnj in conjuntoDeEtiquetas)
                {
                    direcciones.Add(cnj, parseYSeleccionar(bd.getDireccionDePaquete_All(seccion, cnj.etiquetas.ToArray())));
                }

                SeccionSeriesPaquete seccionDePaquete = seccion == TipoDeSeccion.ANIME ? this.mngPaquete.animes : this.mngPaquete.seriesPersona;

                seccionDePaquete.actualizar(direcciones);
            });
            

        }

        private void actualizarSeries_EnPaquete(TipoDeSeccion seccion)
        {
            Dictionary<ConjuntoDeEtiquetasDeSerie, List<FileSystemInfo>> direcciones = ConjuntoDeEtiquetasDeSerie.getNewDictionary<List<FileSystemInfo>>();

            //SeccionSeriesPaquete seccionDePaquete = seccion == TipoDeSeccion.ANIME ? this.mngPaquete.animes : this.mngPaquete.seriesPersona;
            List<DireccionDePaquete_MD> ld = bd.getDireccionDePaquete_MD_All_Seccion(seccion.ToString());

            foreach (DireccionDePaquete_MD d in ld)
            {
                if (d.seleccionada??false)
                {
                    ConjuntoDeEtiquetasDeSerie cn
                    = ConjuntoDeEtiquetasDeSerie.getNewConjunto(from e in d.getListaDe_EtiquetaDeDireccionPaquete_MD()
                                                                select TipoDeEtiquetaDeSerie.get(e.nombre));
                    if (direcciones.ContainsKey(cn))
                    {
                        direcciones[cn].Add(Archivos.getFileSystemCorrecto(d.url));
                    }
                    else
                    {
                        FileSystemInfo fsy = null;
                        if (TipoDeDestino.get(d.tipo_de_destino) == TipoDeDestino.CARPETA)
                        {
                            fsy = new DirectoryInfo(d.url);
                        }
                        else
                        {
                            fsy = new FileInfo(d.url);
                        }
                        direcciones.Add(cn, new FileSystemInfo[] { fsy }.ToList());
                    }
                }

            }

        }



        private List<Capitulo_R> __getCapitulos_EnPaquete(
            TipoDeSeccion seccion
            , TipoDeCategoriaPropias categoria//null para todas
            , bool extrenos
            , TipoDeFiltroPropio? filtro// null sin filtro
            , params TipoDeEtiquetaDeSerie[] etiquetas)
        {

            ConjuntoDeSeries cn = __getConjuntoDeSeries_EnPaquete(seccion, categoria, extrenos, filtro, etiquetas);
            return getCapitulos_Detalles_Propios(cn, filtro);
        }
        /// <summary>
        /// 
        /// si se quiere todas las tx seria {TX} 
        /// pero si se quiere solo las que tengan tx {TX,TX}
        /// lo mismo con las finalizadas
        /// 
        /// </summary>
        /// <param name="seccion"></param>
        /// <param name="categoria"></param>
        /// <param name="extrenos"></param>
        /// <param name="filtro"></param>
        /// <param name="etiquetas"></param>
        /// <returns></returns>
        private ConjuntoDeSeries __getConjuntoDeSeries_EnPaquete(TipoDeSeccion seccion
            , TipoDeCategoriaPropias categoria//null para todas
            , bool extrenos
            , TipoDeFiltroPropio? filtro// null sin filtro
            , params TipoDeEtiquetaDeSerie[] etiquetas)
        {
            SeccionSeriesPaquete seccionDePaquete = seccion == TipoDeSeccion.ANIME ? this.mngPaquete.animes : this.mngPaquete.seriesPersona;
            //ConjuntoDeEtiquetasDeSerie ce = new ConjuntoDeEtiquetasDeSerie(ComparadorTipoDeEtiquetaDeSerie.getNewSortedSet_TipoDeEtiquetaDeSerie(etiquetas));

            //SeriesEnSeccionDelPaquete seriesEnEtiquetas = etiquetas.Length > 0 ? seccionDePaquete.seriesEnEstaSeccion[new ConjuntoDeEtiquetasDeSerie(etiquetas)] : seccionDePaquete.todasLasSeries;
            SeriesEnSeccionDelPaquete seriesEnEtiquetas = null;
            if (etiquetas.Length > 0) {
                ConjuntoDeEtiquetasDeSerie cne= new ConjuntoDeEtiquetasDeSerie(etiquetas);
                if (cne.esFinalizadas())
                {
                    seriesEnEtiquetas = etiquetas.Length==1? seccionDePaquete.todasLasSeries_finalizadas: seccionDePaquete.seriesEnEstaSeccion_finalizadas[cne];
                }
                else {
                    seriesEnEtiquetas = etiquetas.Length == 1 ? seccionDePaquete.todasLasSeries_tx : seccionDePaquete.seriesEnEstaSeccion_tx[cne];
                }
            } else {
                seriesEnEtiquetas=seccionDePaquete.todasLasSeries;
            }
            ConvinacionesDeSeries cnv = categoria == null ? seriesEnEtiquetas.seriesEnCategoriaTodas : seriesEnEtiquetas.convinacionesPorCategorias[categoria];
            ConjuntoDeSeries cn = extrenos ? cnv.seriesExtrenos : cnv.seriesCoincidentes;
            return cn;
        }



        public List<Capitulo_R> getCapitulos_EnPaquete(
            TipoDeSeccion seccion
            , TipoDeCategoriaPropias categoria//null para todas

            , TipoDeFiltroPropio? filtro// null sin filtro
            , params TipoDeEtiquetaDeSerie[] etiquetas)
        {
            return __getCapitulos_EnPaquete(seccion, categoria, false, filtro, etiquetas);
        }

        public List<Capitulo_R> getCapitulos_Extrenos_DelPaquete(
            TipoDeSeccion seccion
            , TipoDeFiltroPropio? filtro
            , params TipoDeEtiquetaDeSerie[] etiquetas)
        {
            return __getCapitulos_EnPaquete(seccion, null, true, filtro, etiquetas);
        }



        private List<Serie_R> __getSeries_EnPaquete(TipoDeSeccion seccion
            , TipoDeCategoriaPropias categoria//null para todas
            , bool extrenos
            , TipoDeFiltroPropio? filtro// null sin filtro
            , params TipoDeEtiquetaDeSerie[] etiquetas)
        {

            ConjuntoDeSeries cn = __getConjuntoDeSeries_EnPaquete(seccion, categoria, extrenos, filtro, etiquetas);
            return UtilesSeriesR.getSeriesR(cn);
        }
        public List<Serie_R> getSeries_EnPaquete(TipoDeSeccion seccion
            , TipoDeCategoriaPropias categoria//null para todas

            , TipoDeFiltroPropio? filtro// null sin filtro
            , params TipoDeEtiquetaDeSerie[] etiquetas)
        {
            return __getSeries_EnPaquete(seccion, categoria, false, filtro, etiquetas);
        }

        public List<ConjuntoDeEtiquetasDeSerie> getEtiquetasDeSerie_EnPaquete(TipoDeSeccion seccion,bool? tx)
        {
            SeccionSeriesPaquete seccionDePaquete = seccion == TipoDeSeccion.ANIME ? this.mngPaquete.animes : this.mngPaquete.seriesPersona;

            if (tx==null) {
                List<ConjuntoDeEtiquetasDeSerie> l = seccionDePaquete.seriesEnEstaSeccion_tx.Keys.ToList();
                l.AddRange(seccionDePaquete.seriesEnEstaSeccion_finalizadas.Keys.ToList());
                return ConjuntoDeEtiquetasDeSerie.getNewHashSet(l).ToList();
            }
            if (tx==true) {
                return seccionDePaquete.seriesEnEstaSeccion_tx.Keys.ToList();
            }
            return seccionDePaquete.seriesEnEstaSeccion_finalizadas.Keys.ToList();

            
        }

        public List<Serie_R> getSeries_Extrenos_EnPaquete(
            TipoDeSeccion seccion
            , TipoDeFiltroPropio? filtro, params TipoDeEtiquetaDeSerie[] etiquetas)
        {
            return __getSeries_EnPaquete(seccion, null, true, filtro, etiquetas);
        }

        public List<DireccionDePaquete> getDireccionesDePaquete(TipoDeSeccion seccion, params TipoDeEtiquetaDeSerie[] etiquetas)
        {

            Func<DireccionDePaquete_MD, DireccionDePaquete> parse = d => new DireccionDePaquete(
                          url: d.url
                          , seleccionada: d.seleccionada??false
                          , seccion: TipoDeSeccion.get(d.seccion)
                          , etiquetas: ConjuntoDeEtiquetasDeSerie.getNewConjunto((from e in d.getListaDe_EtiquetaDeDireccionPaquete_MD()
                                                                                  select TipoDeEtiquetaDeSerie.get(e.nombre)).ToList())
                                                                                  , id: d.idkey

                          );
            List<DireccionDePaquete_MD> ld = bd.getDireccionDePaquete_All(seccion, etiquetas); //= seccion != null ? bd.getDireccionDePaquete_All(seccion, etiquetas) :(etiquetas.Length==0)? bd.getDireccionDePaquete_MD_All():bd.;
            return (from d in ld
                    select parse(d)).ToList();

        }

        public void agregarDireccionesDePaquete(TipoDeSeccion seccion
            // , TipoDeCategoriaPropias categoria
            , TipoDeEtiquetaDeSerie[] etiquetas
            , string url
            , bool estaSeleccionada = true)
        {
            DireccionDePaquete_MD d = new DireccionDePaquete_MD(bd,
                url: url
                , seleccionada: estaSeleccionada
                , tipo_de_destino: TipoDeDestino.getTipoDeDestino_url(url).ToString()
                , seccion: seccion.ToString()).s();
            foreach (TipoDeEtiquetaDeSerie tag in etiquetas)
            {
                d.addEtiquetaDeDireccionPaquete_MD(new EtiquetaDeDireccionPaquete_MD(bd
                    , nombre: tag.nombreTag
                    , direccion_de_paquete: d
                    ));
            }
            //
        }


        public void updateDireccionesDePaquete(DireccionDePaquete d)
        {
            if (d.id != null && this.bd.existeDireccionDePaquete_MD_id((int)d.id))
            {
                DireccionDePaquete_MD dm = this.bd.getDireccionDePaquete_MD_id((int)d.id);
                dm.seccion = d.seccion.ToString();
                dm.seleccionada = d.seleccioniada;
                dm.url = d.url;
                dm.s();

                bd.setEtiquetasDeDireccionPaquete(dm, d.etiquetas.etiquetas.ToArray());
            }
            else
            {

                DireccionDePaquete_MD dm = new DireccionDePaquete_MD(bd,
                url: d.url
                , seleccionada: d.seleccioniada
                , tipo_de_destino: TipoDeDestino.getTipoDeDestino_url(d.url).ToString()
                , seccion: d.seccion.ToString()).s();
                bd.setEtiquetasDeDireccionPaquete(dm, d.etiquetas.etiquetas.ToArray());

                d.id = dm.idkey;

            }
        }

        public void eliminarDireccionesDePaquete(DireccionDePaquete d)
        {
            if (d.id != null && this.bd.existeDireccionDePaquete_MD_id((int)d.id))
            {
                this.bd.deleteDireccionDePaquete_MD_ForId_CASCADE((int)d.id);
            }
        }


        public void importarDireccionesPaquetes_Subproseso(FileInfo f, EventosEnSubproceso eventos)
        {
            this.ejs.ejecutar(
                eventos,
                () =>
                {
                    BDAdmin newBD = new BDAdmin(f.ToString());
                    List<DireccionDePaquete_MD> dsEnNewBD = newBD.getDireccionDePaquete_MD_All();
                    foreach (DireccionDePaquete_MD dm in dsEnNewBD)
                    {

                        List<EtiquetaDeDireccionPaquete_MD> ltags = dm.getListaDe_EtiquetaDeDireccionPaquete_MD();
                        dm.idkey = -1;
                        DireccionDePaquete_MD dm_new = this.bd.insertarDireccionDePaquete_MD(dm);
                        foreach (EtiquetaDeDireccionPaquete_MD tag in ltags)
                        {
                            dm_new.addEtiquetaDeDireccionPaquete_MD(new EtiquetaDeDireccionPaquete_MD(bd, tag.nombre, dm_new));
                        }


                    }



                }
            );
        }

        public void exportarDireccionesPaquete_Subproseso(FileInfo f, EventosEnSubproceso eventos)
        {
            this.ejs.ejecutar(
                eventos,
                () =>
                {
                    BDAdmin newBD = new BDAdmin(f.ToString());
                    newBD.crearTodasLasTablas();
                    List<DireccionDePaquete_MD> dsEnBD = this.bd.getDireccionDePaquete_MD_All();
                    foreach (DireccionDePaquete_MD dm in dsEnBD)
                    {
                        newBD.insertarDireccionDePaquete_MD(dm);
                    }
                    List<EtiquetaDeDireccionPaquete_MD> Letp = this.bd.getEtiquetaDeDireccionPaquete_MD_All();
                    foreach (EtiquetaDeDireccionPaquete_MD tag in Letp)
                    {
                        newBD.insertarEtiquetaDeDireccionPaquete_MD(tag);
                    }
                }
            );


        }

        public void guardarPaquete(DirectoryInfo carpetaPaquete,int nivelesCarpetasInternas) {
            if (carpetaPaquete.Exists&&nivelesCarpetasInternas>0) {
                File_MD filePaquete = almacenar_contenido_de_carpeta(carpetaPaquete, nivelesCarpetasInternas);
                
            }
        }

        public File_MD almacenar_contenido_de_carpeta(DirectoryInfo carpeta, int nivelesCarpetasInternas,File_MD padre=null) {
            if (nivelesCarpetasInternas>0) {
                File_MD carpet_actual = new File_MD(this.bd
                , nombre: carpeta.Name
                , tipo: TipoDeFile_R.CARPETA.ToString()).s();
                if (padre != null)
                {
                    new ArchivoInterno_MD(this.bd
                        , id_carpeta_padre: padre
                        , id_file_hijo: carpet_actual
                        ).s();
                }

                FileInfo[] FI = carpeta.GetFiles();
                foreach (FileInfo f in FI)
                {
                    File_MD archivoInterno = new File_MD(this.bd
                    , nombre: carpeta.Name
                    , tipo: TipoDeFile_R.ARCHIVO.ToString()).s();
                    new ArchivoInterno_MD(this.bd
                        , id_carpeta_padre: carpet_actual
                        , id_file_hijo: archivoInterno
                        ).s();
                }

                DirectoryInfo[] DI = carpeta.GetDirectories();
                foreach (DirectoryInfo d in DI)
                {
                    almacenar_contenido_de_carpeta(d, nivelesCarpetasInternas - 1, carpet_actual);
                }
                return carpet_actual;
            }
            return null;
            //carpeta.GetDirectories
        }
    }

}
//	public enum TipoDeDestino
//	{
//		CARPETA,
//		TXT
//
//	}
//	public enum TipoDeUbicacionDeSeccion
//	{
//		PROPIAS,
//		PAQUETE
//
//	}
//	public enum TipoDeSeccion
//	{
//		SERIES,
//		ANIME
//
//	}
//	public enum TipoDeCategoriaPropias
//	{
//		SEGUIDAS,
//		POR_ENTRAR,
//		EN_ESPERA,
//		FINALIZADAS
//
//	}
//public class ConfiguracionDeFiltrosPropios
//	{
//		public ConfiguracionDeFiltrosPropios()
//		{
//		}
//	}

//			this.bd.deleteDireccion_MD_For_Tipo_de_ubicacion_de_seccion_Tipo_de_seccion_Clasificacion_de_seccion(
//				tipo_de_ubicacion_de_seccion:TipoDeUbicacionDeSeccion.PROPIAS
//				,tipo_de_seccion:tipo
//				,clasificacion_de_seccion:clasificacion
//			);
//
//			foreach (DireccionDeActualizadorPropia d in direcciones) {
//				new Direccion_MD(this.bd,url:d.url,seleccionada:d.seleccioniada
//				                 ,tipo_de_destino:TipoDeDestino.getTipoDeDestino_url(d.url)
//				                 ,tipo_de_ubicacion_de_seccion:TipoDeUbicacionDeSeccion.PROPIAS
//				                 ,tipo_de_seccion:tipo
//				                 ,clasificacion_de_seccion:clasificacion);
//			}
