using VirtualShoppingStore.Models;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class SQLStatusRepository : IStatusRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext"></param>
        public SQLStatusRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
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

            throw new Exception($"Status with ID {id} not found");
        }




        /// <summary>
        /// add status
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void AddStatus(string statusname)
        {
            var statusdomain = virtualShoppingStoreDbContext.Statuses.Where(x => x.StatusName == statusname);
            if (statusdomain.Any()) {
                throw new Exception("Status name already exists");
            }

            var x = new Status()
            {
                StatusName = statusname,
            };

            virtualShoppingStoreDbContext.Add(x);
            virtualShoppingStoreDbContext.SaveChanges();
            

           
        }




        /// <summary>
        /// delete
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteStatus(string statusname)
        {
            var status = virtualShoppingStoreDbContext.Statuses.FirstOrDefault(y => y.StatusName == statusname)?? throw new Exception("SUCH STATUS NAME DOES NOT EXIST"); ;

            
            //if (status == null)
            //{
            //    throw new Exception("SUCH STATUS NAME DOES NOT EXIST");
            //}

            
            virtualShoppingStoreDbContext.Statuses.Remove(status);

            
            virtualShoppingStoreDbContext.SaveChanges();

        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// /// <param name="statusname"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Status UpdateStatus(int id, string statusname)
        {
            //throw new NotImplementedException();


            var isfound = virtualShoppingStoreDbContext.Statuses.FirstOrDefault(x => x.StatusId == id) ?? throw new Exception("Invalid StatusID");
            
            isfound.StatusName= statusname;

            virtualShoppingStoreDbContext.SaveChanges();
            return isfound;
        }
    }
}
