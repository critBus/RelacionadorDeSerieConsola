/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 26/8/2022
 * Hora: 19:15
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles;
//using System.IO;
using Delimon.Win32.IO;

namespace RelacionadorDeSerie
{
	/// <summary>
	/// Description of TipoDeDestino.
	/// </summary>
	public class TipoDeDestino
	{
		public static readonly TipoDeDestino CARPETA=new TipoDeDestino ("CARPETA");
		public static readonly TipoDeDestino TXT=new TipoDeDestino ("TXT");
		
		public static readonly TipoDeDestino[] VALUES={CARPETA,TXT};
		public readonly string valor;
		private TipoDeDestino(string valor)
		{
			this.valor=valor;
		}
		public string getValor()
		{
			return this.valor;
		}
		public override string ToString()
		{
			return this.valor;
		}
		
		public static TipoDeDestino get(object tipo){
			if(tipo==null){
				return null;
			}
			foreach (TipoDeDestino t in VALUES) {
				if(t.valor==tipo.ToString()){
					return t;
				}
			}
			return null;
		}
		
		
		public static TipoDeDestino getTipoDeDestino_url(string url)
		{
			return Archivos.esTXT(new FileInfo(url)) ? TipoDeDestino.TXT : TipoDeDestino.CARPETA;
		}
	}
}
