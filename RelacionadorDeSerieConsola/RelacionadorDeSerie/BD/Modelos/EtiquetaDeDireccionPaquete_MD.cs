

using ReneUtiles.Clases.BD;
using ReneUtiles;
using ReneUtiles.Clases;
using System;
using System.IO;
using System.Collections.Generic;
namespace RelacionadorDeSerie.BD.Modelos{
public class EtiquetaDeDireccionPaquete_MD:ModeloDeApiBD<I_BDAdmin> {
		public static readonly string TABLA_ETIQUETA_DE_DIRECCION_PAQUETE="TABLA_ETIQUETA_DE_DIRECCION_PAQUETE";
		public static readonly string COLUMNA_NOMBRE="COLUMNA_NOMBRE";
		public static readonly string COLUMNA_ID_TABLA_DIRECCION_DE_PAQUETE="COLUMNA_ID_TABLA_DIRECCION_DE_PAQUETE";
		public static readonly string COLUMNA_ID="id";
		
		public string nombre;
		public int? idkey_direccion_de_paquete;
		
		public EtiquetaDeDireccionPaquete_MD(string nombre,int? idkey_direccion_de_paquete,int? idkey,I_BDAdmin apibd):base(idkey,apibd){
			this.nombre=nombre;
			this.idkey_direccion_de_paquete=idkey_direccion_de_paquete;
		}
		public EtiquetaDeDireccionPaquete_MD(I_BDAdmin apibd,string nombre,int? idkey_direccion_de_paquete):this(nombre,idkey_direccion_de_paquete,-1,apibd){
		}
		public EtiquetaDeDireccionPaquete_MD(I_BDAdmin apibd,string nombre,DireccionDePaquete_MD direccion_de_paquete):this(nombre,direccion_de_paquete.idkey,-1,apibd){
		}
		public EtiquetaDeDireccionPaquete_MD(EtiquetaDeDireccionPaquete_MD v)
			:this(v.nombre,v.idkey_direccion_de_paquete,v.idkey,v.apibd){
		}
		public DireccionDePaquete_MD getDireccion_de_paquete(){
			return this.apibd.getDireccionDePaquete_MD_id(this.idkey_direccion_de_paquete);
		}
		public EtiquetaDeDireccionPaquete_MD s(){
			if (this.idkey==-1){
				return this.apibd.insertarEtiquetaDeDireccionPaquete_MD(this);
			}
			return this.apibd.updateEtiquetaDeDireccionPaquete_MD(this);
		}
		public EtiquetaDeDireccionPaquete_MD sn(I_BDAdmin bd){
			int? idAnterior=this.idkey;
			this.idkey=-1;
			I_BDAdmin bdAnterior=this.apibd;
			this.apibd=bd;
			EtiquetaDeDireccionPaquete_MD n=s();
			this.idkey=idAnterior;
			this.apibd=bdAnterior;
			return n;
		}
		public EtiquetaDeDireccionPaquete_MD cn(I_BDAdmin bd){
			EtiquetaDeDireccionPaquete_MD n= new EtiquetaDeDireccionPaquete_MD(this);
			n.idkey=-1;
			n.apibd=bd;
			return n;
		}
		public EtiquetaDeDireccionPaquete_MD si(){
			if (this.apibd.existeEtiquetaDeDireccionPaquete_MD_id(this.idkey)){
				return this.apibd.updateEtiquetaDeDireccionPaquete_MD(this);
			}
			return this.apibd.insertarEtiquetaDeDireccionPaquete_MD(this);
		}
		public void d(){
			if (this.idkey!=-1){
				this.apibd.deleteEtiquetaDeDireccionPaquete_MD_ForId(this.idkey);
			}
		}
		public string getStr(String textoInicial){
			EtiquetaDeDireccionPaquete_MD s = this;
			return textoInicial+"EtiquetaDeDireccionPaquete_MD: idkey="+ s.idkey
				+" nombre="+s.nombre
				+" idkey_direccion_de_paquete="+s.idkey_direccion_de_paquete
			;
		}
		public string getStr(){ return getStr("");}
}
}
