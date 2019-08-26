using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRSjudge.Check
{
    class Check
    {
        static string assy_cd = System.Configuration.ConfigurationManager.AppSettings["assy_cd"];
        public static bool checkLine(string SN,ref string line)
        {
            switch (SN.Substring(7, 1))
            {
                case "1":
                    line = "L01";
                    break;
                case "2":
                    line = "L02";
                    break;
                case "3":
                    line = "L03";
                    break;
                case "4":
                    line = "L04";
                    break;
                case "5":
                    line = "L05";
                    break;
                case "6":
                    line = "L06";
                    break;
                case "7":
                    line = "L07";
                    break;
                case "8":
                    line = "L08";
                    break;
                case "9":
                    line = "L09";
                    break;
                case "A":
                    line = "L10";
                    break;
                case "B":
                    line = "L11";
                    break;
                case "C":
                    line = "L12";
                    break;
                case "D":
                    line = "L13";
                    break;
                case "E":
                    line = "L14";
                    break;
                case "F":
                    line = "L15";
                    break;
                case "G":
                    line = "L16";
                    break;
                case "H":
                    line = "L17";
                    break;
                case "I":
                    line = "L18";
                    break;
                case "J":
                    line = "L19";
                    break;
                case "K":
                    line = "L20";
                    break;
                default:
                    line = "L??";
                    break;
            }
            if (line == Pqm.line)
                return true;
            else
                return false;
        }
    }
}
