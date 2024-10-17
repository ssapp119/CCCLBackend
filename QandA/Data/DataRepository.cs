using Microsoft.Data.SqlClient;
using Dapper;
using QandA.Models;
using HtmlAgilityPack;
using System.Globalization;
using Newtonsoft.Json;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using Azure.Core;

namespace QandA.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public DataRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public int PostCustomer(CustomerModel model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                
                    string payment = string.Empty;

                    if (model.payment == "Credit Card")
                        payment = "A";
                    else if (model.payment == "Cash")
                        payment = "B";
                    else if (model.payment == "Check")
                        payment = "C";
                    else
                        payment = "C";



//                select CustomerID, Email, Phone, FullName, PreferredNotification, PreferredPayment from Customers
                var id = connection.ExecuteScalar<int>(

                   @" INSERT INTO Customers VALUES (@Email, @Phone, @FullName, @PreferredNotification, @PreferredPayment, GetDate())  SELECT @@IDENTITY", new
                   {

                       Email = model.email,
                       Phone = model.phone,
                       FullName = model.name,
                       PreferredNotification = model.notification[0],
                       PreferredPayment = payment

                   }
                   );

                    return id;
                
            }
        }

        public int PostGoogleToken(GoogleModel model)
        {
            //ScheduledJobID, HouseID, CustomerID
            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                /* var job = connection.QueryFirstOrDefault<ScheduledJobModel>(
                       @"SELECT ScheduledJobID FROM ScheduledJobs where HouseID = @HouseID AND CustomerID = @CustomerID", new { HouseID = model.houseID, CustomerID = model.customerID }
                      );

                 */
                var newModel = connection.QueryFirstOrDefault<GoogleDatabaseModel>(
                       @"SELECT * FROM AccountLookup WITH (NOLOCK) where Email = @Email AND  TokenSource = @TokenSource", new { Email = model.email, TokenSource = "Google" }
                      );

 
               /* var job = connection.QueryFirstOrDefault<int>(
                       @"SELECT AccountID FROM AccountLookup WITH (NOLOCK) where Email = @Email AND  TokenSource = @TokenSource", new { Email = model.email, TokenSource = "Google"}
                      );
               */

                // select ScheduledJobID, HouseID, CustomerID, Paid, OrderDate, PaymentID from ScheduledJobs with (nolock)
                if (newModel == null)
                {
                    var id = connection.ExecuteScalar<int>(

                   @" INSERT INTO AccountLookup VALUES (@AccessToken, @Email, @Name, @TokenSource, GetDate())  SELECT @@IDENTITY", new
                   {

                       AccessToken = model.accessToken,
                       Email = model.email,
                       Name = model.name,
                       TokenSource = "Google"

                   }
                   );


                    return id;
                }
                else
                {
                    return newModel.AccountID;
                }
            }
        }

        public int PostFacebookToken(FacebookModel model)
        {
            //ScheduledJobID, HouseID, CustomerID
            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                /* var job = connection.QueryFirstOrDefault<ScheduledJobModel>(
                       @"SELECT ScheduledJobID FROM ScheduledJobs where HouseID = @HouseID AND CustomerID = @CustomerID", new { HouseID = model.houseID, CustomerID = model.customerID }
                      );

                 */
                var newModel = connection.QueryFirstOrDefault<FacebookDatabaseModel>(
                       @"SELECT * FROM AccountLookup WITH (NOLOCK) where Email = @Email AND  TokenSource = @TokenSource", new { Email = model.email, TokenSource = "Facebook" }
                      );

                if (newModel != null && newModel.accessToken != model.accessToken)
                {
                    connection.Execute(
                       @"UPDATE AccountLookup SET AccessToken = @AccessToken WHERE Email = @Email AND  TokenSource = @TokenSource", new { AccessToken = model.accessToken, Email = model.email, TokenSource = "Facebook" }
                      );
                }

                /* var job = connection.QueryFirstOrDefault<int>(
                        @"SELECT AccountID FROM AccountLookup WITH (NOLOCK) where Email = @Email AND  TokenSource = @TokenSource", new { Email = model.email, TokenSource = "Google"}
                       );
                */

                // select ScheduledJobID, HouseID, CustomerID, Paid, OrderDate, PaymentID from ScheduledJobs with (nolock)
                if (newModel == null)
                {
                    var id = connection.ExecuteScalar<int>(

                   @" INSERT INTO AccountLookup VALUES (@AccessToken, @Email, @Name, @TokenSource, GetDate())  SELECT @@IDENTITY", new
                   {

                       AccessToken = model.accessToken,
                       Email = model.email,
                       Name = model.name,
                       TokenSource = "Facebook"

                   }
                   );


                    return id;
                }
                else
                {
                    return newModel.AccountID;
                }
            }
        }

        public int PostScheduleJob(ScheduledJobModel model)
        {
            //ScheduledJobID, HouseID, CustomerID
            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                /* var job = connection.QueryFirstOrDefault<ScheduledJobModel>(
                       @"SELECT ScheduledJobID FROM ScheduledJobs where HouseID = @HouseID AND CustomerID = @CustomerID", new { HouseID = model.houseID, CustomerID = model.customerID }
                      );

                 */


                var job = connection.QueryFirstOrDefault<int>(
                       @"SELECT ScheduledJobID FROM ScheduledJobs WITH (NOLOCK) where HouseID = @HouseID AND CustomerID = @CustomerID AND PaymentID = @PaymentID", new { HouseID = model.houseID, CustomerID = model.customerID, PaymentID = model.paymentID }
                      );


                // select ScheduledJobID, HouseID, CustomerID, Paid, OrderDate, PaymentID from ScheduledJobs with (nolock)
                if (job == 0)
                {
                    var id = connection.ExecuteScalar<int>(

                   @" INSERT INTO ScheduledJobs VALUES (@HouseID, @CustomerID, @Paid, GetDate(), @PaymentID, null, null)  SELECT @@IDENTITY", new
                   {

                       HouseID = model.houseID,
                       CustomerID = model.customerID,
                       Paid = model.paid,
                       PaymentID = model.paymentID

                   }
                   );


                    return id;
                }
                else
                {
                    return job;
                }
            }
        }

        public AccountLookupModel GetAccount(AccountTokenModel model)
        {
            
            //address = street.Replace("Avenue", "Ave");
/*
 * 	[AccountID] [int] IDENTITY(1,1) NOT NULL,
[AccessToken] [varchar](1000) NOT NULL,
[Email] [varchar](255) NULL,
[Name] [varchar](255) NULL,
[TokenSource] [varchar](255) NULL,
[InsertedDate] [smalldatetime] NULL*/

using (var connection = new SqlConnection(_connectionString))
{
    connection.Open();
    var customer = connection.QueryFirstOrDefault<AccountLookupModel>(
        @"select al.Email, al.Name, case when sj.PaymentID in ('Cash', 'Check') Then sj.PaymentID ELSE 'Credit Card' END PaymentMethod, sj.OrderDate, sj.InstallDate, sj.RemovalDate 
from AccountLookup al WITH (NOLOCK)
outer apply(
select top(1) * from Customers cu WITH (NOLOCK) 
WHERE LOWER(al.Email) = LOWER(cu.Email)
ORDER BY cu.InsertedDate DESC
) c
left outer join ScheduledJobs sj WITH (NOLOCK) on sj.CustomerID = c.CustomerID AND year(sj.OrderDate) = '2024'
where al.AccessToken = @AccessToken", new { AccessToken = model.token }
        );
                return customer;

}

}
    public HouseModel GetHouse(string address)
{
long floors = 1;
long squareFootage = 0;
string newAddress = address;
//address = street.Replace("Avenue", "Ave");


using (var connection = new SqlConnection(_connectionString))
{
    connection.Open();
    var house = connection.QueryFirstOrDefault<HouseModel>(
        @"SELECT HouseID, Address, SquareFootage, FloorCount  FROM Houses WITH (NOLOCK) where Address = @Address", new { Address = address }
        );

    if (house == null)
    {
        if (newAddress.Contains("W ") || newAddress.Contains("N ") || newAddress.Contains("E ") || newAddress.Contains("S "))
        {
            if (newAddress.Contains("W "))
                newAddress = newAddress.Replace("W ", "West ");

            if (newAddress.Contains("N "))
                newAddress = newAddress.Replace("N ", "North ");

            if (newAddress.Contains("E "))
                newAddress = newAddress.Replace("E ", "East ");

            if (newAddress.Contains("S "))
                newAddress = newAddress.Replace("S ", "South ");



        }
        var webscrape = WebScrape.WebScrape.scrape(newAddress);
        if (webscrape.Result.Contains("does not exist or its address is not valid"))
        {
            return new HouseModel { Address = "NOT FOUND" };
        }
        dynamic stuff = JsonConvert.DeserializeObject(webscrape.Result);
        //dynamic stuff = JsonConvert.DeserializeObject(webscrape);
        try
        {
            floors = stuff.First.features.floorCount.Value;

        }
        catch(Exception ex)
        {
            floors = 1;
            //return new HouseModel { Address = "NO FLOORS" };
        }
        try
        {
            squareFootage = stuff.First.squareFootage.Value;
        }
        catch (Exception ex)
        {
            return new HouseModel { Address = "NO SQUAREFOOTAGE" };
        }



        var id = connection.ExecuteScalar<int>(

       @" INSERT INTO Houses VALUES (@Address, @SquareFootage, @FloorCount)  SELECT @@IDENTITY", new
       {

           Address = address,
           SquareFootage = squareFootage,
           FloorCount = floors
       }
       );
        HouseModel newHouse = new HouseModel();
        if (squareFootage < 1000)
            newHouse.Price = 150.00M;
        else if (squareFootage >= 1000 && squareFootage < 1250)
            newHouse.Price = 175.00M;
        else if (squareFootage >= 1250 && squareFootage < 1500)
            newHouse.Price = 200.00M;
        else if (squareFootage >= 1500 && squareFootage < 1750)
            newHouse.Price = 225.00M;
        else
            newHouse.Price = 250.00M;

        newHouse.TotalPriceBeforeTaxes = newHouse.Price;

        if (floors > 1)
            newHouse.TotalPriceBeforeTaxes += 50.00M;

        return new HouseModel { HouseID = id, Address = address, FloorCount = floors, SquareFootage = squareFootage, Price = newHouse.Price, TotalPriceBeforeTaxes = newHouse.TotalPriceBeforeTaxes };
    }
    else
    {
        if (house.SquareFootage < 1000)
            house.Price = 150.00M;
        else if (house.SquareFootage >= 1000 && house.SquareFootage < 1250)
            house.Price = 175.00M;
        else if (house.SquareFootage >= 1250 && house.SquareFootage < 1500)
            house.Price = 200.00M;
        else if (house.SquareFootage >= 1500 && house.SquareFootage < 1750)
            house.Price = 225.00M;
        else
            house.Price = 250.00M;

        house.TotalPriceBeforeTaxes = house.Price;

        if (house.FloorCount > 1)
            house.TotalPriceBeforeTaxes += 50.00M;


        return house;
    }
}



}
}
}
