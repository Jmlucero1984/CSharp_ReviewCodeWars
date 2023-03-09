/*   M I N E   */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HelloWorld
{       static List<int> routesCost= new List<int>(){};
         static string printNumbs(List<int> nums){
            var sb="";
            foreach(int i in nums){
                sb+=""+i+" ";
                }
            return sb;
         } 
       
        
        
        
    static List<List<int>>  QMS (int floor, int top, int size ){
        List<List<int>> l= new List<List<int>>();
        if( size==1){
            for (int i=floor;i<top;i++){
                var val=new List<int>(){i};
                l.Add(val);
            }
        } else {
            for (int i=floor;i<top;i++){
               // Console.WriteLine("ITERANDO CON: "+i);
                foreach(List<int> e in QMS(i+1, top, size-1)){
                    var val=new List<int>(){i};
                     
                    l.Add(val.Concat(e).ToList());
                }
            }
        }
     
        
        return l;
    } 
    
    
    public static void Main(string[] args){   
        List<int> ls= new List<int>(){100, 76 ,56 ,44 ,89 ,73, 68, 56, 64, 123, 2333, 144, 50 ,132 ,123 ,34 ,89 };
        
        int k=4;
        int t=230;
        if(k==1){
            Console.WriteLine(ls.FindAll(e => e< t).AsQueryable().Max());
            
        } else if (ls.Count<k){  Console.WriteLine("NULLLLL");}
        else{
            var patterns= QMS(0,ls.Count,k);
            foreach(List<int> i in patterns){
             int sum=0;
             foreach(int e in i){
            sum+=ls[e];
            }
          if(sum<=t) routesCost.Add(sum);
         }
         if(routesCost.Count==0) {Console.WriteLine("NADA");}
         else {
         Console.WriteLine(routesCost.AsQueryable().Max());}
        }
        
        
    }
}







*/ ///////////////////////////////////////////////////*













using System.Collections.Generic;
using System.Linq;

public static class SumOfK 
{
  public static int? chooseBestSum(int t, int k, List<int> ls) =>
    ls.Combinations(k)
      .Select(c => (int?) c.Sum())
      .Where(sum => sum <= t)
      .DefaultIfEmpty()
      .Max();

  // Inspired by http://stackoverflow.com/questions/127704/algorithm-to-return-all-combinations-of-k-elements-from-n
  public static IEnumerable<IEnumerable<int>> Combinations(this IEnumerable<int> ls, int k) =>
    k == 0 ? new[] { new int[0] } :
      ls.SelectMany((e, i) =>
        ls.Skip(i + 1)
          .Combinations(k - 1)
          .Select(c => (new[] {e}).Concat(c)));
}










using System;
using System.Collections.Generic;
using System.Linq;

public static class SumOfK 
{
  public static int? chooseBestSum(int t, int k, List<int> ls)
  {
    var _ls = ls.Where(x => x <= t);
    return _ls.Count() == 0 ? null : _ls.Select((x, i) => x + (k > 1 ? chooseBestSum(t-x, k-1, _ls.Skip(i+1).ToList()) : 0)).Max();
  }
}








using System;
using System.Collections.Generic;

public static class SumOfK
{
    public static int? chooseBestSum(int t, int k, List<int> ls)
    {
        return Best(t, k, ls, 0, 0);
    }

    public static int? Best(int t, int k, List<int> ls, int Start, int Sum)
    {
        if (k == 0)
            return (Sum <= t) ? (int?)Sum : null;

        if (Start >= ls.Count)
            return null;

        int? S1 = Best(t, k - 1, ls, Start + 1, Sum + ls[Start]);
        int? S2 = Best(t, k, ls, Start + 1, Sum + 0);

        if (S1 == null && S2 == null)
            return null;
        if (S1 == null)
            return S2;
        if (S2 == null)
            return S1;
        return (int?)Math.Max(S1.Value, S2. Value);
    }
}










using System.Linq;
using System.Collections.Generic;
public static class SumOfK 
{
    public static int? chooseBestSum(int t, int k, List<int> ls) {
            if (ls.Count<k) return null;
            ls=ls.Where(x=>x<=t).ToList();
            var ls1=Enumerable.Range(0,ls.Count);
            var rs = ls1.Select(x => new int[] { x });
            for (int i = 0; i < k - 1; i++)   rs = rs.SelectMany(x => ls1.Where(y =>y.CompareTo(x.First()) < 0).Select(y => new int[] { y }.Concat(x).ToArray()));
            rs=rs.Select(x=>x.Select(y=>ls[y]).ToArray());
            if (rs.Count(x=>x.Sum()<=t)==0) return null;
            return rs.Select(x=>x.Sum()).Where(x=>x<=t).Max();
    }
}







