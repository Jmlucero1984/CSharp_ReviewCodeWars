using System.Collections.Generic;
using System.Linq;
public class Kata
{  
    public static List<string> GetPINs(string s) => 
        new []{"08", "124", "1235", "236", "1457", "24568", "3569", "478", "57890", "689" }[s[0]-'0']
        .SelectMany(c => s.Length > 1 ? GetPINs(s.Substring(1)).Select(p => c+p) : new []{""+c}).ToList();
}



/*****************************************************/
// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

using System;
using System.Collections.Generic;

public class HelloWorld
{
    static string printCombs(List<string> nums){
            var sb="";
            foreach(var i in nums){
                sb+='"'+i+"', ";
                }
            return sb;
         } 
    static Dictionary<string, List<string>> possibles = new Dictionary<string, List<string>>(){
	{"1", new List<string>(){"1","2","4"}},
	{"2",new List<string>(){"1","2","3","5"}},
	{"3",new List<string>(){"2","3","6"}},
	{"4",new List<string>(){"1","4","5","7"}},
	{"5",new List<string>(){"2","4","5","6","8"}},
	{"6",new List<string>(){"9","3","5","6"}},
	{"7",new List<string>(){"4","7","8"}},
	{"8",new List<string>(){"5","7","8","9","0"}},
	{"9",new List<string>(){"6","8","9"}},
    {"0", new List<string>(){"8","0"}},
    };

    static List<string> Combinate(string digits){
        if(digits.Length==1) {
            return possibles[digits];
        } else {
            var s=digits.Substring(0,1);
            var next=digits.Substring(1);
            List<string> output= new List<string>();
            var subcall=Combinate( next);
            foreach(var i in possibles[s]){
                foreach(var j in subcall){
                    var combo=""+i+j;
                    if(!output.Contains(combo)){
                        output.Add(combo);
                    }
                    
                }
            }
            return output;
        }
     
    }
 
 
 
     static public List<string> GetPINs(string observed)
    {
    
        return Combinate(observed);
    }
    
    public static void Main(string[] args)
    {
        var combinations=GetPINs("369");
        Console.WriteLine (printCombs(combinations));
       
    }
}





/***************************************************/












using System.Collections.Generic;
using System.Linq;

public class Kata
{
    public static List<IEnumerable<string>> _numbers = new List<IEnumerable<string>>
    {
        new List<string> {"0", "8"},
        new List<string> {"1", "2", "4"},
        new List<string> {"2", "1", "3", "5"},
        new List<string> {"3", "2", "6"},
        new List<string> {"4", "1", "5", "7"},
        new List<string> {"5", "4", "2", "6", "8"},
        new List<string> {"6", "3", "5", "9"},
        new List<string> {"7", "4", "8"},
        new List<string> {"8", "5", "7", "9", "0"},
        new List<string> {"9", "6", "8"}
    };
    
    public static List<string> GetPINs(string observed)
    {
        return observed.Select(x => _numbers[x - '0'])
                .Aggregate((x, y) => x.Join(y, a => 1, b => 1, (a, b) => a + b))
                .ToList();
    }
}



using System.Collections.Generic;
using System.Linq;

public class Kata
{
    private static List<string> answers;
    
    private static void FindAnswers(string str, List<char[]> options, int pos)
    {
        char[] head = options[pos];
        if(pos == options.Count-1) answers.AddRange(head.Select(o=>str+o));
        else foreach(char c in head) FindAnswers(str+c, options, pos+1);
    }

    public static List<string> GetPINs(string observed)
    {
        answers = new List<string>();
        char[][] neighbours = "80,124,1235,236,1457,24568,3569,478,57890,689".Split(',').Select(s=>s.ToArray()).ToArray();
        var options = observed.ToArray().Select(c => neighbours[c-'0']).ToList();
        FindAnswers("",options,0);
        return answers;
    }
}



using System;
using System.Collections.Generic;
using System.Linq;

public class Kata
{
    public static List<string> GetPINs(string observed)
    {
        var v = "08|124|2135|326|4157|52468|6359|748|85790|968".Split('|');
        var w = observed.Select(o => v[o - '0']).ToArray();
        var n = w.Aggregate(1, (x, s) => x * s.Length);
        var a = Enumerable.Repeat("", n).ToList();
        for (int i = 0, d = 1; i < w.Length; d *= w[i].Length, i++)
            for (int j = 0; j < n; j++)
                a[j] += w[i][j / d % w[i].Length];

        return a;
    }
}













