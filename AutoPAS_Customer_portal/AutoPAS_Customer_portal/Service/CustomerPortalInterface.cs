using AutoPAS_Customer_portal.Models.Domain;
using AutoPAS_Customer_portal.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace AutoPAS_Customer_portal.Service
{
    public interface CustomerPortalInterface
    {
        public Task<PortalUser> validateLogin(string userName, string password);
        public Task<Policy> validatePolicyNumber(int policyNumber); 
        public Task<Policyvehicle> GetPolicyVehicleRecord(int policyNumber);
        public Task<Vehicle> validateChasisNumber(string vehicleId, string chasisNumber);
        public Task<UserPloicyList> AddPolicyNumberToPolicyList(Guid userId, int policyNumber);
        public Task<List<UserPloicyList>> GetPolicyNumbersOfUser(Guid userId);
        public Task<VehicleDto> GetVehicle(int policyNumber);
        public Task<string> DeletePolicyNumber(int policyNumber);

    }
}
