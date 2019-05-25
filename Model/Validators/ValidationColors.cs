using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class ValidationColors
    {
        private static readonly string _validBackground = "Transparent";
        private static readonly string _invalidBackground = "#DC143C";

        /// <summary>
        /// Get's the valid background color.
        /// </summary>
        /// <returns></returns>
        public static string Valid => _validBackground;

        /// <summary>
        /// Get's the invalid background color.
        /// </summary>
        /// <returns></returns>
        public static string InValid => _invalidBackground;
        
    }
}
