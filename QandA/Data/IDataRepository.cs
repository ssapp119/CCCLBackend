using QandA.Models;

namespace QandA.Data
{
    public interface IDataRepository
    {
        HouseModel GetHouse(string address);
        int PostCustomer(CustomerModel model);

        int PostScheduleJob(ScheduledJobModel model);


    }
}
