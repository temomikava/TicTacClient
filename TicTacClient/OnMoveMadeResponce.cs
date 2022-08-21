using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacClient
{
    internal class OnMoveMadeResponce
    {
        public int Errorcode { get; set; }
        public string ErrorMessage { get; set; }
        public int RowCordinate { get; set; }
        public int ColumnCoordinate { get; set; }
        public string MarK { get; set; }
    }
}
