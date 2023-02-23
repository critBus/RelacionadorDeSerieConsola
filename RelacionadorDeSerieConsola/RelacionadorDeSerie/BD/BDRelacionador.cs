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
namespace RelacionadorDeSerie.BD
{
    public class BDRelacionador : BDAdmin
    {
        public BDRelacionador() : base() { }

        public List<DireccionDePaquete_MD> getDireccionDePaquete_All(TipoDeSeccion seccion,params TipoDeEtiquetaDeSerie []etiquetas) {
            List<DireccionDePaquete_MD> lds = seccion != null ? getDireccionDePaquete_MD_All_Seccion(seccion.ToString()):getDireccionDePaquete_MD_All();
            if (etiquetas.Length==0) { return lds; }
            List<DireccionDePaquete_MD> ld = new List<DireccionDePaquete_MD>();
            foreach (DireccionDePaquete_MD d in lds)
            {
                List<EtiquetaDeDireccionPaquete_MD> le = d.getListaDe_EtiquetaDeDireccionPaquete_MD();
                Func<bool> tieneTodasLasEtiquetas = () => {
                    if (le.Count() != etiquetas.Length)
                    {
                        return false;
                    }
                    foreach (TipoDeEtiquetaDeSerie te in etiquetas)
                    {
                        bool loEncontro = false;
                        foreach (EtiquetaDeDireccionPaquete_MD e in le)
                        {
                            if (te.ToString() == e.nombre)
                            {
                                loEncontro = true;
                                break;
                            }
                        }
                        if (!loEncontro)
                        {
                            return false;
                        }
                    }
                    return true;
                };
                if (tieneTodasLasEtiquetas())
                {

                    ld.Add(d);
                }
            }
            return ld;
        }

        public List<EtiquetaDeDireccionPaquete_MD> setEtiquetasDeDireccionPaquete(DireccionDePaquete_MD d, params TipoDeEtiquetaDeSerie[] etiquetas) {

            foreach (EtiquetaDeDireccionPaquete_MD e in d.getListaDe_EtiquetaDeDireccionPaquete_MD())
            {
                e.d();
            }
            List<EtiquetaDeDireccionPaquete_MD> l = new List<EtiquetaDeDireccionPaquete_MD>();
            foreach (TipoDeEtiquetaDeSerie te in etiquetas)
            {
                new EtiquetaDeDireccionPaquete_MD(this, te.ToString(), d).s();
            }
            return l;
        }
    }
}
