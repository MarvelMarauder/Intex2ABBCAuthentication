using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2ABBCAuthentication.Models
{
    public class CarCrash
    {
        [Key]
        [Required]
        public int crash_id { get; set; }
        public double bicyclist_involved { get; set; }
        public string city { get; set; }
        public double commercial_motor_veh_involved { get; set; }
        public string county_name { get; set; }
        public DateTime crash_datetime { get; set; }
        public double crash_severity_id { get; set; }
        public double distracted_driving { get; set; }
        public double domestic_animal_related { get; set; }
        public double drowsy_driving { get; set; }
        public double dui { get; set; }
        public double improper_restraint { get; set; }
        public double intersection_related { get; set; }
        public double lat_utm_y { get; set; }
        public double long_utm_x { get; set; }
        public string main_road_name { get; set; }
        public double milepoint { get; set; }
        public double motorcycle_involved { get; set; }
        public double night_dark_condition { get; set; }
        public double older_driver_involved { get; set; }
        public double overturn_rollover { get; set; }
        public double pedestrian_involved { get; set; }
        public double roadway_departure { get; set; }
        public int route { get; set; }
        public double single_vehicle { get; set; }
        public double teenage_driver_involved { get; set; }
        public double unrestrained { get; set; }
        public double wild_animal_related { get; set; }
        public double work_zone_related { get; set; }
        public DateTime crash_date { get; set; }
        public TimeSpan crash_time { get; set; }
        public bool Holiday { get; set; }
        public bool Weekend { get; set; }
        public string Freeway { get; set; }
        public int column1 { get; set; }

    }
}
