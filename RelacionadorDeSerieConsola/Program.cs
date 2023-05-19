using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ReneUtiles;
using ReneUtiles.Clases;
using ReneUtiles.Clases.BD;
using ReneUtiles.Clases.BD.SesionEstorage;
using ReneUtiles.Clases.BD.Factory;
//using RelacionadorDeSerie.BD.Modelos;
using RelacionadorDeSerie;
using RelacionadorDeSerie.Representaciones;
//using System.IO;
using Delimon.Win32.IO;

using ReneUtiles.Clases.Multimedia.Paquetes;
using ReneUtiles.Clases.Multimedia.Paquetes.Representaciones;
using ReneUtiles.Clases.Multimedia.Relacionadores;
using ReneUtiles.Clases.Multimedia.Series;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using System.Text.RegularExpressions;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;

using RelacionadorDeSerie.BD.Modelos;

using System.Reflection;
using ReneUtiles.Clases.Multimedia.Series.Contextos;
using RelacionadorDeSerie.Privado;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores;
using ReneUtiles.Clases.Multimedia.Series.Representaciones;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos;
using System.Diagnostics;
using System.Windows;
//using Microsoft.VisualBasic.FileIO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Specialized;

using RelacionadorDeSerie.Privado.Consolas;
namespace RelacionadorDeSerieConsola
{
    class Program: UtilesMostrarEnConsola
    {
        static void Main(string[] args)
        {
           // crear_BDActualizador_CSharp();
            //provarCapitulos();
            endC();
        }

        //public static EsquemaBD getEsquema_BDActualizador2()
        //{
        //    int TAMAÑO_NOMBRE = 50;
        //    int TAMAÑO_TIPO_DE_MONEDA = 50;
        //    int TAMAÑO_NORMAL = 50;
        //    int TAMAÑO_TELEFONO = 50;
        //    int TAMAÑO_CORREO = 50;
        //    int TAMAÑO_URL = 500;




        //    ModeloBD_ID Direccion = new ModeloBD_ID("TABLA_DIRECCION");
        //    ColumnaDeModeloBD c_url_direccion = Direccion.addC("COLUMNA_URL", TAMAÑO_URL);
        //    ColumnaDeModeloBD c_seleccionada_direccion = Direccion.addC("COLUMNA_SELECCIONADA", TipoDeDatoSQL.BOOLEAN);
        //    ColumnaDeModeloBD c_tipo_de_destino_direccion = Direccion.addC("COLUMNA_TIPO_DE_DESTINO", TAMAÑO_NORMAL);
        //    ColumnaDeModeloBD c_ubicacion_direccion = Direccion.addC("COLUMNA_TIPO_DE_UBICACION_DE_SECCION", TAMAÑO_NORMAL);
        //    ColumnaDeModeloBD c_tipo_seccion_direccion = Direccion.addC("COLUMNA_SECCION", TAMAÑO_NORMAL);
        //    ColumnaDeModeloBD c_categoria_seccion_direccion = Direccion.addC("COLUMNA_CATEGORIA", TAMAÑO_NORMAL);

        //    Direccion.addBuscarListaPor(c_ubicacion_direccion);
        //    Direccion.addBuscarListaPor(c_ubicacion_direccion, c_tipo_seccion_direccion);
        //    Direccion.addBuscarListaPor(c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);
        //    Direccion.addBuscarListaPor(c_ubicacion_direccion, c_categoria_seccion_direccion);
        //    //			
        //    Direccion.addDeletePor(c_ubicacion_direccion);
        //    Direccion.addDeletePor(c_ubicacion_direccion, c_tipo_seccion_direccion);
        //    Direccion.addDeletePor(c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);
        //    Direccion.addDeletePor(c_ubicacion_direccion, c_categoria_seccion_direccion);

        //    Direccion.addExiste(c_url_direccion, c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);
        //    Direccion.addBuscarPor(c_url_direccion, c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);

        //    EsquemaBD e = new EsquemaBD();
        //    e.addModelo(Direccion);




        //    return e;
        //}
        //public static void crear_BDActualizador_CSharp()
        //{
        //    EsquemaBD e = getEsquema_BDActualizador2();


        //    FactoryBD f = new FactoryBD(e);
        //    f.addConexion_SQLite(
        //        nombreBDAdmin:"BDAdmin"
        //        ,direccionBDSqlite: "bdActualizadoor.sqlite"
        //        );
        //    //f.DireccionBDSqlite = "bdActualizadoor.sqlite";
        //    f.DireccionPaquete = "RelacionadorDeSerie.BD.Modelos";
        //    //f.TipoDeConexion = TipoDeConexionBD.SQL_LITE;
        //    f.crearCodigoCSharp(new DirectoryInfo(@"D:\_Cosas\Programacion\Proyectos\C#\RelacionadorDeSerieConsola\RelacionadorDeSerie\BD\Modelos"));
        //    cwl("termino");
        //}

        public static void provarCapitulos()
        {
            cwl("Comenzando...");
            ManagerRelacionadorDeSerie mng = new ManagerRelacionadorDeSerie();
            ManagerDeSeries mngS = mng.animes;



            ContextoDeConjuntoDeSeries cx = new ContextoDeConjuntoDeSeries();
            ContextoDeSerie cxs = new ContextoDeSerie();



            //			cx.add_caracteristicaDeLosCapitulosAnalizados(
            //				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO);

            //string url = @"C:\_COSAS\temporal\mangas\Dungeon ni Deai wo Motomeru no wa Machigatteiru Darou ka IV Episodio 12.mp4";
            //string url = @"C:\_COSAS\temporal\contenidos\manga\Anime Online [Transmision]";
            //string url = @"C:\Users\Rene\Desktop\Nueva carpeta (2)\salida\Series";
            //cx.ConjuntoDeNombres = new string[]{ "One Piece" };
            //cx.MarcasDeSerie = new string[]{ "onepiece" };

            //			string url = @"C:\Users\Rene\Desktop\Nueva carpeta (2)\salida\Bleach";
            //			cx.ConjuntoDeNombres = new string[]{ "Bleach" };
            //			cx.MarcasDeSerie = new string[]{ "bleach" };



            //cxs.IndiceConjuntoDeNombres = 0;



            List<DatosDeNombreCapitulo> ld = new List<DatosDeNombreCapitulo>();





            FileInfo dd = new FileInfo(@"C:\_COSAS\temporal\mangas\Dungeon ni Deai wo Motomeru no wa Machigatteiru Darou ka IV Episodio 12.mp4");
            cxs.Url = dd.ToString();
            //cxs.IndiceExtencion = dd.Name.LastIndexOf(".");
            cxs.EsVideo = true;
            cxs.EsCarpeta = false;
            cxs.EsArchivo = true;
            cwl("dd=" + dd.ToString());


            FileSystemInfo f = dd;
            cxs.Url = f.ToString();
            string nombre = f is DirectoryInfo || (!(UtilesVideos.esVideo(f.ToString()) || UtilesVideos.esSubtitulo(f.ToString()))) ? f.Name : Archivos.getNombre(new FileInfo(f.ToString()));


            ProcesadorDeNombreDeSerie pro = mngS.getProcesadorDeSerie(cx, cxs, nombre);
            DatosDeNombreCapitulo dn = pro.getCapitulosDeNombre(); ;
            mostrarDatosDeNombreCapitulo(dn, dd.ToString(), nombre);


            //{(?i:(?:(?:[^\w]|_)*)(?:(?<!(?:(?![_])\w))(?<fecha>(?:(?<dia>(?:1[3-9])|(?:2[0-9])|(?:3[0-1]))(?<separacion_fecha>[-_. ])(?<mes>(?:0?[0-9])|(?:1[0-2]))\k<separacion_fecha>(?<anno>(?:19[5-9][0-9])|(?:20[0-9][0-9])))|(?:(?<mes>(?:0?[0-9])|(?:1[0-2]))(?<separacion_fecha>[-_. ])(?<dia>(?:1[3-9])|(?:2[0-9])|(?:3[0-1]))\k<separacion_fecha>(?<anno>(?:19[5-9][0-9])|(?:20[0-9][0-9])))|(?:(?<dia>(?:[0-2]?[0-9])|(?:3[0-1]))(?<separacion_fecha>[-_. ])(?<mes>(?:0?[0-9])|(?:1[0-2]))\k<separacion_fecha>(?<anno>(?:19[5-9][0-9])|(?:20[0-9][0-9])))|(?:(?<envol_ini_fecha>[[]|[(]|[{])(?<anno>(?:19[5-9][0-9])|(?:20[0-9][0-9]))(?<envol_ini_fecha>[]]|[)]|[}]))|(?:(?<anno>(?:19[5-9][0-9])|(?:20[0-9][0-9]))))(?!(?:(?![_])\w)))(?:(?:[^\w]|_)*))}

            cwl("termino");

        }
    }
}
