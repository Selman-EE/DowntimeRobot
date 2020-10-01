using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Entity.Entity
{
    public class Job : EntityBaseExtended
    {
        public int ReqMaxLimit { get; set; } = 5;// request limit
        public int RangeType { get; set; } = 4;// request range type 
        public int RangeValue { get; set; } = 4;// request range as hour
        public bool Status { get; set; } = true;
        public DateTime StartDate { get; set; }
        public int AppId { get; set; }
        [ForeignKey("AppId")]
        public virtual Application Application { get; set; }

    }
}
