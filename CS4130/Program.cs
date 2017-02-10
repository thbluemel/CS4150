using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CS4130 {
  class Program {
    static public int DURATION = 1000;

    static void Main(string[] args) {
      //string inputAmount = Console.ReadLine();
      //int wordTotal;
      //string[] inputs = Regex.Split(inputAmount, " ");
      //wordTotal = int.Parse(inputs[0]);

      //if (wordTotal > 0) {
      //  string[] words = new string[wordTotal];
      //  for (int i = 0; i < wordTotal; i++) {
      //    words[i] = Console.ReadLine();
      //  }
      //  Console.WriteLine(CountUnique(words));
      //}
      //else {
      //  Console.WriteLine(0);
      ////}
      //Console.WriteLine("Words\tTime");
      //for (int wordCount = 1000; wordCount < 100000; wordCount *= 2) {
      //  double elapsedTime = TimeAnagrammer(wordCount, 5);
      //  Console.WriteLine("{0}\t{1}", wordCount, elapsedTime);
      //}

      Console.WriteLine("Word Size\tTime");
      for (int wordSize  = 1000; wordSize < 64000; wordSize *= 2) {
        double elapsedTime = TimeAnagrammer(2000, wordSize);
        Console.WriteLine("{0}\t{1}", wordSize, elapsedTime);
      }
    }

    private static int CountUnique(string[] _words) {
      string[] words = _words;
      HashSet<string> accepted = new HashSet<string>();
      HashSet<string> rejected = new HashSet<string>();

      foreach (string word in _words) {
        string alphabetizedWord = "";
        if (word != null) {
          alphabetizedWord = Alphabetize(word);
        }
        if (accepted.Contains(alphabetizedWord)) {
          accepted.Remove(alphabetizedWord);
          rejected.Add(alphabetizedWord);
        }
        if (!rejected.Contains(alphabetizedWord)) {
          accepted.Add(alphabetizedWord);
        }
      }
      return accepted.Count;
    }

    private static string Alphabetize(string s) {
      char[] letters = s.ToCharArray();
      Array.Sort(letters);
      string result = new string(letters);
      return result;
    }

    private static double TimeAnagrammer(int wordCount, int wordSize) {


      // Create a stopwatch
      Stopwatch sw = new Stopwatch();

      string[] words = wordsGenerator(wordCount, wordSize);

      // Keep increasing the number of repetitions until one second elapses.
      double elapsed = 0;
      long repetitions = 1;
      do {
        repetitions *= 2;
        sw.Restart();
        for (int i = 0; i < repetitions; i++) {
            CountUnique(words);
        }
        sw.Stop();
        elapsed = msecs(sw);
      } while (elapsed < DURATION);
      double totalAverage = elapsed / repetitions;

      // Create a stopwatch
      sw = new Stopwatch();

      // Keep increasing the number of repetitions until one second elapses.
      elapsed = 0;
      repetitions = 1;
      do {
        repetitions *= 2;
        sw.Restart();
        for (int i = 0; i < repetitions; i++) {
        }
        sw.Stop();
        elapsed = msecs(sw);
      } while (elapsed < DURATION);
      double overheadAverage = elapsed / repetitions;

      // Return the difference
      return totalAverage - overheadAverage;
    }

    private static string[] wordsGenerator(int wordCount, int wordSize) {
      string[] words = new string[wordCount];
      for (int i = 0; i < wordCount; i++) {
        string word = "";
        for (int j = 0; j < wordSize; j++) {
          word += 'a';
        }
        words[0] = word;
      }
      return words;
    }

    /// <summary>
    /// Returns the number of milliseconds that have elapsed on the Stopwatch.
    /// </summary>
    public static double msecs(Stopwatch sw) {
      return (((double)sw.ElapsedTicks) / Stopwatch.Frequency) * 1000;
    }
  }

}
