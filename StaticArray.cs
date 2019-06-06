using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursach
{
    class StaticArray
    {
        public Dictionary<int, String> stringPosition = new Dictionary<int, String>();
        public StaticArray() {
            InitStringPosition();
        }
        public void InitStringPosition()
        {
            stringPosition[0] = "Директор";
            stringPosition[1] = "Менеджер";
            stringPosition[2] = "Универсальный сотрудник";
            stringPosition[3] = "Менеджер клининга";
            stringPosition[4] = "Клининг";
        }
        
    }
}
