using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelacionadorDeSerie
{
    public class TipoDeFile_R
    {
        public static readonly TipoDeFile_R CARPETA = new TipoDeFile_R("CARPETA");
        public static readonly TipoDeFile_R ARCHIVO = new TipoDeFile_R("ARCHIVO");

        public static readonly TipoDeFile_R[] VALUES = { CARPETA, ARCHIVO };
        public readonly string valor;
        private TipoDeFile_R(string valor)
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

        public static TipoDeFile_R get(object tipo)
        {
            if (tipo == null)
            {
                return null;
            }
            foreach (TipoDeFile_R t in VALUES)
            {
                if (t.valor == tipo.ToString())
                {
                    return t;
                }
            }
            return null;
        }
    }
}
