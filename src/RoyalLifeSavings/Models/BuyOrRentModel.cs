using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoyalLifeSavings.Models;

public enum BuyOrRentOptions
{
    None,

    [Description("Looking to rent a single copy of an eBook")]
    RentSingle,

    [Description("Looking to rent multiple copies of an eBook")]
    RentMultiple,

    [Description("Looking to buy multiple copies of an eBook")]
    BuyMultiple
}

public class BuyOrRentModel
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public BuyOrRentOptions BuyOrRentOptions { get; set; }

    public bool LifeguardingManual { get; set; }

    public bool SwimmingAndWaterSafetyManual { get; set; }

    public bool FirstAidManual { get; set; }

    [Required(ErrorMessage = "The First Name field is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "The Last Name field is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "The Email field is required"), EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "The State field is required")]
    public string State { get; set; }

    [Required(ErrorMessage = "The Phone Number field is required"), Phone]
    public string PhoneNumber { get; set; } = null!;

    public string SchoolOrg { get; set; }

    public string OtherInformation { get; set; }
}
