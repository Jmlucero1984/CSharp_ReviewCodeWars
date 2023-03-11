using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


public static class Extensions
    {
        public static IEnumerator<int> GetEnumerator(this Range range)
        {
            var start = range.Start.Value;
            var end = range.End.Value;
            var current = start;
            while (current <= end)
            {
                yield return current++;
            }
        }
    }
public static class HelloWorld
{
    
    static string printNumbs(List<int> nums){
            var sb="";
            foreach(var i in nums){
                sb+=""+i+" ";
                }
            return sb;
         } 
         
    static string printNumbs2(IEnumerable<int> nums){
            var sb="";
            foreach(var i in nums){
                sb+=""+i+" ";
                }
            return sb;
         } 

    static IEnumerable<IEnumerable<int>>  QMS (int floor, int top, int size ){
         if( size==1){
            for (int i=floor;i<top;i++){
                yield return new int[1] {i};
            }
        } else {
            
            for (int i=floor;i<top;i++){
                IEnumerable<int> j=new int[1] {i};
                foreach(var e in QMS(i+1, top, size-1)){
                     yield return ((IEnumerable<int>) j.Concat(e));
                     
                    
                }
            }
        }
    }
          
    
    public static int? chooseBestSum(int t, int k, List<int> ls) =>
    ls.Combinations(k)
      .Select(c => (int?) c.Sum())
      .Where(sum => sum <= t)
      .DefaultIfEmpty()
      .Max();
     
    public static IEnumerable<IEnumerable<int>> Combinations(this IEnumerable<int> ls, int k) =>
    k == 0 ? new[] { new int[0] } :
      ls.SelectMany((e, i) =>
        ls.Skip(i + 1)
          .Combinations(k - 1)
          .Select(c => (new[] {e}).Concat(c)));
          
          
          
    public static void Main(string[] args)
    {   Stopwatch stopWatch = new Stopwatch();
        int option=2;
        //Stopwatch stopWatch2 = new Stopwatch();
        if(option==1){
        int sum=0;
            for(int o=0;o<30;o++){
        List<int> lista = new List<int>(){2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30};
        Console.WriteLine ("LINQ");
        stopWatch.Start();
        var ob=lista.Combinations(5);
        Console.WriteLine("CANT:"+ob.Count());
        TimeSpan ts = stopWatch.Elapsed;
        sum+=ts.Milliseconds;
        Console.WriteLine("RunTime " + ts.Milliseconds);
        stopWatch.Stop();}
        Console.WriteLine("MEAN: "+sum/30); 
        } else if(option==2) {
            int sum=0;
        for(int o=0;o<20;o++){
        stopWatch.Start();
        Console.WriteLine("QMS");
        var ab=QMS(2,31,5);
        Console.WriteLine("CANT:"+ab.Count());
        TimeSpan ts = stopWatch.Elapsed;
        ts = stopWatch.Elapsed;
        sum+=ts.Milliseconds;
        Console.WriteLine("RunTime " + ts.Milliseconds);
        stopWatch.Stop();
        }
        Console.WriteLine("MEAN: "+sum/30); 
        }/*
        var asList=new List<IEnumerable<int>>();
        foreach(var i in ob){
            asList.Add(i);
        }
        var asList2=new List<IEnumerable<int>>();
        foreach(var i in ab){
            asList2.Add(i);
        }
        /*foreach(var i in 500..600){
            Console.Write("ITEM "+i+"   ");
            if(i<asList.Count()) {
            Console.Write(printNumbs2(asList[i]));
            } else {
              Console.Write("              ");
            }
            if(i<asList2.Count()) {
               Console.Write("   "+printNumbs2(asList2[i]));
            }
            Console.Write("\n"); //Console.WriteLine(printNumbs(i));
        }*/
        
        
        
    }
    
}
