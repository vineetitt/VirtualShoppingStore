using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Repositories;
using VirtualShoppingStore.Models.DTO.StatusDto;
using VirtualShoppingStore.Models;

namespace VirtualShoppingStore.Controllers
{
    /// <summary>
    /// Controller for managing status entities.
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]

    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository statusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusController"/> class.
        /// </summary>
        /// <param name="statusRepository">The repository to manage status entities.</param>
        public StatusController(IStatusRepository statusRepository)
        {
            this.statusRepository = statusRepository;
        }

        /// <summary>
        /// Retrieves all statuses.
        /// </summary>
        /// <returns>A list of <see cref="ResponseStatusDto"/> objects representing all statuses.</returns>
        /// <response code="200">Returns the list of statuses.</response>
        /// <response code="400">If an unexpected error occurs.</response>

        [HttpGet]

        public IActionResult GetAllStatus()
        {
            try
            {
                var statusData = statusRepository.GetAllStatus();
                var statusList= new List<ResponseStatusDto>();

                foreach (var status in statusData)
                {
                    statusList.Add(new ResponseStatusDto()
                    {
                        StatusId = status.StatusId,
                        StatusName = status.StatusName,
                    });
                }

                return Ok(statusList); 
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Retrieves a status by its ID.
        /// </summary>
        /// <param name="id">The ID of the status to retrieve.</param>
        /// <returns>The <see cref="ResponseStatusDto"/> object representing the status.</returns>
        /// <response code="200">Returns the status.</response>
        /// <response code="400">If an unexpected error occurs.</response>
        /// <response code="404">If the status with the specified ID is not found.</response>

        [HttpGet]
        [Route("{id}")]

        public IActionResult GetStatus(int id)
        {
            try
            {
                var getData = statusRepository.GetStatusById(id);
                return Ok(getData);
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adds a new status.
        /// </summary>
        /// <param name="statusName">The name of the status to add.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns a success message.</response>
        /// <response code="400">If an unexpected error occurs.</response>

        [HttpPost]

        public IActionResult AddStatus(string statusName)
        {

            try
            {
                statusRepository.AddStatus(statusName);
                return Ok("Succesfully Added");
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Deletes a status by its ID.
        /// </summary>
        /// <param name="statusId">The ID of the status to delete.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns a success message.</response>
        /// <response code="400">If an unexpected error occurs.</response>
        /// <response code="404">If the status with the specified ID is not found.</response>

        [HttpDelete]
        [Route("{statusId:int}")]

        public IActionResult DeleteStatus(int statusId)
        {
            try
            {
                statusRepository.DeleteStatus(statusId);
                return Ok();
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates the name of a status by its ID.
        /// </summary>
        /// <param name="id">The ID of the status to update.</param>
        /// <param name="statusname">The new name of the status.</param>
        /// <returns>The updated <see cref="ResponseStatusDto"/> object.</returns>
        /// <response code="200">Returns the updated status.</response>
        /// <response code="400">If an unexpected error occurs.</response>
        /// <response code="404">If the status with the specified ID is not found.</response>

        [HttpPatch]

        public IActionResult UpdateStatus(int id, string statusname)
        {
            try
            {
                var newStatusName = statusRepository.UpdateStatus(id, statusname);
                return Ok(newStatusName);
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
