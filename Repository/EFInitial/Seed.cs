namespace Repository.EFInitial;

public static class Seed
{
    public static async Task SeedData(DataContext context)
    {
        var taskList = new List<Models.Task>();

        var rnd = new Random();

        if (!context.Task.Any())
        {
            taskList =
            [
                new()
                {
                    Details = "Praise the Lord",
                    IsDone = true
                },

                new()
                {
                    Details = "Break the law",
                    IsDone = false
                },

                new()
                {
                    Details = "Take what's mine",
                    IsDone = false
                },
                new()
                {
                    Details = "Take some more",
                    IsDone = false
                }
            ];
            await context.Task.AddRangeAsync(taskList);
        }
        else
        {
            taskList = context.Task.ToList();
        }
        
        await context.SaveChangesAsync();
    }
}