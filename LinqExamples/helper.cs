using System;
using System.Collections.Generic;
using System.Xml.Linq;

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
        SingleExample();
        SingleOrDefaultExample();
        AnyExample();
        SelectExample();
        WhereExample();
        OrderByExample();
        OrderByDescendingExample();
        ToLookUpExample();
        GroupByExample();
        MultipleExamples();
    }

    private IEnumerable<Helper> SeedHelper()
    {
        var names = new List<string>() { "Um", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove", "Dez"};
        for (int i = 1; i <= 10; i++)
        {
            yield return new Helper
            {
                Id = i,
                Name = names[i-1],
            };
        }
    }

    public void FirstExample()
    {
        PrintTitle("First");

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
        PrintTitle("FirstOrDefault");

        var first = this.helpers.FirstOrDefault();
        Console.WriteLine(first);

        first = this.helpers.FirstOrDefault(x => x.Id % 2 == 0);
        Console.WriteLine(first);

        //Returns NULL
        first = this.helpers.FirstOrDefault(x => x.Id % 11 == 0);
        Console.WriteLine(first);

        PrintSeparator();
    }

    public void SingleExample()
    {
        PrintTitle("Single");

        var single = this.helpers.Single(x => x.Id % 9 == 0);
        Console.WriteLine(single);

        //Throws Exception
        //single = this.helpers.Single(x => x.Id % 2 == 0);
        //Console.WriteLine(single);

        PrintSeparator();
    }

    public void SingleOrDefaultExample()
    {
        PrintTitle("SingleOrDefault");

        var single = this.helpers.SingleOrDefault(x => x.Id % 9 == 0);
        Console.WriteLine(single);

        //Throws Exception
        //single = this.helpers.SingleOrDefault(x => x.Id % 2 == 0);
        //Console.WriteLine(single);

        //Returns null
        single = this.helpers.SingleOrDefault(x => x.Id % 11 == 0);
        Console.WriteLine(single);

        PrintSeparator();
    }

    public void AnyExample()
    {
        PrintTitle("Any");

        var any = this.helpers.Any();
        Console.WriteLine(any);

        // returns false
        any = this.helpers.Any(x => x.Id % 11 == 0);
        Console.WriteLine(any);

        // returns true
        any = this.helpers.Any(x => x.Id % 2 == 0);
        Console.WriteLine(any);

        PrintSeparator();
    }

    public void SelectExample()
    {
        PrintTitle("Select");

        var select = this.helpers.Select(h => h.Id);

        foreach (var s in select)
        {
            Console.WriteLine(s);
        }

        Console.WriteLine();

        var select2 = this.helpers.Select(h => $"{h.Id} - {h.Name}");

        foreach(var s in select2)
        {
            Console.WriteLine(s);
        }

        PrintSeparator();
    }

    public void WhereExample()
    {
        PrintTitle("Where");

        var evenHelper = this.helpers.Where(x => x.Id % 2 == 0);
        foreach (var s in evenHelper)
        {
            Console.WriteLine(s);
        }

        Console.WriteLine();

        // LINQ query syntax
        var evenHelper2 = from r in this.helpers
                             where r.Id % 2 == 0
                             select r.Id;

        foreach (var s in evenHelper2)
        {
            Console.WriteLine(s);
        }

        PrintSeparator();
    }

    public void OrderByExample()
    {
        PrintTitle("OrderBy");

        var copy = new List<Helper>(this.helpers);

        copy.Add(new Helper
        {
            Id = 1,
            Name = "Segundo um",
        });

        var order = copy.OrderBy(x => x.Name);
        foreach (var element in order)
        {
            Console.WriteLine(element);
        }

        Console.WriteLine();

        order = copy.OrderBy(x => x.Id).ThenBy(x=> x.Name);
        foreach (var element in order)
        {
            Console.WriteLine(element);
        }

        PrintSeparator();
    }

    public void OrderByDescendingExample()
    {
        PrintTitle("OrderByDescending");

        var copy = new List<Helper>(this.helpers);

        copy.Add(new Helper
        {
            Id = 1,
            Name = "Segundo um",
        });

        var order = copy.OrderByDescending(x => x.Name);
        foreach (var element in order)
        {
            Console.WriteLine(element);
        }

        Console.WriteLine();

        order = copy.OrderByDescending(x => x.Id).ThenByDescending(x => x.Name);
        foreach (var element in order)
        {
            Console.WriteLine(element);
        }

        PrintSeparator();
    }

    public void ToLookUpExample()
    {
        PrintTitle("ToLookUp");


        var lookup = this.helpers.ToLookup(x => x.Id % 2);
        foreach (var element in lookup)
        {
            Console.WriteLine(element.Key);
            foreach(var e in element)
            {
                Console.WriteLine("- " + e);
            }
        }

        PrintSeparator();
    }

    public void GroupByExample()
    {
        PrintTitle("GroupBy");


        var groupby = this.helpers.GroupBy(x => x.Id % 2);
        foreach (var element in groupby)
        {
            Console.WriteLine("Resto da divisão por 2 = " + element.Key);
            foreach (var e in element)
            {
                Console.WriteLine("- " + e);
            }
        }

        PrintSeparator();

        /*
           What happens when you call ToLookup on an object representing a remote database table with a billion rows in it?

            The billion rows are sent over the wire, and you build the lookup table locally.

           What happens when you call GroupBy on such an object?

            A query object is built; end of story.

           When that query object is enumerated then the analysis of the table is done on the database server and the grouped results are sent back on demand a few at a time.

           Logically they are the same thing but the performance implications of each are completely different. Calling ToLookup means I want a cache of the entire thing right now organized by group. 
           Calling GroupBy means "I am building an object to represent the question 'what would these things look like if I organized them by group?'"
        */
    }

    public void MultipleExamples()
    {
        var groupby = this.helpers.GroupBy(x => x.Id % 2);
        foreach (var element in groupby)
        {
            Console.WriteLine("Resto da divisão por 2 = "+ element.Key);
            var example = element.Select(e => $"{e.Id} - {e.Name}");
            foreach (var e in example)
            {
                Console.WriteLine("- " + e);
            }
        }

        Console.WriteLine();

        var example2 = this.helpers.Where(x => x.Id % 2 == 0).OrderByDescending(x => x.Id).Select(x => $"{x.Id} - {x.Name}");
        foreach (var e in example2)
        {
            Console.WriteLine("- " + e);
        }
    }

    private void PrintTitle(string title)
    {
        Console.WriteLine("**************************");
        Console.WriteLine(title);
        Console.WriteLine();
    }
    private void PrintSeparator()
    {
        Console.WriteLine("**************************\n");
    }
}

