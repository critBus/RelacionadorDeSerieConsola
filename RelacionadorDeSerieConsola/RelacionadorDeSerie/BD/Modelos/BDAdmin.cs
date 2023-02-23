

using ReneUtiles.Clases.BD;
using ReneUtiles;
using ReneUtiles.Clases;
using System;
using System.IO;
using System.Collections.Generic;
namespace RelacionadorDeSerie.BD.Modelos{
	
public class BDAdmin:BasicoBD{
		private string urlBD;
		private BDConexion BD;
		private BDUpdates __Upd;
		private bool usarUpdater;
		public BDAdmin():
				
	this(null)
		{
		}
		public BDAdmin(string url){
		if (url==null){
			this.urlBD="bdActualizadoor.sqlite";
		}else{
			this.urlBD=url;
		}
			
		this.BD =BDConexion.getConexionSQL_LITE(this.urlBD);
			this.__Upd =new BDUpdates(this.BD);
			this.usarUpdater=false;
		}
		public string getUrlBD(){
			return  this.urlBD;
		}
		public BDAdmin crearTablaDireccion_MD(){
			 this.BD.crearTablaYBorrarSiExiste(Direccion_MD.TABLA_DIRECCION
							,Direccion_MD.COLUMNA_URL,500
							,Direccion_MD.COLUMNA_SELECCIONADA,TipoDeDatoSQL.BOOLEAN
							,Direccion_MD.COLUMNA_TIPO_DE_DESTINO,50
							,Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,50
							,Direccion_MD.COLUMNA_SECCION,50
							,Direccion_MD.COLUMNA_CATEGORIA,50
							);
			return this;
		}
		public BDAdmin crearTablaDireccion_MDSiNoExiste(){
			 this.BD.crearTablaSiNoExiste(Direccion_MD.TABLA_DIRECCION
							,Direccion_MD.COLUMNA_URL,500
							,Direccion_MD.COLUMNA_SELECCIONADA,TipoDeDatoSQL.BOOLEAN
							,Direccion_MD.COLUMNA_TIPO_DE_DESTINO,50
							,Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,50
							,Direccion_MD.COLUMNA_SECCION,50
							,Direccion_MD.COLUMNA_CATEGORIA,50
							);
			return this;
		}
		public Direccion_MD getDireccion_MD_Args(Object[] listaDeArgumentos){
			return new Direccion_MD(to_String(listaDeArgumentos[1])
					,toBool(listaDeArgumentos[2])
					,to_String(listaDeArgumentos[3])
					,to_String(listaDeArgumentos[4])
					,to_String(listaDeArgumentos[5])
					,to_String(listaDeArgumentos[6])
					,toInt(listaDeArgumentos[0])
					,this
					);
			}
		public Object[] __content_Direccion_MD(Direccion_MD direccion){
			Object[] lista = {new Object[]{Direccion_MD.COLUMNA_URL,direccion.url}
				,new Object[]{Direccion_MD.COLUMNA_SELECCIONADA,direccion.seleccionada}
				,new Object[]{Direccion_MD.COLUMNA_TIPO_DE_DESTINO,direccion.tipo_de_destino}
				,new Object[]{Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,direccion.tipo_de_ubicacion_de_seccion}
				,new Object[]{Direccion_MD.COLUMNA_SECCION,direccion.seccion}
				,new Object[]{Direccion_MD.COLUMNA_CATEGORIA,direccion.categoria}
				};
			return lista;
			}
		public Direccion_MD getDireccion_MD_id(int id){
			Object[] O = this.BD.select_forID(Direccion_MD.TABLA_DIRECCION, id);
			if (O == null){
				return null;}
			return this.getDireccion_MD_Args(O);
			}
		public Direccion_MD insertarDireccion_MD(Direccion_MD direccion){
			if (direccion.idkey==-1){
				int id=this.BD.insertar(Direccion_MD.TABLA_DIRECCION
						,direccion.url
						,direccion.seleccionada
						,direccion.tipo_de_destino
						,direccion.tipo_de_ubicacion_de_seccion
						,direccion.seccion
						,direccion.categoria
						).id;
				return this.getDireccion_MD_id(id);
			}else{
				this.BD.insertar_SinIdAutomatico(Direccion_MD.TABLA_DIRECCION,direccion.idkey
						,direccion.url
						,direccion.seleccionada
						,direccion.tipo_de_destino
						,direccion.tipo_de_ubicacion_de_seccion
						,direccion.seccion
						,direccion.categoria
						);
				return this.getDireccion_MD_id(direccion.idkey);}
			}
		public List<Direccion_MD> getDireccion_MD_All(){
				List<Direccion_MD> lista=new List<Direccion_MD>();
				Object [][]O=this.BD.select_Todo(Direccion_MD.TABLA_DIRECCION);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getDireccion_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public Direccion_MD updateDireccion_MD(Direccion_MD direccion){
				this.BD.update_Id(Direccion_MD.TABLA_DIRECCION,direccion.idkey
							 , Direccion_MD.COLUMNA_URL , direccion.url
							 , Direccion_MD.COLUMNA_SELECCIONADA , direccion.seleccionada
							 , Direccion_MD.COLUMNA_TIPO_DE_DESTINO , direccion.tipo_de_destino
							 , Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION , direccion.tipo_de_ubicacion_de_seccion
							 , Direccion_MD.COLUMNA_SECCION , direccion.seccion
							 , Direccion_MD.COLUMNA_CATEGORIA , direccion.categoria);
				return getDireccion_MD_id(direccion.idkey);
		}
		public void deleteDireccion_MD_ForId(int id){
				this.BD.delete_id(Direccion_MD.TABLA_DIRECCION,id);
		}
		public void deleteDireccion_MD_ForId(Direccion_MD direccion){
				deleteDireccion_MD_ForId(direccion.idkey);
		}
		public bool existeDireccion_MD_id(int id){
			Object[] O = this.BD.select_forID(Direccion_MD.TABLA_DIRECCION, id);
			return O != null;
			}
		public bool existeDireccion_MD(string url,string tipo_de_ubicacion_de_seccion,string seccion,string categoria){
				return this.BD.existe(Direccion_MD.TABLA_DIRECCION
						,Direccion_MD.COLUMNA_URL,url
						,Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,tipo_de_ubicacion_de_seccion
						,Direccion_MD.COLUMNA_SECCION,seccion
						,Direccion_MD.COLUMNA_CATEGORIA,categoria);
		}
		public BDAdmin crearTablaEtiquetaDeDireccionPaquete_MD(){
			 this.BD.crearTablaYBorrarSiExiste(EtiquetaDeDireccionPaquete_MD.TABLA_ETIQUETA_DE_DIRECCION_PAQUETE
							,EtiquetaDeDireccionPaquete_MD.COLUMNA_NOMBRE,50
							,EtiquetaDeDireccionPaquete_MD.COLUMNA_ID_TABLA_DIRECCION_DE_PAQUETE,TipoDeDatoSQL.INTEGER
							);
			return this;
		}
		public BDAdmin crearTablaEtiquetaDeDireccionPaquete_MDSiNoExiste(){
			 this.BD.crearTablaSiNoExiste(EtiquetaDeDireccionPaquete_MD.TABLA_ETIQUETA_DE_DIRECCION_PAQUETE
							,EtiquetaDeDireccionPaquete_MD.COLUMNA_NOMBRE,50
							,EtiquetaDeDireccionPaquete_MD.COLUMNA_ID_TABLA_DIRECCION_DE_PAQUETE,TipoDeDatoSQL.INTEGER
							);
			return this;
		}
		public EtiquetaDeDireccionPaquete_MD getEtiquetaDeDireccionPaquete_MD_Args(Object[] listaDeArgumentos){
			return new EtiquetaDeDireccionPaquete_MD(to_String(listaDeArgumentos[1])
					,toInt(listaDeArgumentos[2])
					,toInt(listaDeArgumentos[0])
					,this
					);
			}
		public Object[] __content_EtiquetaDeDireccionPaquete_MD(EtiquetaDeDireccionPaquete_MD etiqueta_de_direccion_paquete){
			Object[] lista = {new Object[]{EtiquetaDeDireccionPaquete_MD.COLUMNA_NOMBRE,etiqueta_de_direccion_paquete.nombre}
				,new Object[]{EtiquetaDeDireccionPaquete_MD.COLUMNA_ID_TABLA_DIRECCION_DE_PAQUETE,etiqueta_de_direccion_paquete.idkey_direccion_de_paquete}
				};
			return lista;
			}
		public EtiquetaDeDireccionPaquete_MD getEtiquetaDeDireccionPaquete_MD_id(int id){
			Object[] O = this.BD.select_forID(EtiquetaDeDireccionPaquete_MD.TABLA_ETIQUETA_DE_DIRECCION_PAQUETE, id);
			if (O == null){
				return null;}
			return this.getEtiquetaDeDireccionPaquete_MD_Args(O);
			}
		public EtiquetaDeDireccionPaquete_MD insertarEtiquetaDeDireccionPaquete_MD(EtiquetaDeDireccionPaquete_MD etiqueta_de_direccion_paquete){
			if (etiqueta_de_direccion_paquete.idkey==-1){
				int id=this.BD.insertar(EtiquetaDeDireccionPaquete_MD.TABLA_ETIQUETA_DE_DIRECCION_PAQUETE
						,etiqueta_de_direccion_paquete.nombre
						,etiqueta_de_direccion_paquete.idkey_direccion_de_paquete
						).id;
				return this.getEtiquetaDeDireccionPaquete_MD_id(id);
			}else{
				this.BD.insertar_SinIdAutomatico(EtiquetaDeDireccionPaquete_MD.TABLA_ETIQUETA_DE_DIRECCION_PAQUETE,etiqueta_de_direccion_paquete.idkey
						,etiqueta_de_direccion_paquete.nombre
						,etiqueta_de_direccion_paquete.idkey_direccion_de_paquete
						);
				return this.getEtiquetaDeDireccionPaquete_MD_id(etiqueta_de_direccion_paquete.idkey);}
			}
		public List<EtiquetaDeDireccionPaquete_MD> getEtiquetaDeDireccionPaquete_MD_All(){
				List<EtiquetaDeDireccionPaquete_MD> lista=new List<EtiquetaDeDireccionPaquete_MD>();
				Object [][]O=this.BD.select_Todo(EtiquetaDeDireccionPaquete_MD.TABLA_ETIQUETA_DE_DIRECCION_PAQUETE);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getEtiquetaDeDireccionPaquete_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public EtiquetaDeDireccionPaquete_MD updateEtiquetaDeDireccionPaquete_MD(EtiquetaDeDireccionPaquete_MD etiqueta_de_direccion_paquete){
				this.BD.update_Id(EtiquetaDeDireccionPaquete_MD.TABLA_ETIQUETA_DE_DIRECCION_PAQUETE,etiqueta_de_direccion_paquete.idkey
							 , EtiquetaDeDireccionPaquete_MD.COLUMNA_NOMBRE , etiqueta_de_direccion_paquete.nombre
							 , EtiquetaDeDireccionPaquete_MD.COLUMNA_ID_TABLA_DIRECCION_DE_PAQUETE , etiqueta_de_direccion_paquete.idkey_direccion_de_paquete);
				return getEtiquetaDeDireccionPaquete_MD_id(etiqueta_de_direccion_paquete.idkey);
		}
		public void deleteEtiquetaDeDireccionPaquete_MD_ForId(int id){
				this.BD.delete_id(EtiquetaDeDireccionPaquete_MD.TABLA_ETIQUETA_DE_DIRECCION_PAQUETE,id);
		}
		public void deleteEtiquetaDeDireccionPaquete_MD_ForId(EtiquetaDeDireccionPaquete_MD etiqueta_de_direccion_paquete){
				deleteEtiquetaDeDireccionPaquete_MD_ForId(etiqueta_de_direccion_paquete.idkey);
		}
		public bool existeEtiquetaDeDireccionPaquete_MD_id(int id){
			Object[] O = this.BD.select_forID(EtiquetaDeDireccionPaquete_MD.TABLA_ETIQUETA_DE_DIRECCION_PAQUETE, id);
			return O != null;
			}
		public BDAdmin crearTablaDireccionDePaquete_MD(){
			 this.BD.crearTablaYBorrarSiExiste(DireccionDePaquete_MD.TABLA_DIRECCION_DE_PAQUETE
							,DireccionDePaquete_MD.COLUMNA_URL,500
							,DireccionDePaquete_MD.COLUMNA_SELECCIONADA,TipoDeDatoSQL.BOOLEAN
							,DireccionDePaquete_MD.COLUMNA_TIPO_DE_DESTINO,50
							,DireccionDePaquete_MD.COLUMNA_SECCION,50
							);
			return this;
		}
		public BDAdmin crearTablaDireccionDePaquete_MDSiNoExiste(){
			 this.BD.crearTablaSiNoExiste(DireccionDePaquete_MD.TABLA_DIRECCION_DE_PAQUETE
							,DireccionDePaquete_MD.COLUMNA_URL,500
							,DireccionDePaquete_MD.COLUMNA_SELECCIONADA,TipoDeDatoSQL.BOOLEAN
							,DireccionDePaquete_MD.COLUMNA_TIPO_DE_DESTINO,50
							,DireccionDePaquete_MD.COLUMNA_SECCION,50
							);
			return this;
		}
		public DireccionDePaquete_MD getDireccionDePaquete_MD_Args(Object[] listaDeArgumentos){
			return new DireccionDePaquete_MD(to_String(listaDeArgumentos[1])
					,toBool(listaDeArgumentos[2])
					,to_String(listaDeArgumentos[3])
					,to_String(listaDeArgumentos[4])
					,toInt(listaDeArgumentos[0])
					,this
					);
			}
		public Object[] __content_DireccionDePaquete_MD(DireccionDePaquete_MD direccion_de_paquete){
			Object[] lista = {new Object[]{DireccionDePaquete_MD.COLUMNA_URL,direccion_de_paquete.url}
				,new Object[]{DireccionDePaquete_MD.COLUMNA_SELECCIONADA,direccion_de_paquete.seleccionada}
				,new Object[]{DireccionDePaquete_MD.COLUMNA_TIPO_DE_DESTINO,direccion_de_paquete.tipo_de_destino}
				,new Object[]{DireccionDePaquete_MD.COLUMNA_SECCION,direccion_de_paquete.seccion}
				};
			return lista;
			}
		public DireccionDePaquete_MD getDireccionDePaquete_MD_id(int id){
			Object[] O = this.BD.select_forID(DireccionDePaquete_MD.TABLA_DIRECCION_DE_PAQUETE, id);
			if (O == null){
				return null;}
			return this.getDireccionDePaquete_MD_Args(O);
			}
		public DireccionDePaquete_MD insertarDireccionDePaquete_MD(DireccionDePaquete_MD direccion_de_paquete){
			if (direccion_de_paquete.idkey==-1){
				int id=this.BD.insertar(DireccionDePaquete_MD.TABLA_DIRECCION_DE_PAQUETE
						,direccion_de_paquete.url
						,direccion_de_paquete.seleccionada
						,direccion_de_paquete.tipo_de_destino
						,direccion_de_paquete.seccion
						).id;
				return this.getDireccionDePaquete_MD_id(id);
			}else{
				this.BD.insertar_SinIdAutomatico(DireccionDePaquete_MD.TABLA_DIRECCION_DE_PAQUETE,direccion_de_paquete.idkey
						,direccion_de_paquete.url
						,direccion_de_paquete.seleccionada
						,direccion_de_paquete.tipo_de_destino
						,direccion_de_paquete.seccion
						);
				return this.getDireccionDePaquete_MD_id(direccion_de_paquete.idkey);}
			}
		public List<DireccionDePaquete_MD> getDireccionDePaquete_MD_All(){
				List<DireccionDePaquete_MD> lista=new List<DireccionDePaquete_MD>();
				Object [][]O=this.BD.select_Todo(DireccionDePaquete_MD.TABLA_DIRECCION_DE_PAQUETE);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getDireccionDePaquete_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public DireccionDePaquete_MD updateDireccionDePaquete_MD(DireccionDePaquete_MD direccion_de_paquete){
				this.BD.update_Id(DireccionDePaquete_MD.TABLA_DIRECCION_DE_PAQUETE,direccion_de_paquete.idkey
							 , DireccionDePaquete_MD.COLUMNA_URL , direccion_de_paquete.url
							 , DireccionDePaquete_MD.COLUMNA_SELECCIONADA , direccion_de_paquete.seleccionada
							 , DireccionDePaquete_MD.COLUMNA_TIPO_DE_DESTINO , direccion_de_paquete.tipo_de_destino
							 , DireccionDePaquete_MD.COLUMNA_SECCION , direccion_de_paquete.seccion);
				return getDireccionDePaquete_MD_id(direccion_de_paquete.idkey);
		}
		public void deleteDireccionDePaquete_MD_ForId(int id){
				this.BD.delete_id(DireccionDePaquete_MD.TABLA_DIRECCION_DE_PAQUETE,id);
		}
		public void deleteDireccionDePaquete_MD_ForId(DireccionDePaquete_MD direccion_de_paquete){
				deleteDireccionDePaquete_MD_ForId(direccion_de_paquete.idkey);
		}
		public bool existeDireccionDePaquete_MD_id(int id){
			Object[] O = this.BD.select_forID(DireccionDePaquete_MD.TABLA_DIRECCION_DE_PAQUETE, id);
			return O != null;
			}
		public void deleteDireccionDePaquete_MD_ForId_CASCADE(int idkey_direccion_de_paquete){
			deleteDireccionDePaquete_MD_ForId_CASCADE(idkey_direccion_de_paquete,null);
		}
		public void deleteDireccionDePaquete_MD_ForId_CASCADE(int idkey_direccion_de_paquete,Object modeloQueLoLlamo){
			DireccionDePaquete_MD direccion_de_paquete=getDireccionDePaquete_MD_id(idkey_direccion_de_paquete);
			deleteEtiquetaDeDireccionPaquete_MD_For_Idkey_direccion_de_paquete(idkey_direccion_de_paquete);
			deleteDireccionDePaquete_MD_ForId(idkey_direccion_de_paquete);
		}
		public BDAdmin crearTablaFile_MD(){
			 this.BD.crearTablaYBorrarSiExiste(File_MD.TABLA_FILE
							,File_MD.COLUMNA_NOMBRE,50
							,File_MD.COLUMNA_TIPO,50
							);
			return this;
		}
		public BDAdmin crearTablaFile_MDSiNoExiste(){
			 this.BD.crearTablaSiNoExiste(File_MD.TABLA_FILE
							,File_MD.COLUMNA_NOMBRE,50
							,File_MD.COLUMNA_TIPO,50
							);
			return this;
		}
		public File_MD getFile_MD_Args(Object[] listaDeArgumentos){
			return new File_MD(to_String(listaDeArgumentos[1])
					,to_String(listaDeArgumentos[2])
					,toInt(listaDeArgumentos[0])
					,this
					);
			}
		public Object[] __content_File_MD(File_MD file){
			Object[] lista = {new Object[]{File_MD.COLUMNA_NOMBRE,file.nombre}
				,new Object[]{File_MD.COLUMNA_TIPO,file.tipo}
				};
			return lista;
			}
		public File_MD getFile_MD_id(int id){
			Object[] O = this.BD.select_forID(File_MD.TABLA_FILE, id);
			if (O == null){
				return null;}
			return this.getFile_MD_Args(O);
			}
		public File_MD insertarFile_MD(File_MD file){
			if (file.idkey==-1){
				int id=this.BD.insertar(File_MD.TABLA_FILE
						,file.nombre
						,file.tipo
						).id;
				return this.getFile_MD_id(id);
			}else{
				this.BD.insertar_SinIdAutomatico(File_MD.TABLA_FILE,file.idkey
						,file.nombre
						,file.tipo
						);
				return this.getFile_MD_id(file.idkey);}
			}
		public List<File_MD> getFile_MD_All(){
				List<File_MD> lista=new List<File_MD>();
				Object [][]O=this.BD.select_Todo(File_MD.TABLA_FILE);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getFile_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public File_MD updateFile_MD(File_MD file){
				this.BD.update_Id(File_MD.TABLA_FILE,file.idkey
							 , File_MD.COLUMNA_NOMBRE , file.nombre
							 , File_MD.COLUMNA_TIPO , file.tipo);
				return getFile_MD_id(file.idkey);
		}
		public void deleteFile_MD_ForId(int id){
				this.BD.delete_id(File_MD.TABLA_FILE,id);
		}
		public void deleteFile_MD_ForId(File_MD file){
				deleteFile_MD_ForId(file.idkey);
		}
		public bool existeFile_MD_id(int id){
			Object[] O = this.BD.select_forID(File_MD.TABLA_FILE, id);
			return O != null;
			}
		public void deleteFile_MD_ForId_CASCADE(int idkey_file){
			deleteFile_MD_ForId_CASCADE(idkey_file,null);
		}
		public void deleteFile_MD_ForId_CASCADE(int idkey_file,Object modeloQueLoLlamo){
			File_MD file=getFile_MD_id(idkey_file);
			deleteArchivoInterno_MD_For_Idkey_id_carpeta_padre(idkey_file);
			deleteCarpetasPaquete_MD_For_Idkey_file(idkey_file);
			deleteFile_MD_ForId(idkey_file);
		}
		public BDAdmin crearTablaArchivoInterno_MD(){
			 this.BD.crearTablaYBorrarSiExiste(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO
							,ArchivoInterno_MD.COLUMNA_ID_CARPETA_PADRE,TipoDeDatoSQL.INTEGER
							,ArchivoInterno_MD.COLUMNA_ID_FILE_HIJO,TipoDeDatoSQL.INTEGER
							);
			return this;
		}
		public BDAdmin crearTablaArchivoInterno_MDSiNoExiste(){
			 this.BD.crearTablaSiNoExiste(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO
							,ArchivoInterno_MD.COLUMNA_ID_CARPETA_PADRE,TipoDeDatoSQL.INTEGER
							,ArchivoInterno_MD.COLUMNA_ID_FILE_HIJO,TipoDeDatoSQL.INTEGER
							);
			return this;
		}
		public ArchivoInterno_MD getArchivoInterno_MD_Args(Object[] listaDeArgumentos){
			return new ArchivoInterno_MD(toInt(listaDeArgumentos[1])
					,toInt(listaDeArgumentos[2])
					,toInt(listaDeArgumentos[0])
					,this
					);
			}
		public Object[] __content_ArchivoInterno_MD(ArchivoInterno_MD archivo_interno){
			Object[] lista = {new Object[]{ArchivoInterno_MD.COLUMNA_ID_CARPETA_PADRE,archivo_interno.idkey_id_carpeta_padre}
				,new Object[]{ArchivoInterno_MD.COLUMNA_ID_FILE_HIJO,archivo_interno.idkey_id_file_hijo}
				};
			return lista;
			}
		public ArchivoInterno_MD getArchivoInterno_MD_id(int id){
			Object[] O = this.BD.select_forID(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO, id);
			if (O == null){
				return null;}
			return this.getArchivoInterno_MD_Args(O);
			}
		public ArchivoInterno_MD insertarArchivoInterno_MD(ArchivoInterno_MD archivo_interno){
			if (archivo_interno.idkey==-1){
				int id=this.BD.insertar(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO
						,archivo_interno.idkey_id_carpeta_padre
						,archivo_interno.idkey_id_file_hijo
						).id;
				return this.getArchivoInterno_MD_id(id);
			}else{
				this.BD.insertar_SinIdAutomatico(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO,archivo_interno.idkey
						,archivo_interno.idkey_id_carpeta_padre
						,archivo_interno.idkey_id_file_hijo
						);
				return this.getArchivoInterno_MD_id(archivo_interno.idkey);}
			}
		public List<ArchivoInterno_MD> getArchivoInterno_MD_All(){
				List<ArchivoInterno_MD> lista=new List<ArchivoInterno_MD>();
				Object [][]O=this.BD.select_Todo(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getArchivoInterno_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public ArchivoInterno_MD updateArchivoInterno_MD(ArchivoInterno_MD archivo_interno){
				this.BD.update_Id(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO,archivo_interno.idkey
							 , ArchivoInterno_MD.COLUMNA_ID_CARPETA_PADRE , archivo_interno.idkey_id_carpeta_padre
							 , ArchivoInterno_MD.COLUMNA_ID_FILE_HIJO , archivo_interno.idkey_id_file_hijo);
				return getArchivoInterno_MD_id(archivo_interno.idkey);
		}
		public void deleteArchivoInterno_MD_ForId(int id){
				this.BD.delete_id(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO,id);
		}
		public void deleteArchivoInterno_MD_ForId(ArchivoInterno_MD archivo_interno){
				deleteArchivoInterno_MD_ForId(archivo_interno.idkey);
		}
		public bool existeArchivoInterno_MD_id(int id){
			Object[] O = this.BD.select_forID(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO, id);
			return O != null;
			}
		public bool existeArchivoInterno_MD(int idkey_id_carpeta_padre,int idkey_id_file_hijo){
				return this.BD.existe(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO
						,ArchivoInterno_MD.COLUMNA_ID_CARPETA_PADRE,idkey_id_carpeta_padre
						,ArchivoInterno_MD.COLUMNA_ID_FILE_HIJO,idkey_id_file_hijo);
		}
		public BDAdmin crearTablaCarpetasPaquete_MD(){
			 this.BD.crearTablaYBorrarSiExiste(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE
							,CarpetasPaquete_MD.COLUMNA_ID_TABLA_FILE,TipoDeDatoSQL.INTEGER
							,CarpetasPaquete_MD.COLUMNA_MARCA,50
							,CarpetasPaquete_MD.COLUMNA_FECHA,TipoDeDatoSQL.DATE
							);
			return this;
		}
		public BDAdmin crearTablaCarpetasPaquete_MDSiNoExiste(){
			 this.BD.crearTablaSiNoExiste(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE
							,CarpetasPaquete_MD.COLUMNA_ID_TABLA_FILE,TipoDeDatoSQL.INTEGER
							,CarpetasPaquete_MD.COLUMNA_MARCA,50
							,CarpetasPaquete_MD.COLUMNA_FECHA,TipoDeDatoSQL.DATE
							);
			return this;
		}
		public CarpetasPaquete_MD getCarpetasPaquete_MD_Args(Object[] listaDeArgumentos){
			return new CarpetasPaquete_MD(toInt(listaDeArgumentos[1])
					,to_String(listaDeArgumentos[2])
					,toDate(listaDeArgumentos[3])
					,toInt(listaDeArgumentos[0])
					,this
					);
			}
		public Object[] __content_CarpetasPaquete_MD(CarpetasPaquete_MD carpetas_paquete){
			Object[] lista = {new Object[]{CarpetasPaquete_MD.COLUMNA_ID_TABLA_FILE,carpetas_paquete.idkey_file}
				,new Object[]{CarpetasPaquete_MD.COLUMNA_MARCA,carpetas_paquete.marca}
				,new Object[]{CarpetasPaquete_MD.COLUMNA_FECHA,carpetas_paquete.fecha}
				};
			return lista;
			}
		public CarpetasPaquete_MD getCarpetasPaquete_MD_id(int id){
			Object[] O = this.BD.select_forID(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE, id);
			if (O == null){
				return null;}
			return this.getCarpetasPaquete_MD_Args(O);
			}
		public CarpetasPaquete_MD insertarCarpetasPaquete_MD(CarpetasPaquete_MD carpetas_paquete){
			if (carpetas_paquete.idkey==-1){
				int id=this.BD.insertar(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE
						,carpetas_paquete.idkey_file
						,carpetas_paquete.marca
						,carpetas_paquete.fecha
						).id;
				return this.getCarpetasPaquete_MD_id(id);
			}else{
				this.BD.insertar_SinIdAutomatico(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE,carpetas_paquete.idkey
						,carpetas_paquete.idkey_file
						,carpetas_paquete.marca
						,carpetas_paquete.fecha
						);
				return this.getCarpetasPaquete_MD_id(carpetas_paquete.idkey);}
			}
		public List<CarpetasPaquete_MD> getCarpetasPaquete_MD_All(){
				List<CarpetasPaquete_MD> lista=new List<CarpetasPaquete_MD>();
				Object [][]O=this.BD.select_Todo(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getCarpetasPaquete_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public CarpetasPaquete_MD updateCarpetasPaquete_MD(CarpetasPaquete_MD carpetas_paquete){
				this.BD.update_Id(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE,carpetas_paquete.idkey
							 , CarpetasPaquete_MD.COLUMNA_ID_TABLA_FILE , carpetas_paquete.idkey_file
							 , CarpetasPaquete_MD.COLUMNA_MARCA , carpetas_paquete.marca
							 , CarpetasPaquete_MD.COLUMNA_FECHA , carpetas_paquete.fecha);
				return getCarpetasPaquete_MD_id(carpetas_paquete.idkey);
		}
		public void deleteCarpetasPaquete_MD_ForId(int id){
				this.BD.delete_id(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE,id);
		}
		public void deleteCarpetasPaquete_MD_ForId(CarpetasPaquete_MD carpetas_paquete){
				deleteCarpetasPaquete_MD_ForId(carpetas_paquete.idkey);
		}
		public bool existeCarpetasPaquete_MD_id(int id){
			Object[] O = this.BD.select_forID(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE, id);
			return O != null;
			}
		public bool existeCarpetasPaquete_MD_Fecha(DateTime fecha){
				return this.BD.existe(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE
						,CarpetasPaquete_MD.COLUMNA_FECHA,fecha);
		}
		public bool existeCarpetasPaquete_MD_Marca(string marca){
				return this.BD.existe(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE
						,CarpetasPaquete_MD.COLUMNA_MARCA,marca);
		}
		public bool existeCarpetasPaquete_MD_Fecha_Marca(DateTime fecha,string marca){
				return this.BD.existe(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE
						,CarpetasPaquete_MD.COLUMNA_FECHA,fecha
						,CarpetasPaquete_MD.COLUMNA_MARCA,marca);
		}
		public void crearTodasLasTablas(){
			crearTablaDireccion_MD();
			crearTablaEtiquetaDeDireccionPaquete_MD();
			crearTablaDireccionDePaquete_MD();
			crearTablaFile_MD();
			crearTablaArchivoInterno_MD();
			crearTablaCarpetasPaquete_MD();
		}
		public void crearTodasLasTablasSiNoExisten(){
			crearTablaDireccion_MDSiNoExiste();
			crearTablaEtiquetaDeDireccionPaquete_MDSiNoExiste();
			crearTablaDireccionDePaquete_MDSiNoExiste();
			crearTablaFile_MDSiNoExiste();
			crearTablaArchivoInterno_MDSiNoExiste();
			crearTablaCarpetasPaquete_MDSiNoExiste();
		}
		public List<Direccion_MD> getDireccion_MD_All_Tipo_de_ubicacion_de_seccion(string tipo_de_ubicacion_de_seccion){
				List<Direccion_MD> lista=new List<Direccion_MD>();
				Object [][]O=this.BD.select_Where(Direccion_MD.TABLA_DIRECCION,Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,tipo_de_ubicacion_de_seccion);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getDireccion_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public void deleteDireccion_MD_For_Tipo_de_ubicacion_de_seccion(string tipo_de_ubicacion_de_seccion){
				this.BD.delete(Direccion_MD.TABLA_DIRECCION,Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,tipo_de_ubicacion_de_seccion);
		}
		public List<Direccion_MD> getDireccion_MD_All_Tipo_de_ubicacion_de_seccion_Seccion(string tipo_de_ubicacion_de_seccion,string seccion){
				List<Direccion_MD> lista=new List<Direccion_MD>();
				Object [][]O=this.BD.select_Where(Direccion_MD.TABLA_DIRECCION,Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,tipo_de_ubicacion_de_seccion,Direccion_MD.COLUMNA_SECCION,seccion);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getDireccion_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public List<Direccion_MD> getDireccion_MD_All_Tipo_de_ubicacion_de_seccion_Seccion_Categoria(string tipo_de_ubicacion_de_seccion,string seccion,string categoria){
				List<Direccion_MD> lista=new List<Direccion_MD>();
				Object [][]O=this.BD.select_Where(Direccion_MD.TABLA_DIRECCION,Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,tipo_de_ubicacion_de_seccion,Direccion_MD.COLUMNA_SECCION,seccion,Direccion_MD.COLUMNA_CATEGORIA,categoria);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getDireccion_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public List<Direccion_MD> getDireccion_MD_All_Tipo_de_ubicacion_de_seccion_Categoria(string tipo_de_ubicacion_de_seccion,string categoria){
				List<Direccion_MD> lista=new List<Direccion_MD>();
				Object [][]O=this.BD.select_Where(Direccion_MD.TABLA_DIRECCION,Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,tipo_de_ubicacion_de_seccion,Direccion_MD.COLUMNA_CATEGORIA,categoria);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getDireccion_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public Direccion_MD getDireccion_MD_For_Url_Tipo_de_ubicacion_de_seccion_Seccion_Categoria(string url,string tipo_de_ubicacion_de_seccion,string seccion,string categoria){
				List<Direccion_MD> lista=new List<Direccion_MD>();
				Object []O=this.BD.select_Where_FirstRow(Direccion_MD.TABLA_DIRECCION,Direccion_MD.COLUMNA_URL,url,Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,tipo_de_ubicacion_de_seccion,Direccion_MD.COLUMNA_SECCION,seccion,Direccion_MD.COLUMNA_CATEGORIA,categoria);
				if (O!=null){
					return getDireccion_MD_Args(O);
				}
				return null;
		}
		public void deleteDireccion_MD_For_Tipo_de_ubicacion_de_seccion_Seccion(string tipo_de_ubicacion_de_seccion,string seccion){
				this.BD.delete(Direccion_MD.TABLA_DIRECCION,Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,tipo_de_ubicacion_de_seccion,Direccion_MD.COLUMNA_SECCION,seccion);
		}
		public void deleteDireccion_MD_For_Tipo_de_ubicacion_de_seccion_Seccion_Categoria(string tipo_de_ubicacion_de_seccion,string seccion,string categoria){
				this.BD.delete(Direccion_MD.TABLA_DIRECCION,Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,tipo_de_ubicacion_de_seccion,Direccion_MD.COLUMNA_SECCION,seccion,Direccion_MD.COLUMNA_CATEGORIA,categoria);
		}
		public void deleteDireccion_MD_For_Tipo_de_ubicacion_de_seccion_Categoria(string tipo_de_ubicacion_de_seccion,string categoria){
				this.BD.delete(Direccion_MD.TABLA_DIRECCION,Direccion_MD.COLUMNA_TIPO_DE_UBICACION_DE_SECCION,tipo_de_ubicacion_de_seccion,Direccion_MD.COLUMNA_CATEGORIA,categoria);
		}
		public List<EtiquetaDeDireccionPaquete_MD> getEtiquetaDeDireccionPaquete_MD_All_Idkey_direccion_de_paquete(int idkey_direccion_de_paquete){
				List<EtiquetaDeDireccionPaquete_MD> lista=new List<EtiquetaDeDireccionPaquete_MD>();
				Object [][]O=this.BD.select_Where(EtiquetaDeDireccionPaquete_MD.TABLA_ETIQUETA_DE_DIRECCION_PAQUETE,EtiquetaDeDireccionPaquete_MD.COLUMNA_ID_TABLA_DIRECCION_DE_PAQUETE,idkey_direccion_de_paquete);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getEtiquetaDeDireccionPaquete_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public List<EtiquetaDeDireccionPaquete_MD> getEtiquetaDeDireccionPaquete_MD_All_Idkey_direccion_de_paquete(DireccionDePaquete_MD direccion_de_paquete){
				return getEtiquetaDeDireccionPaquete_MD_All_Idkey_direccion_de_paquete(direccion_de_paquete.idkey);
		}
		public void deleteEtiquetaDeDireccionPaquete_MD_For_Idkey_direccion_de_paquete(int idkey_direccion_de_paquete){
				this.BD.delete(EtiquetaDeDireccionPaquete_MD.TABLA_ETIQUETA_DE_DIRECCION_PAQUETE,EtiquetaDeDireccionPaquete_MD.COLUMNA_ID_TABLA_DIRECCION_DE_PAQUETE,idkey_direccion_de_paquete);
		}
		public void deleteEtiquetaDeDireccionPaquete_MD_For_Idkey_direccion_de_paquete(DireccionDePaquete_MD direccion_de_paquete){
				deleteEtiquetaDeDireccionPaquete_MD_For_Idkey_direccion_de_paquete(direccion_de_paquete.idkey);
		}
		public List<DireccionDePaquete_MD> getDireccionDePaquete_MD_All_Seccion(string seccion){
				List<DireccionDePaquete_MD> lista=new List<DireccionDePaquete_MD>();
				Object [][]O=this.BD.select_Where(DireccionDePaquete_MD.TABLA_DIRECCION_DE_PAQUETE,DireccionDePaquete_MD.COLUMNA_SECCION,seccion);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getDireccionDePaquete_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public void deleteDireccionDePaquete_MD_For_Seccion(string seccion){
				this.BD.delete(DireccionDePaquete_MD.TABLA_DIRECCION_DE_PAQUETE,DireccionDePaquete_MD.COLUMNA_SECCION,seccion);
		}
		public void deleteDireccionDePaquete_MD_For_Seccion_CASCADE(string seccion){
			deleteDireccionDePaquete_MD_For_Seccion_CASCADE(seccion,null);
		}
		public void deleteDireccionDePaquete_MD_For_Seccion_CASCADE(string seccion,Object modeloQueLoLlamo){
			List<DireccionDePaquete_MD> l=getDireccionDePaquete_MD_All_Seccion(seccion);
			for(int i=0;i<l.Count;i++){
				DireccionDePaquete_MD direccion_de_paquete=l[i];
				deleteEtiquetaDeDireccionPaquete_MD_For_Idkey_direccion_de_paquete(direccion_de_paquete.idkey);
			}
			deleteDireccionDePaquete_MD_For_Seccion(seccion);
		}
		public List<ArchivoInterno_MD> getArchivoInterno_MD_All_Idkey_id_carpeta_padre(int idkey_id_carpeta_padre){
				List<ArchivoInterno_MD> lista=new List<ArchivoInterno_MD>();
				Object [][]O=this.BD.select_Where(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO,ArchivoInterno_MD.COLUMNA_ID_CARPETA_PADRE,idkey_id_carpeta_padre);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getArchivoInterno_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public List<ArchivoInterno_MD> getArchivoInterno_MD_All_Idkey_id_carpeta_padre(File_MD file){
				return getArchivoInterno_MD_All_Idkey_id_carpeta_padre(file.idkey);
		}
		public void deleteArchivoInterno_MD_For_Idkey_id_carpeta_padre(int idkey_id_carpeta_padre){
				this.BD.delete(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO,ArchivoInterno_MD.COLUMNA_ID_CARPETA_PADRE,idkey_id_carpeta_padre);
		}
		public void deleteArchivoInterno_MD_For_Idkey_id_carpeta_padre(File_MD file){
				deleteArchivoInterno_MD_For_Idkey_id_carpeta_padre(file.idkey);
		}
		public ArchivoInterno_MD getArchivoInterno_MD_For_Idkey_id_file_hijo(int idkey_id_file_hijo){
				Object []O=this.BD.select_Where_FirstRow(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO,ArchivoInterno_MD.COLUMNA_ID_FILE_HIJO,idkey_id_file_hijo);
				if (O!=null){
					return getArchivoInterno_MD_Args(O);
				}
				return null;
		}
		public ArchivoInterno_MD getArchivoInterno_MD_For_Idkey_id_file_hijo(File_MD file){
				return getArchivoInterno_MD_For_Idkey_id_file_hijo(file.idkey);
		}
		public List<ArchivoInterno_MD> getArchivoInterno_MD_All_Idkey_id_carpeta_padre_Sort_Idkey_id_carpeta_padre(int idkey_id_carpeta_padre){
				List<ArchivoInterno_MD> lista=new List<ArchivoInterno_MD>();
				Object [][]O=this.BD.select_Where_ORDER_BY(ArchivoInterno_MD.TABLA_ARCHIVO_INTERNO,
					new Object []{
						ArchivoInterno_MD.COLUMNA_ID_CARPETA_PADRE,idkey_id_carpeta_padre
					}
						,ArchivoInterno_MD.COLUMNA_ID_CARPETA_PADRE);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getArchivoInterno_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public List<CarpetasPaquete_MD> getCarpetasPaquete_MD_All_Idkey_file(int idkey_file){
				List<CarpetasPaquete_MD> lista=new List<CarpetasPaquete_MD>();
				Object [][]O=this.BD.select_Where(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE,CarpetasPaquete_MD.COLUMNA_ID_TABLA_FILE,idkey_file);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getCarpetasPaquete_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public List<CarpetasPaquete_MD> getCarpetasPaquete_MD_All_Idkey_file(File_MD file){
				return getCarpetasPaquete_MD_All_Idkey_file(file.idkey);
		}
		public void deleteCarpetasPaquete_MD_For_Idkey_file(int idkey_file){
				this.BD.delete(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE,CarpetasPaquete_MD.COLUMNA_ID_TABLA_FILE,idkey_file);
		}
		public void deleteCarpetasPaquete_MD_For_Idkey_file(File_MD file){
				deleteCarpetasPaquete_MD_For_Idkey_file(file.idkey);
		}
		public void deleteCarpetasPaquete_MD_For_Marca(string marca){
				this.BD.delete(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE,CarpetasPaquete_MD.COLUMNA_MARCA,marca);
		}
		public void deleteCarpetasPaquete_MD_For_Fecha(DateTime fecha){
				this.BD.delete(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE,CarpetasPaquete_MD.COLUMNA_FECHA,fecha);
		}
		public void deleteCarpetasPaquete_MD_For_Fecha_Marca(DateTime fecha,string marca){
				this.BD.delete(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE,CarpetasPaquete_MD.COLUMNA_FECHA,fecha,CarpetasPaquete_MD.COLUMNA_MARCA,marca);
		}
		public List<CarpetasPaquete_MD> getCarpetasPaquete_MD_All_Marca_Sort_Marca(string marca){
				List<CarpetasPaquete_MD> lista=new List<CarpetasPaquete_MD>();
				Object [][]O=this.BD.select_Where_ORDER_BY(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE,
					new Object []{
						CarpetasPaquete_MD.COLUMNA_MARCA,marca
					}
						,CarpetasPaquete_MD.COLUMNA_MARCA);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getCarpetasPaquete_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public List<CarpetasPaquete_MD> getCarpetasPaquete_MD_All_Fecha_Sort_Fecha(DateTime fecha){
				List<CarpetasPaquete_MD> lista=new List<CarpetasPaquete_MD>();
				Object [][]O=this.BD.select_Where_ORDER_BY(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE,
					new Object []{
						CarpetasPaquete_MD.COLUMNA_FECHA,fecha
					}
						,CarpetasPaquete_MD.COLUMNA_FECHA);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getCarpetasPaquete_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public List<CarpetasPaquete_MD> getCarpetasPaquete_MD_All_Fecha_Marca_Sort_Fecha(DateTime fecha,string marca){
				List<CarpetasPaquete_MD> lista=new List<CarpetasPaquete_MD>();
				Object [][]O=this.BD.select_Where_ORDER_BY(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE,
					new Object []{
						CarpetasPaquete_MD.COLUMNA_FECHA,fecha
						,CarpetasPaquete_MD.COLUMNA_MARCA,marca
					}
						,CarpetasPaquete_MD.COLUMNA_FECHA);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getCarpetasPaquete_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public List<CarpetasPaquete_MD> getCarpetasPaquete_MD_All_Fecha_Marca_Sort_Marca(DateTime fecha,string marca){
				List<CarpetasPaquete_MD> lista=new List<CarpetasPaquete_MD>();
				Object [][]O=this.BD.select_Where_ORDER_BY(CarpetasPaquete_MD.TABLA_CARPETAS_PAQUETE,
					new Object []{
						CarpetasPaquete_MD.COLUMNA_FECHA,fecha
						,CarpetasPaquete_MD.COLUMNA_MARCA,marca
					}
						,CarpetasPaquete_MD.COLUMNA_MARCA);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getCarpetasPaquete_MD_Args(O[i]));
					}
				}
				return lista;
		}
	}
}
