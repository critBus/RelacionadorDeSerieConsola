using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using ReneUtiles.Clases;
using RelacionadorDeSerie.Privado;

using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
namespace RelacionadorDeSerie
{
    public class DireccionDePaquete:DireccionEnActualizador
    {

        public ConjuntoDeEtiquetasDeSerie etiquetas;

        public DireccionDePaquete(
            string url,
            bool seleccionada,
            TipoDeSeccion seccion,
            ConjuntoDeEtiquetasDeSerie etiquetas,
            int? id = null
            ):base(url, seleccionada, seccion, id)
        {
            this.etiquetas = etiquetas;
        }
    }
}
