/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 27/8/2022
 * Hora: 10:18
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
namespace RelacionadorDeSerie.Representaciones
{
	/// <summary>
	/// Description of Serie_R.
	/// </summary>
	public class Serie_R
	{
		public string id;
		public List<string> nombres_de_serie;
		public List<Temporada_R> temporadas;
		public Serie_R()
		{
		}
	}
}
