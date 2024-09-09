using VirtualShoppingStore.Models;

namespace VirtualShoppingStore.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    public interface IStatusRepository
    {

        /// <summary>
        /// GetAllStatus
        /// </summary>
        /// <returns></returns>
        public List<Status> GetAllStatus();

        /// <summary>
        /// GetStatusById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Status GetStatusById(int id);

        /// <summary>
        /// AddStatus
        /// </summary>
        /// <param name="statusName"></param>
        public void AddStatus(string statusName);

        /// <summary>
        /// DeleteStatus
        /// </summary>
        public void DeleteStatus(int statusId);

        /// <summary>
        /// UpdateStatus
        /// </summary>
        /// <param name="id"></param>
        /// <param name="statusname"></param>
        /// <returns></returns>
        public Status UpdateStatus(int id, string statusname);

    }

}
