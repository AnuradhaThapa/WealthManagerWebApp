using System.ComponentModel.DataAnnotations;

namespace WealthManager.Core.Enum
{
    public enum ClientType
    {
        [Display(Name = "Donor Advised Fund")]
        DonorAdvisedFund = 1,

        Corporate = 2,

        [Display(Name = "Joint/Family")]
        JointFamily = 3,

        Personal = 4,
    }
}
