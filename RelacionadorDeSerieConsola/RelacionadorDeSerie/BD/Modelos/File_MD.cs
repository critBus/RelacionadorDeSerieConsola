

using ReneUtiles.Clases.BD;
using ReneUtiles;
using ReneUtiles.Clases;
using System;
using System.IO;
using System.Collections.Generic;
namespace RelacionadorDeSerie.BD.Modelos{
public class File_MD:ModeloDeApiBD<BDAdmin> {
		public static readonly string TABLA_FILE="TABLA_FILE";
		public static readonly string COLUMNA_NOMBRE="COLUMNA_NOMBRE";
		public static readonly string COLUMNA_TIPO="COLUMNA_TIPO";
		
		public string nombre;
		public string tipo;
		
		public File_MD(string nombre,string tipo,int idkey,BDAdmin apibd):base(idkey,apibd){
			this.nombre=nombre;
			this.tipo=tipo;
		}
		public File_MD(BDAdmin apibd,string nombre,string tipo):this(nombre,tipo,-1,apibd){
		}
		public File_MD s(){
			if (this.idkey==-1){
				return this.apibd.insertarFile_MD(this);
			}
			return this.apibd.updateFile_MD(this);
		}
		public void d(){
			if (this.idkey!=-1){
				this.apibd.deleteFile_MD_ForId_CASCADE(this.idkey);
			}
		}
		public string getStr(String textoInicial){
			File_MD s = this;
			return textoInicial+"File_MD: idkey="+ s.idkey
				+" nombre="+s.nombre
				+" tipo="+s.tipo
			;
		}
		public string getStr(){ return getStr("");}
}
}
