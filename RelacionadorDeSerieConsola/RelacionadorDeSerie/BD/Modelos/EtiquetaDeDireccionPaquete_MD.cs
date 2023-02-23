

using ReneUtiles.Clases.BD;
using ReneUtiles;
using ReneUtiles.Clases;
using System;
using System.IO;
using System.Collections.Generic;
namespace RelacionadorDeSerie.BD.Modelos{
public class EtiquetaDeDireccionPaquete_MD:ModeloDeApiBD<BDAdmin> {
		public static readonly string TABLA_ETIQUETA_DE_DIRECCION_PAQUETE="TABLA_ETIQUETA_DE_DIRECCION_PAQUETE";
		public static readonly string COLUMNA_NOMBRE="COLUMNA_NOMBRE";
		public static readonly string COLUMNA_ID_TABLA_DIRECCION_DE_PAQUETE="COLUMNA_ID_TABLA_DIRECCION_DE_PAQUETE";
		
		public string nombre;
		public int idkey_direccion_de_paquete;
		
		public EtiquetaDeDireccionPaquete_MD(string nombre,int idkey_direccion_de_paquete,int idkey,BDAdmin apibd):base(idkey,apibd){
			this.nombre=nombre;
			this.idkey_direccion_de_paquete=idkey_direccion_de_paquete;
		}
		public EtiquetaDeDireccionPaquete_MD(BDAdmin apibd,string nombre,int idkey_direccion_de_paquete):this(nombre,idkey_direccion_de_paquete,-1,apibd){
		}
		public EtiquetaDeDireccionPaquete_MD(BDAdmin apibd,string nombre,DireccionDePaquete_MD direccion_de_paquete):this(nombre,direccion_de_paquete.idkey,-1,apibd){
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
