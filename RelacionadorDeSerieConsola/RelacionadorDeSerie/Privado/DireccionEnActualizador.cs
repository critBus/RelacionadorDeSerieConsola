using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles.Clases;
using System.IO;

namespace RelacionadorDeSerie.Privado
{
    public class DireccionEnActualizador: ConsolaBasica
    {
        public string url;
        
        
        public bool seleccioniada;
        public TipoDeSeccion seccion;
        public int? id;

        public string letra;
        public string nombreCarpeta;

        

        public DireccionEnActualizador(string url, bool seleccioniada, TipoDeSeccion seccion, int? id=null)
        {
            this.url = url;
            this.seleccioniada = seleccioniada;
            this.seccion = seccion;
            this.id = id;

            setLetraDesdeUrl();

            DirectoryInfo d = new DirectoryInfo(this.url);
            this.nombreCarpeta = (d.Parent != null) ? d.Parent.Name : "";
        }

        public void setLetraDesdeUrl()
        {
            this.letra = subs(this.url, 0, 3);
        }
    }
}
