using VirtualShoppingStore.Models;

namespace VirtualShoppingStore.Repositories
{


    /// <summary>
    /// Defines the contract for status-related data operations.
    /// </summary>

    public interface IStatusRepository
    {

        /// <summary>
        /// Retrieves all statuses.
        /// </summary>
        /// <returns>A list of <see cref="Status"/> objects representing all statuses.</returns>
        public List<Status> GetAllStatus();

        /// <summary>
        /// Retrieves a status by its ID.
        /// </summary>
        /// <param name="id">The ID of the status to retrieve.</param>
        /// <returns>The <see cref="Status"/> object representing the status.</returns>
        public Status GetStatusById(int id);

        /// <summary>
        /// Adds a new status.
        /// </summary>
        /// <param name="statusName">The name of the status to add.</param>
        public void AddStatus(string statusName);

        /// <summary>
        /// Deletes a status by its ID.
        /// </summary>
        /// <param name="statusId">The ID of the status to delete.</param>
        public void DeleteStatus(int statusId);

        /// <summary>
        /// Updates the name of a status by its ID.
        /// </summary>
        /// <param name="id">The ID of the status to update.</param>
        /// <param name="statusName">The new name of the status.</param>
        /// <returns>The updated <see cref="Status"/> object.</returns>
        public Status UpdateStatus(int id, string statusName);

    }

}
