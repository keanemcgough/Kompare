using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kompare
{
    class pixelMinMax
    {
        int min = 0;
        int max = 0;
        int id;

        public pixelMinMax(int pix, int id)
        {
            edit(pix);  
            this.id = id;
        }

        public pixelMinMax(int min, int max, int id)
        {
            this.min = min;
            this.max = max;
            this.id = id;
        }

        public void edit(int pix)
        {
            if (pix < min)
                min = pix;
            if (pix > max)
                max = pix;
        }

        public string toString()
        {
            return id +  "," + min + "," + max;
        }

        public Boolean compare(int pix)
        {
            return pix >= min  && pix <= max;
        }
    }
}
