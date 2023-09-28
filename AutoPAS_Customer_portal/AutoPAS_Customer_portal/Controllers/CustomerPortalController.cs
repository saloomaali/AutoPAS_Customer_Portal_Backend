using AutoPAS_Customer_portal.Models.Domain;
using AutoPAS_Customer_portal.Service;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace AutoPAS_Customer_portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPortalController : ControllerBase
    {
        private readonly CustomerPortalInterface _customerPortalInterface;

        public CustomerPortalController(CustomerPortalInterface @customerPortalInterface)
        {
            _customerPortalInterface = @customerPortalInterface;
        }

        //Validating Login credentials
        [HttpGet]
        public async Task<IActionResult> Login(string userName, string password)
        {
            try
            {
                var result = await _customerPortalInterface.validateLogin(userName, password);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
            
        }

        //Validate PolicyNumber and ChasisNumber and add PolicyNumber to UserPolicyList  if validation success 
        [HttpPost]
        public async Task<IActionResult> AddPloicyNumber(Guid userId, int policyNumber, string chasisNumber)
        {
            try
            {
                var policy = await _customerPortalInterface.validatePolicyNumber(policyNumber);
                if (policy != null)
                {
                    var policyVehicleRecord = await _customerPortalInterface.GetPolicyVehicleRecord(policyNumber);
                    if (policyVehicleRecord != null)
                    {
                        var vehicle = await _customerPortalInterface.validateChasisNumber(policyVehicleRecord.VehicleId,chasisNumber);
                        if (vehicle != null)
                        {
                            var userPlociy = await _customerPortalInterface.AddPolicyNumberToPolicyList(userId, policyNumber);
                            if (userPlociy != null)
                            {
                                return Ok(userPlociy);
                            }

                        }
                        return NotFound("No Such ChasisNumber Exists");
                    }
                    return NotFound("No Vehicle Found For Corresponding PolicyNumber");
                }
                return NotFound("No Such PolicyNumber Exists");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        //Show policyNumbers inuserPolicyList
        [HttpGet]
        [Route("{userId:Guid}")]
        public async Task<IActionResult> GetPolicyNumbers([FromRoute] Guid userId)
        {
            try
            {
                var policyNumbers = await _customerPortalInterface.GetPolicyNumbersOfUser(userId);
                if (policyNumbers.Count <= 0)
                {
                    return NotFound("There is No policyNumber added for corresponding user");
                }
                return Ok(policyNumbers);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Get Vehicle Details
        [HttpGet]
        [Route("{policyNumber:int}")]
        public async Task<IActionResult> GetVehicleDetails([FromRoute] int policyNumber)
        {
            try
            {
                var vehicleDetails = await _customerPortalInterface.GetVehicle(policyNumber);
                return Ok(vehicleDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Delete policyNumber from userPolicyList
        [HttpDelete]
        [Route("{policyNumber:int}")]
        public async Task<IActionResult> DeletePolicynumberFromUserPolicyList([FromRoute] int policyNumber)
        {
            try
            {
                string response = await _customerPortalInterface.DeletePolicyNumber(policyNumber);
                if (response == null)
                {
                    return BadRequest();
                }
                return Ok("Deleted Successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }

}