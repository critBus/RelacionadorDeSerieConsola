

using ReneUtiles.Clases.BD;
using ReneUtiles;
using ReneUtiles.Clases;
using System;
using System.IO;
using System.Collections.Generic;
namespace RelacionadorDeSerie.BD.Modelos{
public class Direccion_MD:ModeloDeApiBD<I_BDAdmin> {
		public static readonly string TABLA_DIRECCION="TABLA_DIRECCION";
		public static readonly string COLUMNA_URL="COLUMNA_URL";
		public static readonly string COLUMNA_SELECCIONADA="COLUMNA_SELECCIONADA";
		public static readonly string COLUMNA_TIPO_DE_DESTINO="COLUMNA_TIPO_DE_DESTINO";
		public static readonly string COLUMNA_TIPO_DE_UBICACION_DE_SECCION="COLUMNA_TIPO_DE_UBICACION_DE_SECCION";
		public static readonly string COLUMNA_SECCION="COLUMNA_SECCION";
		public static readonly string COLUMNA_CATEGORIA="COLUMNA_CATEGORIA";
		public static readonly string COLUMNA_ID="id";
		
		public string url;
		public bool? seleccionada;
		public string tipo_de_destino;
		public string tipo_de_ubicacion_de_seccion;
		public string seccion;
		public string categoria;
		
		public Direccion_MD(string url,bool? seleccionada,string tipo_de_destino,string tipo_de_ubicacion_de_seccion,string seccion,string categoria,int? idkey,I_BDAdmin apibd):base(idkey,apibd){
			this.url=url;
			this.seleccionada=seleccionada;
			this.tipo_de_destino=tipo_de_destino;
			this.tipo_de_ubicacion_de_seccion=tipo_de_ubicacion_de_seccion;
			this.seccion=seccion;
			this.categoria=categoria;
		}
		public Direccion_MD(I_BDAdmin apibd,string url,bool? seleccionada,string tipo_de_destino,string tipo_de_ubicacion_de_seccion,string seccion,string categoria):this(url,seleccionada,tipo_de_destino,tipo_de_ubicacion_de_seccion,seccion,categoria,-1,apibd){
		}
		public Direccion_MD(Direccion_MD v)
			:this(v.url,v.seleccionada,v.tipo_de_destino,v.tipo_de_ubicacion_de_seccion,v.seccion,v.categoria,v.idkey,v.apibd){
		}
		public Direccion_MD s(){
			if (this.idkey==-1){
				return this.apibd.insertarDireccion_MD(this);
			}
			return this.apibd.updateDireccion_MD(this);
		}
		public Direccion_MD sn(I_BDAdmin bd){
			int? idAnterior=this.idkey;
			this.idkey=-1;
			I_BDAdmin bdAnterior=this.apibd;
			this.apibd=bd;
			Direccion_MD n=s();
			this.idkey=idAnterior;
			this.apibd=bdAnterior;
			return n;
		}
		public Direccion_MD cn(I_BDAdmin bd){
			Direccion_MD n= new Direccion_MD(this);
			n.idkey=-1;
			n.apibd=bd;
			return n;
		}
		public Direccion_MD si(){
			if (this.apibd.existeDireccion_MD_id(this.idkey)){
				return this.apibd.updateDireccion_MD(this);
			}
			return this.apibd.insertarDireccion_MD(this);
		}
		public void d(){
			if (this.idkey!=-1){
				this.apibd.deleteDireccion_MD_ForId(this.idkey);
			}
		}
		public string getStr(String textoInicial){
			Direccion_MD s = this;
			return textoInicial+"Direccion_MD: idkey="+ s.idkey
				+" url="+s.url
				+" seleccionada="+s.seleccionada
				+" tipo_de_destino="+s.tipo_de_destino
				+" tipo_de_ubicacion_de_seccion="+s.tipo_de_ubicacion_de_seccion
				+" seccion="+s.seccion
				+" categoria="+s.categoria
			;
		}
		public string getStr(){ return getStr("");}
}
}
