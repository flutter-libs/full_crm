namespace backend.Areas.Main.Models.ViewModels;

public class AddJobTaskViewModel
{
    public Tasks Tasks { get; set; }
    public int JobId { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}

public class UpdateJobTaskViewModel
{
    public Tasks Tasks { get; set; }
    public int JobId { get; set; }
    public DateTime Updated { get; set; } = DateTime.Now;
}