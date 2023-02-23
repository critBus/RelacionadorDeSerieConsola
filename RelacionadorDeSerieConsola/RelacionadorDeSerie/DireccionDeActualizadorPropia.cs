/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 23/8/2022
 * Hora: 19:58
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.IO;
using ReneUtiles.Clases;
using RelacionadorDeSerie.Privado;
namespace RelacionadorDeSerie 
{
	/// <summary>
	/// Description of DireccionDeActualizador.
	/// </summary>
	public class DireccionDeActualizadorPropia: DireccionEnActualizador
    {
		//public string url;
		//public string letra;
		//public string nombreCarpeta;
		//public bool seleccioniada;
		
		//public TipoDeSeccion seccion;
		//public int? id;

        public TipoDeCategoriaPropias categoria;

        public DireccionDeActualizadorPropia(
			string url,
			bool seleccioniada,
			TipoDeSeccion seccion,
			TipoDeCategoriaPropias categoria, 
			
			int? id = null
		):base(url,seleccioniada,seccion,id)
		{
            //this.url = url;
            //this.seleccioniada = seleccioniada;
            this.categoria = categoria;
            //this.seccion = seccion;
            //this.id = id;
            //setLetraDesdeUrl();
            //this.letra=letra;

            //DirectoryInfo d=new DirectoryInfo(this.url);
            //this.nombreCarpeta=(d.Parent!=null)?d.Parent.Name:"";

        }
		
	}
}
