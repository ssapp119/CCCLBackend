using QandA.Models;

namespace QandA.Data
{
    public interface IDataRepository
    {
        HouseModel GetHouse(string address);
        int PostCustomer(CustomerModel model);

        int PostScheduleJob(ScheduledJobModel model);

        public int PostFacebookToken(FacebookModel model);
        public int PostGoogleToken(GoogleModel model);
        public AccountLookupModel GetAccount(string token);



    }
}
