namespace QandA.Models
{
    public class CustomerModel
    {
        /*
         * CREATE TABLE Houses (
    HouseID int IDENTITY(1,1) PRIMARY KEY,
    Address varchar(255) NOT NULL, 
	SquareFootage BigInt,
	HouseType varchar(255)
);*/

        // public int HouseID { get; set; }
       /* public string Address { get; set; }
        public long SquareFootage { get; set; }
        public string HouseType { get; set; }

        public decimal Eff { get; set; }

        public decimal Price { get; set; }*/
       public int customerID { get; set; }

        public string name { get; set;}
        public string email { get; set; }
        public string payment { get; set; }

        public string notification { get; set; }
        
        public string phone { get; set; }

        public DateTime orderTime { get; set; }

        //        body: JSON.stringify({ houseID: data.houseID, name: name,email: email,payment:payment, notification: notification, phone: phone })

    }
}
