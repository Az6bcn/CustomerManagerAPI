using Model;
using Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerAPI.DictStrategy
{
    public class ClaimsForRolesStrategy
    {
        private Dictionary<ManagerRolesEnum, CustomClaimTypes> strategyDict = new Dictionary<ManagerRolesEnum, CustomClaimTypes>();

        public ClaimsForRolesStrategy()
        {
            DefineStrategy();
        }

        private void DefineStrategy()
        {
            strategyDict.Add(ManagerRolesEnum.CustomerServiceManager,
                                new CustomClaimTypes { CanCreateCustomer = "True", ManagerRole = ManagerRolesEnum.CustomerServiceManager, CanCreateProduct = "False" });
            strategyDict.Add(ManagerRolesEnum.GeneralManager,
                                new CustomClaimTypes { CanCreateCustomer = "True", ManagerRole = ManagerRolesEnum.GeneralManager, CanCreateProduct = "True" });
            strategyDict.Add(ManagerRolesEnum.ProductsManager,
                                new CustomClaimTypes { CanCreateCustomer = "False", ManagerRole = ManagerRolesEnum.ProductsManager, CanCreateProduct = "True" });
            strategyDict.Add(ManagerRolesEnum.SectionManager,
                                new CustomClaimTypes { CanCreateCustomer = "False", ManagerRole = ManagerRolesEnum.SectionManager, CanCreateProduct = "False" });
        }


        // Product to return the Claim object of Manager depending on the type of Manager we pass
        public CustomClaimTypes getManagerClaims(ManagerRolesEnum managerType)
        {
            // Return the passed Manager claims Value by Key
            return strategyDict[managerType];
        }

    }
}
