/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 26/8/2022
 * Hora: 15:27
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Linq;
using System.Collections.Generic;

namespace RelacionadorDeSerie
{
	/// <summary>
	/// Description of TipoDeCategoriaPropias.
	/// </summary>
	public class TipoDeCategoriaPropias
	{
		public static readonly TipoDeCategoriaPropias SEGUIDAS = new TipoDeCategoriaPropias("SEGUIDAS");
        public static readonly TipoDeCategoriaPropias QUE_TENGO = new TipoDeCategoriaPropias("QUE_TENGO");
        public static readonly TipoDeCategoriaPropias POR_ENTRAR = new TipoDeCategoriaPropias("POR_ENTRAR");
		public static readonly TipoDeCategoriaPropias EN_ESPERA = new TipoDeCategoriaPropias("EN_ESPERA");
		public static readonly TipoDeCategoriaPropias FINALIZADAS = new TipoDeCategoriaPropias("FINALIZADAS");
		
		public static readonly TipoDeCategoriaPropias[] VALUES = {
			SEGUIDAS,
            QUE_TENGO,

            POR_ENTRAR,
			EN_ESPERA,
			FINALIZADAS
		};
		public readonly string valor;
		private TipoDeCategoriaPropias(string valor)
		{
			this.valor = valor;
		}
		public string getValor()
		{
			return this.valor;
		}
		public override string ToString()
		{
			return this.valor;
		}
		
		public static TipoDeCategoriaPropias get(object tipo)
		{
			if (tipo == null) {
				return null;
			}
			foreach (TipoDeCategoriaPropias t in VALUES) {
				if (t.valor == tipo.ToString()) {
					return t;
				}
			}
			return null;
		}


        public static HashSet<TipoDeCategoriaPropias> getNewHashSet()
        {
            return ComparadorTipoDeCategoriaPropia.getNewHashSet_TipoDeCategoriaPropias();
        }
        public static HashSet<TipoDeCategoriaPropias> getNewHashSet(params TipoDeCategoriaPropias[] categorias)
        {
            return ComparadorTipoDeCategoriaPropia.getNewHashSet_TipoDeCategoriaPropias(categorias);
        }
        public static HashSet<TipoDeCategoriaPropias> getNewHashSet(IEnumerable<TipoDeCategoriaPropias> anterior)
        {
            return ComparadorTipoDeCategoriaPropia.getNewHashSet_TipoDeCategoriaPropias(anterior);
        }

        public static Dictionary<TipoDeCategoriaPropias, E> getNewDictionary<E>()
        {
            return ComparadorTipoDeCategoriaPropia.getNewDictionary_TipoDeCategoriaPropias<E>();
        }
    }
	
	
	
	
	public class ComparadorTipoDeCategoriaPropia:IEqualityComparer<TipoDeCategoriaPropias>
	{
		private static readonly ComparadorTipoDeCategoriaPropia comparadorDeIgualdad_TipoDeCategoriaPropia = new ComparadorTipoDeCategoriaPropia();
		
		public static readonly Dictionary<string,int> codigosHash = new  Dictionary<string,int>();
		public static int ultimoHash = 0;
		private string getKey(TipoDeCategoriaPropias obj)
		{
			return obj.getValor();
		}
		public bool Equals(TipoDeCategoriaPropias x, TipoDeCategoriaPropias y)
		{
			return getKey(x) == getKey(y);
		}
		public int GetHashCode(TipoDeCategoriaPropias obj)
		{
			string key = getKey(obj);
			if (codigosHash.ContainsKey(key)) {
				return codigosHash[key];
			}
			int hash = ultimoHash++;
			codigosHash.Add(key, hash);
			return hash;
		}
			
			
			
		public static HashSet<TipoDeCategoriaPropias> getNewHashSet_TipoDeCategoriaPropias()
		{
			return new HashSet<TipoDeCategoriaPropias>(comparadorDeIgualdad_TipoDeCategoriaPropia);
		}
		public static HashSet<TipoDeCategoriaPropias> getNewHashSet_TipoDeCategoriaPropias(IEnumerable<TipoDeCategoriaPropias> anterior)
		{
			return new HashSet<TipoDeCategoriaPropias>(anterior, comparadorDeIgualdad_TipoDeCategoriaPropia);
		}
		
		public static Dictionary<TipoDeCategoriaPropias,E> getNewDictionary_TipoDeCategoriaPropias<E>()
		{
			return new Dictionary<TipoDeCategoriaPropias, E>(comparadorDeIgualdad_TipoDeCategoriaPropia);
		}
	}
		
		
}
