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
    public class SeriesEnSeccionDelPaquete:ConsolaBasica
    {
        //private Paquete paquete;
        private ManagerDeSeries mngSeries;

        private ConjuntoDeEtiquetasDeSerie etiquetas;

        public HashSet<DirectorioDeSeriesDelPaquete> directorios;

        public ConjuntoDeSeries seriesEnEstosDirectorios;

        public Dictionary<TipoDeCategoriaPropias, ConvinacionesDeSeries> convinacionesPorCategorias;

        public ConvinacionesDeSeries seriesEnCategoriaTodas;







        public SeriesEnSeccionDelPaquete(
            //Paquete paquete,
            ManagerDeSeries mngSeries, ConjuntoDeEtiquetasDeSerie etiquetas, HashSet<DirectorioDeSeriesDelPaquete> directorios, ConjuntoDeSeries seriesEnEstosDirectorios)
        {
           // this.paquete = paquete;
            this.mngSeries = mngSeries;
            this.etiquetas = etiquetas;
            this.directorios = directorios;
            this.seriesEnEstosDirectorios = seriesEnEstosDirectorios;


            this.convinacionesPorCategorias = TipoDeCategoriaPropias.getNewDictionary<ConvinacionesDeSeries>();//new Dictionary<TipoDeCategoriaPropias, ConvinacionesDeSeries>();
        }

        private ConvinacionesDeSeries getConvinaciones(ConjuntoDeSeries seriesActuales) {
            //if ((!seriesActuales.isEmpty()) && (!this.seriesEnEstosDirectorios.isEmpty())) {
            //    cwl();
            //}
            ConjuntoDeSeries a = seriesActuales;
            ConjuntoDeSeries b = this.seriesEnEstosDirectorios;
            ConvinacionesDeSeries convinaciones = new ConvinacionesDeSeries(this.mngSeries);
            convinaciones.seriesCoincidentes = b.getSeriesPropiasQueCoincidenConLasDe(a);
            convinaciones.seriesExtrenos = b.getSeriesConCapitulosUno();

            convinaciones.seriesTodas = this.mngSeries.getNewConjuntoDeSeries();
            convinaciones.seriesTodas.unirCon(convinaciones.seriesCoincidentes);
            convinaciones.seriesTodas.unirCon(convinaciones.seriesExtrenos);

            return convinaciones;
        }

        public void actualizar_Convinaciones(params TipoDeCategoriaPropias[] categorias) {

            IEnumerable<TipoDeCategoriaPropias> categoriasARecorrer = categorias;
            if (categorias.Length==0) {
                categorias = TipoDeCategoriaPropias.VALUES;
            }

            foreach (TipoDeCategoriaPropias tipo in categoriasARecorrer)//TipoDeCategoriaPropias.VALUES
            {
                ConvinacionesDeSeries convinaciones = null;



                if (this.mngSeries.seriesPropias.ContainsKey(tipo))
                {
                    ConjuntoDeSeries seriesActuales = this.mngSeries.seriesPropias[tipo];
                    convinaciones = getConvinaciones(seriesActuales);


                }
                else
                {
                    convinaciones = new ConvinacionesDeSeries(this.mngSeries);
                    convinaciones.seriesCoincidentes = this.mngSeries.getNewConjuntoDeSeries();
                    convinaciones.seriesExtrenos = this.mngSeries.getNewConjuntoDeSeries();
                    convinaciones.seriesTodas = this.mngSeries.getNewConjuntoDeSeries();
                }

                if (this.convinacionesPorCategorias.ContainsKey(tipo))
                {
                    this.convinacionesPorCategorias[tipo] = convinaciones;
                }
                else {
                    this.convinacionesPorCategorias.Add(tipo, convinaciones);
                }
                
            }

            this.seriesEnCategoriaTodas = getConvinaciones(this.mngSeries.todasLasSeries);

        }

        public void actualizar()
        {

            actualizar_Convinaciones(TipoDeCategoriaPropias.VALUES);
            
        }
    }
}
