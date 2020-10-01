using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.Model
{
    public class DataTableParams
    {
        public string JobStatus { get; set; }        
        public string SortColumn { get; set; }
        public int StartIndex { get; set; }
        public int DisplayLength { get; set; }
        public string SearchValue { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
