internal class Helper
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"Id: {Id} Name: {Name}";
    }
}

public class LinqHelper
{
    private readonly List<Helper> helpers;

    public LinqHelper()
    {
        this.helpers = SeedHelper().ToList();
        FirstExample();
        FirstOrDefaultExample();
    }

    private IEnumerable<Helper> SeedHelper()
    {
        for (int i = 1; i <= 10; i++)
        {
            yield return new Helper
            {
                Id = i,
                Name = $"Helper #{i}",
            };
        }
    }

    public void FirstExample()
    {
        PrintTitle("First Example");

        var first = this.helpers.First();
        Console.WriteLine(first);

        first = this.helpers.First(x => x.Id % 2 == 0);
        Console.WriteLine(first);

        //Throws Exception
        //first = this.helpers.First(x => x.Id % 11  == 0);
        //Console.WriteLine(first);

        PrintSeparator();
    }

    public void FirstOrDefaultExample()
    {
        PrintTitle("FirstOrDefault Example");

        var first = this.helpers.FirstOrDefault();
        Console.WriteLine(first);

        first = this.helpers.FirstOrDefault(x => x.Id % 2 == 0);
        Console.WriteLine(first);

        //Returns NULL
        first = this.helpers.FirstOrDefault(x => x.Id % 11 == 0);
        Console.WriteLine(first);

        PrintSeparator();
    }

    private void PrintTitle(string title)
    {
        Console.WriteLine("**************************");
        Console.WriteLine(title);
        PrintSeparator();
    }
    private void PrintSeparator()
    {
        Console.WriteLine("**************************\n");
    }
}

