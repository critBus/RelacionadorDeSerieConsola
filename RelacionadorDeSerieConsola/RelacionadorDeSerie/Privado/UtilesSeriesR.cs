using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




using ReneUtiles;
using System.Collections.Generic;
using ReneUtiles.Clases;
//using System.IO;
using Delimon.Win32.IO;
using RelacionadorDeSerie.BD.Modelos;
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
using RelacionadorDeSerie.Representaciones;
using ReneUtiles.Clases.BD;
using ReneUtiles.Clases.BD.SesionEstorage;

using RelacionadorDeSerie.Privado.Consolas;

using ReneUtiles.Clases.Multimedia.Paquetes.Representaciones;
using ReneUtiles.Clases.Multimedia.Paquetes;
using ReneUtiles.Clases.Videos;

namespace RelacionadorDeSerie.Privado
{
    public abstract class UtilesSeriesR : ConsolaBasica
    {
        private static int idDeSerieAnonimaActual = 0;
        private static int idDeSerieActual = 0;
        private static int idDeTemporadaActual = 0;
        private static int idDeCapituloActual = 0;
        public static List<Capitulo_R> getCapitulosR(RepresentacionDeCapitulo rc, Serie_R serieAlQuePertenecen = null)
        {
            HashSet<string> ls = new HashSet<string>();
            foreach (KeySerie k in rc.temporada.serie.getKeysDeSerie())
            {
                ls.Add(k.Nombre);
            }

            string idDeSerie = "";//.
            if (serieAlQuePertenecen != null)
            {
                idDeSerie = serieAlQuePertenecen.id;
            }
            else
            {
                Serie sDeEsta = rc.temporada.serie;

                if (sDeEsta.tieneFuentes())
                {
                    HashSet<DatosDeFuente> cfuentes = sDeEsta.Fuentes.getDatosDefuentes();
                    HashSet<DatosDeFuente>.Enumerator en = cfuentes.GetEnumerator();
                    en.MoveNext();
                    idDeSerie = en.Current.Ctx.Url;
                }
                else
                {
                    if (ls.Count > 0)
                    {
                        foreach (string ns in ls)
                        {
                            idDeSerie += ns + "&";
                        }
                    }
                    else
                    {
                        idDeSerie = (UtilesSeriesR.idDeSerieAnonimaActual++) + "";
                    }

                }
            }

            List<Capitulo_R> lr = new List<Capitulo_R>();
            foreach (DatosDeFuente df in rc.Fuentes.getDatosDefuentes())
            {
                Capitulo_R c = new Capitulo_R();
                if (rc is CapituloDeSerie)
                {
                    CapituloDeSerie cs = (CapituloDeSerie)rc;
                    c.capitulo = cs.NumeroDeCapitulo.ToString();
                    c.capituloInicial = cs.NumeroDeCapitulo;
                }
                else
                {
                    CapituloDeSerieMultiples cs = (CapituloDeSerieMultiples)rc;
                    c.capitulo = cs.NumeroCapituloInicial + "-" + cs.NumeroCapituloFinal;
                    c.capituloInicial = cs.NumeroCapituloInicial;
                    c.capituloFinal = cs.NumeroCapituloFinal;
                }
                c.esUnExtra = rc is CapituloDeSerieMultiplesOva || rc is CapituloDeSerieOva;

                c.temporada = rc.temporada.NumeroTemporada;

                c.nombres_de_serie = new List<string>(ls);
                c.idDeSerie = idDeSerie;

                c.url = df.Ctx.Url;

                string url = null;

                if (df.Ldns.Count > 0)
                {

                    c.etiquetas = new List<string>();
                    foreach (DatosDeNombreSerie dn in df.Ldns)
                    {
                        if (dn.etiquetasEnNombre != null && dn.etiquetasEnNombre.Keys != null)
                        {
                            foreach (TipoDeEtiquetaDeSerie te in dn.etiquetasEnNombre.Keys)
                            {
                                c.etiquetas.Add(te.ToString());
                            }
                        }

                    }
                    foreach (DatosDeNombreSerie dn in df.Ldns)
                    {
                        if (dn.fechasEnNombre.Count > 0)
                        {
                            c.fecha = dn.fechasEnNombre[0].fechaStr;
                            break;
                        }
                    }



                    if (df.Ctx.EsVideo)
                    {


                        //FileInfo f = null;
                        if (df.Ctx.F != null)
                        {
                            //c.listaDeVideos.Add(new FileInfo(df.Ctx.F.ToString()));
                            //f = new FileInfo(df.Ctx.F.ToString());
                            // url = f.ToString();
                            url = df.Ctx.F.ToString();
                        }
                        else if (df.Ctx.Url != null)
                        {
                            url = df.Ctx.Url;

                        }



                    }//fin if si es video


                }//fin if si df.Ldns.Count > 0

                if (df.Ctx.Url != null && df.Ctx.Url.Trim().Length > 0)
                {
                    c.id = df.Ctx.Url;
                }
                else
                {
                    c.id = (UtilesSeriesR.idDeCapituloActual++).ToString();
                }

                foreach (DatosDeNombreSerie dn in df.Ldns)
                {
                    if (dn.NombreOriginal.Trim().Length > 0)
                    {
                        c.nombre_capitulo = dn.NombreOriginal;
                        break;
                    }
                }
                if (df is DatosDeArchivoFisico)
                {
                    DatosDeArchivoFisico da = (DatosDeArchivoFisico)df;

                    c.tieneSubtituloEnSuCarpeta = da.datosVideosConSubtitulos != null && da.datosVideosConSubtitulos.tieneSubtitulos;

                    if (da.datosVideosConSubtitulos != null && (url == null || url.Length == 0)
                        && da.datosVideosConSubtitulos.videos.Count() > 0
                        )
                    {
                        url = da.datosVideosConSubtitulos.videos[0].ToString();
                    }
                    if (da.datosVideosConSubtitulos!=null) {
                        c.listaDeVideos.AddRange(da.datosVideosConSubtitulos.videos);
                        c.listaDeSubtitulos.AddRange(da.datosVideosConSubtitulos.subtitulos);
                    }
                    

                    if (da.Ctx.EsVideo && c.listaDeVideos.Count() == 0 && da.Ctx.F != null)
                    {
                        c.listaDeVideos.Add(new FileInfo(df.Ctx.F.ToString()));
                    }
                    if (da.Ctx.F != null) {
                        if (da.Ctx.EsCarpeta)
                        {
                            c.size = Archivos.getEspacioQueOcupa(new DirectoryInfo(da.Ctx.F.ToString()));
                        } else if (da.Ctx.EsVideo) {
                            if (c.listaDeVideos.Count>0) {
                                c.size = new FileInfo(df.Ctx.F.ToString()).Length;
                            }
                        }
                    }
                    
                }
                else
                {
                    c.tieneSubtituloEnSuCarpeta = false;
                }


                if (url != null && url.Trim().Length > 0)
                {
                    string extencion = Archivos.getExtencion(url);
                    TipoDeVideo tex = TipoDeVideo.get(extencion);
                    if (tex != null)
                    {
                        c.formato = subs(tex.ToString(), 1).ToUpper();
                    }
                }


                //DatosDeNombreSerie dn=df.Ldns[0];


                //}
                //salio de df.Ldns.Count > 0
                lr.Add(c);
            }
            //if (lr.Count>0) {
            //    cwl("vamos a ver");
            //}
            return lr;
        }
        public static List<Capitulo_R> getCapitulosR(TemporadaDeSerie t, Serie_R serieAlQuePertenecen = null)
        {
            List<Capitulo_R> lc = new List<Capitulo_R>();
            foreach (RepresentacionDeCapitulo rc in t.Capitulos)
            {
                lc.AddRange(getCapitulosR(rc, serieAlQuePertenecen));
            }
            return lc;
        }
        public static List<Capitulo_R> getCapitulosR(List<RepresentacionDeCapitulo> lr)
        {
            List<Capitulo_R> lc = new List<Capitulo_R>();
            foreach (RepresentacionDeCapitulo rc in lr)
            {
                lc.AddRange(getCapitulosR(rc));
            }
            return lc;
        }
        public static List<Capitulo_R> getCapitulosR(ConjuntoDeSeries cn)
        {
            List<Capitulo_R> lc = new List<Capitulo_R>();
            foreach (Serie s in cn.series)
            {
                Serie_R sr = getNewSerieEmpty(s);
                foreach (TemporadaDeSerie t in s.Temporadas)
                {
                    lc.AddRange(getCapitulosR(t, sr));
                }
            }
            return lc;
        }

        private static Serie_R getNewSerieEmpty(Serie s)
        {
            Serie_R sr = new Serie_R();

            HashSet<string> lns = new HashSet<string>();
            foreach (KeySerie k in s.getKeysDeSerie())
            {
                lns.Add(k.Nombre);
            }
            sr.nombres_de_serie = new List<string>(lns);
            sr.id = (UtilesSeriesR.idDeSerieActual++).ToString();
            sr.temporadas = new List<Temporada_R>();

            return sr;
        }

        public static List<Serie_R> getSeriesR(ConjuntoDeSeries cn)
        {
            List<Serie_R> lsr = new List<Serie_R>();
            foreach (Serie s in cn.series)
            {
                Serie_R sr = getNewSerieEmpty(s);


                foreach (TemporadaDeSerie t in s.Temporadas)
                {
                    Temporada_R tr = new Temporada_R();
                    tr.cantidadDeCapitulos_distintos = t.setNumerosDeCapitulos.Count + t.setNumerosDeCapitulosOva.Count;
                    tr.capitulos = UtilesSeriesR.getCapitulosR(t, sr);
                    tr.cantidadDeCapitulos = tr.capitulos.Count;
                    tr.id = (UtilesSeriesR.idDeTemporadaActual++).ToString();
                    sr.temporadas.Add(tr);
                    //lc.AddRange(getCapitulosR(t));
                }
                lsr.Add(sr);
            }
            return lsr;
        }


        public static ConjuntoDeSeries buscarSeriesDe(
            ProcesadorDeSeries pro
            , ProcesadorDeRelacionesDeNombresClaveSeries proR
            , ConfiguracionDeSeries cf
            , FileSystemInfo carpeta
            , bool esCarpetaDeCapitulosSueltos
            , Predicate<DirectoryInfo> usarCarpeta)
        {

            FileSystemInfo fs = carpeta;
            bool esCarpeta = fs is DirectoryInfo;

            ConjuntoDeSeries series = new ConjuntoDeSeries(proR, cf);

            ContextoDeConjuntoDeSeries contextoDeConjunto = new ContextoDeConjuntoDeSeries();
            ContextoDeSerie ctxT = new ContextoDeSerie();
            ctxT.Url = carpeta.ToString();
            ctxT.F = fs;
            ctxT.Parent = esCarpeta ? ((DirectoryInfo)fs).Parent : ((FileInfo)fs).Directory;
            ctxT.EsArchivo = !esCarpeta;
            ctxT.EsCarpeta = esCarpeta;
            ctxT.EsSoloNombre = false;
            ctxT.EsVideo = false;
            DatosDePosicionDeRecorridoDeSeries dps = new DatosDePosicionDeRecorridoDeSeries(
                                                         contexto: ctxT

                                                     );
            if (carpeta is DirectoryInfo)
            {
                if (esCarpetaDeCapitulosSueltos)
                {
                    RecorredorConjuntoDeSeries recoConjunto = new RecorredorConjuntoDeSeries(
                                                                              contextoDeConjunto: contextoDeConjunto
                                , procesador: pro
                                , dpr: dps
                                , series: series);
                    recoConjunto.usarCarpeta = usarCarpeta;
                    recoConjunto.recorrer();
                }
                else
                {
                    RecorredorDeDirectorioCapitulosSueltos recoSueltos = new RecorredorDeDirectorioCapitulosSueltos(
                                                                                         contextoDeConjunto: contextoDeConjunto
                                , procesador: pro
                                , dpr: dps
                                , series: series);
                    recoSueltos.usarCarpeta = usarCarpeta;
                    recoSueltos.recorrer();
                }
            }
            else
            {
                RecorredorDeTXT recoTXT = new RecorredorDeTXT(
                        contextoDeConjunto: contextoDeConjunto
                        , procesador: pro
                        , dpr: dps
                        , series: series);
                recoTXT.recorrer();

            }


            return series;
        }
    }
}
