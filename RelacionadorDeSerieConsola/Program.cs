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
using Microsoft.VisualBasic.FileIO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Specialized;

namespace RelacionadorDeSerieConsola
{
    class Program : ConsolaBasica
    {
        static void Main(string[] args)
        {
            //provarAnalizarDePaquete();
            crear_BDActualizador_CSharp();
            //probarBDActualize();
            //recrearContenidDeCarpetasConTXT();
            //probarExpreciones();
            //provarEventos();
            //pruebaComparacion();
            //provarCapitulos();
            //provarCMD();
            //provarHarware();
            //provarSQL();
            //provarCopiador();
            //provarCMD();
            //provarCosasConsola();
            agregarARchivosAlPortapales();
            provarCopiador3();
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);

            
            
        }
        [STAThread]
        public static void agregarARchivosAlPortapales() {
            StringCollection paths = new StringCollection();
            paths.Add(@"D:\_Cosas\Temporal\ANÁLISIS del DOCK OFICIAL de STEAM DECK - tiene de TODO pero ¿NECESITAS todo lo que tiene.mp4");
            //paths.Add("c:\\temp\\test2.txt");
            Clipboard.Clear(); //Para borrar lo que esté en el Clipboard
            Clipboard.SetFileDropList(paths);
        }


        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public static void provarCopiador3()
        {
            //abre una ventana del explorer en la ruta indicada
            var procceso = Process.Start("explorer.exe", @"C:\_COSAS\temporal\experimentos\b");
            Thread.Sleep(1000); //En mi caso esto no era necesario pero lo puse por las dudas

            //En el mejor de los casos se debería haber podido usar procceso.MainWindowHandle, pero no tengo idea porque no resulta
            //La solución fue buscar la lista de procesos activos, y determinar por el título de la ventana a cual correspondia
            Process[] processes = Process.GetProcessesByName("explorer");

            foreach (Process proc in processes)
            {
                if (proc.MainWindowTitle == "b") //prueba es el título de mi ventana
                {
                    keybd_event(0xA2, 0x9d, 0, 0); //se mantiene la tecla control presionada
                    PostMessage(proc.MainWindowHandle, 0x0100, 86, 0); //se envía a la ventana la tecla V
                    Thread.Sleep(1000); //puede que esto tampoco sea necesario
                    keybd_event(0xA2, 0x9d, 0x0002, 0); //se suelta la tecla control
                }

               
            }
        }

        public static void provarCopiador2() {
            FileSystem.CopyFile(@"D:\_Cosas\Temporal\manga\S01E06.mkv", @"C:\_COSAS\temporal\experimentos\b\S01E06.mkv", UIOption.AllDialogs, UICancelOption.DoNothing);
        }

        public static void provarCosasConsola() {
            cwl(String.Format("{0:0.00}", 123.456));//%.2f
        }

        //public static void provarCopiador()
        //{
            

        //    bool cut = false;
        //    string[] files = { @"D:\_Cosas\Temporal\manga\Chainsaw Man Episodio 10 Sub Español — AnimeFLV.ts" };
        //    if (files != null)
        //    {
        //        IDataObject data = new DataObject(DataFormats.FileDrop, files);
        //        System.IO.MemoryStream memo = new System.IO.MemoryStream(4);
        //        byte[] bytes = new byte[] { (byte)(cut ? 2 : 5), 0, 0, 0 };
        //        memo.Write(bytes, 0, bytes.Length);
        //        data.SetData("Preferred DropEffect", memo);
        //        Clipboard.SetDataObject(data);
        //    }
        //    //cwl(SQLUtiles.select_Where("nombreTabla", "columna",2));
        //}

        public static void provarSQL()
        {
            cwl(SQLUtiles.select_Where("Consumo_Electrico", "cant_mensuales",200));
            //SELECT * FROM Consumo_Electrico WHERE cant_mensuales = '200'
        }
        public static void provarHarware()
        {
            cwl(UtilesHardware.GetMotherBoardID());
        }
        public static void provarCMD()
        {
            //string url = @"D:\_Cosas\Temporal\manga\APARI3NCIAS (Temporada 1) [08 Cap.] [1080p] [Dual Audio] FDT\S01E02.mkv";
            //Utiles.ejecutarCMD("call \""+ url+"\"");

            //string comando = "\"" + @"C:\Program Files\DAUM\PotPlayer\PotPlayerMini64.exe" + "\" \"" + @"D:\_Cosas\Temporal\manga\APARI3NCIAS (Temporada 1) [08 Cap.] [1080p] [Dual Audio] FDT\S01E02.mkv" + "\"";


            //Utiles.ejecutarCMD(comando);

            //ProcessStartInfo psi = new ProcessStartInfo();
            //psi.UseShellExecute = false;
            //psi.Arguments = "\"" + @"D:\_Cosas\Temporal\manga\APARI3NCIAS (Temporada 1) [08 Cap.] [1080p] [Dual Audio] FDT\S01E02.mkv" + "\""; //"-jar -XX:+UseConcMarkSweepGC -Xmx1024M -Xms1024M START.jar";
            //psi.CreateNoWindow = true;
            //psi.WindowStyle = ProcessWindowStyle.Hidden;
            //psi.FileName = @"C:\Program Files\DAUM\PotPlayer\PotPlayerMini64.exe";//"jre8\\bin\\javaw.exe";
            //Process.Start(psi);

            //Utiles.ejecutarCMD(@"C:\Program Files\DAUM\PotPlayer\PotPlayerMini64.exe", "\"" + @"D:\_Cosas\Temporal\manga\APARI3NCIAS (Temporada 1) [08 Cap.] [1080p] [Dual Audio] FDT\S01E02.mkv" + "\"");

            //Utiles.ejecutarCMD(@"C:\Windows\explorer.exe", "/select,\"" + @"D:\_Cosas\Temporal\manga\Tonikaku Kawaii- Seifuku Especial Sub Español Online Gratis.mp4" + "\"");
            //UtilesNavegador.abrirArchivo(@"D:\_Cosas\Temporal\manga\Tonikaku Kawaii- Seifuku Especial Sub Español Online Gratis.mp4");
            UtilesNavegador.abrirCarpeta(@"D:\_Cosas\Temporal\manga");
        }

        public static void provarRelacionador()
        {
            string ka = "monogatari";
            string kb = "clannadafterstory";
            RelacionadorDeNombresClaveSeries proR = new RelacionadorDeNombresClaveSeries();
            cwl(proR.estanRelacionados(ka, kb));


        }

        public static void provarCapitulos()
        {
            cwl("Comenzando...");
            ManagerRelacionadorDeSerie mng = new ManagerRelacionadorDeSerie();
            ManagerDeSeries mngS = mng.animes;



            ContextoDeConjuntoDeSeries cx = new ContextoDeConjuntoDeSeries();
            ContextoDeSerie cxs = new ContextoDeSerie();



            //			cx.add_caracteristicaDeLosCapitulosAnalizados(
            //				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO);

            string url = @"C:\_COSAS\temporal\contenidos\series\Series";
            //string url = @"C:\_COSAS\temporal\contenidos\manga\Anime Online [Transmision]";
            //string url = @"C:\Users\Rene\Desktop\Nueva carpeta (2)\salida\Series";
            //cx.ConjuntoDeNombres = new string[]{ "One Piece" };
            //cx.MarcasDeSerie = new string[]{ "onepiece" };

            //			string url = @"C:\Users\Rene\Desktop\Nueva carpeta (2)\salida\Bleach";
            //			cx.ConjuntoDeNombres = new string[]{ "Bleach" };
            //			cx.MarcasDeSerie = new string[]{ "bleach" };



            //cxs.IndiceConjuntoDeNombres = 0;



            List<DatosDeNombreCapitulo> ld = new List<DatosDeNombreCapitulo>();





            FileInfo dd = new FileInfo(@"F:\Paquete\Manga\Series Mangas [TX]\Urusei Yatsura (2022) Episodio 9.mp4");
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
        public static void mostrarConjuntoDeSeries(ConjuntoDeSeries cns)
        {
            cwl("cantidad de series: " + cns.series.Count);
            foreach (Serie s in cns.series)
            {
                mostrarSerie(s);
                cwl("..................................................  ...........................................................................................................");
            }
        }
        public static void mostrarSerie(Serie s)
        {
            HashSet<KeySerie> hk = s.getKeysDeSerie();
            //cwl("cantidad hk="+hk.Count);
            foreach (KeySerie k in hk)
            {
                cwl("Nombre Serie:" + k.Nombre);
                cwl("Clave Serie:" + k.Clave);
                cwl("Tipo:" + k.TipoDeSerie);
            }


            List<TemporadaDeSerie> lt = s.Temporadas;
            cwl("Cantidad de Temporadas=" + lt.Count);
            HashSet<int> numerosDeTemporda = new HashSet<int>();
            foreach (TemporadaDeSerie t in lt)
            {
                mostrarTemporada(t);
                cwl("***********************************************************************");
                numerosDeTemporda.Add(t.NumeroTemporada);
            }
            foreach (int nt in numerosDeTemporda)
            {
                cwl("nt " + nt);
            }
        }
        public static void mostrarListaDeCapitulos(List<RepresentacionDeCapitulo> lr)
        {
            foreach (RepresentacionDeCapitulo rc in lr)
            {
                if (rc is CapituloDeSerie)
                {
                    CapituloDeSerie cs = (CapituloDeSerie)rc;
                    cwl("Capitulo simple: " + cs.NumeroDeCapitulo);

                }
                else
                {
                    CapituloDeSerieMultiples csm = (CapituloDeSerieMultiples)rc;
                    cwl("Capitulos: " + csm.NumeroCapituloInicial + " - " + csm.NumeroCapituloFinal);
                }
                foreach (DatosDeFuente df in rc.Fuentes.getDatosDefuentes())
                {
                    foreach (DatosDeNombreSerie Dns in df.Ldns)
                    {
                        //						DatosNumericosDeNombreDeSerie Dns = df.Dns;
                        if (Dns is DatosDeNombreSerie)
                        {
                            mostrarDatosDeNombreSerie((DatosDeNombreSerie)Dns, df.Ctx.Url, new DirectoryInfo(df.Ctx.Url).Name);
                        }
                        else
                        {
                            mostrarDatosNumericosDeNombreDeSerie(Dns, df.Ctx.Url, new DirectoryInfo(df.Ctx.Url).Name);
                        }
                    }



                    if (df is DatosDeArchivoFisico)
                    {
                        DatosVideosConSubtitulos dv = ((DatosDeArchivoFisico)df).datosVideosConSubtitulos;
                        if (dv != null)
                        {
                            cwl("tieneVideos=" + dv.tieneVideos);
                            cwl("tieneSubtitulos=" + dv.tieneSubtitulos);
                            cwl("tieneSubtitulosTodos=" + dv.tieneSubtitulosTodos);
                            cwl("tieneSubtitulosAlMenosUno=" + dv.tieneSubtitulosAlMenosUno);
                            cwl("cantidadDeVideos=" + dv.cantidadDeVideos);
                            cwl("cantidadDeSubtitulos=" + dv.cantidadDeSubtitulos);
                        }

                    }
                    cwl("++++++++++++++++++++++++++++");
                }

                cwl("----------------------------");
            }

        }

        public static void mostrarTemporada(TemporadaDeSerie t)
        {
            //			if(t.NumeroTemporada==2){
            //				cwl();
            //			}
            cwl("NumeroTemporada=" + t.NumeroTemporada);
            cwl("Cantidad de Capitulos=" + t.Capitulos.Count);
            //t.Capitulos
            mostrarListaDeCapitulos(t.Capitulos);

            foreach (int n in t.setNumerosDeCapitulos)
            {
                cwl("n " + n);
            }
            cwl();
        }

        public static void mostrarDatosDeNombreSerie(DatosDeNombreSerie dn
                                                      , string url
                                                        , string nombreUrl
        // , FileSystemInfo f
        )
        {
            cwl("Nombre:" + dn.NombreAdaptado);
            cwl("Calve:" + dn.Clave);
            cwl("Tipo de nombre:" + dn.getTipoDeNombre());
            mostrarDatosNumericosDeNombreDeSerie(dn, url, nombreUrl);
        }

        public static void mostrarDatosNumericosDeNombreDeSerie(DatosNumericosDeNombreDeSerie Dns
                                                                 , string url
                                                        , string nombreUrl
        //, FileSystemInfo f
        )
        {
            DatosDeNombreSerie dn = null;
            if (Dns is DatosDeNombreSerie)
            {
                dn = (DatosDeNombreSerie)Dns;
            }
            cwl("datos del principio:");
            mostrarDatosDeNombreCapitulo(Dns.datosDelPrincipio, url, nombreUrl, dn);
            cwl("datos del final:");
            mostrarDatosDeNombreCapitulo(Dns.datosDelFinal, url, nombreUrl, dn);
        }


        public static void mostrarDatosDeNombreCapitulo(DatosDeNombreCapitulo d
                                                        // FileSystemInfo f,
                                                        , string url
                                                        , string nombreUrl
                                                         , DatosDeNombreSerie dn = null)
        {
            if (d != null)
            {
                string temporada = d.TieneTemporada ? " T=" + d.Temporada : "";
                string esSoloNumero = d.EsSoloNumeros ? " sn" : "";
                //string alFinal = temporada + esSoloNumero + " i=" + d.IndiceDeInicioDespuesDeLosNumeros;
                string alFinal = temporada + esSoloNumero; //+ " " + dn.Clave;
                if (dn != null)
                {
                    alFinal += " | " + dn.Clave + " | " + dn.NombreAdaptado;
                }
                alFinal += " " + d.TipoDeNombre;
                if (d.EsConjuntoDeCapitulos)
                {
                    cwl(d.CapituloInicial + " - " + d.CapituloFinal + alFinal);
                    //cwl(d.CapituloInicial + " - " + d.CapituloFinal + alFinal);
                }
                else
                {
                    if (d.EsContenedorDeTemporada)
                    {
                        //cwl("c " + alFinal);
                        cwl("c=" + d.CantidadDeCapitulosQueContiene + " " + alFinal);
                    }
                    else
                    {
                        cwl(d.Capitulo + alFinal);//+" :"
                                                  //cwl(d.Capitulo + alFinal);
                    }

                }
                //awStringIndices(5, f.Name);
                UtilesConsola.cwStringIndices(5, nombreUrl);
                //return !d.EsContenedorDeTemporada;
                cwl("fue capitulo =" + !d.EsContenedorDeTemporada);

            }
            else
            {
                cwl("null " + url);
                //cwl("null " + f.ToString());
            }
            //cwl("fue capitulo =" + false);

        }


        public static void pruebaComparacion()
        {
            ConjuntoDeEtiquetasDeSerie cn_1 = new ConjuntoDeEtiquetasDeSerie(TipoDeEtiquetaDeSerie.MANGAS, TipoDeEtiquetaDeSerie.TX);
            ConjuntoDeEtiquetasDeSerie cn_2 = new ConjuntoDeEtiquetasDeSerie(TipoDeEtiquetaDeSerie.TX, TipoDeEtiquetaDeSerie.MANGAS);

            HashSet<ConjuntoDeEtiquetasDeSerie> hs = ConjuntoDeEtiquetasDeSerie.getNewHashSet();
            hs.Add(cn_1);
            hs.Add(cn_2);
            cwl(hs.Count());
            Dictionary<ConjuntoDeEtiquetasDeSerie, string> dic = ConjuntoDeEtiquetasDeSerie.getNewDictionary<string>();
            dic.Add(cn_1, "a");
            cwl(dic.ContainsKey(cn_2));

        }
        public static void provarEventos()
        {
            Action<string> a = (v) => cwl("hola a" + v);

            //Type tExForm = a.GetType();
            //Assembly assem = tExForm.Assembly;
            //Object exFormAsObj = a;
            //EventInfo evClick = tExForm.GetEvent()


            Action<string> b = (v) => cwl("hola b" + v);
            Action<string> c = (v) => cwl("hola c" + v);

            //a.GetInvocationList().Ad;
            //a.DynamicInvoke();

            // a+= () => cwl("hola2");
            //a.Method.Invoke(a.GetInvocationList().ElementAt(0), new object[0]);
            //a.Method.Invoke(a.GetInvocationList().ElementAt(0), null);
            //a.GetInvocationList().ElementAt(0).Method.Invoke(a,new object[0]);

            //cwl(a.GetInvocationList());
            //Type ty = a.GetType();
            //MethodInfo[] metodos = ty.GetMethods();
            //cwl(metodos.Length);
            //cwl(a.GetType());
            //(from m in metodos select m.Name).ToList().ForEach(v=>cwl(v));
            //cwl(a.GetType().IsClass);

            ConjuntoDeEventos<Action<string>> cv = new ConjuntoDeEventos<Action<string>>();
            cv.add(a);
            cv.add(b);
            cv.add(c);
            cv.ejecutar("_eee");
            //cv.evento();


            //cv.ejecutar();
        }
        public static void probarExpreciones()
        {
            string m = @"^(?:\d+)(?:[.,]\d+)?$";
            string a = "123123";
            Match ma = new Regex(m).Match(a);
            cwl(ma.Success);
        }

        public static void probarBDActualize()
        {
            BDAdmin bd = new BDAdmin();
            TipoDeEtiquetaDeSerie[] etiquetas = {
                TipoDeEtiquetaDeSerie.PRINCIPAL_SERIES_PERSONA
                    , TipoDeEtiquetaDeSerie.TX };
            TipoDeSeccion seccion = TipoDeSeccion.SERIES;

            List<DireccionDePaquete_MD> lds = bd.getDireccionDePaquete_MD_All_Seccion(seccion.ToString());

            List<DireccionDePaquete_MD> ld = new List<DireccionDePaquete_MD>();
            foreach (DireccionDePaquete_MD d in lds)
            {
                List<EtiquetaDeDireccionPaquete_MD> le = d.getListaDe_EtiquetaDeDireccionPaquete_MD();
                Func<bool> tieneTodasLasEtiquetas = () =>
                {
                    if (le.Count() != etiquetas.Length)
                    {
                        return false;
                    }
                    foreach (TipoDeEtiquetaDeSerie te in etiquetas)
                    {
                        bool loEncontro = false;
                        foreach (EtiquetaDeDireccionPaquete_MD e in le)
                        {
                            if (te.ToString() == e.nombre)
                            {
                                loEncontro = true;
                                break;
                            }
                        }
                        if (!loEncontro)
                        {
                            return false;
                        }
                    }
                    return true;
                };
                if (tieneTodasLasEtiquetas())
                {

                    ld.Add(d);
                }
            }

            foreach (DireccionDePaquete_MD d in ld)
            {
                cwl(d.url);
            }
        }
        public static void crearBDActualize()
        {
            //int[] indiceActual = { 0 };
            int indiceActual = 0;
            Func<string> gurl = () => "C://Cosas/carpeta" + (indiceActual++);
            Func<int, string> gurli = (i) => "C://Cosas/carpeta" + i;
            object[] url_tx_anime = { gurli(0), false, TipoDeEtiquetaDeSerie.MANGAS, TipoDeEtiquetaDeSerie.TX };
            object[] urls_tx_series = { gurli(1),gurli(7),true
                    , TipoDeEtiquetaDeSerie.PRINCIPAL_SERIES_PERSONA, TipoDeEtiquetaDeSerie.TX };
            object[] urls_tx_series_clasicas = { gurli(2),true
                    , TipoDeEtiquetaDeSerie.PRINCIPAL_SERIES_PERSONA, TipoDeEtiquetaDeSerie.TX
                    ,TipoDeEtiquetaDeSerie.CLASICAS
            };
            object[] urls_tx_series_dobladas = { gurli(3),true
                    , TipoDeEtiquetaDeSerie.PRINCIPAL_SERIES_PERSONA, TipoDeEtiquetaDeSerie.TX
                    ,TipoDeEtiquetaDeSerie.DOBLADAS
            };

            object[] urls_finalizadas_series = { gurli(4),true
                    , TipoDeEtiquetaDeSerie.PRINCIPAL_SERIES_PERSONA, TipoDeEtiquetaDeSerie.FINALIZADAS };
            object[] urls_finalizadas_series_clasicas = { gurli(5),gurli(8),true
                    , TipoDeEtiquetaDeSerie.PRINCIPAL_SERIES_PERSONA, TipoDeEtiquetaDeSerie.FINALIZADAS
                    ,TipoDeEtiquetaDeSerie.CLASICAS
            };
            object[] urls_finalizadas_series_dobladas = { gurli(6),true
                    , TipoDeEtiquetaDeSerie.PRINCIPAL_SERIES_PERSONA , TipoDeEtiquetaDeSerie.FINALIZADAS
                    ,TipoDeEtiquetaDeSerie.DOBLADAS
            };

            object[] listas = {
                url_tx_anime,urls_tx_series,urls_tx_series_clasicas,urls_tx_series_dobladas
                ,urls_finalizadas_series,urls_finalizadas_series_clasicas,urls_finalizadas_series_dobladas
            };

            BDAdmin bd = new BDAdmin();
            bd.crearTodasLasTablasSiNoExisten();

            foreach (var urls in listas)
            {
                List<DireccionDePaquete_MD> ld = new List<DireccionDePaquete_MD>();
                List<string> lurs = new List<string>();

                foreach (var o in (object[])urls)
                {
                    if (o is string)
                    {
                        lurs.Add(o.ToString());
                    }
                    else if (o is bool)
                    {
                        foreach (string url in lurs)
                        {
                            ld.Add(new DireccionDePaquete_MD(bd
                                , url: url
                                , seleccionada: true
                                , tipo_de_destino: TipoDeDestino.CARPETA.ToString()
                                , seccion: (bool)o ? TipoDeSeccion.SERIES.ToString() : TipoDeSeccion.ANIME.ToString()).s()
                                );
                        }

                    }
                    else if (o is TipoDeEtiquetaDeSerie)
                    {
                        foreach (DireccionDePaquete_MD d in ld)
                        {
                            new EtiquetaDeDireccionPaquete_MD(bd
                            , nombre: o.ToString()
                            , direccion_de_paquete: d
                            ).s();
                        }


                    }
                }
            }

            cwl("termino");

        }
        public static void deslozar()
        {
            /*
             * (?i:^
             *      (?:(?:[^\w]|_)*)
             *      (?:
             *          (?:
             *              (?<g_prin_manga>
             *                  (?:Series(?:(?:[^\w]|_)+)Anime)
             *                  |(?:Anime(?:(?:[^\w]|_)+)Online)
             *                  |(?:Animados(?:(?:[^\w]|_)+)Mangas)
             *                  |(?:Series(?:(?:[^\w]|_)+)Mangas)
             *                  |(?:Mangas)
             *                  )
             *              )
             *          (?:(?<g_tx>(?:tx)|(?:En(?:(?:[^\w]|_)+)Transmision)|(?:Transmision)|(?:x(?:(?:[^\w]|_)+)Capitulos)))(?:(?:[^\w]|_)*))(?:(?:[^\w]|_)*)$)
             * 
             * 
             * 
             * 
             * 
             * 
             * 
             (?i:^
                (?:(?:[^\w]|_)*)
                (?:
                    (?:
                        (?<g_prin_manga>(?:
                                (?:(?:[^\w]|_)+)Series(?:(?:[^\w]|_)+)Anime)
                                |(?:(?:(?:[^\w]|_)+)Anime(?:(?:[^\w]|_)+)Online)
                                |(?:(?:(?:[^\w]|_)+)Animados(?:(?:[^\w]|_)+)Mangas)
                                |(?:(?:(?:[^\w]|_)+)Series(?:(?:[^\w]|_)+)Mangas)
                                |(?:(?:(?:[^\w]|_)+)Mangas)
                                )
                        )
                    (?:(?<g_tx>(?:(?:(?:[^\w]|_)+)tx)|(?:(?:(?:[^\w]|_)+)En(?:(?:[^\w]|_)+)Transmision)|(?:(?:(?:[^\w]|_)+)Transmision)|(?:(?:(?:[^\w]|_)+)x(?:(?:[^\w]|_)+)Capitulos)))(?:(?:[^\w]|_)*))(?:(?:[^\w]|_)*)$)
              */
        }

        public static void pruebasVariadas2()
        {

            string m = "";//@"(?i:^(?<g_prin_manga>(?:Series(?:(?:[^\w]|_)+)Anime)|(?:Anime(?:(?:[^\w]|_)+)Online)|(?:Animados(?:(?:[^\w]|_)+)Mangas)|(?:Series(?:(?:[^\w]|_)+)Mangas)|(?:Mangas)))";
            m += @"(?i:^";
            m += @"(?:(?:[^\w]|_)*)";
            m += @"(?:";
            m += @"(?:";
            m += @"(?<g_prin_manga>";
            m += @"(?:Series(?:(?:[^\w]|_)+)Anime)";
            m += @"|(?:Anime(?:(?:[^\w]|_)+)Online)";
            m += @"|(?:(?:(?:[^\w]|_)+)Animados(?:(?:[^\w]|_)+)Mangas)";
            m += @"|(?:Series(?:(?:[^\w]|_)+)Mangas)";
            m += @"|(?:(?:(?:[^\w]|_)+)Mangas)";
            m += @")";
            m += @")";

            //m += @")";
            //m += @")";
            m += @"(?:(?:[^\w]|_)*)";


            m += @"(?:";
            m += @"(?<g_tx>";
            m += @"(?:tx)|(?:En(?:(?:[^\w]|_)+)Transmision)|(?:Transmision)|(?:x(?:(?:[^\w]|_)+)Capitulos)))(?:(?:[^\w]|_)*))(?:(?:[^\w]|_)*)$)";
            m += @"";
            m += @"";
            m += @"";
            m += @"";
            m += @"";
            m += @"";
            m += @"";
            m += @"";
            m += @"";
            m += @"";
            m += @"";
            m += @"";

            string a = "Series Mangas [TX]";

            Match ma = new Regex(m).Match(a);
            cwl(ma.Success);
        }

        public static void pruebasVariadas()
        {
            //string p2=TipoDeEtiquetaDeSerie.getPatronTags(TipoDeEtiquetaDeSerie.PRINCIPAL_MANGA);
            //string p2=TipoDeEtiquetaDeSerie.getPatronTags(TipoDeEtiquetaDeSerie.TX);
            //cwl("p2="+p2);
            //string p=@"(?i:^(?:(?:[^\w]|_)*)(?:(?<gru_tags_prin>(?:(?<g_prin_manga>(?:(?<!(?:(?![_])\w))Series(?<!(?:(?![_])\w))Anime)|(?:(?<!(?:(?![_])\w))Anime(?<!(?:(?![_])\w))Online)|(?:(?<!(?:(?![_])\w))Animados(?<!(?:(?![_])\w))Mangas)|(?:(?<!(?:(?![_])\w))Series(?<!(?:(?![_])\w))Mangas)|(?:(?<!(?:(?![_])\w))Mangas))|(?<g_prin_ser_per>(?:(?<!(?:(?![_])\w))Series))))(?<gru_tags_prin>(?:(?<g_mangas>(?:(?<!(?:(?![_])\w))Mangas))|(?<g_tx>(?:(?<!(?:(?![_])\w))tx)|(?:(?<!(?:(?![_])\w))En(?<!(?:(?![_])\w))Transmision)|(?:(?<!(?:(?![_])\w))Transmision)|(?:(?<!(?:(?![_])\w))x(?<!(?:(?![_])\w))Capitulos))|(?<g_finalizadas>(?:(?<!(?:(?![_])\w))Finalizadas)|(?:(?<!(?:(?![_])\w))x(?<!(?:(?![_])\w))Temporadas)|(?:(?<!(?:(?![_])\w))Temporadas(?<!(?:(?![_])\w))Finalizadas))|(?<g_hd>(?:(?<!(?:(?![_])\w))hd))|(?<g_clasicas>(?:(?<!(?:(?![_])\w))clasicas))|(?<g_dual_audio>(?:(?<!(?:(?![_])\w))Dual(?<!(?:(?![_])\w))Audio))|(?<g_subtituladas>(?:(?<!(?:(?![_])\w))Subtituladas))|(?<g_extreno>(?:(?<!(?:(?![_])\w))Estreno)|(?:(?<!(?:(?![_])\w))Estrenos))|(?<g_dobladas>(?:(?<!(?:(?![_])\w))Dobladas)|(?:(?<!(?:(?![_])\w))Dobladas(?<!(?:(?![_])\w))al(?<!(?:(?![_])\w))Espanol))|(?<g_espanol>(?:(?<!(?:(?![_])\w))ESPANOL))|(?<g_espanolas>(?:(?<!(?:(?![_])\w))Espanolas))))*)(?:(?:[^\w]|_)*)$)";

            //string m2 = TipoDeEtiquetaDeSerie.getPatronTags(TipoDeEtiquetaDeSerie.PRINCIPAL_MANGA)
            //           + TipoDeEtiquetaDeSerie.getPatronTags(TipoDeEtiquetaDeSerie.TX) + "*";

            //string m2 = //ConstantesExprecionesRegulares.separaciones +
            //            TipoDeEtiquetaDeSerie.getPatronTags(TipoDeEtiquetaDeSerie.PRINCIPAL_MANGA)
            //           + ConstantesExprecionesRegulares.separaciones
            //           + TipoDeEtiquetaDeSerie.getPatronTags(TipoDeEtiquetaDeSerie.TX)
            //           + ConstantesExprecionesRegulares.separaciones
            //           ;

            string m2 = Matchs.grupoNombrado(TipoDeEtiquetaDeSerie.KEY_GRUPO_PRINCIPAL, TipoDeEtiquetaDeSerie.getPatronTags(TipoDeEtiquetaDeSerie.ETIQUETAS_PRINCIPALES))
                + ConstantesExprecionesRegulares.separaciones_UnoAlMenos
                       + Matchs.grupoNombrado(TipoDeEtiquetaDeSerie.KEY_GRUPO_SECUNDARIO, TipoDeEtiquetaDeSerie.getPatronTags(TipoDeEtiquetaDeSerie.ETIQUETAS_DE_CLASIFICACION)) + "*";
            //			

            string a = "Series Mangas [TX]";
            //string a = "Mangas [TX]";
            //string a="Series Mangas";
            //new PatronRegex(a).InicialSReSFinal.
            //Match m =new Regex(p2).Match(a);
            PatronRegex pr = new PatronRegex(m2);
            //cwl("pr.Re="+pr.Re);
            cwl("pr.InicialSReSFinal=" + pr.InicialSReSFinal);
            Match m = pr.InicialSReSFinal.Match(a);
            //Match m = pr.ReInicialFinal.Match(a);
            cwl(m.Success);
        }
        public static void provarAnalizarDePaquete()
        {
            RecursosDePatronesDeSeriesGenerales reg = new RecursosDePatronesDeSeriesGenerales();
            Paquete p = new Paquete(
            carpeta: new DirectoryInfo(@"C:\_COSAS\Para pruebas Actualize\info de paquetes\[[01-08-2022]]")
            , proR: new ProcesadorDeRelacionesDeNombresClaveSeries()
            , cf_series_anime: ConfiguracionDeSeries.getConfiguracionParaAnime(reg)
            , cf_series_persona: ConfiguracionDeSeries.getConfiguracionParaSeriesPersona(reg)
            );

            AnalizadorDelPaquete an = new AnalizadorDelPaquete(p, reg);
            an.buscarUrls();

            cwl("Mangas*******************!!!!!!!!!!!!!!!!!!!!!!!!!!");
            verDirectorios(p.seriesMangas);

            cwl();
            cwl();
            cwl();
            cwl("Personas*******************!!!!!!!!!!!!!!!");
            verDirectorios(p.seriesPersona);
        }

        public static void verDirectorios(SeriesDelPaquete srp)
        {
            foreach (KeyValuePair<ConjuntoDeEtiquetasDeSerie, HashSet<DirectorioDeSeriesDelPaquete>> k in srp.directoriosDeSeries)
            {
                cwl("++++++++++++++++++++++++++++++++++++++++++++");
                cwl(Utiles.str(k.Key.etiquetas));
                cwl("-------------------------------------------");
                foreach (DirectorioDeSeriesDelPaquete d in k.Value)
                {
                    cwl(d.carpeta);
                }

            }
        }

        public static void recrearContenidDeCarpetasConTXT()
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\_COSAS\Para pruebas Actualize\contenidos\series\Conjunto de series\6\12 Monkeys\12 Monkeys (Temporada 3) [10 Cap.] FDT");
            Archivos.recorrerCarpeta(d: d
                                     , recorrerCarpetasInternas: true
                                     , metodo: f =>
                                     {
                                         DirectoryInfo c = f.Directory;

                                         Archivos.recorrerTXT(f, (ln, i) =>
                                         {
                                             ln = ln.Replace('?', ' ');
                                             ln = Archivos.eliminarCarateresNoPermitidos(ln);
                                             if (ln.Trim().Length > 0 && UtilesVideos.esVideo(ln))
                                             {
                                                 Archivos.crearArchivo(c, ln);
                                             }

                                         });
                                     });
        }

        public static void provarManagerSeries()
        {


            ManagerRelacionadorDeSerie mng = new ManagerRelacionadorDeSerie();
            TipoDeSeccion seccion = TipoDeSeccion.ANIME;
            //mng.eliminarDireccionPropia(mng.getDireccionesPropias()[0]);
            ;
            //			mng.agregarDireccionPropia(
            //				seccion: seccion
            //				, categoria: TipoDeCategoriaPropias.SEGUIDAS
            //				, url: @"C:\_COSAS\temporal\Nueva Carpeta (3)\c"
            //			);
            List<DireccionDeActualizadorPropia> ldp = mng.getDireccionesPropias();
            DireccionDeActualizadorPropia dp = ldp[0];
            Console.WriteLine("sel" + dp.seleccioniada);
            dp.seleccioniada = !dp.seleccioniada;
            Console.WriteLine("sel" + dp.seleccioniada);
            mng.updateDireccionesPropias(dp);
            //			foreach (DireccionDeActualizadorPropia dp in ldp) {
            //				cwl("dp");
            //			}

            //			mng.actualizar(seccion, new EventosEnSubproceso(() => {
            //			                                                	
            //			}, e => {
            //			                                                	
            //			}));
            //			
            //			List<Capitulo_R> lc_seguidos = mng.getCapitulos_Detalles_Propios(
            //				                              seccion: seccion
            //				, categoria: TipoDeCategoriaPropias.SEGUIDAS
            //				, filtro: null
            //			                              );
            //			
            //			Action<object> wl = cwl;
            //			
            //			wl("cantidad de Capitulos="+lc_seguidos.Count);
            //			mostrarCapitulos(wl, lc_seguidos);
        }
        public static void mostrarCapitulos(Action<object> wl, List<Capitulo_R> lc)
        {

            foreach (Capitulo_R c in lc)
            {
                wl(c.nombre_capitulo + " T=" + c.temporada + " C=" + c.capitulo + " F=" + c.formato
                + (c.esUnExtra ? " ova" : "")
                + (c.tieneSubtituloEnSuCarpeta ? " subtitulado" : "")
                );



                wl("\t\t"
                + (
                    c.nombres_de_serie != null && c.nombres_de_serie.Count > 0 ? " " + Utiles.str(c.nombres_de_serie) : ""
                )
                + (
                    c.fecha != null && c.fecha.Length > 0 ? " Fecha: " + c.fecha : ""
                )
                + (
                    c.etiquetas != null && c.etiquetas.Count > 0 ? " Tags: " + Utiles.str(c.etiquetas) : ""
                )
                );
            }
        }



        public static EsquemaBD getEsquema_BDSessionEstorage()
        {
            int TAMAÑO_NOMBRE = 50;
            int TAMAÑO_TIPO_DE_MONEDA = 50;
            int TAMAÑO_NORMAL = 50;
            int TAMAÑO_TELEFONO = 50;
            int TAMAÑO_CORREO = 50;
            int TAMAÑO_URL = 500;
            int TAMAÑO_STR = 256;
            int TAMAÑO_GRANDE = 500;




            ModeloBD_ID Dato = new ModeloBD_ID("TABLA_PROPIEDAD_SESION_STORAGE");
            ColumnaDeModeloBD c_dato_session = Dato.addC("COLUMNA_SESION", TAMAÑO_STR);
            ColumnaDeModeloBD c_dato_propiedad = Dato.addC("COLUMNA_PROPIEDAD", TAMAÑO_STR);
            //Datos.addC("COLUMNA_VALOR", TAMAÑO_GRANDE);
            //Datos.addC("COLUMNA_TIPO_DE_DATO", TAMAÑO_NORMAL);
            Dato.addC("COLUMNA_ES_LISTA", TipoDeDatoSQL.BOOLEAN);

            Dato.addExiste(c_dato_session, c_dato_propiedad);
            Dato.addBuscarPor(c_dato_session, c_dato_propiedad);


            ModeloBD_ID DatoDePropiedadSimple = new ModeloBD_ID("TABLA_VALOR_SIMPLE_SESION_STORAGE");
            ColumnaDeModeloBD c_id_propiedad_en_valor = DatoDePropiedadSimple.addC_ID(Dato);
            DatoDePropiedadSimple.addC("COLUMNA_VALOR", TAMAÑO_GRANDE);

            DatoDePropiedadSimple.addBuscarPor(c_id_propiedad_en_valor);

            ModeloBD_ID DatoDeLista = new ModeloBD_ID("TABLA_DATO_EN_LISTA_SESION_STORAGE");
            DatoDeLista.addC_ID(Dato);
            DatoDeLista.addC("COLUMNA_VALOR", TAMAÑO_GRANDE);

            DatoDeLista.addBuscarListaPor(Dato);

            EsquemaBD e = new EsquemaBD();
            e.addModelo(Dato, DatoDePropiedadSimple, DatoDeLista);



            return e;
        }
        public static void crear_BDSessionEstorage_CSharp()
        {
            EsquemaBD e = getEsquema_BDSessionEstorage();


            FactoryBD f = new FactoryBD(e);
            f.NombreBDAdmin = "BDAdminSesionStorage";
            f.DireccionBDSqlite = "bdSesionEstorage.sqlite";
            f.DireccionPaquete = "ReneUtiles.Clases.BD.SesionEstorage.Modelos";
            f.TipoDeConexion = TipoDeConexionBD.SQL_LITE;
            f.crearCodigoCSharp(new DirectoryInfo(@"D:\_Cosas\Programacion\Proyectos\C#\ReneUtiles\ReneUtiles\ReneUtiles\Clases\BD\SesionEstorage\Modelos"));
            cwl("termino");
        }

        public static EsquemaBD getEsquema_BDActualizador4()
        {
            int TAMAÑO_NOMBRE = 50;
            int TAMAÑO_TIPO_DE_MONEDA = 50;
            int TAMAÑO_NORMAL = 50;
            int TAMAÑO_TELEFONO = 50;
            int TAMAÑO_CORREO = 50;
            int TAMAÑO_URL = 500;




            ModeloBD_ID Direccion = new ModeloBD_ID("TABLA_DIRECCION");
            ColumnaDeModeloBD c_url_direccion = Direccion.addC("COLUMNA_URL", TAMAÑO_URL);
            ColumnaDeModeloBD c_seleccionada_direccion = Direccion.addC("COLUMNA_SELECCIONADA", TipoDeDatoSQL.BOOLEAN);
            ColumnaDeModeloBD c_tipo_de_destino_direccion = Direccion.addC("COLUMNA_TIPO_DE_DESTINO", TAMAÑO_NORMAL);
            ColumnaDeModeloBD c_ubicacion_direccion = Direccion.addC("COLUMNA_TIPO_DE_UBICACION_DE_SECCION", TAMAÑO_NORMAL);
            ColumnaDeModeloBD c_tipo_seccion_direccion = Direccion.addC("COLUMNA_SECCION", TAMAÑO_NORMAL);
            ColumnaDeModeloBD c_categoria_seccion_direccion = Direccion.addC("COLUMNA_CATEGORIA", TAMAÑO_NORMAL);

            Direccion.addBuscarListaPor(c_ubicacion_direccion);
            Direccion.addBuscarListaPor(c_ubicacion_direccion, c_tipo_seccion_direccion);
            Direccion.addBuscarListaPor(c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);
            Direccion.addBuscarListaPor(c_ubicacion_direccion, c_categoria_seccion_direccion);
            //			
            Direccion.addDeletePor(c_ubicacion_direccion);
            Direccion.addDeletePor(c_ubicacion_direccion, c_tipo_seccion_direccion);
            Direccion.addDeletePor(c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);
            Direccion.addDeletePor(c_ubicacion_direccion, c_categoria_seccion_direccion);

            Direccion.addExiste(c_url_direccion, c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);
            Direccion.addBuscarPor(c_url_direccion, c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);






            ModeloBD_ID DireccionDelPaquete = new ModeloBD_ID("TABLA_DIRECCION_DE_PAQUETE");
            ColumnaDeModeloBD c_url_DireccionDelPaquete = DireccionDelPaquete.addC("COLUMNA_URL", TAMAÑO_URL);
            ColumnaDeModeloBD c_seleccionada_DireccionDelPaquete = DireccionDelPaquete.addC("COLUMNA_SELECCIONADA", TipoDeDatoSQL.BOOLEAN);
            ColumnaDeModeloBD c_tipo_de_destino_DireccionDelPaquete = DireccionDelPaquete.addC("COLUMNA_TIPO_DE_DESTINO", TAMAÑO_NORMAL);
            ColumnaDeModeloBD c_tipo_seccion_DireccionDelPaquete = DireccionDelPaquete.addC("COLUMNA_SECCION", TAMAÑO_NORMAL);


            DireccionDelPaquete.addBuscarListaPor(c_tipo_seccion_DireccionDelPaquete);


            //			
            DireccionDelPaquete.addDeletePor(c_tipo_seccion_DireccionDelPaquete);



            ModeloBD_ID Etiqueta = new ModeloBD_ID("TABLA_ETIQUETA_DE_DIRECCION_PAQUETE");
            Etiqueta.addC("COLUMNA_NOMBRE", TAMAÑO_NOMBRE);
            Etiqueta.addC_ID(DireccionDelPaquete);


            DireccionDelPaquete.addGetListaDe(Etiqueta);

            ModeloBD_ID File_R = new ModeloBD_ID("TABLA_FILE");
            ColumnaDeModeloBD c_nombre_File_R = File_R.addC("COLUMNA_NOMBRE", TAMAÑO_NOMBRE);
            File_R.addC("COLUMNA_TIPO", TAMAÑO_NORMAL);
            //File_R.addBuscarPor(c_nombre_File_R);


            ModeloBD_ID File_ArchivoInterno = new ModeloBD_ID("TABLA_ARCHIVO_INTERNO");
            ColumnaDeModeloBD c_carpeta_padre_File_ArchivoInterno = File_ArchivoInterno.addC_ID("COLUMNA_ID_CARPETA_PADRE", File_R);
            ColumnaDeModeloBD c_file_hijo_File_ArchivoInterno = File_ArchivoInterno.addC_ID("COLUMNA_ID_FILE_HIJO", File_R);
            File_ArchivoInterno.addBuscarListaSortPor(li(c_carpeta_padre_File_ArchivoInterno), c_carpeta_padre_File_ArchivoInterno);
            File_ArchivoInterno.addBuscarPor(c_file_hijo_File_ArchivoInterno);
            File_ArchivoInterno.addDeletePor(c_carpeta_padre_File_ArchivoInterno);
            File_ArchivoInterno.addExiste(c_carpeta_padre_File_ArchivoInterno, c_file_hijo_File_ArchivoInterno);

            ModeloBD_ID Carpetas_Paquete = new ModeloBD_ID("TABLA_CARPETAS_PAQUETE");
            Carpetas_Paquete.addC_ID( File_R);
            ColumnaDeModeloBD c_marca_Carpetas_Paquete = Carpetas_Paquete.addC("COLUMNA_MARCA", TAMAÑO_NOMBRE);
            ColumnaDeModeloBD c_fecha_Carpetas_Paquete = Carpetas_Paquete.addC("COLUMNA_FECHA", TipoDeDatoSQL.DATE);
            Carpetas_Paquete.addDeletePor(c_fecha_Carpetas_Paquete);
            Carpetas_Paquete.addDeletePor(c_marca_Carpetas_Paquete);
            Carpetas_Paquete.addDeletePor(c_fecha_Carpetas_Paquete,c_marca_Carpetas_Paquete);
            Carpetas_Paquete.addExiste(c_fecha_Carpetas_Paquete);
            Carpetas_Paquete.addExiste(c_marca_Carpetas_Paquete);
            Carpetas_Paquete.addExiste(c_fecha_Carpetas_Paquete, c_marca_Carpetas_Paquete);
            

            Carpetas_Paquete.addBuscarListaSortPor(li(c_marca_Carpetas_Paquete), c_marca_Carpetas_Paquete);
            Carpetas_Paquete.addBuscarListaSortPor(li(c_fecha_Carpetas_Paquete), c_fecha_Carpetas_Paquete);
            Carpetas_Paquete.addBuscarListaSortPor(li(c_fecha_Carpetas_Paquete, c_marca_Carpetas_Paquete), c_fecha_Carpetas_Paquete);
            Carpetas_Paquete.addBuscarListaSortPor(li(c_fecha_Carpetas_Paquete, c_marca_Carpetas_Paquete), c_marca_Carpetas_Paquete);

            EsquemaBD e = new EsquemaBD();
            e.addModelo(Direccion);
            e.addModelo(Etiqueta);
            e.addModelo(DireccionDelPaquete);
            e.addModelo(File_R);
            e.addModelo(File_ArchivoInterno);
            e.addModelo(Carpetas_Paquete);




            return e;
        }


        public static EsquemaBD getEsquema_BDActualizador3()
        {
            int TAMAÑO_NOMBRE = 50;
            int TAMAÑO_TIPO_DE_MONEDA = 50;
            int TAMAÑO_NORMAL = 50;
            int TAMAÑO_TELEFONO = 50;
            int TAMAÑO_CORREO = 50;
            int TAMAÑO_URL = 500;




            ModeloBD_ID Direccion = new ModeloBD_ID("TABLA_DIRECCION");
            ColumnaDeModeloBD c_url_direccion = Direccion.addC("COLUMNA_URL", TAMAÑO_URL);
            ColumnaDeModeloBD c_seleccionada_direccion = Direccion.addC("COLUMNA_SELECCIONADA", TipoDeDatoSQL.BOOLEAN);
            ColumnaDeModeloBD c_tipo_de_destino_direccion = Direccion.addC("COLUMNA_TIPO_DE_DESTINO", TAMAÑO_NORMAL);
            ColumnaDeModeloBD c_ubicacion_direccion = Direccion.addC("COLUMNA_TIPO_DE_UBICACION_DE_SECCION", TAMAÑO_NORMAL);
            ColumnaDeModeloBD c_tipo_seccion_direccion = Direccion.addC("COLUMNA_SECCION", TAMAÑO_NORMAL);
            ColumnaDeModeloBD c_categoria_seccion_direccion = Direccion.addC("COLUMNA_CATEGORIA", TAMAÑO_NORMAL);

            Direccion.addBuscarListaPor(c_ubicacion_direccion);
            Direccion.addBuscarListaPor(c_ubicacion_direccion, c_tipo_seccion_direccion);
            Direccion.addBuscarListaPor(c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);
            Direccion.addBuscarListaPor(c_ubicacion_direccion, c_categoria_seccion_direccion);
            //			
            Direccion.addDeletePor(c_ubicacion_direccion);
            Direccion.addDeletePor(c_ubicacion_direccion, c_tipo_seccion_direccion);
            Direccion.addDeletePor(c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);
            Direccion.addDeletePor(c_ubicacion_direccion, c_categoria_seccion_direccion);

            Direccion.addExiste(c_url_direccion, c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);
            Direccion.addBuscarPor(c_url_direccion, c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);






            ModeloBD_ID DireccionDelPaquete = new ModeloBD_ID("TABLA_DIRECCION_DE_PAQUETE");
            ColumnaDeModeloBD c_url_DireccionDelPaquete = DireccionDelPaquete.addC("COLUMNA_URL", TAMAÑO_URL);
            ColumnaDeModeloBD c_seleccionada_DireccionDelPaquete = DireccionDelPaquete.addC("COLUMNA_SELECCIONADA", TipoDeDatoSQL.BOOLEAN);
            ColumnaDeModeloBD c_tipo_de_destino_DireccionDelPaquete = DireccionDelPaquete.addC("COLUMNA_TIPO_DE_DESTINO", TAMAÑO_NORMAL);
            ColumnaDeModeloBD c_tipo_seccion_DireccionDelPaquete = DireccionDelPaquete.addC("COLUMNA_SECCION", TAMAÑO_NORMAL);


            DireccionDelPaquete.addBuscarListaPor(c_tipo_seccion_DireccionDelPaquete);


            //			
            DireccionDelPaquete.addDeletePor(c_tipo_seccion_DireccionDelPaquete);



            ModeloBD_ID Etiqueta = new ModeloBD_ID("TABLA_ETIQUETA_DE_DIRECCION_PAQUETE");
            Etiqueta.addC("COLUMNA_NOMBRE", TAMAÑO_NOMBRE);
            Etiqueta.addC_ID(DireccionDelPaquete);


            DireccionDelPaquete.addGetListaDe(Etiqueta);


            EsquemaBD e = new EsquemaBD();
            e.addModelo(Direccion);
            e.addModelo(Etiqueta);
            e.addModelo(DireccionDelPaquete);




            return e;
        }

        public static EsquemaBD getEsquema_BDActualizador2()
        {
            int TAMAÑO_NOMBRE = 50;
            int TAMAÑO_TIPO_DE_MONEDA = 50;
            int TAMAÑO_NORMAL = 50;
            int TAMAÑO_TELEFONO = 50;
            int TAMAÑO_CORREO = 50;
            int TAMAÑO_URL = 500;




            ModeloBD_ID Direccion = new ModeloBD_ID("TABLA_DIRECCION");
            ColumnaDeModeloBD c_url_direccion = Direccion.addC("COLUMNA_URL", TAMAÑO_URL);
            ColumnaDeModeloBD c_seleccionada_direccion = Direccion.addC("COLUMNA_SELECCIONADA", TipoDeDatoSQL.BOOLEAN);
            ColumnaDeModeloBD c_tipo_de_destino_direccion = Direccion.addC("COLUMNA_TIPO_DE_DESTINO", TAMAÑO_NORMAL);
            ColumnaDeModeloBD c_ubicacion_direccion = Direccion.addC("COLUMNA_TIPO_DE_UBICACION_DE_SECCION", TAMAÑO_NORMAL);
            ColumnaDeModeloBD c_tipo_seccion_direccion = Direccion.addC("COLUMNA_SECCION", TAMAÑO_NORMAL);
            ColumnaDeModeloBD c_categoria_seccion_direccion = Direccion.addC("COLUMNA_CATEGORIA", TAMAÑO_NORMAL);

            Direccion.addBuscarListaPor(c_ubicacion_direccion);
            Direccion.addBuscarListaPor(c_ubicacion_direccion, c_tipo_seccion_direccion);
            Direccion.addBuscarListaPor(c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);
            Direccion.addBuscarListaPor(c_ubicacion_direccion, c_categoria_seccion_direccion);
            //			
            Direccion.addDeletePor(c_ubicacion_direccion);
            Direccion.addDeletePor(c_ubicacion_direccion, c_tipo_seccion_direccion);
            Direccion.addDeletePor(c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);
            Direccion.addDeletePor(c_ubicacion_direccion, c_categoria_seccion_direccion);

            Direccion.addExiste(c_url_direccion, c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);
            Direccion.addBuscarPor(c_url_direccion, c_ubicacion_direccion, c_tipo_seccion_direccion, c_categoria_seccion_direccion);

            EsquemaBD e = new EsquemaBD();
            e.addModelo(Direccion);




            return e;
        }

        public static EsquemaBD getEsquema_BDActualizador()
        {
            int TAMAÑO_NOMBRE = 50;
            int TAMAÑO_TIPO_DE_MONEDA = 50;
            int TAMAÑO_NORMAL = 50;
            int TAMAÑO_TELEFONO = 50;
            int TAMAÑO_CORREO = 50;
            int TAMAÑO_URL = 500;

            ModeloBD_ID Seccion = new ModeloBD_ID("TABLA_SECCION");
            Seccion.addC("COLUMNA_NOMBRE", TAMAÑO_NORMAL);
            Seccion.addC("COLUMNA_TIPO", TAMAÑO_NORMAL);
            Seccion.addC("COLUMNA_CLASIFICACION", TAMAÑO_NORMAL);


            ModeloBD_ID Direccion = new ModeloBD_ID("TABLA_DIRECCION");
            Direccion.addC("COLUMNA_URL", TAMAÑO_URL);
            Direccion.addC("COLUMNA_TIPO", TAMAÑO_NORMAL);

            Seccion.addGetListaDe_enTablaExterna(Direccion);
            //Direccion.addBorrarJuntoA(Seccion);


            EsquemaBD e = new EsquemaBD();
            e.addModelo(Seccion, Direccion);



            return e;
        }

        public static void crear_BDActualizador_CSharp()
        {
            EsquemaBD e = getEsquema_BDActualizador4();


            FactoryBD f = new FactoryBD(e);
            f.DireccionBDSqlite = "bdActualizadoor.sqlite";
            f.DireccionPaquete = "RelacionadorDeSerie.BD.Modelos";
            f.TipoDeConexion = TipoDeConexionBD.SQL_LITE;
            f.crearCodigoCSharp(new DirectoryInfo(@"D:\_Cosas\Programacion\Proyectos\C#\RelacionadorDeSerieConsola\RelacionadorDeSerieConsola\RelacionadorDeSerie\BD\Modelos"));
            cwl("termino");
        }


        private static  List<ColumnaDeModeloBD> li(params ColumnaDeModeloBD[] C) {
            return C.ToList();
        }
    }
}
/*
            lo que identifique es que lo que esta entre las palabras que deben ser
            consecutivas como "Series Mangas"
            (?<!(?:(?![_])\w))
            pide que lo anterior o lo siguienete (hay que ver pero en cualquier caso el problema es el mismo)
            no sea una letra y en el caso de una solo separacion esto no se cumple
            plq es mejor separar consolo posibles separaciones y no pedir presisamente esto
            ademas hay que decisle que puede terminar en separaciones o 
            comenzar con separacion  que en caso en cuestion termina con "]"
            */
/*
 * (?i:^
 *      (?:(?:[^\w]|_)*)
 *      (?:
 *          (?:
 *              (?<g_prin_manga>
 *                  (?:
 *                      (?<!(?:(?![_])\w))
 *                      Series(?<!(?:(?![_])\w))Anime
 *                  )|
 *                  (?:(?<!(?:(?![_])\w))Anime(?<!(?:(?![_])\w))Online)
 *                  |(?:(?<!(?:(?![_])\w))Animados(?<!(?:(?![_])\w))Mangas)
 *                  |(?:(?<!(?:(?![_])\w))Series(?<!(?:(?![_])\w))Mangas)
 *                  |(?:(?<!(?:(?![_])\w))Mangas))
 *              )
 *              (?:(?<g_tx>(?:(?<!(?:(?![_])\w))tx)|(?:(?<!(?:(?![_])\w))En(?<!(?:(?![_])\w))Transmision)|(?:(?<!(?:(?![_])\w))Transmision)|(?:(?<!(?:(?![_])\w))x(?<!(?:(?![_])\w))Capitulos)))*)(?:(?:[^\w]|_)*)$)
*/
