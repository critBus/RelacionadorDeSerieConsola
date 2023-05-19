

using ReneUtiles.Clases.BD;
using ReneUtiles;
using ReneUtiles.Clases;
using System;
using System.IO;
using System.Collections.Generic;
namespace RelacionadorDeSerie.BD.Modelos{
public class ArchivoInterno_MD:ModeloDeApiBD<I_BDAdmin> {
		public static readonly string TABLA_ARCHIVO_INTERNO="TABLA_ARCHIVO_INTERNO";
		public static readonly string COLUMNA_ID_CARPETA_PADRE="COLUMNA_ID_CARPETA_PADRE";
		public static readonly string COLUMNA_ID_FILE_HIJO="COLUMNA_ID_FILE_HIJO";
		public static readonly string COLUMNA_ID="id";
		
		public int? idkey_id_carpeta_padre;
		public int? idkey_id_file_hijo;
		
		public ArchivoInterno_MD(int? idkey_id_carpeta_padre,int? idkey_id_file_hijo,int? idkey,I_BDAdmin apibd):base(idkey,apibd){
			this.idkey_id_carpeta_padre=idkey_id_carpeta_padre;
			this.idkey_id_file_hijo=idkey_id_file_hijo;
		}
		public ArchivoInterno_MD(I_BDAdmin apibd,int? idkey_id_carpeta_padre,int? idkey_id_file_hijo):this(idkey_id_carpeta_padre,idkey_id_file_hijo,-1,apibd){
		}
		public ArchivoInterno_MD(I_BDAdmin apibd,File_MD id_carpeta_padre,File_MD id_file_hijo):this(id_carpeta_padre.idkey,id_file_hijo.idkey,-1,apibd){
		}
		public ArchivoInterno_MD(ArchivoInterno_MD v)
			:this(v.idkey_id_carpeta_padre,v.idkey_id_file_hijo,v.idkey,v.apibd){
		}
		public File_MD getId_carpeta_padre(){
			return this.apibd.getFile_MD_id(this.idkey_id_carpeta_padre);
		}
		public File_MD getId_file_hijo(){
			return this.apibd.getFile_MD_id(this.idkey_id_file_hijo);
		}
		public ArchivoInterno_MD s(){
			if (this.idkey==-1){
				return this.apibd.insertarArchivoInterno_MD(this);
			}
			return this.apibd.updateArchivoInterno_MD(this);
		}
		public ArchivoInterno_MD sn(I_BDAdmin bd){
			int? idAnterior=this.idkey;
			this.idkey=-1;
			I_BDAdmin bdAnterior=this.apibd;
			this.apibd=bd;
			ArchivoInterno_MD n=s();
			this.idkey=idAnterior;
			this.apibd=bdAnterior;
			return n;
		}
		public ArchivoInterno_MD cn(I_BDAdmin bd){
			ArchivoInterno_MD n= new ArchivoInterno_MD(this);
			n.idkey=-1;
			n.apibd=bd;
			return n;
		}
		public ArchivoInterno_MD si(){
			if (this.apibd.existeArchivoInterno_MD_id(this.idkey)){
				return this.apibd.updateArchivoInterno_MD(this);
			}
			return this.apibd.insertarArchivoInterno_MD(this);
		}
		public void d(){
			if (this.idkey!=-1){
				this.apibd.deleteArchivoInterno_MD_ForId(this.idkey);
			}
		}
		public string getStr(String textoInicial){
			ArchivoInterno_MD s = this;
			return textoInicial+"ArchivoInterno_MD: idkey="+ s.idkey
				+" idkey_id_carpeta_padre="+s.idkey_id_carpeta_padre
				+" idkey_id_file_hijo="+s.idkey_id_file_hijo
			;
		}
		public string getStr(){ return getStr("");}
}
}
