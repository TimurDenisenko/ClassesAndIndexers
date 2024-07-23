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
        Console.WriteLine("---");
        Console.WriteLine(team[0] > team[1]);
        Console.WriteLine(team[9] < team[1]);
        Console.WriteLine(team[2] == team[3]);
        Console.WriteLine(team[3] != team[5]);
        Console.WriteLine("---");
        int a = 2;
        a.Calculation((int x1, int x2) => Console.WriteLine(x1 + x2), 2, 5);
        Console.WriteLine("---");
        Console.WriteLine(a);
        Console.WriteLine("---");
        a.Calculation((int x1, int x2) => x1 + x2, 2, 2);
        Console.WriteLine(a);
        Console.WriteLine("---");
        Console.WriteLine(string.Join(", ",a.Calculation((int x) => x == a, 2, 3, 4, 5, 6, 7, 8)));
        Console.WriteLine(string.Join(", ", a.Calculation((int x) => x == a, a, a)));
        Console.WriteLine("---");
        team[0].SalaryIncreased += (s, e) => Console.WriteLine(e.SalaryIncreaseInfo);
        team[0].IncreaseSalary(100);
    }
}
public static class IntExtensions
{
    public static void Calculation(this ref int num, Func<int, int, int> action, params int[] nums)
    {
        foreach (int numItem in nums)
        {
            num = action(num, numItem);
        }
    }
    public static void Calculation(this ref int num, Action<int, int> action, params int[] nums)
    {
        foreach (int numItem in nums)
        {
            action(num, numItem);
        }
    }
    public static bool[] Calculation(this int num, Predicate<int> action, params int[] nums)
    {
        bool[] bools = new bool[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            bools[i] = action.Invoke(nums[i]);
        }
        return bools;
    }
}
public class SalaryIncreaseEventArgs : EventArgs
{
    public string SalaryIncreaseInfo { get; set; }
    public SalaryIncreaseEventArgs(string salaryIncreaseInfo) => SalaryIncreaseInfo = salaryIncreaseInfo;
}
public delegate void SalaryIncreaseEventHandler(object sender, SalaryIncreaseEventArgs args);
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int Salary { get; private set; }
    public event SalaryIncreaseEventHandler SalaryIncreased;
    public Person(string name, int age, int salary = 100)
    {
        Name = name;
        Age = age;
        Salary = salary;
    }
    public void IncreaseSalary(int salary)
    {
        Salary += salary;
        SalaryIncrease();
    }
    private void SalaryIncrease()
    {
        SalaryIncreased.Invoke(this, new SalaryIncreaseEventArgs("Salary was increased, current salary is "+Salary));
    }
    public static string operator >(Person leftObj, Person rightObj) => AgeComparison(leftObj, rightObj);
    public static string operator <(Person leftObj, Person rightObj) => AgeComparison(leftObj, rightObj);
    public static string operator <=(Person leftObj, Person rightObj) => AgeComparison(leftObj, rightObj);
    public static string operator >=(Person leftObj, Person rightObj) => AgeComparison(leftObj, rightObj);
    public static string operator ==(Person leftObj, Person rightObj) => AgeComparison(leftObj, rightObj, true);
    public static string operator !=(Person leftObj, Person rightObj) => AgeComparison(leftObj, rightObj, true);

    private static string AgeComparison(Person leftObj, Person rightObj, bool notEqual = false)
    {
        if (notEqual && leftObj.Age != rightObj.Age)
            return $"{leftObj.Name} and {rightObj.Name}  are of different ages";
        if (leftObj.Age == rightObj.Age)
            return $"{leftObj.Name} and {rightObj.Name} are the same age";
        else if (leftObj.Age > rightObj.Age)
            return $"{leftObj.Name} is older";
        else
            return $"{rightObj.Name} is older";
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
    public FootballPlayer(string name, int age, int num, int salary = 200) : base(name, age, salary)
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
    public delegate void Action(FootballPlayer player);
}