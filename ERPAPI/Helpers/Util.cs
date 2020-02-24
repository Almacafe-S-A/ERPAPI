using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Helpers
{
    public static class Util
    {
        public static async Task<DateTime> ParseHora(string hora)
        {
            string cadena = hora;
            cadena = cadena.ToLower().Replace("a.m.", "AM");
            cadena = cadena.ToLower().Replace("p.m.", "PM");
            cadena = cadena.ToLower().Replace("am.", "AM");
            cadena = cadena.ToLower().Replace("pm.", "PM");
            cadena = cadena.ToLower().Replace("a.m", "AM");
            cadena = cadena.ToLower().Replace("p.m", "PM");
            cadena = cadena.ToLower().Replace("am", "AM");
            cadena = cadena.ToLower().Replace("pm", "PM");
            cadena = cadena.ToUpper();
            if (cadena.Contains("AM") || cadena.Contains("PM"))
            {
                return await Task.Run(()=>DateTime.ParseExact(cadena, "hh:mm tt", CultureInfo.InvariantCulture));
            }
            return await Task.Run(() => DateTime.ParseExact(cadena, "hh:mm", CultureInfo.InvariantCulture));
        }
    }
}

