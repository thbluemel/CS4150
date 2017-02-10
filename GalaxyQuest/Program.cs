using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GalaxyQuest {
  class Program {
    static void Main(string[] args) {
      string input = Console.ReadLine();
      string[] inputs = Regex.Split(input, " ");
      int starCount = int.Parse(inputs[1]);
      long diameter = long.Parse(inputs[0]);

      List<Point> stars = new List<Point>();
      if (starCount != 0) {
        for (int i = 0; i < starCount; i++) {
          string coordsString = Console.ReadLine();
          string[] coords = Regex.Split(coordsString, " ");
          long x = long.Parse(coords[0]);
          long y = long.Parse(coords[1]);
          Point star = new Point(x, y);
          stars.Add(star);
        }
      }
      bool contains;
      List<Point> candidates = new List<Point>();
      if (starCount %2 == 0) {
        for (int i = 0; i < starCount-2; i+=2) {
          if (stars[i].areWithin(stars[i + 1], diameter)) {
            contains = false;
            foreach (Point other in candidates) {
              if (stars[i].areWithin(other, diameter)) {
                contains = true;
                break;
              }
            }
            if (contains == false) {
              candidates.Add(stars[i]);
            }
          }
        }
      }
      else {
        for (int i = 0; i < starCount-3; i += 2) {
          if (stars[i].areWithin(stars[i+1], diameter)) {
            contains = false;
            foreach (Point other in candidates) {
              if (stars[i].areWithin(other, diameter)) {
                contains = true;
                break;
              }
            }
            if (contains == false) {
              candidates.Add(stars[i]);
            }
          }
        }
        contains = false;
        foreach (Point other in candidates) {
          if (stars[starCount-1].areWithin(other, diameter)) {
            contains = true;
            break;
          }
        }
        if (contains == false) {
          candidates.Add(stars[starCount-1]);
        }
      }

      bool majorityExists = false;
        foreach (Point star in candidates) {
          int inGalaxy = 0;
          foreach (Point other in stars) {
            if (star.areWithin(other, diameter)) {
              inGalaxy++;
            }
          }
          if (inGalaxy >= starCount / 2 + 1) {
            Console.WriteLine(inGalaxy);
            majorityExists = true;
            break;
          }
        }
      if (!majorityExists) {
        Console.WriteLine("NO");
      }
    }
  }

  public class Point {
    long x, y;
    public Point(long _x, long _y) {
      x = _x;
      y = _y;
    }

    public bool areWithin (Point other, long dist) {
      return Math.Pow(this.x - other.x, 2) + Math.Pow(this.y - other.y, 2) <= Math.Pow(dist, 2);
    }
  }
}
