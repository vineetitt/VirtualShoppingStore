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
                return Ok(statusdata); 
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
            catch(Exception ex) { 
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

            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }





        /// <summary>
        /// Delete Status
        /// </summary>
        /// <param name="statusname"></param>
        /// <returns></returns>
        [HttpDelete]

        public IActionResult DeleteStatus(string statusname)
        {
            try
            {
                statusRepository.DeleteStatus(statusname);
                
                return Ok();

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
