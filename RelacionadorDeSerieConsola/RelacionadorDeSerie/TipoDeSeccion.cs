/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 26/8/2022
 * Hora: 15:21
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReneUtiles;
using ReneUtiles.Clases;

namespace RelacionadorDeSerie
{
	/// <summary>
	/// Description of TipoDeSeccion.
	/// </summary>
	public class TipoDeSeccion:ComparableHash<TipoDeSeccion>
	{
		public static readonly TipoDeSeccion SERIES=new TipoDeSeccion ("SERIES");
		public static readonly TipoDeSeccion ANIME=new TipoDeSeccion ("ANIME");
		
		public static readonly TipoDeSeccion[] VALUES={SERIES,ANIME};
		public readonly string valor;
		private TipoDeSeccion(string valor)
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
		
		public static TipoDeSeccion get(object tipo){
			if(tipo==null){
				return null;
			}
			foreach (TipoDeSeccion t in VALUES) {
				if(t.valor==tipo.ToString()){
					return t;
				}
			}
			return null;
		}
	}
}
