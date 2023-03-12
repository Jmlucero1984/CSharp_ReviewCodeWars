using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

public class HelloWorld
{
   static int expOrder=0;
     static string varLetter="";
  
  
     static List<string>  reduceAndOrder(List<string> lista, string variableB, int maxOrder){
        Dictionary<string, long> orders=new Dictionary<string, long>();
        string variable=variableB;
        for(int i=maxOrder;i>1;i--) {
            var key=""+variable+"^"+i;
            orders.Add(key,0);
        }
        orders.Add(variable,0);
        orders.Add("C",0);
        foreach(var i in lista){
            var ind=i.IndexOf(variable);
            if (ind>-1){
            var coeff=i.Substring(0,ind);
            var varexp=i.Substring(ind);
            if(coeff=="") {coeff="1";}
         
                orders[varexp]+=long.Parse(coeff);
            } else{
            
               orders["C"] +=long.Parse(i);
            }

        }
        List<string> output= new List<string>();
        foreach(KeyValuePair<string, long> kvp in orders){
          if(kvp.Value!=0){
            if(kvp.Key!="C") {
              var c="";
              if(kvp.Value==1) {c="";}
              else if(kvp.Value==-1) {c="-";}
              else {c=""+kvp.Value;}
              
                output.Add(""+c+kvp.Key);
            } else {
                output.Add(""+kvp.Value);
            }
          }
        }
        return output;
    }
    static string multCoeff(string coeff1, string coeff2){
        string output="";
        var cs1=coeff1.Split("^");
        var cs2=coeff2.Split("^");
        if(cs1.Length>1 && cs2.Length>1){
            if(cs1[0]==cs2[0]){
                var exp=long.Parse(cs1[1])+long.Parse(cs2[1]);
                output= ""+cs1[0]+"^"+exp;
            } else {
                output= coeff1.Length>coeff2.Length?coeff2+coeff1:coeff1+coeff2;
            }
        } else if(cs1.Length>1 &&  cs1[0]==coeff2){
             output= ""+cs1[0]+"^"+(long.Parse(cs1[1])+1);
        } else if(cs2.Length>1 && cs2[0]==coeff1){
            output= ""+cs2[0]+"^"+(long.Parse(cs2[1])+1);
        }
         else if(coeff1==coeff2) output=coeff1+"^2";
         else output= coeff1.Length>coeff2.Length? coeff2+coeff1:coeff1+coeff2;
         return output;
        
    }
    
    static string multAlg(string op1, string op2){
        string alg="";
        string pattern = @"([a-zA-Z][\^]?[0-9]*)";
        Regex rg = new Regex(pattern);
         if(op1!="" && op2!=""){
        MatchCollection matchAlgOp1= rg.Matches(op1);
        MatchCollection matchAlgOp2= rg.Matches(op2);
        for (int count = 0; count < matchAlgOp1.Count; count++){
            string algOp1 = matchAlgOp1[count].Value;
            for (int count2 = 0; count2 < matchAlgOp2.Count; count2++){
            string algOp2 = matchAlgOp2[count2].Value;
            alg+=multCoeff(algOp1,algOp2);
            }  
        }
        
        } else{ alg=op1+op2;}
         return alg;
         
    }
             
 
    static string multiply(string op1, string op2){
         string emptynum=@"[0-9].*";
        Regex nonum = new Regex(emptynum);
        if(!op1.Contains(varLetter)&&!op2.Contains(varLetter)){
            return ""+((long.Parse(op1))*(long.Parse(op2)));
        } else if (op1.Contains(varLetter)&&op2.Contains(varLetter)){
            string intop1=op1.Substring(0,op1.IndexOf(varLetter));
            if(!nonum.IsMatch(intop1)){
                intop1+="1";}
            
            string algop1=op1.Substring(op1.IndexOf(varLetter));
            string intop2=op2.Substring(0,op2.IndexOf(varLetter));
            if(!nonum.IsMatch(intop2)){
                intop2+="1";}
            
            string algop2=op2.Substring(op2.IndexOf(varLetter));
            var numeric=(long.Parse(intop1))*(long.Parse(intop2));
            
            var alge=multAlg(algop1,algop2);
            return ""+numeric+alge;
            
        } else {
            string intop1=op1;
            string algop1="";
            if(op1.Contains(varLetter)){
            intop1=op1.Substring(0,op1.IndexOf(varLetter));
            algop1=op1.Substring(op1.IndexOf(varLetter));
            } 
            if(!nonum.IsMatch(intop1)){
                intop1+="1";}
            string intop2=op2;
            string algop2="";
            if(op2.Contains(varLetter)){
            intop2=op2.Substring(0,op2.IndexOf(varLetter));
            algop2=op2.Substring(op2.IndexOf(varLetter));
            } 
            if(!nonum.IsMatch(intop2)){
                intop2+="1";}
            var numeric=(long.Parse(intop1))*(long.Parse(intop2));
            var alge=algop1+algop2;
            return ""+numeric+alge;
        }
    }
    
    
    
     static List<string> extractTerms(string expression){
        
        string pattern = @"([+,-]?[0-9a-zA-Z]*[[\^]?[0-9]?)";
        List<string> terms = new List<string>();
        Regex rg = new Regex(pattern);
        MatchCollection matchedAuthors = rg.Matches(expression);
        for (int count = 0; count < matchedAuthors.Count; count++){
            string extracted = matchedAuthors[count].Value;
            if(extracted!="") terms.Add(extracted);
        }
        return terms;
    }
    
    static List<string> expand(string expression, int iters){
        List<string> output= new List<string>();
        if(iters==1){
            return extractTerms(expression);
        };
        foreach(var i in extractTerms(expression)){    
            foreach(var j in expand(expression,iters-1)){
                output.Add(multiply(i,j));
            }
        }
        return output;
        
    }
    
    public static void Main(string[] args)
    {
        string expre="(y-5)^15";
        string pattern = @"\(.*?\)";
       
        Regex rg = new Regex(pattern);
        string output="";
 
        int coeff=Int32.Parse(expre.Split("^")[1]);
        expOrder=coeff;
        if(coeff==0) Console.WriteLine(1);
        else if(coeff==1) {
            var extracted=expre.Split("^")[0];
            int chainLen=extracted.Length;
            Console.WriteLine(extracted.Substring(1,chainLen-2));}
      
        else {
            
        int _p = expre.IndexOf("(")+1;
        int p_= expre.IndexOf(")")-1;
        var extracted=expre.Substring(_p,p_);
        Console.WriteLine("CCC:"+extracted+"\n");
        string letter = @"[a-zA-Z]";
        Regex rgletter = new Regex(letter);
        
        var let = rgletter.Matches(extracted);
        varLetter=let[0].Value;
        Console.WriteLine("LETTER "+varLetter);
        
        
        var expanded= expand(extracted, coeff);
        expanded=reduceAndOrder(expanded,varLetter,expOrder);
        for(int i=0;i<expanded.Count; i++) {
                  if(expanded[i].Contains("-")){
                    output+=expanded[i];
                  } else {
                    if(i!=0) { 
                    output+="+"+expanded[i];
                      }
                    else{
                      output+=expanded[i];
                      
                    }
                  }
         }
        Console.WriteLine("Output: "+output);
        }
        
    }
}











/*******************************************************/


using System;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

public class KataSolution
{
    private readonly static Regex pattern = new Regex(@"^\((-?\d*)(.)([-+]\d+)\)\^(\d+)$", RegexOptions.Compiled);
  
    public static string Expand(string expr)  
    {
        var matches = pattern.Matches(expr).Cast<Match>().First().Groups.Cast<Group>().Skip(1).Select(g => g.Value).ToArray();
        var a = matches[0].Length == 0 ? 1 : matches[0] == "-" ? -1 : int.Parse(matches[0]);
        var x = matches[1];
        var b = int.Parse(matches[2]);
        var n = int.Parse(matches[3]);
        var f = new BigInteger(Math.Pow(a, n));
        var c = f == -1 ? "-" : f == 1 ? "" : f.ToString();
      
        if (n == 0) return "1";
        if (b == 0) return $"{c}{x}{(n > 1) ? "^" : ""}{n}";
        var res = new StringBuilder();
      
        for (var i = 0; i <= n; i++) 
        {
            if (f > 0 && i > 0) res.Append("+");
            if (f < 0) res.Append("-");
            if (i > 0 || f * f > 1) res.Append($"{BigInteger.Abs(f)}");
            if (i < n) res.Append(x);
            if (i < n - 1) res.Append($"^{n - i}");
            f = f * (n - i) * b / a / (i + 1);
        }
      
        return res.ToString();
    }
}





/***********************************************************************/
using System;
public class KataSolution
{
    public static string Expand(string s)
    {
        int pow = int.Parse(s.Split('^')[1]);
        if (pow == 0) return "1";
        string c = "";
        foreach (char cc in s = s.Split('^')[0].Trim('(', ')')) if (char.IsLetter(cc)) c += cc;
        if (pow == 1) return s;
        int b = int.Parse(s.Split(c[0])[1]);
        if (!int.TryParse(s = s.Split(c[0])[0], out int a)) a = int.Parse(s + "1");
        s = (a * a == 1 ? a == -1 && pow % 2 != 0 ? "-" : "" : Math.Pow(a, pow).ToString()) + c + "^" + pow;
        if (b == 0) return s;
        for (int i = pow - 1; i > 0; i--) s += (Coeff(pow, i) * Math.Pow(a, i) * Math.Pow(b, pow - i)).ToString("+#;-#") + (i > 1 ? c + "^" + i : c);
        return s += Math.Pow(b, pow).ToString("+#;-#");
    }
    public static int Coeff(int n, int k)
    {
        int r = 1;
        for (int d = 0; d < k; ) r = r * n-- / ++d;
        return r;
    }
}
/**********************************************************/
using System;
using System.Text.RegularExpressions;
public class KataSolution
{
  public static long Factorial(int n) => n > 1 ? n * Factorial(--n) : 1;
    public static int Pascal(int n, int k) => (int)(Factorial(n)/(Factorial(k) * Factorial(n-k)));
    public static string Expand(string expr)  
    {
        var groups = Regex.Match(expr, @"(-?\d*(?=[a-z]))([a-z])([+-]\d+)\)\^(\d+$)").Groups;
        int x_mult = (groups[1].Value == "" ? 1 : (groups[1].Value == "-" ? -1: int.Parse(groups[1].Value)));
        string x_ch = groups[2].Value;
        int num = int.Parse(groups[3].Value);
        int pow = int.Parse(groups[4].Value);
    
        if (num == 0) return $"{(x_mult != 1 ? (int)Math.Pow(x_mult, pow) : "")}{x_ch}^{pow}";
        if (x_mult == 0) return ((int)Math.Pow(num, pow)).ToString();
        string result = "";
        for (int i = 0; i < pow; i++)
        {
            long mult = Pascal(pow, pow - i) * (long)Math.Pow(x_mult, pow - i) * (long)Math.Pow(num, i);
            if (mult >= 0 && result.Length > 0) result += "+";
            if (mult == -1) result += "-";
            if (Math.Abs(mult) > 1) result += mult;
            result += x_ch;
            if (pow - i > 1) result += "^" + (pow - i);
        }
        long last_num = (long)Math.Pow(num, pow);
        if (last_num > 0 && result.Length > 0) result += "+";  
        if (last_num != 0) result += last_num;
        return result;
    }
}






/**********************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public class KataSolution
{

    private static readonly Regex re = new Regex(@"\((-?\d*)([a-z])([\+\-]\d+)\)\^(\d+)");

	public static string Expand(string expr)
    {

        Match m = re.Match(expr);
        
        string sa = m.Groups[1].Value;
        int a = ("".Equals(sa) ? 1 : ("-".Equals(sa) ? -1 : int.Parse(sa)));

        string x = m.Groups[2].Value;

        string sb = m.Groups[3].Value;
        int b = "".Equals(sb) ? 0 : int.Parse(sb);

        string se = m.Groups[4].Value;
        int exp = "".Equals(se) ? 1 : int.Parse(se);
        if (exp == 0)
            return "1";

        if (exp == 1)
            return sa + x + sb;

        if (b == 0)
        {
            long coeff = (long)Math.Pow(a, exp);
            return (coeff == 1 ? "" : (coeff == -1 ? "-" : coeff.ToString())) + x + "^" + exp;
        }

        List<long> binoms = new List<long>();
        for (int i = 0; i <= exp; ++i)
            binoms.Add(nk(exp, i));

        long coeff1 = (long)Math.Pow(a, exp);
        StringBuilder terms = new StringBuilder();
        for (int i = exp; i >= 0; --i)
        {

            long coeff = coeff1 * binoms[i];

            if (i != exp && coeff > 0)
                terms.Append('+');

            if (coeff < 0)
                terms.Append('-');

            if ((coeff != 1 && coeff != -1) || i == 0)
                terms.Append(coeff > 0 ? coeff : -coeff);

            if (i > 0)
                terms.Append(x);

            if (i > 1)
                terms.Append("^" + i);

            coeff1 = coeff1 / a * b;
        }

        return terms.ToString();
    }

    private static readonly List<List<long>> nka = new List<List<long>>();

    private static long nk(int n, int k)
    {

        if (n == 0 || k == 0)
            return 1;

        if (k == 1)
            return n;

        if (n - k < k)
            return nk(n, n - k);

        for (int i = nka.Count; i <= n; ++i)
            nka.Add(new List<long>());

        List<long> ns = nka[n];
        for (int i = ns.Count; i <= k; ++i)
            ns.Add(0L);

        if (ns[k] != 0)
            return ns[k];
        else
        {
            long b = nk(n - 1, k - 1) + nk(n - 1, k);
            ns[k] = b;
            return b;
        }
    }
}


/*****************************************************************************/

