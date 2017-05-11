using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanmemdowntruyen
{
    class TruyenMGK
    {
        string _ten;
        string _linkanh;


        public string Ten
        {
            get
            {
                return _ten;
            }

            set
            {
                _ten = value;
            }
        }

        public string Linkanh
        {
            get
            {
                return _linkanh;
            }

            set
            {
                _linkanh = value;
            }
        }

        public TruyenMGK(string _ten, string _linkanh)
        {
            this._ten = _ten;
            this._linkanh = _linkanh;
        }
    }
}
