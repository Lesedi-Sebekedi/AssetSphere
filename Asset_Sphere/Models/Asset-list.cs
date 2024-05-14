using System.ComponentModel.DataAnnotations;

namespace Asset_Sphere.Models
{
    public class Asset_list
    {
        [Key]
        public long Asset_Number {  get; set; }
        public string Description { get; set;}
        public string Catergory { get; set;}

        public string Acq_Date { get; set;}

        public string Location { get; set;}

        public string Label { get; set;}

        public string Custodian { get; set;}

        public string Condition { get; set;}

        public string PO_Number { get; set;}

        public string Model { get; set;}

        public string Serial_Number { get; set;}

        public Decimal Asset_Cost { get; set;}
    }
}
