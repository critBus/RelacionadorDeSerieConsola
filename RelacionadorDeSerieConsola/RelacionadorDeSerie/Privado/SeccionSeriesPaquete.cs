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

namespace RelacionadorDeSerie.Privado
{
    public class Url_Tipo
    {
        public string url { get; set; }
        public bool esCarpeta { get; set; }
    }
    public class SeccionSeriesPaquete
    {
        //public Paquete paquete;
        public ManagerDeSeries mngSeries;

        public Dictionary<ConjuntoDeEtiquetasDeSerie, SeriesEnSeccionDelPaquete> seriesEnEstaSeccion_tx;
        public Dictionary<ConjuntoDeEtiquetasDeSerie, SeriesEnSeccionDelPaquete> seriesEnEstaSeccion_finalizadas;
        public SeriesEnSeccionDelPaquete todasLasSeries;
        public SeriesEnSeccionDelPaquete todasLasSeries_tx;
        public SeriesEnSeccionDelPaquete todasLasSeries_finalizadas;

        public SeccionSeriesPaquete(
            //Paquete paquete,
            ManagerDeSeries mngSeries)
        {
            //this.paquete = paquete;
            this.mngSeries = mngSeries;

            this.seriesEnEstaSeccion_finalizadas = ConjuntoDeEtiquetasDeSerie.getNewDictionary<SeriesEnSeccionDelPaquete>();
            this.seriesEnEstaSeccion_tx = ConjuntoDeEtiquetasDeSerie.getNewDictionary<SeriesEnSeccionDelPaquete>(); //new Dictionary<ConjuntoDeEtiquetasDeSerie, SeriesEnSeccionDelPaquete>();
        }

        public void actualizar_Convinaciones(ConjuntoDeEtiquetasDeSerie[] etiquetasAActualizar, params TipoDeCategoriaPropias[] categorias)
        {
            IEnumerable<ConjuntoDeEtiquetasDeSerie> etiquetasARecorrer = etiquetasAActualizar;
            if (etiquetasAActualizar == null || etiquetasAActualizar.Length==0 ) {
                HashSet<ConjuntoDeEtiquetasDeSerie> cnar = ConjuntoDeEtiquetasDeSerie.getNewHashSet();
                foreach (ConjuntoDeEtiquetasDeSerie keycn in this.seriesEnEstaSeccion_tx.Keys)
                {
                    cnar.Add(keycn);
                }
                foreach (ConjuntoDeEtiquetasDeSerie keycn in this.seriesEnEstaSeccion_finalizadas.Keys)
                {
                    cnar.Add(keycn);
                }
                etiquetasARecorrer = cnar; //seriesEnEstaSeccion.Keys;//from k in seriesEnEstaSeccion select k.Key;

            }

            foreach (ConjuntoDeEtiquetasDeSerie tags in etiquetasARecorrer)
            {
                if (seriesEnEstaSeccion_tx.ContainsKey(tags)) {
                    seriesEnEstaSeccion_tx[tags].actualizar_Convinaciones(categorias);
                } else if(seriesEnEstaSeccion_finalizadas.ContainsKey(tags)) {
                    seriesEnEstaSeccion_finalizadas[tags].actualizar_Convinaciones(categorias);
                }
                //seriesEnEstaSeccion[tags].actualizar_Convinaciones(categorias);
            }
            actualizarTodasLasSeries();
        }

        public void cargar(Paquete paquete)
        {
            SeriesDelPaquete series = this.mngSeries.seccion == TipoDeSeccion.ANIME ? paquete.seriesMangas : paquete.seriesPersona;
            foreach (KeyValuePair<ConjuntoDeEtiquetasDeSerie, HashSet<DirectorioDeSeriesDelPaquete>> k in series.directoriosDeSeries)
            {
                k.Value.RemoveWhere(v => !v.carpeta.Exists);
                if (k.Value.Count()>0) {
                    actualizar_SinTodas(k.Key, k.Value);
                }
                
            }
            actualizarTodasLasSeries();
        }
        private void actualizarTodasLasSeries() {
            ConjuntoDeSeries series_todas = this.mngSeries.getNewConjuntoDeSeries();
            ConjuntoDeSeries series_todas_tx = this.mngSeries.getNewConjuntoDeSeries();
            ConjuntoDeSeries series_todas_finalizadas = this.mngSeries.getNewConjuntoDeSeries();

            HashSet<DirectorioDeSeriesDelPaquete> ld_tx = DirectorioDeSeriesDelPaquete.getNewHashSet();
            foreach (KeyValuePair<ConjuntoDeEtiquetasDeSerie, SeriesEnSeccionDelPaquete> item in seriesEnEstaSeccion_tx)
            {
                series_todas_tx.unirCon(item.Value.seriesEnEstosDirectorios);
                foreach (var d in item.Value.directorios)
                {
                    ld_tx.Add(d);
                }
                
            }

            HashSet<DirectorioDeSeriesDelPaquete> ld_finalizadas = DirectorioDeSeriesDelPaquete.getNewHashSet();
            foreach (KeyValuePair<ConjuntoDeEtiquetasDeSerie, SeriesEnSeccionDelPaquete> item in seriesEnEstaSeccion_finalizadas)
            {
                series_todas_finalizadas.unirCon(item.Value.seriesEnEstosDirectorios);
                foreach (var d in item.Value.directorios)
                {
                    ld_finalizadas.Add(d);
                }

            }

            HashSet<DirectorioDeSeriesDelPaquete> ld_todas= DirectorioDeSeriesDelPaquete.getNewHashSet(ld_tx);
            foreach (DirectorioDeSeriesDelPaquete d in ld_finalizadas)
            {
                ld_todas.Add(d);
            }


            series_todas.unirCon(series_todas_tx.getCopia());
            series_todas.unirCon(series_todas_finalizadas.getCopia());

            todasLasSeries = new SeriesEnSeccionDelPaquete(
                // paquete:this.paquete,
                mngSeries: this.mngSeries
                , etiquetas: new ConjuntoDeEtiquetasDeSerie()
                , directorios: ld_todas
                , seriesEnEstosDirectorios: series_todas
                );

            todasLasSeries.actualizar();


            todasLasSeries_tx = new SeriesEnSeccionDelPaquete(
                // paquete:this.paquete,
                mngSeries: this.mngSeries
                , etiquetas: new ConjuntoDeEtiquetasDeSerie()
                , directorios: ld_tx
                , seriesEnEstosDirectorios: series_todas_tx
                );

            todasLasSeries_tx.actualizar();


            todasLasSeries_finalizadas = new SeriesEnSeccionDelPaquete(
                // paquete:this.paquete,
                mngSeries: this.mngSeries
                , etiquetas: new ConjuntoDeEtiquetasDeSerie()
                , directorios: ld_finalizadas
                , seriesEnEstosDirectorios: series_todas_finalizadas
                );

            todasLasSeries_finalizadas.actualizar();
        }

        private void actualizar_SinTodas(ConjuntoDeEtiquetasDeSerie etiquetas, HashSet<DirectorioDeSeriesDelPaquete> direcciones) {
            if (direcciones.Count() == 0)
            {
                if (seriesEnEstaSeccion_finalizadas.ContainsKey(etiquetas))
                {
                    seriesEnEstaSeccion_finalizadas.Remove(etiquetas);

                }else if (seriesEnEstaSeccion_tx.ContainsKey(etiquetas))
                {
                    seriesEnEstaSeccion_tx.Remove(etiquetas);

                }
            }
            else {
                ConjuntoDeSeries series = this.mngSeries.getNewConjuntoDeSeries();
                foreach (DirectorioDeSeriesDelPaquete d in direcciones)
                {
                    d.series = UtilesSeriesR.buscarSeriesDe(
                        pro: this.mngSeries.pro
                        , proR: this.mngSeries.prs
                        , cf: this.mngSeries.cf
                        , carpeta: d.carpeta
                        , esCarpetaDeCapitulosSueltos: !d.etiquetas.esFinalizadas()
                        , usarCarpeta: mngSeries.usarCarpeta
                        );


                    series.unirCon(d.series);

                }
                SeriesEnSeccionDelPaquete seriesEnSeccion = new SeriesEnSeccionDelPaquete(
                    // paquete:this.paquete,
                    mngSeries: this.mngSeries
                    , etiquetas: etiquetas
                    , directorios: direcciones
                    , seriesEnEstosDirectorios: series
                    );

                seriesEnSeccion.actualizar();



                if (seriesEnEstaSeccion_finalizadas.ContainsKey(etiquetas))
                {
                    seriesEnEstaSeccion_finalizadas[etiquetas] = seriesEnSeccion;

                }else if (seriesEnEstaSeccion_tx.ContainsKey(etiquetas))
                {
                    seriesEnEstaSeccion_tx[etiquetas] = seriesEnSeccion;

                }
                else 
                {
                    if (etiquetas.esFinalizadas())
                    {
                        seriesEnEstaSeccion_finalizadas.Add(etiquetas, seriesEnSeccion);

                    }
                    else {
                        seriesEnEstaSeccion_tx.Add(etiquetas, seriesEnSeccion);
                    }
                    
                }
            }
            
        }


        private void actualizar(ConjuntoDeEtiquetasDeSerie etiquetas, HashSet<DirectorioDeSeriesDelPaquete> direcciones)
        {
            actualizar_SinTodas(etiquetas, direcciones);
            actualizarTodasLasSeries();
        }





        private FileSystemInfo getFileSystemCorrecto(Url_Tipo tf)
        {
            if (tf.esCarpeta)
            {
                return new DirectoryInfo(tf.url);
            }
            return new FileInfo(tf.url);
        }
        public void actualizar(IEnumerable<Url_Tipo> lurls, IEnumerable<TipoDeEtiquetaDeSerie> tags)
        {
            // Func<Url_Tipo, FileSystemInfo> 
            ConjuntoDeEtiquetasDeSerie etiquetas = new ConjuntoDeEtiquetasDeSerie(ComparadorTipoDeEtiquetaDeSerie.getNewSortedSet_TipoDeEtiquetaDeSerie(tags));
            //HashSet<DirectorioDeSeriesDelPaquete> direcciones
            //    = ComparadorDirectorioDeSeriesDelPaquete
            //    .getNewHashSet_DirectorioDeSeriesDelPaquete(from u in lurls
            //                                                where getFileSystemCorrecto(u).Exists
            //                                                select new DirectorioDeSeriesDelPaquete(
            //                                                    carpeta: getFileSystemCorrecto(u)//Archivos.getFileSystemCorrecto(u)
            //                                                    , etiquetas: etiquetas
            //                                                    , series: this.mngSeries.getNewConjuntoDeSeries()
            //                                                    ));
            actualizar(etiquetas, parse(lurls,etiquetas));

            
        }

        private HashSet<DirectorioDeSeriesDelPaquete> parse(IEnumerable<Url_Tipo> lurls, ConjuntoDeEtiquetasDeSerie etiquetas) {
            HashSet<DirectorioDeSeriesDelPaquete> direcciones
                = ComparadorDirectorioDeSeriesDelPaquete
                .getNewHashSet_DirectorioDeSeriesDelPaquete(from u in lurls
                                                            where getFileSystemCorrecto(u).Exists
                                                            select new DirectorioDeSeriesDelPaquete(
                                                                carpeta: getFileSystemCorrecto(u)//Archivos.getFileSystemCorrecto(u)
                                                                , etiquetas: etiquetas
                                                                , series: this.mngSeries.getNewConjuntoDeSeries()
                                                                ));
            return direcciones;
        }

        public void actualizar(Dictionary<ConjuntoDeEtiquetasDeSerie, List<Url_Tipo>> direcciones)
        {



            foreach (KeyValuePair<ConjuntoDeEtiquetasDeSerie, List<Url_Tipo>> k in direcciones)
            {
                actualizar_SinTodas(k.Key, parse(k.Value, k.Key));
            }
            actualizarTodasLasSeries();



        }

        public void actualizar(IEnumerable<Url_Tipo> lurls, params TipoDeEtiquetaDeSerie[] tags)
        {
            actualizar(lurls,tags.ToList());
            // Func<Url_Tipo, FileSystemInfo> 
            //ConjuntoDeEtiquetasDeSerie etiquetas = new ConjuntoDeEtiquetasDeSerie(ComparadorTipoDeEtiquetaDeSerie.getNewSortedSet_TipoDeEtiquetaDeSerie(tags));
            //HashSet<DirectorioDeSeriesDelPaquete> direcciones
            //    = ComparadorDirectorioDeSeriesDelPaquete
            //    .getNewHashSet_DirectorioDeSeriesDelPaquete(from u in lurls
            //                                                select new DirectorioDeSeriesDelPaquete(
            //                                                    carpeta: getFileSystemCorrecto(u)//Archivos.getFileSystemCorrecto(u)
            //                                                    , etiquetas: etiquetas
            //                                                    , series: this.mngSeries.getNewConjuntoDeSeries()
            //                                                    ));
            //actualizar(etiquetas, direcciones);
        }

        private bool remove(Dictionary<ConjuntoDeEtiquetasDeSerie, SeriesEnSeccionDelPaquete> seriesEnEstaSeccion, ConjuntoDeEtiquetasDeSerie etiquetas, string carpeta) {
            if (seriesEnEstaSeccion.ContainsKey(etiquetas))
            {
                SeriesEnSeccionDelPaquete seccion = seriesEnEstaSeccion[etiquetas];
                foreach (DirectorioDeSeriesDelPaquete d in seccion.directorios)
                {
                    if (d.ToString() == carpeta)
                    {
                        seccion.directorios.Remove(d);
                        break;
                    }
                }
                ConjuntoDeSeries series = this.mngSeries.getNewConjuntoDeSeries();
                foreach (DirectorioDeSeriesDelPaquete d in seccion.directorios)
                {
                    series.unirCon(d.series);
                }
                seccion.seriesEnEstosDirectorios = series;
                seccion.actualizar();
                return true;
            }
            return false;
        }

        public void remove(ConjuntoDeEtiquetasDeSerie etiquetas, string carpeta)
        {
            if (remove(this.seriesEnEstaSeccion_tx,etiquetas,carpeta)
                || remove(this.seriesEnEstaSeccion_finalizadas, etiquetas, carpeta)) {
                actualizarTodasLasSeries();
            }
        }


        public bool add(Dictionary<ConjuntoDeEtiquetasDeSerie, SeriesEnSeccionDelPaquete> seriesEnEstaSeccion,
            ConjuntoDeEtiquetasDeSerie etiquetas, DirectorioDeSeriesDelPaquete d) {
            if (seriesEnEstaSeccion.ContainsKey(etiquetas))
            {
                d.series = UtilesSeriesR.buscarSeriesDe(
                    pro: this.mngSeries.pro
                    , proR: this.mngSeries.prs
                    , cf: this.mngSeries.cf
                    , carpeta: d.carpeta
                    , esCarpetaDeCapitulosSueltos: !d.etiquetas.esFinalizadas()
                    , usarCarpeta: mngSeries.usarCarpeta
                    );
                SeriesEnSeccionDelPaquete seccion = seriesEnEstaSeccion[etiquetas];
                seccion.directorios.Add(d);
                seccion.seriesEnEstosDirectorios.unirCon(d.series);
                seccion.actualizar();
                return true;
            }
            return false;
        }
        public void add(ConjuntoDeEtiquetasDeSerie etiquetas, FileSystemInfo carpeta)
        {
            DirectorioDeSeriesDelPaquete d = new DirectorioDeSeriesDelPaquete(carpeta, etiquetas, this.mngSeries.getNewConjuntoDeSeries());
            if (!(add(this.seriesEnEstaSeccion_finalizadas,etiquetas,d)
                || add(this.seriesEnEstaSeccion_tx, etiquetas, d)) )
            {
                actualizar_SinTodas(etiquetas, ComparadorDirectorioDeSeriesDelPaquete.getNewHashSet_DirectorioDeSeriesDelPaquete(new DirectorioDeSeriesDelPaquete[] { d }));
            }

            actualizarTodasLasSeries();

        }

        public void set(Dictionary<ConjuntoDeEtiquetasDeSerie, IEnumerable<FileSystemInfo>> direcciones)
        {
            //this.seriesEnEstaSeccion = ConjuntoDeEtiquetasDeSerie.getNewDictionary<SeriesEnSeccionDelPaquete>();//new Dictionary<ConjuntoDeEtiquetasDeSerie, SeriesEnSeccionDelPaquete>();
            this.seriesEnEstaSeccion_finalizadas = ConjuntoDeEtiquetasDeSerie.getNewDictionary<SeriesEnSeccionDelPaquete>();
            this.seriesEnEstaSeccion_tx = ConjuntoDeEtiquetasDeSerie.getNewDictionary<SeriesEnSeccionDelPaquete>();
            foreach (KeyValuePair<ConjuntoDeEtiquetasDeSerie, IEnumerable<FileSystemInfo>> k in direcciones)
            {
                
                HashSet<DirectorioDeSeriesDelPaquete> directoriosPaquete = ComparadorDirectorioDeSeriesDelPaquete.getNewHashSet_DirectorioDeSeriesDelPaquete();
                foreach (FileSystemInfo d in k.Value)
                {
                    if (d.Exists) {
                        directoriosPaquete.Add(new DirectorioDeSeriesDelPaquete(d, k.Key, this.mngSeries.getNewConjuntoDeSeries()));
                    }
                    
                }
                actualizar_SinTodas(k.Key, directoriosPaquete);
            }
            actualizarTodasLasSeries();
        }
        public void set(Dictionary<ConjuntoDeEtiquetasDeSerie, List<Url_Tipo>> direcciones) {
            Dictionary<ConjuntoDeEtiquetasDeSerie, IEnumerable<FileSystemInfo>> dic = ConjuntoDeEtiquetasDeSerie.getNewDictionary<IEnumerable<FileSystemInfo>>();
            foreach (KeyValuePair<ConjuntoDeEtiquetasDeSerie, List<Url_Tipo>> k in direcciones)
            {
                dic.Add(k.Key,(from f in k.Value where getFileSystemCorrecto(f).Exists select getFileSystemCorrecto(f) ));
            }
            set(dic);
        }


    }
}
//private void actualizarSoloConjunto(ConjuntoDeEtiquetasDeSerie etiquetas) {}



//public void actualizar(IEnumerable<string> lurls,params TipoDeEtiquetaDeSerie[] tags) {
//    ConjuntoDeEtiquetasDeSerie etiquetas = new ConjuntoDeEtiquetasDeSerie(ComparadorTipoDeEtiquetaDeSerie.getNewSortedSet_TipoDeEtiquetaDeSerie(tags) );
//    HashSet<DirectorioDeSeriesDelPaquete> direcciones
//        = ComparadorDirectorioDeSeriesDelPaquete
//        .getNewHashSet_DirectorioDeSeriesDelPaquete(from u in lurls
//                                                    select new DirectorioDeSeriesDelPaquete(
//                                                        carpeta: Archivos.getFileSystemCorrecto(u)
//                                                        , etiquetas: etiquetas
//                                                        , series: this.mngSeries.getNewConjuntoDeSeries()
//                                                        ));
//    actualizar(etiquetas,direcciones);
//}
