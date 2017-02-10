using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ceiling {
  class Program {
    static void Main(string[] args) {
      string inputAmount = Console.ReadLine();
      string[] inputs = Regex.Split(inputAmount, " ");
      int treeTotal = int.Parse(inputs[0]);

      if (treeTotal > 0) {
        Tree[] forest = new Tree[treeTotal];
        HashSet<Tree> uniqueGrove =  new HashSet<Tree>();
        HashSet<Tree> similarGrove = new HashSet<Tree>(); 
        for (int i = 0; i < treeTotal; i++) {
          string seed = Console.ReadLine();
          string[] sapling = Regex.Split(seed, " ");
          Tree elmer = new Tree();
          foreach (string node in sapling) {
            elmer.addValue(int.Parse(node));
          }
          forest[i] = elmer;
        }
        for (int i = 0; i < treeTotal; i++) {
          Tree candidate = forest[i];
          bool isUnique = true;
          foreach (Tree other in uniqueGrove) {
            if (candidate.StructureEquals(other)) {
              isUnique = false;
              break;
            }
          }
          if (isUnique) {
            uniqueGrove.Add(candidate);
          }
        }
        Console.WriteLine(uniqueGrove.Count);
      }
    }

    class Tree {
      public TreeNode root = null;

      public Tree() {

      }

      public Tree(int val) {
        root = new TreeNode(val);     // I AM ROOOOOT!
      }

      public void addValue(int val) {
        TreeNode node = new TreeNode(val);
        if (this.root != null) {
          add(node, root);
        }
        else {
          this.root = node;
        }
      }

      private void add(TreeNode node, TreeNode current) {
        if (node.value < current.value) {
          if (current.nodesOnLeft == 0) {
            current.nodesOnLeft++;
            current.AddLeft(node);
            return;
          }
          else {
            current.nodesOnLeft++;
            add(node, current.left);
          }
        }
        else {
          if (current.nodesOnRight == 0) {
            current.nodesOnRight++;
            current.AddRight(node);
            return;
          }
          else {
            current.nodesOnRight++;
            add(node, current.right);
          }
        }
      }

      public string traverse(TreeNode node, ref string rep) {
        if (node.left != null) {
          traverse(node.left, ref rep);
        }
        if (node.right != null) {
          traverse(node.right, ref rep);
        }
        rep += node.value.ToString() + " ";
        return rep;
      }

      public bool StructureEquals(Tree other) {
        return TreeNode.ChildrenAreEqual(this.root, other.root);
      }
    }

    class TreeNode {
      public int value { private set; get; }
      public TreeNode left { private set; get; }
      public TreeNode right { private set; get; }
      public TreeNode parent { private set; get; }

      public int nodesOnLeft = 0;
      public int nodesOnRight = 0;

      public TreeNode(int _value) {
        value = _value;
      }

      public void AddLeft(TreeNode node) {
        this.left = node;
      }

      public void AddRight(TreeNode node) {
        this.right = node;
      }

      public static bool ChildrenAreEqual(TreeNode node1, TreeNode node2) {
        if (node1.nodesOnLeft == node2.nodesOnLeft) {
          if (node1.nodesOnLeft != 0) {
            if (!TreeNode.ChildrenAreEqual(node1.left, node2.left)) {
              return false;
            }
          }
        }
        else {
          return false;
        }
        if (node1.nodesOnRight == node2.nodesOnRight) {
          if (node1.nodesOnRight != 0) {
            if (!TreeNode.ChildrenAreEqual(node1.right, node2.right)) {
              return false;
            }
          }
        }
        else {
          return false;
        }
        return true;
      }
    }
  }
}
