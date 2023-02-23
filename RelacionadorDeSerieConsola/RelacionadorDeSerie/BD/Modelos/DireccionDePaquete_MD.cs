

using ReneUtiles.Clases.BD;
using ReneUtiles;
using ReneUtiles.Clases;
using System;
using System.IO;
using System.Collections.Generic;
namespace RelacionadorDeSerie.BD.Modelos{
public class DireccionDePaquete_MD:ModeloDeApiBD<BDAdmin> {
		public static readonly string TABLA_DIRECCION_DE_PAQUETE="TABLA_DIRECCION_DE_PAQUETE";
		public static readonly string COLUMNA_URL="COLUMNA_URL";
		public static readonly string COLUMNA_SELECCIONADA="COLUMNA_SELECCIONADA";
		public static readonly string COLUMNA_TIPO_DE_DESTINO="COLUMNA_TIPO_DE_DESTINO";
		public static readonly string COLUMNA_SECCION="COLUMNA_SECCION";
		
		public string url;
		public bool seleccionada;
		public string tipo_de_destino;
		public string seccion;
		
		public DireccionDePaquete_MD(string url,bool seleccionada,string tipo_de_destino,string seccion,int idkey,BDAdmin apibd):base(idkey,apibd){
			this.url=url;
			this.seleccionada=seleccionada;
			this.tipo_de_destino=tipo_de_destino;
			this.seccion=seccion;
		}
		public DireccionDePaquete_MD(BDAdmin apibd,string url,bool seleccionada,string tipo_de_destino,string seccion):this(url,seleccionada,tipo_de_destino,seccion,-1,apibd){
		}
		public List<EtiquetaDeDireccionPaquete_MD> getListaDe_EtiquetaDeDireccionPaquete_MD(){
			return this.apibd.getEtiquetaDeDireccionPaquete_MD_All_Idkey_direccion_de_paquete(this.idkey);
		}
		public EtiquetaDeDireccionPaquete_MD addEtiquetaDeDireccionPaquete_MD(EtiquetaDeDireccionPaquete_MD etiqueta_de_direccion_paquete){
			if (this.idkey==-1){
				this.idkey=this.apibd.insertarDireccionDePaquete_MD(this).idkey;
				etiqueta_de_direccion_paquete.idkey_direccion_de_paquete=this.idkey;
			}
			if (etiqueta_de_direccion_paquete.idkey==-1){
				etiqueta_de_direccion_paquete=this.apibd.insertarEtiquetaDeDireccionPaquete_MD(etiqueta_de_direccion_paquete);
			}
			return etiqueta_de_direccion_paquete;
		}
		public DireccionDePaquete_MD s(){
			if (this.idkey==-1){
				return this.apibd.insertarDireccionDePaquete_MD(this);
			}
			return this.apibd.updateDireccionDePaquete_MD(this);
		}
		public void d(){
			if (this.idkey!=-1){
				this.apibd.deleteDireccionDePaquete_MD_ForId_CASCADE(this.idkey);
			}
		}
		public string getStr(String textoInicial){
			DireccionDePaquete_MD s = this;
			return textoInicial+"DireccionDePaquete_MD: idkey="+ s.idkey
				+" url="+s.url
				+" seleccionada="+s.seleccionada
				+" tipo_de_destino="+s.tipo_de_destino
				+" seccion="+s.seccion
			;
		}
		public string getStr(){ return getStr("");}
}
}
