/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 27/8/2022
 * Hora: 10:13
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;

namespace RelacionadorDeSerie.Representaciones
{
	/// <summary>
	/// Description of Temporada_R.
	/// </summary>
	public class Temporada_R
	{
		public string id;
		public int numeroTemporada;
		public int cantidadDeCapitulos_distintos;
		public int cantidadDeCapitulos;
		public List<Capitulo_R> capitulos;
		public Temporada_R()
		{
		}
	}
}
