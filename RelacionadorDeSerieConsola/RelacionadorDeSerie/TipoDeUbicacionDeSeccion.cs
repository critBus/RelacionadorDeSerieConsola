/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 26/8/2022
 * Hora: 19:17
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace RelacionadorDeSerie
{
	/// <summary>
	/// Description of TipoDeUbicacionDeSeccion.
	/// </summary>
	public class TipoDeUbicacionDeSeccion
	{
		public static readonly TipoDeUbicacionDeSeccion PROPIAS=new TipoDeUbicacionDeSeccion ("PROPIAS");
		public static readonly TipoDeUbicacionDeSeccion PAQUETE=new TipoDeUbicacionDeSeccion ("PAQUETE");
		
		public static readonly TipoDeUbicacionDeSeccion[] VALUES={PROPIAS,PAQUETE};
		public readonly string valor;
		private TipoDeUbicacionDeSeccion(string valor)
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
		
		public static TipoDeUbicacionDeSeccion get(object tipo){
			if(tipo==null){
				return null;
			}
			foreach (TipoDeUbicacionDeSeccion t in VALUES) {
				if(t.valor==tipo.ToString()){
					return t;
				}
			}
			return null;
		}
		
	}
}
