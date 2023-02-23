

using ReneUtiles.Clases.BD;
using ReneUtiles;
using ReneUtiles.Clases;
using System;
using System.IO;
using System.Collections.Generic;
namespace RelacionadorDeSerie.BD.Modelos{
public class ArchivoInterno_MD:ModeloDeApiBD<BDAdmin> {
		public static readonly string TABLA_ARCHIVO_INTERNO="TABLA_ARCHIVO_INTERNO";
		public static readonly string COLUMNA_ID_CARPETA_PADRE="COLUMNA_ID_CARPETA_PADRE";
		public static readonly string COLUMNA_ID_FILE_HIJO="COLUMNA_ID_FILE_HIJO";
		
		public int idkey_id_carpeta_padre;
		public int idkey_id_file_hijo;
		
		public ArchivoInterno_MD(int idkey_id_carpeta_padre,int idkey_id_file_hijo,int idkey,BDAdmin apibd):base(idkey,apibd){
			this.idkey_id_carpeta_padre=idkey_id_carpeta_padre;
			this.idkey_id_file_hijo=idkey_id_file_hijo;
		}
		public ArchivoInterno_MD(BDAdmin apibd,int idkey_id_carpeta_padre,int idkey_id_file_hijo):this(idkey_id_carpeta_padre,idkey_id_file_hijo,-1,apibd){
		}
		public ArchivoInterno_MD(BDAdmin apibd,File_MD id_carpeta_padre,File_MD id_file_hijo):this(id_carpeta_padre.idkey,id_file_hijo.idkey,-1,apibd){
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
