using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIOPClient.Models
{
    public class statusListModel
    {
        public int Id { get; set; }
        public String dateBegin { get; set; }
        public String dateEnd { get; set; }
        public String teaching { get; set; }
        public String promotion { get; set; }
        public String computers { get; set; }
        public String projector { get; set; }
        public String status { get; set; }
    }
}