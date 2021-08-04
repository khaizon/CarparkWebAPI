using System;
using System.Collections.Generic;

namespace CarparkAPIApp.Models
{
    public class CarparkData
    {
        public List<CarparkAvailability> Items {get; set;}
    }

    public class Carpark_info
    {
        public string Total_lots { get; set; }
        public string Lot_type { get; set; }
        public string Lots_available { get; set; }
    }
    public class Carpark_data {
        public List<Carpark_info> carpark_Info {get; set;}
        public string Carpark_number {get; set;}
        public DateTime Update_datetime {get; set;}
    }
    public class CarparkAvailability
    {
        public DateTime Timestamp { get; set; }
        public List<Carpark_data> carpark_data { get; set; }
    }    
}
