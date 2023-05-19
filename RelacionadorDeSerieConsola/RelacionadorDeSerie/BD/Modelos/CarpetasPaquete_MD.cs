

using ReneUtiles.Clases.BD;
using ReneUtiles;
using ReneUtiles.Clases;
using System;
using System.IO;
using System.Collections.Generic;
namespace RelacionadorDeSerie.BD.Modelos{
public class CarpetasPaquete_MD:ModeloDeApiBD<I_BDAdmin> {
		public static readonly string TABLA_CARPETAS_PAQUETE="TABLA_CARPETAS_PAQUETE";
		public static readonly string COLUMNA_ID_TABLA_FILE="COLUMNA_ID_TABLA_FILE";
		public static readonly string COLUMNA_MARCA="COLUMNA_MARCA";
		public static readonly string COLUMNA_FECHA="COLUMNA_FECHA";
		public static readonly string COLUMNA_ID="id";
		
		public int? idkey_file;
		public string marca;
		public DateTime? fecha;
		
		public CarpetasPaquete_MD(int? idkey_file,string marca,DateTime? fecha,int? idkey,I_BDAdmin apibd):base(idkey,apibd){
			this.idkey_file=idkey_file;
			this.marca=marca;
			this.fecha=fecha;
		}
		public CarpetasPaquete_MD(I_BDAdmin apibd,int? idkey_file,string marca,DateTime? fecha):this(idkey_file,marca,fecha,-1,apibd){
		}
		public CarpetasPaquete_MD(I_BDAdmin apibd,File_MD file,string marca,DateTime? fecha):this(file.idkey,marca,fecha,-1,apibd){
		}
		public CarpetasPaquete_MD(CarpetasPaquete_MD v)
			:this(v.idkey_file,v.marca,v.fecha,v.idkey,v.apibd){
		}
		public File_MD getFile(){
			return this.apibd.getFile_MD_id(this.idkey_file);
		}
		public CarpetasPaquete_MD s(){
			if (this.idkey==-1){
				return this.apibd.insertarCarpetasPaquete_MD(this);
			}
			return this.apibd.updateCarpetasPaquete_MD(this);
		}
		public CarpetasPaquete_MD sn(I_BDAdmin bd){
			int? idAnterior=this.idkey;
			this.idkey=-1;
			I_BDAdmin bdAnterior=this.apibd;
			this.apibd=bd;
			CarpetasPaquete_MD n=s();
			this.idkey=idAnterior;
			this.apibd=bdAnterior;
			return n;
		}
		public CarpetasPaquete_MD cn(I_BDAdmin bd){
			CarpetasPaquete_MD n= new CarpetasPaquete_MD(this);
			n.idkey=-1;
			n.apibd=bd;
			return n;
		}
		public CarpetasPaquete_MD si(){
			if (this.apibd.existeCarpetasPaquete_MD_id(this.idkey)){
				return this.apibd.updateCarpetasPaquete_MD(this);
			}
			return this.apibd.insertarCarpetasPaquete_MD(this);
		}
		public void d(){
			if (this.idkey!=-1){
				this.apibd.deleteCarpetasPaquete_MD_ForId(this.idkey);
			}
		}
		public string getStr(String textoInicial){
			CarpetasPaquete_MD s = this;
			return textoInicial+"CarpetasPaquete_MD: idkey="+ s.idkey
				+" idkey_file="+s.idkey_file
				+" marca="+s.marca
				+" fecha="+s.fecha
			;
		}
		public string getStr(){ return getStr("");}
}
}
