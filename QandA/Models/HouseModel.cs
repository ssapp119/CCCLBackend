namespace QandA.Models
{
    public class HouseModel
    {
        /*
         * CREATE TABLE Houses (
    HouseID int IDENTITY(1,1) PRIMARY KEY,
    Address varchar(255) NOT NULL, 
	SquareFootage BigInt,
	HouseType varchar(255)
);*/

       public int HouseID { get; set; }
        public string Address { get; set; } 
        public long SquareFootage { get; set; }
        public string HouseType { get; set; }

        public long FloorCount { get; set; }
        public decimal Price { get; set; }

        public decimal TotalPriceBeforeTaxes { get; set; }

        /*  public decimal Eff { get; set; }

          public decimal Price { get; set; }

          public decimal TotalPriceBeforeTaxes { get; set; }

          public string Error { get; set; }*/
    }
}
