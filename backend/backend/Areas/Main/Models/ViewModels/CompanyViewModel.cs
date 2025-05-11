namespace backend.Areas.Main.Models.ViewModels;

public class AddCompanyViewModel
{
    
}

public class UpdateCompanyViewModel
{
    
}

public class AddCompanyTaskViewModel
{
    public Tasks Tasks { get; set; }
    public int CompanyId { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}

public class UpdateCompanyTaskViewModel
{
    public Tasks Tasks { get; set; }
    public int CompanyId { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}