using System;
using System.Collections;
using System.Collections.Generic;
namespace Game {
 
  public class Player
  {
      static int[] numbers = {0,1,2,3,4,5,6,7,8};
      static List<List<int>> postcombs = new List<List<int>>(){ 
         new List<int>(){0,0,1,1},
         new List<int>(){1,1,2,2},
         new List<int>(){2,2,3,3},
         new List<int>(){3,3,0,0}
 
      };
      static int[,,] final = { 
      {{0,2,1,3},{0,2,3,1},{2,0,1,3},{2,0,3,1} },
       {{0,1,2,3},{0,1,3,2},{1,0,2,3},{1,0,3,2}},
      {{0,3,1,2},{0,3,2,1},{3,0,1,2},{3,0,2,1}},
      {{2,3,1,0},{2,3,0,1},{3,2,0,1},{3,2,1,0}},
       {{1,3,2,0},{1,3,0,2},{3,1,0,2},{3,1,2,0}},
        {{1,2,0,3},{1,2,3,0},{2,1,0,3},{2,1,3,0}}};

 
      static Dictionary <string,int> finalTries= new Dictionary<string,int>() {
          { "2020",0},{"1210",1},{ "2101",2},{ "1012",3},{ "0202",4},{ "0121",5}};
      static ArrayList accepted = new ArrayList();
      static ArrayList paireds = new ArrayList();
      static int index=0;
      static int indexSP=0;
      static int indexTP=0;
      static bool firstPhase=false;
      static bool secondPhase=false;
      static bool thirdPhase=false;
      static string combchain="";
    
    
      public static int[] TryToGuess(int matches)
      {
        
        Console.WriteLine("INICIANDO... INDEX: "+index);
        int[] oute =new int[4];
         
        if(matches==-1){
              Console.WriteLine("ENTRO EN EL PRIMER BUCLE");
              accepted = new ArrayList();
              paireds = new ArrayList();
              index=0;
              indexSP=0;
              indexTP=0;
              firstPhase=true;
              secondPhase=false;
              thirdPhase=false;
              combchain="";
              index++;
              return new int[]{0,0,0,0};
        }
       
        if(firstPhase==true) {
              if (matches>0){ accepted.Add(numbers[index-1]);}
              if(index<9 && accepted.Count<4) {
                for(int i=0;i<4;i++){ oute[i]=numbers[index];}
             
              Console.WriteLine("ACEPTADOS");
     

             foreach (int element in accepted){
                    Console.Write(" "+element+" ");
                }
                Console.WriteLine("----------------- ");
              Console.WriteLine("LOQUE ENVIA 1 Phase: "+oute[0]+oute[1]+oute[2]+oute[3]);
        } else {firstPhase=false;secondPhase=true;}
        }
          
        if(secondPhase==true){
                foreach (int element in accepted){
                    Console.Write(" "+element+" ");
                }
                Console.WriteLine("----------------- ");
                if (accepted.Count<4){ accepted.Add(9);}
                if (indexSP>0 && indexSP<4) {paireds.Add(matches);}
                if(indexSP<3) {
                      Console.WriteLine("MATCHES: "+matches);
                      oute[0]=(int)accepted[postcombs[indexSP][0]];
                      oute[1]=(int)accepted[postcombs[indexSP][1]];
                      oute[2]=(int)accepted[postcombs[indexSP][2]];
                      oute[3]=(int)accepted[postcombs[indexSP][3]];
                      indexSP++;
                      
                      Console.WriteLine("LO QUE HAY EN COMBCHAIN: "+combchain);
                      Console.WriteLine("LOQUE ENVIA: "+oute[0]+oute[1]+oute[2]+oute[3]);

                      
                  } else {
                      int sums=4;
                      foreach (int element in paireds) {
                              sums-=element;
                              combchain+=element;
                          }
                      combchain+=sums;
                      Console.WriteLine("COMB :"+combchain);
                      secondPhase=false;
                      thirdPhase=true;
                     }
                  }
              
         
        if(thirdPhase==true){
            Console.WriteLine("FINALCOMBS");
           
            if(indexTP<4){
            oute[0]=(int)accepted[final[finalTries[combchain],indexTP,0]];
            oute[1]=(int)accepted[final[finalTries[combchain],indexTP,1]];
            oute[2]=(int)accepted[final[finalTries[combchain],indexTP,2]];
            oute[3]=(int)accepted[final[finalTries[combchain],indexTP,3]];
            Console.WriteLine("LOQUE ENVIA: "+oute[0]+oute[1]+oute[2]+oute[3]);
              indexTP+=1;
           } else {
              thirdPhase=false;
            }
            
          }
          if(index<17) {
            index++;}
            return oute;
             
        }
   
  }
}







/*  TESTT
namespace Game 
{
  using NUnit.Framework;
  using System.Linq;
  [TestFixture]
  public class PlayerTest
  {
    [Test]
    public void Play()
    {
      int playersAttempts = 0;
      int[] dig = new int[] {7,2,5,3};
      int matches = -1;
      int[] answer = new int[4];
      answer = Player.TryToGuess(matches);
      while (!Enumerable.SequenceEqual(answer, dig) && playersAttempts < 100)
      {
        int result = 0;
        for (int k = 0; k < 4; k++)
          if (answer[k] == dig[k])
            result++;
        matches = result;
        if (matches < 4)
        {
          playersAttempts++;
          answer = Player.TryToGuess(matches);
        }
      }
      Assert.LessOrEqual(playersAttempts, 16,"The code was {0}", string.Join("", dig));
    }
  }
}
*/
    
