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
    public class ManagerDePaquetes
    {
        //private List<Paquete> paquetes;
        private Paquete paquete;
        public SeccionSeriesPaquete animes;
        public SeccionSeriesPaquete seriesPersona;



        //private ConfiguracionDeSeries cnf_persona;
        //private ConfiguracionDeSeries cnf_anime;
        //private ProcesadorDeRelacionesDeNombresClaveSeries prs;



        public ManagerDePaquetes(
            ManagerDeSeries animes,
            ManagerDeSeries seriesPersona
            ) {
            this.paquete = new Paquete(
                carpeta: null
                , proR: animes.prs
                , cf_series_anime: animes.cf
                , cf_series_persona: seriesPersona.cf
                );
            //this.paquetes = new List<Paquete>();
            this.animes = new SeccionSeriesPaquete(animes);//this.paquete, 
            this.seriesPersona = new SeccionSeriesPaquete(seriesPersona);//this.paquete,
            //this.seriesPersona = seriesPersona;
        }

        public Paquete cargarPaquete(DirectoryInfo carpeta) {
            Paquete p = new Paquete(
            carpeta: carpeta //new DirectoryInfo(@"C:\_COSAS\Para pruebas Actualize\info de paquetes\[[01-08-2022]]")
            , proR: animes.mngSeries.prs
            , cf_series_anime: animes.mngSeries.cf
            , cf_series_persona: seriesPersona.mngSeries.cf
            );

            AnalizadorDelPaquete an = new AnalizadorDelPaquete(p, animes.mngSeries.cf.re.reg);
            an.buscarUrls();



            this.paquete = p;

            this.animes.cargar(p);
            this.seriesPersona.cargar(p);

            return p;
        }
    }
}
