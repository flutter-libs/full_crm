

namespace backend.Areas.Main.Models.ViewModels;

public class AddCampaignViewModel : Campaign
{
    
}

public class UpdateCampaignViewModel : Campaign
{
    
}

public class AddCampaignTaskViewModel
{
    public Tasks Tasks { get; set; }
    public int CampaignId { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}

public class UpdateCampaignTaskViewModel
{
    public Tasks Tasks { get; set; }
    public int CampaignId { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}