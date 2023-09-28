using AutoPAS_Customer_portal.Data;
using AutoPAS_Customer_portal.Models.Domain;
using AutoPAS_Customer_portal.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace AutoPAS_Customer_portal.Service
{
    public class CustomerPortalRepository: CustomerPortalInterface
    {
        private readonly LocalDbContext _localDbContext;
        private readonly VMDbContext _vMDbContext;

        public CustomerPortalRepository(LocalDbContext localDbContext, VMDbContext vMDbContext)
        {
            _localDbContext = localDbContext;
            _vMDbContext = vMDbContext;
        }

        public async Task<PortalUser> validateLogin(string userName, string password)
        {
            var result = await _localDbContext.portalUsers.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<Policy> validatePolicyNumber(int policyNumber)
        {
            var policy = await _vMDbContext.Policy.FirstOrDefaultAsync(p => p.PolicyNumber == policyNumber);
            if (policy == null)
            {
                return null;
            }
           return policy;
        }

        public async Task<Policyvehicle> GetPolicyVehicleRecord(int policyNumber)
        {
            var policy = await _vMDbContext.Policy.FirstOrDefaultAsync(p => p.PolicyNumber == policyNumber);
            var policyVehicleRecord = await _vMDbContext.Policyvehicle.FirstOrDefaultAsync(v => v.PolicyId == policy.PolicyId);
            if (policyVehicleRecord == null)
            {
                return null;
            }
            return policyVehicleRecord;
        }

        public async Task<Vehicle> validateChasisNumber(string vehicleId, string chasisNumber)
        {
            var vehicle = await _vMDbContext.Vehicle.FirstOrDefaultAsync(v => v.VehicleId == vehicleId && v.ChasisNumber == chasisNumber);
            if (vehicle == null)
            {
                return null;
            }
            return vehicle;
            
            
        }

        public async Task<UserPloicyList> AddPolicyNumberToPolicyList(Guid userId, int policyNumber)
        {
            UserPloicyList userPloicyList = new UserPloicyList
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PolicyNumber = policyNumber
            };

            await _localDbContext.userPloicyList.AddAsync(userPloicyList);
            await _localDbContext.SaveChangesAsync();
            return userPloicyList;
        }

        public async Task<List<UserPloicyList>> GetPolicyNumbersOfUser(Guid userId)
        {
            var policyNumbers = await _localDbContext.userPloicyList.Where(p => p.UserId == userId).ToListAsync();
            if (policyNumbers == null)
            {
                return null;
            }
            return policyNumbers;
        }

        public async Task<VehicleDto> GetVehicle(int policyNumber)
        {
            var policy = await _vMDbContext.Policy.FirstOrDefaultAsync(p => p.PolicyNumber == policyNumber);
            var policyVehicle = await _vMDbContext.Policyvehicle.FirstOrDefaultAsync(v => v.PolicyId == policy.PolicyId);
            var vehicleDetails = await _vMDbContext.Vehicle.Where(v => v.VehicleId == policyVehicle.VehicleId)
                .Select(v => new VehicleDto
                {
                    PolicyNumber = policyNumber,
                    VehicleType = v.VehicleType.VehicleType,
                    Rtoname = v.Rto.Rtoname,
                    City = v.Rto.City,
                    State = v.Rto.State,
                    RegistrationNumber = v.RegistrationNumber,
                    DateOfPurchase = v.DateOfPurchase,
                    Brand = v.Brand.brand,
                    Modelname = v.Model.Modelname,
                    Variant = v.Variant.variant,
                    BodyType = v.BodyType.BodyType,
                    FuelType = v.FuelType.FuelType,
                    TransmissionType = v.TransmissionType.TransmissionType,
                    Color = v.Color,
                    ChasisNumber = v.ChasisNumber,
                    EngineNumber = v.EngineNumber,
                    CubicCapacity = v.CubicCapacity,
                    SeatingCapacity = v.SeatingCapacity,
                    YearOfManufacture = v.YearOfManufacture,
                    Idv = v.Idv,
                    ExShowroomPrice = v.ExShowroomPrice
        
                }).FirstOrDefaultAsync();

            return vehicleDetails;
        }

        public async Task<string> DeletePolicyNumber(int policyNumber)
        {
            var userPolicy = await _localDbContext.userPloicyList.FirstOrDefaultAsync(p => p.PolicyNumber == policyNumber);
            _localDbContext.userPloicyList.Remove(userPolicy);
            await _localDbContext.SaveChangesAsync();
            return "success";
        }
    }
}
