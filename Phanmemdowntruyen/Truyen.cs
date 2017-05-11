using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanmemdowntruyen
{
    class Truyen
    {
        string _matruyen;
        string _tentruyen;

        public Truyen(string _matruyen, string _tentruyen)
        {
            this.Matruyen = _matruyen;
            this.Tentruyen = _tentruyen;
        }

        public string Matruyen
        {
            get
            {
                return _matruyen;
            }

            set
            {
                _matruyen = value;
            }
        }

        public string Tentruyen
        {
            get
            {
                return _tentruyen;
            }

            set
            {
                _tentruyen = value;
            }
        }
    }
}
