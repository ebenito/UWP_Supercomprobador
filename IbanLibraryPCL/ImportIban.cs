using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace IbanLibraryPCL
{ 
    /// <summary>
    /// This imports IbanData from a text file 
    /// </summary>
    /// <remarks>
    /// Inspired from great article at http://msdn.microsoft.com/en-us/magazine/dd453008.aspx
    /// MSDN Magazine > Issues > 2009 > March >  Extreme ASP.NET: Charting With ASP.NET And LINQ
    /// </remarks>
    public static class ImportIban
    {
        /// <summary>
        /// This reads each line from a text file and convert it to an IBAN List
        /// </summary>
        /// <typeparam name="fileName">Text file name</typeparam>
        //public static List<IbanData> ReadIbanListFromFile(string fileName)
        //{
        //    if (File.Exists(fileName))
        //        return File.ReadAllLines(fileName).DivideIntoFields().MapFieldsToIbanData().ToList();
        //    else
        //        return null;
        //}

        /// <summary>
        /// Extention method for convering IBAN List to a string
        /// you can copy-paste output of this method to 
        /// public static List<IbanData> IBANList()
        /// </summary>
        public static string CreateIbanListCode(this IEnumerable<IbanData> ibanList)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("List<IbanData> newList = new List<IbanData>();");
            foreach (IbanData item in ibanList)
            {
                sb.AppendLine("newList.Add(new IbanData(\"" + item.CountryCode + "\", " + item.Lenght + ", @\"" + item.RegexStructure + "\", " + item.IsEU924.ToString().ToLower() + ", \"" + item.Sample + "\"));");
            }
            return sb.ToString();
        }

        /// <summary>
        /// This extention method divides each file line (comma separated) into fields
        /// </summary>
        private static IEnumerable<string[]> DivideIntoFields(this IEnumerable<string> sequence)
        {
            Regex pattern = new Regex(@"[,\s/""]+");
            return sequence.Select(line => pattern.Split(line.Trim('"', ',')));
        }

        /// <summary>
        /// This extention method maps each field to corresponding value in IbanData structure
        /// </summary>
        private static IEnumerable<IbanData> MapFieldsToIbanData(this IEnumerable<string[]> sequence)
        {
            return sequence.Select(fields =>
                    new IbanData()
                    {
                        CountryCode = fields[0],
                        Lenght = fields[1].ParseInt(),
                        RegexStructure = fields[2],
                        IsEU924 = fields[3].ParseBool(),
                        Sample = fields[4]
                    });
        }

        private static int ParseInt(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return 0;
            return int.Parse(value);
        }

        private static bool ParseBool(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return false;
            if (value.ToLower() == "n")
                return false;
            else if (value.ToLower() == "y")
                return true;
            else
                return bool.Parse(value);
        }
    }
}