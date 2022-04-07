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
        public int Field1 { get; set; }
        [Required]
        public double bicyclist_involved { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public double commercial_motor_veh_involved { get; set; }
        [Required]
        public string county_name { get; set; }
        [Required]
        public string crash_datetime { get; set; }
        [Required]
        public double crash_severity_id { get; set; }
        [Required]
        public double distracted_driving { get; set; }
        [Required]
        public double domestic_animal_related { get; set; }
        [Required]
        public double drowsy_driving { get; set; }
        [Required]
        public double dui { get; set; }
        [Required]
        public double improper_restraint { get; set; }
        [Required]
        public double intersection_related { get; set; }
        [Required]
        public double lat_utm_y { get; set; }
        [Required]
        public double long_utm_x { get; set; }
        [Required]
        public string main_road_name { get; set; }
        [Required]
        public double milepoint { get; set; }
        [Required]
        public double motorcycle_involved { get; set; }
        [Required]
        public double night_dark_condition { get; set; }
        [Required]
        public double older_driver_involved { get; set; }
        [Required]
        public double overturn_rollover { get; set; }
        [Required]
        public double pedestrian_involved { get; set; }
        [Required]
        public double roadway_departure { get; set; }
        [Required]
        public string route { get; set; }
        [Required]
        public double single_vehicle { get; set; }
        [Required]
        public double teenage_driver_involved { get; set; }
        [Required]
        public double unrestrained { get; set; }
        [Required]
        public double wild_animal_related { get; set; }
        [Required]
        public double work_zone_related { get; set; }
        [Required]
        public DateTime crash_date { get; set; }
        [Required]
        public string crash_time { get; set; }
        [Required]
        public string Holiday { get; set; }
        [Required]
        public string Weekend { get; set; }
        [Required]
        public string Freeway { get; set; }
        [Required]
        public int crash_id { get; set; }

    }
}
