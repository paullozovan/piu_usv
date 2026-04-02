using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NivelStocareDate;
using System.Configuration;

namespace testpiu
{
    public class StocareFactory
    {
        private const string NUME_FISIER = "NumeFisier";
        private const string FORMAT_SALVARE = "FormatSalvare";
        public static IStocareData GetAdministratorStocare()
        {
            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "";
            string numeFisier = ConfigurationManager.AppSettings[NUME_FISIER] ?? "";
            string locatieFisier = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName ?? "" ;
            string caleCompleta = locatieFisier + "\\" + numeFisier;
            if (formatSalvare != null)
            {
                switch(formatSalvare)
                {
                    default:
                    case "memorie":
                        return new AdministrareMedicamente();
                    case "txt":
                        return new AdministareFisierTxt(caleCompleta + "." + formatSalvare);
                }

            }
            return null;
        }
    }
}
