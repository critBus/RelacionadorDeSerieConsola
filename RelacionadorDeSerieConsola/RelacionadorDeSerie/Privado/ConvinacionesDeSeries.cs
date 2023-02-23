using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ReneUtiles;
using System.Collections.Generic;
using ReneUtiles.Clases;
using System.IO;
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
    public class ConvinacionesDeSeries
    {
        public ConjuntoDeSeries seriesCoincidentes;

        //public Dictionary<TipoDeSeccion, ConjuntoDeSeries> seriesCoincidentes;
        public ConjuntoDeSeries seriesExtrenos;

        public ConjuntoDeSeries seriesTodas;


        public ManagerDeSeries mngSerie;

       
        public ConvinacionesDeSeries(ManagerDeSeries mngSerie)
        {
            this.mngSerie = mngSerie;

            

            
        }

        //public ConjuntoDeSeries seriesQueFaltan;
        public void clear() {
            this.seriesCoincidentes = new ConjuntoDeSeries(mngSerie.prs, mngSerie.cf);
            //this.seriesCoincidentes = new Dictionary<TipoDeSeccion, ConjuntoDeSeries>();
            this.seriesExtrenos = new ConjuntoDeSeries(mngSerie.prs, mngSerie.cf);
            this.seriesTodas = new ConjuntoDeSeries(mngSerie.prs, mngSerie.cf);

        }
    }
}
