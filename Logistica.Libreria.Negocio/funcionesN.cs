using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Negocio
{
    public class funcionesN
    {
        public string fun_RemplazarLetras(string palabra)
        {
            try
            {
                // ESPACIO EN BLANCO
                palabra = palabra.Replace("&nbsp;", "");

                // VOCALES MAYUSCULAS
                palabra = palabra.Replace("&#193;", "Á");
                palabra = palabra.Replace("&#201;", "É");
                palabra = palabra.Replace("&#205;", "Í");
                palabra = palabra.Replace("&#211;", "Ó");
                palabra = palabra.Replace("&#218;", "Ú");

                // VOCALES MINUSCULAS
                palabra = palabra.Replace("&#225;", "á");
                palabra = palabra.Replace("&#233;", "é");
                palabra = palabra.Replace("&#237;", "í");
                palabra = palabra.Replace("&#243;", "ó");
                palabra = palabra.Replace("&#250;", "ú");

                // LETRAS ESPECIALES
                palabra = palabra.Replace("&#241;", "ñ");
                palabra = palabra.Replace("&#209;", "Ñ");
                palabra = palabra.Replace("&#38;", "&");
                palabra = palabra.Replace("&#35;", "#");
                palabra = palabra.Replace("&#36;", "$");
                palabra = palabra.Replace("&#37;", "%");
                palabra = palabra.Replace("&#176;", "°");

                return palabra;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string fun_RemplazarLetras_acute(string palabra)
        {
            try
            {
                // VOCALES MAYUSCULAS
                //palabra = palabra.Replace("Á", "&Aacute;");
                //palabra = palabra.Replace("É", "&Eacute;");
                //palabra = palabra.Replace("Í", "&Iacute;");
                //palabra = palabra.Replace("Ó", "&Oacute;");
                //palabra = palabra.Replace("Ú", "&Uacute;");

                palabra = palabra.Replace("Á", "A");
                palabra = palabra.Replace("É", "E");
                palabra = palabra.Replace("Í", "I");
                palabra = palabra.Replace("Ó", "O");
                palabra = palabra.Replace("Ú", "U");

                // VOCALES MINUSCULAS
                palabra = palabra.Replace("á", "a");
                palabra = palabra.Replace("é", "e");
                palabra = palabra.Replace("í", "i");
                palabra = palabra.Replace("ó", "o");
                palabra = palabra.Replace("ú", "u");

                // LETRAS ESPECIALES
                palabra = palabra.Replace("ñ", "&ntilde;");
                palabra = palabra.Replace("Ñ", "&Ntilde;");
                palabra = palabra.Replace("°", "&ordm;");

                return palabra;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
