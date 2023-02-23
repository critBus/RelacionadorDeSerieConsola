/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 26/8/2022
 * Hora: 17:55
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles;
using System.Collections.Generic;
using ReneUtiles.Clases;
using System.IO;
using RelacionadorDeSerie.BD.Modelos;
using RelacionadorDeSerie;

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
namespace RelacionadorDeSerie.Privado
{
	/// <summary>
	/// Description of ConjuntoDeSeriesEnCategoria.
	/// </summary>
	public class ConjuntoDeSeriesEnCategoriaPropia
	{
		public TipoDeCategoriaPropias categoria;
		public ConjuntoDeSeries series;
		public ConjuntoDeSeriesEnCategoriaPropia()
		{
		}
	}
}
