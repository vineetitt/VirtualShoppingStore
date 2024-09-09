using VirtualShoppingStore.Models;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// StatusRepository class
    /// </summary>
    public class StatusRepository : IStatusRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext"></param>
        public StatusRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }

        /// <summary>
        /// get status
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Status> GetAllStatus()
        {
            var data= virtualShoppingStoreDbContext.Statuses.ToList();

            if (!data.Any()) {
                throw new Exception("No statuses found");
            }

            return data;

        }

        /// <summary>
        /// Get status by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Status GetStatusById(int id)
        {
            var statusdetail = virtualShoppingStoreDbContext.Statuses.FirstOrDefault(x => x.StatusId == id);
            if (statusdetail != null)
            {
                var found = new Status()
                {
                    StatusId = statusdetail.StatusId,
                    StatusName = statusdetail.StatusName,
                };
                return found;
            }

            throw new CustomException($"Status with ID {id} not found",400);
        }

        /// <summary>
        /// add status
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        
        public void AddStatus(string statusname)
        {
            var statusdomain = virtualShoppingStoreDbContext.Statuses.Where(x => x.StatusName == statusname);
            if (statusdomain.Any()) {
                throw new CustomException("Status name already exists",400);
            }

            var x = new Status()
            {
                StatusName = statusname,
            };

            virtualShoppingStoreDbContext.Add(x);
            virtualShoppingStoreDbContext.SaveChanges();
           
        }

        /// <summary>
        /// delete status
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteStatus(int statusId)
        {
            var status = virtualShoppingStoreDbContext.Statuses.FirstOrDefault(y => y.StatusId == statusId) ?? throw new Exception("Invalid StatusId"); 

            if (status == null)
            {
                throw new CustomException("SUCH STATUS NAME DOES NOT EXIST",400);
            }

            virtualShoppingStoreDbContext.Statuses.Remove(status);

            virtualShoppingStoreDbContext.SaveChanges();

        }

        /// <summary>
        /// UpdateStatus
        /// </summary>
        /// <param name="id"></param>
        /// /// <param name="statusname"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Status UpdateStatus(int id, string statusname)
        {

            var isfound = virtualShoppingStoreDbContext.Statuses.FirstOrDefault(x => x.StatusId == id) ?? throw new CustomException("Invalid StatusID",400);
            
            isfound.StatusName= statusname;

            virtualShoppingStoreDbContext.SaveChanges();
            return isfound;
        }

    }

}
