using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracionISV.Model
{
    class Integracion
    {
        public string token { get; set; }
        public string url { get; set;  }
        public string json { get; set; }

        public Integracion()
        {
           string token = "ISVToken 660336ab4cd14b7f817bfa409504c62e";
           string url = "https://api.instoreview.cl/api/v2/download-zone/sales/"; 
           this.token = token;
           this.url = url;
        }
    }



}
