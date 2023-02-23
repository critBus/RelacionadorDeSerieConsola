/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 27/8/2022
 * Hora: 08:37
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using Delimon.Win32.IO;
using System;
using System.Collections.Generic;
namespace RelacionadorDeSerie.Representaciones
{
	/// <summary>
	/// Description of Capitulo_R.
	/// </summary>
	public class Capitulo_R
	{
		public string id;
		public string nombre_capitulo;
		
		public string idDeSerie;
		public  List<string> nombres_de_serie;
		public int temporada;
		public string capitulo;
		public string formato;
		public string url;
		
		public int capituloInicial;
		public int? capituloFinal;
		public bool tieneSubtituloEnSuCarpeta;
		public string fecha;
		public List<string> etiquetas;
		
		public bool esUnExtra;

        public List<FileInfo> listaDeVideos;
        public List<FileInfo> listaDeSubtitulos;

        public double size;


        public Capitulo_R(
			){
			this.id=null;
			this.nombre_capitulo=null;
			this.capitulo=null;
			this.formato=null;
			this.capituloInicial=-1;
			this.capituloFinal=null;
			this.tieneSubtituloEnSuCarpeta=false;
			this.fecha=null;
			this.etiquetas=null;
			this.esUnExtra=false;
			
			this.nombres_de_serie=null;
			this.temporada=-1;

            this.listaDeVideos = new List<FileInfo>();
            this.listaDeSubtitulos = new List<FileInfo>();
            this.size = 0;

        }
	}
}
