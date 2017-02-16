using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace rumorMill {
  class Program {
    static void Main(string[] args) {
      School hogwarts = new School();
      int studentCount = int.Parse(Console.ReadLine());
      for (int i = 0; i < studentCount; i++) {
        Student s = new Student(Console.ReadLine());
        hogwarts.addStudent(s);
      }
      int friendCount = int.Parse(Console.ReadLine());
      for (int i =0; i < friendCount; i++) {
        string[] pair = Regex.Split(Console.ReadLine(), " ");
        Student friend0 = hogwarts.students[pair[0]];
        Student friend1 = hogwarts.students[pair[1]];
        friend0.addFriend(friend1);
      }
      int rumorCount = int.Parse(Console.ReadLine());
      for (int i=0; i< rumorCount; i++) {
        Student s = hogwarts.students[Console.ReadLine()];
        List<Student> chain = hogwarts.breadthFirstSearch(s);
        foreach (Student l in chain) {
          Console.Write(l.name + " ");
        }
      }
    }
  }

  class School {
    public Dictionary<string, Student> students = new Dictionary<string, Student>();
    public School() {

    }

    public void addStudent(Student s) {
      students.Add(s.name, s);
    }

    public List<Student> breadthFirstSearch(Student start) {
      List<Student> notHeard = new List<Student>();
      List<Student> heard = new List<Student>();
      foreach(Student s in students.Values) {
        s.heardIt = false;
        notHeard.Add(s);
      }
      notHeard.Sort();
      Queue<Student> gossips = new Queue<Student> ();
      gossips.Enqueue(start);
      while (gossips.Count > 0) {
        Student g = gossips.Dequeue();
        heard.Add(g);
        notHeard.Remove(g);
        g.friends.Sort();
        foreach (Student f in g.friends) {
          if (f.heardIt == false) {
            f.heardIt = true;
            gossips.Enqueue(f);
          }
        }
      }
      foreach (Student s in notHeard) {
        heard.Add(s);
      }
      return heard;
    }
  }

  class Student : IComparable {
    public List<Student> friends = new List<Student>();
    public string name;
    public bool heardIt;
    public Student(string _name) {
      name = _name;
    }

    public void addFriend(Student friend) {
      friends.Add(friend);
      friend.friends.Add(this);
    }

    public void addFriend(string fName, Dictionary<string, Student> students) {
      Student friend = students[fName];
      this.addFriend(friend);
    }

    public int CompareTo(object obj) {
      Student other = obj as Student;
      return string.Compare(this.name, other.name);
    }
  }
}
