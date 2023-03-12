// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

public class HelloWorld
{
    
    
    static string multCoeff(string coeff1, string coeff2){
        Console.WriteLine("---- MULTCOEFF { "+coeff1+"   "+coeff2+"  }");
        string output="";
        var cs1=coeff1.Split("^");
        var cs2=coeff2.Split("^");
        Console.WriteLine("CS1: "+cs1.Length);
        Console.WriteLine("CS2: "+cs2.Length);
        if(cs1.Length>1 && cs2.Length>1){
            if(cs1[0]==cs2[0]){
                var exp=Int32.Parse(cs1[1])+Int32.Parse(cs2[1]);
                Console.WriteLine("EXP: "+ exp);
                output= ""+cs1[0]+"^"+exp;
            } else {
                output= coeff1.Length>coeff2.Length?coeff2+coeff1:coeff1+coeff2;
            }
        } else if(cs1.Length>1 &&  cs1[0]==coeff2){
             
             output= ""+cs1[0]+"^"+(Int32.Parse(cs1[1])+1);
          
        } else if(cs2.Length>1 && cs2[0]==coeff1){
            output= ""+cs2[0]+"^"+(Int32.Parse(cs2[1])+1);
        }
         else if(coeff1==coeff2) output=coeff1+"^2";
         else output= coeff1.Length>coeff2.Length? coeff2+coeff1:coeff1+coeff2;
         
         Console.WriteLine("---- RETURN MULTCOEFF { "+output+" }");
         return output;
        
    }
    
    static string multAlg(string op1, string op2){
        Console.WriteLine("---- MULTALG { "+op1+"   "+op2+"  }");
        string alg="";
        string pattern = @"([a-zA-Z][\^]?[0-9]*)";
        Regex rg = new Regex(pattern);
         if(op1!=null && op2!=null){
        MatchCollection matchAlgOp1= rg.Matches(op1);
        MatchCollection matchAlgOp2= rg.Matches(op2);
        for (int count = 0; count < matchAlgOp1.Count; count++){
            string algOp1 = matchAlgOp1[count].Value;
            for (int count2 = 0; count2 < matchAlgOp2.Count; count2++){
            string algOp2 = matchAlgOp2[count2].Value;
            alg+=multCoeff(algOp1,algOp2);
            }
            
            
        }
        
        }
        Console.WriteLine("---- RETURN MULTALG { "+alg+"  }");
         return alg;
         
    }
             
             
             
             
             
             
             
   
        
        
    static string multiply(string op1, string op2){
        Console.WriteLine("---- MULTIPLY { "+op1+"   "+op2+"  } ----");
        
        string sign = @"[-]";
        Regex rg = new Regex(sign);
         
 
        bool signop1=(rg.IsMatch(op1));
        bool signop2=(rg.IsMatch(op2));
        bool overallsign=signop1^signop2;
        if(op1=="1") return ""+overallsign+op2;
        if(op2=="1") return ""+overallsign+op1;
        if(op2=="0" || op1 =="0") return "";
        
        string letop1="";
        string letop2="";
        
        string algeb = @"[a-zA-Z].*";
        rg = new Regex(algeb);
        var algop1=rg.Matches(op1);
        Console.WriteLine("\n---------******---------");
        
        if(algop1.Count!=0 && algop1[0].Value!="") {
            Console.WriteLine(algop1[0]);
            letop1=algop1[0].Value;
            op1=Regex.Replace(op1, algeb, "");
             
            string n = @"[0-9]";
            Regex numb = new Regex(n);
            if(!(numb.IsMatch(op1))){
                op1+=1;
            }
            Console.WriteLine(op1);
        }
        var algop2=rg.Matches(op2);
        Console.WriteLine("\n------------------------");
        
        if(algop2.Count!=0 && algop2[0].Value!="") {
            Console.WriteLine(algop2[0]);
            letop2=algop2[0].Value;
            op2=Regex.Replace(op2, algeb, "");
              string n = @"[0-9]";
            Regex numb = new Regex(n);
            if(!(numb.IsMatch(op2))){
                op2+=1;
            }
            Console.WriteLine(op2);
        }
   
        var alg=multAlg(letop1, letop2);
       
        Console.WriteLine("LETOP:"+alg);
             string finalsing=overallsign? "":"-";
        int mult=(Int32.Parse(op1))*(Int32.Parse(op2));
        Console.WriteLine("RESULT: "+mult+alg);
        Console.WriteLine("\n---------******---------");
        return mult+alg;
    }
    
    
    
     static List<string> extractTerms(string expression){
         Console.WriteLine("---- EXTRACTING TERMS {"+expression+"} ----");
        
        string pattern = @"([+,-]?([0-9a-zA-Z][\^]?[0-9]*))";
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
        Console.WriteLine("---- EXPANDING {"+iters+"} ----");
        Console.WriteLine("     EXPRESSION: "+expression);
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
        string expre="(x+1)^2";
        string pattern = @"\(.*?\)";
        Regex rg = new Regex(pattern);
        Console.WriteLine(rg.IsMatch(expre));
        int coeff=Int32.Parse(expre.Split("^")[1]);
        
        MatchCollection matchedAuthors = rg.Matches(expre);
        for (int count = 0; count < matchedAuthors.Count; count++){
            string extracted = matchedAuthors[count].Value;
            int chainLen=extracted.Length;
            Console.WriteLine("LARGO:"+chainLen);
            extracted=extracted.Substring(1,chainLen-2);
            var expanded= expand(extracted, coeff);
            foreach(var i in expanded) {
                Console.WriteLine("$$ "+i);
            }
            
            
            
        }
        expre= Regex.Replace(expre, pattern, "");
        Console.WriteLine("SOBRANTE:"+expre);
        
    }
    
    
}
