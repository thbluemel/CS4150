using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace autoSink {
  class Program {
    static void Main(string[] args) {
      Interstate roads = new Interstate();

      int cityCount = int.Parse(Console.ReadLine());
      roads.cities.Capacity = cityCount;
      for (int i = 0; i < cityCount; i++) {
        string input = Console.ReadLine();
        string[] inputs = Regex.Split(input, " ");
        City c = new autoSink.City(inputs[0], int.Parse(inputs[1]));
        roads.cities.Add(c);
        roads.cityMap.Add(c.name, c);
      }
      int highwayCount = int.Parse(Console.ReadLine());
      for (int i = 0; i < highwayCount; i++) {
        string input = Console.ReadLine();
        string[] inputs = Regex.Split(input, " ");
        City f = roads.FindCityByName(inputs[0]);
        City t = roads.FindCityByName(inputs[1]);

        f.addHighway(t);
      }
      int tripCount = int.Parse(Console.ReadLine());
      string[] tripResults = new string[tripCount];
      for (int i = 0; i < tripCount; i++) {
        string input = Console.ReadLine();
        string[] inputs = Regex.Split(input, " ");
        City f = roads.FindCityByName(inputs[0]);
        City t = roads.FindCityByName(inputs[1]);

        tripResults[i] = roads.calculateToll(f,t);
      }

      foreach (string s in tripResults) {
        Console.WriteLine(s);
      }
    }
  }

  class City {
    public string name = "";
    public int toll = 0;
    public int pre = 0;
    public int post = 0;
    public bool visited = false;

    public List<Highway> highways = new List<Highway>();
    public City(string _name, int _toll) {
      name = _name;
      toll = _toll;
    }

    public void addHighway (City to) {
      Highway h = new Highway(to, this);
      highways.Add(h);
    }

    public void Print() {
      Console.WriteLine(name);
    }
  }

  class Highway{
    public City to { get; set; }
    public City from { get; set; }
    public Highway(City _to, City _from) {
      to = _to;
      from = _from;
    }
    public void reverse() {
      City temp = to;
      to = from;
      from = temp;
    }
  }

  class Interstate {
    public List<City> cities = new List<City>();
    public Dictionary<string, City> cityMap = new Dictionary<string, City>();
    public int count = 0;

    public Interstate() {

    }

    public City[] DepthFirstSearch(City start) {
      City[] topo = new City[cities.Count];
      int topoPlacement = cities.Count - 1;
      int turnCounter = 1;
      foreach (City c in cities) {
        c.visited = false;
      }
      foreach (City c in cities) {
        if (!c.visited) {
          explore(c, ref turnCounter, ref topoPlacement, topo);
        }
      }
      return topo;
    }

    private void explore(City c, ref int turnCounter, ref int topoPlacement, City[] topo) {
      c.pre = turnCounter++;
      c.visited = true;
      foreach (Highway h in c.highways) {
        if (!h.to.visited) {
          explore(h.to, ref turnCounter, ref topoPlacement, topo);
        }
      }
      c.post = turnCounter++;
      topo[topoPlacement--] = c;
    }

    public string calculateToll(City from, City to) {
      if (from == to) {
        return "0";
      }
      City[] topo = DepthFirstSearch(from);
      int fromI = TopoIndex(from, topo);
      int toI = TopoIndex(to, topo);
      if (fromI != -1) {
        if (fromI < toI) {
          int[] distance = new int[topo.Length];
          for (int i = 0; i < topo.Length; i++) {
            distance[i] = -1;
          }
          distance[fromI] = 0;
          foreach( City c in topo) {
            foreach (Highway h in c.highways) {
              int f = TopoIndex(h.from, topo);
              int t = TopoIndex(h.to, topo);
              if (distance[f] == -1) {
              }
              else if (distance[t] == -1) {
                distance[t] = distance[f] + topo[t].toll;
              }
              else {
                distance[t] = Math.Min(distance[t], distance[f] + topo[t].toll);
              }
            }
          }
          if (distance[toI] == -1) {
            return "NO";
          }
          else {
            return distance[toI].ToString();
          }
        }
        else {
          return "NO";
        }
      }
      else {
        throw new ArgumentOutOfRangeException("From city is not in the highway system.");
      }

    }

    public int TopoIndex(City c , City[] cities) {
      for (int i = 0; i < cities.Length; i++) {
        if (cities[i] == c) {
          return i;
        }
      }
      return -1;
    }

    public List<Highway> GetHighways() {
      List<Highway> hws = new List<Highway>();
      foreach (City c in cities) {
        foreach(Highway h in c.highways) {
          hws.Add(h);
        }
      }
      return hws;
    }

    public void ReverseHighways() {
      foreach (City c in cities) {
        foreach (Highway h in c.highways) {
          h.reverse();
        }
      }
    }

    public City FindCityByName(string n) {
      if (cityMap.ContainsKey(n)) {
        return cityMap[n];
      }
      foreach(City c in cities) {
        if (c.name == n) {
          cityMap.Add(c.name, c);
          return c;
        }
      }
      throw new ArgumentException("City doesn't exist");
    }
  }
}
