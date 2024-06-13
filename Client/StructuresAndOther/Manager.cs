using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client.StructuresAndOther
{
    public class Manager
    {
        public static Frame MainAreaFrame { get; set; }
        public static Frame LowerAreaFrame { get; set; }
        public static Player Player { get; set; }
        public static int GameId { get; set; }

    }
}
