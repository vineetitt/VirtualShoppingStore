using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Repositories;
using VirtualShoppingStore.Models.DTO.StatusDto;
using VirtualShoppingStore.Models;

namespace VirtualShoppingStore.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository statusRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusRepository"></param>
        public StatusController(IStatusRepository statusRepository)
        {
            this.statusRepository = statusRepository;
        }


        /// <summary>
        /// Get All Status
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetAllStatus()
        {
            try
            {
                var statusdata = statusRepository.GetAllStatus();
                var statuslist= new List<ResponseStatusDto>();

                foreach (var status in statusdata) {
                    statuslist.Add(new ResponseStatusDto()
                    {
                        StatusId = status.StatusId,
                        StatusName = status.StatusName,
                    });

                }

                return Ok(statuslist); 
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex) { 
                return Ok(ex.Message);

            }

        }

        /// <summary>
        /// Get status by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetStatus(int id)
        {
            try
            {
                var getdata = statusRepository.GetStatusById(id);
                return Ok(getdata);
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// ADD status
        /// </summary>
        /// <param name="statusName"></param>
        /// <returns></returns>
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

            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete Status by status Id
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>

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

            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update Status Name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="statusname"></param>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult UpdateStatus(int id, string statusname)
        {
            try
            {
                var newstatusname = statusRepository.UpdateStatus(id, statusname);
                return Ok(newstatusname);
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
