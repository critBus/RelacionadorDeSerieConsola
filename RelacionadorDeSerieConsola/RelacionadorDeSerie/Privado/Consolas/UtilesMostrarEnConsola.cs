/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 11/9/2022
 * Hora: 18:40
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles;
using System.Collections.Generic;
using ReneUtiles.Clases;
using System.IO;
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

namespace RelacionadorDeSerie.Privado.Consolas
{
	/// <summary>
	/// Description of UtilesMostrarEnConsola.
	/// </summary>
	public class UtilesMostrarEnConsola:ConsolaBasica
	{
		public UtilesMostrarEnConsola()
		{
		}
		
		
		
		public static void mostrarConjuntoDeSeries(ConjuntoDeSeries cns)
		{
			cwl("cantidad de series: " + cns.series.Count);
			foreach (Serie s in cns.series) {
				mostrarSerie(s);
				cwl("..................................................  ...........................................................................................................");
			}
		}
		
		
		public static void mostrarSerie(Serie s)
		{
			HashSet<KeySerie> hk = s.getKeysDeSerie();
			//cwl("cantidad hk="+hk.Count);
			foreach (KeySerie k in hk) {
				cwl("Nombre Serie:" + k.Nombre);
				cwl("Clave Serie:" + k.Clave);
				cwl("Tipo:" + k.TipoDeSerie);
			}
			
				
			List<TemporadaDeSerie> lt = s.Temporadas;
			cwl("Cantidad de Temporadas=" + lt.Count);
			HashSet<int> numerosDeTemporda = new HashSet<int>();
			foreach (TemporadaDeSerie t in lt) {
				mostrarTemporada(t);
				cwl("***********************************************************************");
				numerosDeTemporda.Add(t.NumeroTemporada);
			}
			foreach (int nt in numerosDeTemporda) {
				cwl("nt " + nt);
			}
		}
		public static void mostrarListaDeCapitulos(List<RepresentacionDeCapitulo> lr)
		{
			foreach (RepresentacionDeCapitulo rc in lr) {
				if (rc is CapituloDeSerie) {
					CapituloDeSerie cs = (CapituloDeSerie)rc;
					cwl("Capitulo simple: " + cs.NumeroDeCapitulo);
					
				} else {
					CapituloDeSerieMultiples csm = (CapituloDeSerieMultiples)rc;
					cwl("Capitulos: " + csm.NumeroCapituloInicial + " - " + csm.NumeroCapituloFinal);
				}
				foreach (DatosDeFuente df in rc.Fuentes.getDatosDefuentes()) {
					foreach (DatosDeNombreSerie Dns in df.Ldns) {
//						DatosNumericosDeNombreDeSerie Dns = df.Dns;
						if (Dns is DatosDeNombreSerie) {
							mostrarDatosDeNombreSerie((DatosDeNombreSerie)Dns, df.Ctx.Url, new DirectoryInfo(df.Ctx.Url).Name);
						} else {
							mostrarDatosNumericosDeNombreDeSerie(Dns, df.Ctx.Url, new DirectoryInfo(df.Ctx.Url).Name);
						}
					}
					
					
					
					if (df is DatosDeArchivoFisico) {
						DatosVideosConSubtitulos dv = ((DatosDeArchivoFisico)df).datosVideosConSubtitulos;
						if (dv != null) {
							cwl("tieneVideos=" + dv.tieneVideos);
							cwl("tieneSubtitulos=" + dv.tieneSubtitulos);
							cwl("tieneSubtitulosTodos=" + dv.tieneSubtitulosTodos);
							cwl("tieneSubtitulosAlMenosUno=" + dv.tieneSubtitulosAlMenosUno);
							cwl("cantidadDeVideos=" + dv.cantidadDeVideos);
							cwl("cantidadDeSubtitulos=" + dv.cantidadDeSubtitulos);
						}
						
					}
					cwl("++++++++++++++++++++++++++++");
				}
				
				cwl("----------------------------");
			}
			
		}
		
		public static void mostrarTemporada(TemporadaDeSerie t)
		{
//			if(t.NumeroTemporada==2){
//				cwl();
//			}
			cwl("NumeroTemporada=" + t.NumeroTemporada);
			cwl("Cantidad de Capitulos=" + t.Capitulos.Count);
			//t.Capitulos
			mostrarListaDeCapitulos(t.Capitulos);
			
			foreach (int n in t.setNumerosDeCapitulos) {
				cwl("n " + n);
			}
			cwl();
		}
		
		public static  void mostrarDatosDeNombreSerie(DatosDeNombreSerie dn
		                                              , string url	
														, string nombreUrl		
		                                             // , FileSystemInfo f
		)
		{
			cwl("Nombre:" + dn.NombreAdaptado);
			cwl("Clave:" + dn.Clave);
			cwl("Tipo de nombre:" + dn.getTipoDeNombre());
			mostrarDatosNumericosDeNombreDeSerie(dn, url, nombreUrl); 
		}
		
		public static  void mostrarDatosNumericosDeNombreDeSerie(DatosNumericosDeNombreDeSerie Dns
		                                                         , string url	
														, string nombreUrl														
		                                                         //, FileSystemInfo f
		)
		{
			DatosDeNombreSerie dn = null;
			if (Dns is DatosDeNombreSerie) {
				dn = (DatosDeNombreSerie)Dns;
			}
			cwl("datos del principio:");
			mostrarDatosDeNombreCapitulo(Dns.datosDelPrincipio, url, nombreUrl, dn);
			cwl("datos del final:");
			mostrarDatosDeNombreCapitulo(Dns.datosDelFinal, url, nombreUrl, dn);
		}
		
		
		public static  void mostrarDatosDeNombreCapitulo(DatosDeNombreCapitulo d
		                                                // FileSystemInfo f,
														, string url	
														, string nombreUrl														
		                                                 , DatosDeNombreSerie dn = null)
		{
			if (d != null) {
				string temporada = d.tieneTemporada_Unica() ? " T=" + d.getTemporada() : "";
				string esSoloNumero = d.EsSoloNumeros ? " sn" : "";
				//string alFinal = temporada + esSoloNumero + " i=" + d.IndiceDeInicioDespuesDeLosNumeros;
				string alFinal = temporada + esSoloNumero; //+ " " + dn.Clave;
				if (dn != null) {
					alFinal += " | " + dn.Clave + " | " + dn.NombreAdaptado;
				}
				alFinal += " " + d.TipoDeNombre;
				if (d.esConjuntoDeCapitulos()) {
					cwl(d.getCapituloInicial() + " - " + d.getCapituloFinal() + alFinal);
					//cwl(d.CapituloInicial + " - " + d.CapituloFinal + alFinal);
				} else {
					if (d.esConjuntoDeCapitulos()) {
						//cwl("c " + alFinal);
						cwl("c=" + d.getCantidadDeCapitulos() + " " + alFinal);
					} else {
						cwl(d.getCapituloInicial() + alFinal);//+" :"
						//cwl(d.Capitulo + alFinal);
					}
						
				}
				//awStringIndices(5, f.Name);
				UtilesConsola.cwStringIndices(5, nombreUrl);
				//return !d.EsContenedorDeTemporada;
				cwl("fue capitulo =" + !d.esConjuntoDeCapitulos());
					
			} else {
				cwl("null " + url);
				//cwl("null " + f.ToString());
			}
			//cwl("fue capitulo =" + false);
		
		}
		//-----------
	}
}
