namespace QandA.Models
{
    public class AccountLookupModel
    {
        //        @"select al.Email, al.Name, case when sj.PaymentID not in ('Cash', 'Check') Then sj.PaymentID ELSE 'Credit Card' END PaymentMethod, sj.OrderDate, sj.InstallDate, sj.RemovalDate 

        public string Email { get; set; }
        public string Name { get; set; }
        public string PaymentMethod { get; set; }

        public string OrderDate { get; set; }

        public string InstallDate { get; set; }

        public string RemovalDate { get; set; }
       /*
 * 	[AccountID][int] IDENTITY(1,1) NOT NULL,
[AccessToken] [varchar] (1000) NOT NULL,
[Email] [varchar] (255) NULL,
[Name][varchar] (255) NULL,
[TokenSource][varchar] (255) NULL,
[InsertedDate][smalldatetime] NULL*/
    }
}
