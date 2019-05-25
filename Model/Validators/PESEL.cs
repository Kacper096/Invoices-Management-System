using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class PESEL
    {
        private static readonly long[] _weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3, 1 };
        private static bool IsDigit(string param)
        {
            if (string.IsNullOrEmpty(param) || string.IsNullOrWhiteSpace(param))
                throw new ArgumentNullException("PESEL nie może być pusty.");

            param = param.Trim(' ');
            if (param.Length.CompareTo(11) > 0 || param.Length.CompareTo(11) < 0)
                return false;

            return long.TryParse(param, out long result);
        }
        /// <summary>
        /// Checks the PESEL number.
        /// </summary>
        /// <param name="pesel">Enter PESEL number.</param>
        /// <returns>True or False</returns>
        public static bool Check(string pesel)
        {
            if (IsDigit(pesel))
            {
                try
                {
                    var _liczba = new List<long>();
                    foreach (var item in pesel)
                    {
                        _liczba.Add((long)Char.GetNumericValue(item));

                    }

                    if (CheckSelf_Control(_liczba))
                        return true;
                    else return false;
                }
                catch (NullReferenceException)
                {

                    throw;
                }
                catch (ArgumentNullException)
                {

                    throw;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
                return false;

        }

        private static bool CheckSelf_Control(IEnumerable<long> collection)
        {
            var result = collection.Zip(_weights, (pesel, weights) => new { Pesel = pesel, Weight = weights, Result = pesel * weights }).Sum(x => x.Result);

            if (result > 0 && (result % 10) == 0)
                return true;
            else return false;
        }
    }
}
