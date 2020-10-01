using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Model
{
    public class DataTableResponse
    {
        public List<List<string>> aaData { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }
}
