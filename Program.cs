public class Program
{
    public static void Main(string[] args)
    {
        FootballTeam team = new FootballTeam("Team 1", new FootballPlayer[]
                {
            new FootballPlayer("P1",18,0),
            new FootballPlayer("P2",17,1),
            new FootballPlayer("P3",19,2),
            new FootballPlayer("P4",19,3),
            new FootballPlayer("P5",19,4),
            new FootballPlayer("P6",13,5),
            new FootballPlayer("P7",19,6),
            new FootballPlayer("P8",16,7),
            new FootballPlayer("P9",21,8),
            new FootballPlayer("P10",54,9),
            new FootballPlayer("P11",32,10),
                });
        Console.WriteLine(team[0]);
        Console.WriteLine(team[0]["Person"]);
        Console.WriteLine(team[0]["Name"]);
        Console.WriteLine(team[0]["Age"]);
        team[0]["Age"] = "12";
        Console.WriteLine(team[0]["Age"]);
        Console.WriteLine("---");
        Person test = new FootballPlayer("P12", 43, 12);
        Console.WriteLine(test);
        team[0] = test as FootballPlayer;
        Console.WriteLine("---");
        Console.WriteLine(team[0]);
        Console.WriteLine("---");
        Console.WriteLine(team);
        Console.WriteLine("---");
        Console.WriteLine(team["P2"]);
    }
}
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person(string name, int age)
    { 
        Name = name;
        Age = age;
    }
    public string this[string _case]
    {
        get => 
            _case switch
            {
                "Person" => this.ToString(),
                "Name" => Name,
                "Age" => Age.ToString(),
                _ => ""
            };
        set
        {
            switch (_case)
            {
                case "Name":
                    Name = value;
                    break;
                case "Age":
                    Age = int.Parse(value);
                    break;
                default:
                    break;
            }
        }
    }
    public override string ToString() => $"{Name}: {Age}";
}
public class FootballPlayer : Person
{
    public int Num { get; set; }
    public FootballPlayer(string name, int age, int num) : base(name, age)
    {
        Num = num;
    }
    public override string ToString() => $"{base.ToString()} | {Num}";
}
public class FootballTeam
{
    private readonly FootballPlayer[] _team = new FootballPlayer[11]; 
    public string TeamName { get; set; }
    public FootballTeam(string teamName, FootballPlayer[] team)
    {
        TeamName = teamName;
        _team = team;
    }
    public string this[string name]
    {
        get => _team.Single(x => x.Name == name).ToString();

    }
    public FootballPlayer this[int num]
    {
        get => CheckIndex(num) ? _team[num] : null;
        set 
        {
            if (!CheckIndex(num)) return;
            _team[num] = value;
        }
    }
    private bool CheckIndex(int num) => num >= 0 && num <= 11;
    public override string ToString() => $"{TeamName} \n{string.Join<FootballPlayer>("\n", _team)}";
}