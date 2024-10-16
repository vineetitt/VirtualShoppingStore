using VirtualShoppingStore.Models;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// Repository for managing status entities.
    /// </summary>
    public class StatusRepository : IStatusRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusRepository"/> class.
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext">The database context to manage status entities.</param>

        public StatusRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }

        /// <summary>
        /// Retrieves all statuses.
        /// </summary>
        /// <returns>A list of <see cref="Status"/> objects representing all statuses.</returns>
        /// <exception cref="CustomException">Thrown when no statuses are found.</exception>
        
        public List<Status> GetAllStatus()
        {

            var statusList= virtualShoppingStoreDbContext.Statuses.ToList();

            if (!statusList.Any())
            {
                throw new CustomException("No statuses found",404);
            }

            return statusList;

        }

        /// <summary>
        /// Retrieves a status by its ID.
        /// </summary>
        /// <param name="id">The ID of the status to retrieve.</param>
        /// <returns>The <see cref="Status"/> object representing the status.</returns>
        /// <exception cref="CustomException">Thrown when the status with the specified ID is not found.</exception>

        public Status GetStatusById(int id)
        {

            var foundStatus = virtualShoppingStoreDbContext.Statuses.FirstOrDefault(status => status.StatusId == id);
            
            if (foundStatus != null)
            {
                return foundStatus;   
            }

            throw new CustomException($"Status with ID {id} not found",404);

        }

        /// <summary>
        /// Adds a new status.
        /// </summary>
        /// <param name="statusName">The name of the status to add.</param>
        /// <exception cref="CustomException">Thrown when the status name already exists.</exception>

        public void AddStatus(string statusName)
        {

            var statusExists = virtualShoppingStoreDbContext.Statuses.AsEnumerable().Any(status => status.StatusName.Equals(statusName, StringComparison.OrdinalIgnoreCase));

            if (statusExists)
            {      
                throw new CustomException("Status name already exists",400);
            }

            var newStatus = new Status()     
            {
                StatusName = statusName,
            };
           
            virtualShoppingStoreDbContext.Add(newStatus);
            virtualShoppingStoreDbContext.SaveChanges();
           
        }

        /// <summary>
        /// Deletes a status by its ID.
        /// </summary>
        /// <param name="statusId">The ID of the status to delete.</param>
        /// <exception cref="CustomException">Thrown when the status with the specified ID does not exist.</exception>

        public void DeleteStatus(int statusId)
        {
            
            var statusToDelete = virtualShoppingStoreDbContext.Statuses.FirstOrDefault(currentStatus => currentStatus.StatusId == statusId); 

            if (statusToDelete == null)
            {
                throw new CustomException($"Status with ID {statusId} does not exist.", 404);
            }

            virtualShoppingStoreDbContext.Statuses.Remove(statusToDelete);
            virtualShoppingStoreDbContext.SaveChanges();

        }

        /// <summary>
        /// Updates the name of a status by its ID.
        /// </summary>
        /// <param name="id">The ID of the status to update.</param>
        /// <param name="statusname">The new name of the status.</param>
        /// <returns>The updated <see cref="Status"/> object.</returns>
        /// <exception cref="CustomException">Thrown when the status with the specified ID is not found or the new status name already exists.</exception>

        public Status UpdateStatus(int id, string statusname)
        {

            var isfound = virtualShoppingStoreDbContext.Statuses.FirstOrDefault(status => status.StatusId == id);

            if (isfound == null)
            {
                throw new CustomException($"Status with ID {id} not found", 404);
            }

            var statusExists = virtualShoppingStoreDbContext.Statuses.AsEnumerable().Any(status => status.StatusName.Equals(statusname, StringComparison.OrdinalIgnoreCase) && status.StatusId != id);

            if (statusExists)
            {
                throw new CustomException("Status name already exists", 400);
            }

            isfound.StatusName= statusname;
            virtualShoppingStoreDbContext.SaveChanges();

            return isfound;

        }

    }

}
