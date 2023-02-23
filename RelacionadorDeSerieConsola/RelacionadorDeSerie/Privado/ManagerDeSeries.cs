/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 26/8/2022
 * Hora: 17:49
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles;
using System.Collections.Generic;
using ReneUtiles.Clases;
//using System.IO;
using Delimon.Win32.IO;
using RelacionadorDeSerie.BD.Modelos;
using RelacionadorDeSerie;
using ReneUtiles.Clases.Tipos;

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
using ReneUtiles.Clases.Videos;
using RelacionadorDeSerie.Representaciones;

using RelacionadorDeSerie.Privado.Consolas;

namespace RelacionadorDeSerie.Privado
{
    /// <summary>
    /// Description of ManagerDeSeries.
    /// </summary>
    public class ManagerDeSeries : ConsolaBasica
    {


        public Dictionary<TipoDeCategoriaPropias, ConjuntoDeSeries> seriesPropias;
        public ConjuntoDeSeries todasLasSeries;

        public ProcesadorDeRelacionesDeNombresClaveSeries prs;
        public ConfiguracionDeSeries cf;
        public ProcesadorDeSeries pro;

        public TipoDeSeccion seccion;

        public Predicate<DirectoryInfo> usarCarpeta;


        public ManagerDeSeries(ProcesadorDeRelacionesDeNombresClaveSeries prs
                             // , ProcesadorDeSeries pr
                             , ConfiguracionDeSeries cf
            , TipoDeSeccion seccion
        )
        {
            this.seccion = seccion;
            //RecursosDePatronesDeSeries re = new RecursosDePatronesDeSeries(cf);
            this.pro = new ProcesadorDeSeries(cf.re);

            this.prs = prs;
            this.cf = pro.re.cf;
            resetear();




        }

        public ConjuntoDeSeries getNewConjuntoDeSeries()
        {
            return new ConjuntoDeSeries(prs, cf);
        }
        private void resetear()
        {
            seriesPropias = ComparadorTipoDeCategoriaPropia.getNewDictionary_TipoDeCategoriaPropias<ConjuntoDeSeries>();
            seriesPropias.Add(TipoDeCategoriaPropias.EN_ESPERA, new ConjuntoDeSeries(prs, this.cf));
            seriesPropias.Add(TipoDeCategoriaPropias.FINALIZADAS, new ConjuntoDeSeries(prs, this.cf));
            seriesPropias.Add(TipoDeCategoriaPropias.POR_ENTRAR, new ConjuntoDeSeries(prs, this.cf));
            seriesPropias.Add(TipoDeCategoriaPropias.SEGUIDAS, new ConjuntoDeSeries(prs, this.cf));
            seriesPropias.Add(TipoDeCategoriaPropias.QUE_TENGO, new ConjuntoDeSeries(prs, this.cf));
            todasLasSeries = new ConjuntoDeSeries(prs, this.cf);
        }


        public ConjuntoDeSeries getSeriesPropias(TipoDeCategoriaPropias categoria)
        {
            return this.seriesPropias[categoria];
        }

        public void actualizar(IEnumerable<Direccion_MD> urls)
        {
            resetear();




            foreach (Direccion_MD d in urls)
            {
                if (d.seleccionada)
                {
                    TipoDeDestino td = TipoDeDestino.get(d.tipo_de_destino);
                    FileSystemInfo fs = null;//td == TipoDeDestino.CARPETA ? new DirectoryInfo(d.url) : new FileInfo(d.url);
                    if (td == TipoDeDestino.CARPETA)
                    {
                        fs = new DirectoryInfo(d.url);
                    }
                    else
                    {
                        fs = new FileInfo(d.url);
                    }
                    if (fs.Exists)
                    {
                        ConjuntoDeSeries series = new ConjuntoDeSeries(this.prs, this.cf);

                        ContextoDeConjuntoDeSeries contextoDeConjunto = new ContextoDeConjuntoDeSeries();
                        ContextoDeSerie ctxT = new ContextoDeSerie();
                        ctxT.Url = d.url;
                        ctxT.F = fs;
                        ctxT.Parent = fs is DirectoryInfo ? ((DirectoryInfo)fs).Parent : ((FileInfo)fs).Directory;
                        ctxT.EsArchivo = td == TipoDeDestino.TXT;
                        ctxT.EsCarpeta = td == TipoDeDestino.CARPETA;
                        ctxT.EsSoloNombre = false;
                        ctxT.EsVideo = false;
                        DatosDePosicionDeRecorridoDeSeries dps = new DatosDePosicionDeRecorridoDeSeries(
                                                                     contexto: ctxT

                                                                 );
                        RecorredorDeTXT recoTXT = null;
                        if (td == TipoDeDestino.TXT)
                        {

                            recoTXT = new RecorredorDeTXT(
                                contextoDeConjunto: contextoDeConjunto
                                , procesador: this.pro
                                , dpr: dps
                                , series: series);
                            recoTXT.recorrer();
                        }





                        TipoDeCategoriaPropias categoria = TipoDeCategoriaPropias.get(d.categoria);

                        if (or(categoria
                              , TipoDeCategoriaPropias.FINALIZADAS
                             , TipoDeCategoriaPropias.EN_ESPERA
                            , TipoDeCategoriaPropias.POR_ENTRAR))
                        {
                            if (td == TipoDeDestino.CARPETA)
                            {
                                RecorredorConjuntoDeSeries recoConjunto = new RecorredorConjuntoDeSeries(
                                                                              contextoDeConjunto: contextoDeConjunto
                                , procesador: this.pro
                                , dpr: dps
                                , series: series);
                                recoConjunto.usarCarpeta = this.usarCarpeta;
                                recoConjunto.recorrer();
                            }
                        }
                        else if (categoria == TipoDeCategoriaPropias.SEGUIDAS)
                        {
                            if (td == TipoDeDestino.CARPETA)
                            {
                                RecorredorDeDirectorioCapitulosSueltos recoSueltos = new RecorredorDeDirectorioCapitulosSueltos(
                                                                                         contextoDeConjunto: contextoDeConjunto
                                , procesador: this.pro
                                , dpr: dps
                                , series: series);
                                recoSueltos.usarCarpeta = this.usarCarpeta;
                                recoSueltos.recorrer();
                            }
                        }
                        //UtilesMostrarEnConsola.mostrarConjuntoDeSeries(series);

                        getSeriesPropias(categoria).unirCon(series);
                    }





                }


            }


            foreach (ConjuntoDeSeries cn in seriesPropias.Values)
            {
                todasLasSeries.unirCon(cn.getCopia());
            }
        }

        /// <summary>
        /// De momento "nombre" no es una url, solo el nombre del capitulo
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public ProcesadorDeNombreDeSerie getProcesadorDeSerie(ContextoDeConjuntoDeSeries contextoDeConjunto
                                , ContextoDeSerie contexto,string nombre) {
           return this.pro.getProcesadorDeSerie(contextoDeConjunto, contexto, nombre);//.getDatosNumericosDeNombre();
        }
    }
}


//public void actualizar(List<DireccionDeActualizadorPropia> urls)
//		{
//			resetear();
//
//
//
//
//			foreach (DireccionDeActualizadorPropia d in urls) {
//				if (d.seleccioniada) {
//					TipoDeDestino td = ManagerRelacionadorDeSerie.TipoDeDestino.getTipoDeDestino_url(d.url);
//					FileSystemInfo fs = td == TipoDeDestino.CARPETA ? new DirectoryInfo(d.url) : new FileInfo(d.url);
//
//
//					ConjuntoDeSeries series = new ConjuntoDeSeries(this.prs, this.cf);
//
//					ContextoDeConjuntoDeSeries contextoDeConjunto = new ContextoDeConjuntoDeSeries();
//					ContextoDeSerie ctxT = new ContextoDeSerie();
//					ctxT.Url = d.url;
//					ctxT.F = fs;
//					ctxT.Parent = fs is DirectoryInfo ? ((DirectoryInfo)fs).Parent : ((FileInfo)fs).Directory;
//					ctxT.EsArchivo = td == TipoDeDestino.TXT;
//					ctxT.EsCarpeta = td == TipoDeDestino.CARPETA;
//					ctxT.EsSoloNombre = false;
//					ctxT.EsVideo = false;
//					DatosDePosicionDeRecorridoDeSeries dps = new DatosDePosicionDeRecorridoDeSeries(
//						                                         contexto: ctxT
//
//					                                         );
//					RecorredorDeTXT recoTXT = null;
//					if (td == TipoDeDestino.TXT) {
//
//						recoTXT = new RecorredorDeTXT(
//							contextoDeConjunto: contextoDeConjunto
//							, procesador: this.pro
//							, dpr: dps
//							, series: series);
//						recoTXT.recorrer();
//					}
//
//
//
//
//
//					const TipoDeCategoriaPropias categoria = d.categoria;
//					switch (categoria) {
//						case TipoDeCategoriaPropias.FINALIZADAS:
//						case TipoDeCategoriaPropias.EN_ESPERA:
//						case TipoDeCategoriaPropias.POR_ENTRAR:
//							if (td == TipoDeDestino.CARPETA) {
//								RecorredorConjuntoDeSeries recoConjunto = new RecorredorConjuntoDeSeries(
//									                                          contextoDeConjunto: contextoDeConjunto
//							, procesador: this.pro
//							, dpr: dps
//							, series: series);
//								recoConjunto.recorrer();
//							}
//							break;
//						case TipoDeCategoriaPropias.SEGUIDAS:
//							if (td == TipoDeDestino.CARPETA) {
//								RecorredorDeDirectorioCapitulosSueltos recoSueltos = new RecorredorDeDirectorioCapitulosSueltos(
//									                                                   contextoDeConjunto: contextoDeConjunto
//							, procesador: this.pro
//							, dpr: dps
//							, series: series);
//								recoSueltos.recorrer();
//							}
//							break;
//					}
//					getSeriesPropias(categoria).unirCon(series);
//
//
//				}
//
//
//			}
//
//
//			foreach (ConjuntoDeSeries cn in seriesPropias.Values) {
//				todasLasSeries.unirCon(cn.getCopia());
//			}
//		}
//
