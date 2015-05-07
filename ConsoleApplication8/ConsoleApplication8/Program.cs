using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ConsoleApplication8
{

    public class Node
    {

        private Node left;
        private Node right;
        private Node parent;
        private int value;


        public Node(int value)
        {
            Contract.Ensures(value >= 0);
            this.setValue(value);
        }

        public Node getLeft()
        {
            return left;
        }

        public void setLeft(Node left)
        {
            Contract.Requires(left != null);
            this.left = left;
        }

        public Node getRight()
        {
            return right;
        }

        public void setRight(Node right)
        {
            Contract.Requires(right != null);
            this.right = right;
        }

        public int getValue()
        {

            return value;
        }

        public void setValue(int value)
        {
            Contract.Requires(value >= 0);
            this.value = value;
        }

        public Node getParent()
        {
            return parent;
        }
        public void setParent(Node parent)
        {
            Contract.Requires(parent != null);
            this.parent = parent;
        }


        public String toString()
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return value.ToString(); // liefert den Wert des Integer-Objekts als String
            // zurück (Basis 10)
        }

        public bool equals(Object o)
        {

            if (o is Node)
            {

                Node node = (Node)o;
                Contract.Invariant(node != null);
                if (node.getValue().Equals(this.getValue()))
                {
                    return true;
                }
            }
            return false;
        }
    }
	


    public class BinaryTree
    {

        private Node root = null;

        public BinaryTree(int value)
        {
            Contract.Requires(value >= 0);
            Contract.Invariant(root != null);
            root = new Node(value);

            if (root != null)
            {
                System.Console.WriteLine("Node is included - succeed.");

            }
        }

    public void insertion(int i)
    {
        Contract.Requires(i >= 0);
        if (root == null)
        {
            root = new Node(i);
        }
        else
        {
            this.insertionRecursion(root, i, null);
        }
    }

    private void insertionRecursion(Node node, int value, Node parent)
    {
        Contract.Requires(node != null);
        Contract.Ensures(Contract.Result<int>() >= 0);

        if (node == null)
        {
            node = new Node(value);

        }
        if (root == null)
        {
            root = node;

        }
        else
        {

            if ((value.CompareTo(node.getValue())) == 0)
            {

                if (node.getRight() == null)
                {
                    Contract.Requires(node.getRight() == null);
                    Contract.Ensures(Contract.Result<Node>() != null);

                    Node newNode = new Node(value);
                    node.setRight(newNode);
                    newNode.setParent(node);

                }
                else
                {

                    insertionRecursion(node.getRight(), value, parent);
                }
            }
        }
        if ((value.CompareTo(node.getValue())) < 0)
        {
            if (node.getLeft() != null)
            {

                insertionRecursion(node.getLeft(), value, node);

            }
            else
            {

                Contract.Requires(node.getLeft() == null);
                Contract.Ensures(Contract.Result<Node>() != null);

                
                Node newNode = new Node(value);
                node.setLeft(newNode);
                newNode.setParent(node);

            }
        }

        if ((value.CompareTo(node.getValue())) > 0)
        {
            if (node.getRight() != null)
            {

                insertionRecursion(node.getRight(), value, node);

            }
            else
            {

                Contract.Requires(node.getRight() == null);
                Contract.Ensures(Contract.Result<Node>() != null);

                Node newNode = new Node(value);
                node.setRight(newNode);
                newNode.setParent(node);

            }

        } 
    }

    public Node getRoot(){

        return this.root;
    }

    
    public int getHeight()
    {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return getHeightRecursion(0, root);
    }

    private int getHeightRecursion(int currentHeight, Node node)
    {
        Contract.Requires(currentHeight >= 0);
        Contract.Requires(node != null);
        if (node != null)
        {
            return Math.Max(getHeightRecursion(currentHeight + 1, node.getLeft()),
                    getHeightRecursion(currentHeight + 1, node.getRight()));
        }
        return currentHeight;
    }

    public int getNodeSmallest()
    {

        Node node = getNodeSmallest(root);

        if (node == null)
        {
            Contract.Requires(node == null);
            return -1;
        }
        else
        {
            Contract.Ensures(Contract.Result<Node>() != null);
            return node.getValue();
        }
    }

    private Node getNodeSmallest(Node start)
    {
        Contract.Requires(start != null);
        Contract.Invariant(start.getLeft() != null);
        if (start != null)
        {
            while (start.getLeft() != null)
            {
                start = start.getLeft();
            }
        }
        return start;
    }

    public int getNodeMaximum()
    {
        Node k = getNodeMaximum(root);
        if (k == null)
        {
            Contract.Requires(k == null);
            return -1;
        }
        else
        {
            Contract.Ensures(Contract.Result<Node>() != null);
            return k.getValue();
        }
    }

    private Node getNodeMaximum(Node start)
    {
        Contract.Requires(start != null);
        Contract.Invariant(start.getRight() != null);
        if (start != null)
        {
            while (start.getRight() != null)
            {
                start = start.getRight();
            }
        }
        return start;
    }



    public Node getNode(int value)
    {
        Contract.Requires(value >= 0);
        return getNodeRecursion(root, value);
    }

    private Node getNodeRecursion(Node node, int value)
    {
        Contract.Requires(value >= 0);

        if (node == null)
            return null;
        if ((value.CompareTo(node.getValue())) == 0)
            return node;
        if ((value.CompareTo(node.getValue())) < 0)
        {
            if (node.getLeft() == null)
                return null;
            return getNodeRecursion(node.getLeft(), value);
        }
        if ((value.CompareTo(node.getValue())) > 0)
        {
            if (node.getRight() == null)
                return null;
            return getNodeRecursion(node.getRight(), value);
        }
        return null;
    }



    public void delete(int value)
    {
        Contract.Requires(value >= 0);
        deleteRecursion(root, value, null, false);
    }

    private bool deleteRecursion(Node node, int value,
            Node parent, bool leftFromParent)
    {
      //  bool breakUpMethod = false;

        if (node == null)
        {
            Contract.Requires(node == null);

            throw new Exception("Failure- the node mustn´t be null.");
        }
        Contract.Requires(node.getLeft() == null);
        Contract.Requires(node.getRight() == null);

        if ((value.CompareTo(node.getValue())) == 0)
        {
            if ((node.getLeft() == null) && (node.getRight() == null))
            {
                if (parent == null)
                {
                    root = null;
                }
                else
                {
                 
                   // hangUnderParent(parent, leftFromParent, null);
                    //k.setParent(null);

                    if (leftFromParent)
                    {
                        Contract.Ensures(Contract.Result<bool>() == true);
                        parent.setLeft(node);
                    }
                    else
                    {
                        Contract.Ensures(Contract.Result<bool>() == false);
                        parent.setRight(node);
                    }
                }
            }
            if ((node.getLeft() == null) && (node.getRight() != null))
            {
                if (parent == null)
                {
                    root = node.getRight();
                }
                else
                {
                    if (leftFromParent)
                    {
                        Contract.Ensures(Contract.Result<bool>() == true);
                        parent.setLeft(node);
                    }
                    else
                    {
                        Contract.Ensures(Contract.Result<bool>() == false);
                        parent.setRight(node);
                    }
                    parent.setParent(node);
                }
            }
            if ((node.getLeft() != null) && (node.getRight() == null))
            {
                if (parent == null)
                {
                    root = node.getLeft();
                }
                else
                {
                    if (leftFromParent)
                    {
                        Contract.Ensures(Contract.Result<bool>() == true);
                        parent.setLeft(node);
                    }
                    else
                    {
                        Contract.Ensures(Contract.Result<bool>() == false);
                        parent.setRight(node);
                    }
                    parent.setParent(node);
                }
            }

            if (node.getLeft() != null && node.getRight() != null)
            {
                Node smallestRight = getNodeSmallest(node.getRight());
                delete(smallestRight.getValue());
                if (parent == null)
                {
                    smallestRight.setLeft(root.getLeft());
                    smallestRight.setRight(root.getRight());
                    root = smallestRight;
                }
                else
                {
                    smallestRight.setLeft(node.getLeft());
                    smallestRight.setRight(node.getRight());

                    if (leftFromParent)
                    {
                        Contract.Ensures(Contract.Result<bool>() == true);
                        parent.setLeft(node);
                    }
                    else
                    {
                        Contract.Ensures(Contract.Result<bool>() == false);
                        parent.setRight(node);
                    }

                    parent.setParent(smallestRight);
                }
            }
            deleteRecursion(root, value, null, false);
            return true;
           // breakUpMethod = true;
        }

        if ((value.CompareTo(node.getValue())) < 0)
        {
            Contract.Requires(node.getLeft() == null);
            if (node.getLeft() == null)
            {
                Contract.Ensures(Contract.Result<bool>() == false);
                return false;
            }
            Contract.Ensures(Contract.Result<bool>() == true);

            return deleteRecursion(node.getLeft(), value, node, true);
         
        }

        if ((value.CompareTo(node.getValue())) > 0)
        {
            Contract.Requires(node.getRight() == null);
            if (node.getRight() == null)
            {
                Contract.Ensures(Contract.Result<bool>() == false);
                return false;
            }
            Contract.Ensures(Contract.Result<bool>() == false);
            return deleteRecursion(node.getRight(), value, node, false);

         
        }

       return false;
    }



    public String preOrderOutput()
    {
        Contract.Invariant(preOrder(root) != null);
        return preOrder(root);
    }

    public String preOrder(Node root)
    {
        String output = "";
        Contract.Requires(root == null);
        if (root == null)
        {
            Contract.Ensures(Contract.Result<String>() == "<leer>");
            return "<leer>";
        }

        output += root.getValue() + " ";

        Contract.Requires(root.getLeft() != null);
        if (root.getLeft() != null)
        {
            output += preOrder(root.getLeft());
        }
        Contract.Requires(root.getRight() != null);
        if (root.getRight() != null)
        {
            output += preOrder(root.getRight());
        }
        return output;
    }



    public String toString()
    {
        Contract.Ensures(Contract.Result<String>() != null);
        return root.toString();
    }


    public bool equals(Object o)
    {
        bool similarToObject = false;

        if (o is BinaryTree)
        {

            BinaryTree bi = (BinaryTree)o;
            Contract.Invariant(bi != null);

            if (bi.root.getValue().Equals(this.root.getValue()))
            {
                Contract.Ensures(Contract.Result<bool>() == true);
                similarToObject = true;
            }
        }
          Contract.Ensures(Contract.Result<bool>() == false);
        return similarToObject;
    }

}

    // Programm

    class Main
    {

        static void main(string[] args)
        {


        BinaryTree tree = new BinaryTree(7);
        BinaryTree secondTree = new BinaryTree(7);

        for (int i = 0; i < 16; i++)
        {
            tree.insertion(i);
        }

        for (int i = 0; i < 16; i++)
        {
            secondTree.insertion(i);
        }

        tree.insertion(15);
        tree.insertion(15);
        tree.insertion(17);

        secondTree.insertion(15);
        secondTree.insertion(15);
        secondTree.insertion(17);
       

        Contract.Assert(tree.getNodeMaximum() == 17);
        Console.WriteLine("größter wert: " + tree.getNodeMaximum());
        Contract.Assert(tree.getNodeSmallest() == 0);
        Console.WriteLine("kleinster wert: " + tree.getNodeSmallest());
        Contract.Assert(tree.getHeight() == 13);
        Console.WriteLine("höhe des baumes: " + tree.getHeight());
        Console.WriteLine("knoten: " + tree.getNode(1).ToString());
    //    Contract.Assert(Equals(tree.ToString(), secondTree.ToString()));
        Console.WriteLine("Search 2nd Node: " + secondTree.getNode(17).toString());
        Console.WriteLine(tree.preOrderOutput());
        tree.delete(15);
        tree.delete(17);
        System.Console.WriteLine(tree.preOrderOutput());


        Console.ReadKey();
         }
    }


    public abstract class Problem<Solution>
    {
        public abstract Solution getSolution(); // getSolution is contained in problem

    }


    abstract class DivisibleProblem<Solution> : Problem<Solution>
    {
        protected bool directlySolvable = false;
        protected BinaryTree tree = null;
  

        public void setBinaryTree(int value)
        {
            tree = new BinaryTree(value);
        }


        public BinaryTree getBinaryTree(){

            return tree;
        }

        public abstract void checkSolvability(Node root);


        public abstract void highestNodeAndSumNodes(Node node); // Method that has to override the highest and the sum of nodes

        public virtual void computeSolution() // Method that checks the methods of the problem Max and problem Sum
        {

            checkSolvability(tree.getRoot());
            if (directlySolvable)
            {
                Console.WriteLine("DirektlySolvabe is true.");
            }
            else
            {
                highestNodeAndSumNodes(tree.getRoot());
            }
        }
    }

        class MaxProblem : DivisibleProblem<MaxSolution>
        {

            protected MaxSolution solution;
     

            public MaxProblem()             
            {
                solution = new MaxSolution();
            }

            public override MaxSolution getSolution()
            {
                return solution;
            }

            public override void checkSolvability(Node root)
            {
                int highestNodeFound = 0;

                if (root.getLeft() == null && root.getRight() == null)
                {
                    directlySolvable = true;
                    highestNodeFound = root.getValue();

                    solution.setMax(highestNodeFound);
                }

            }

            public override void highestNodeAndSumNodes(Node node) // override method - highestNode
            {

                while (node.getRight() != null)
                {
                    node = node.getRight();
                }
                solution.setMax(node.getValue());
            }

          
        }

    //-----------


        class SumProblem : DivisibleProblem<SumSolution>
        {

            protected SumSolution solution;


            public SumProblem()
            {
                solution = new SumSolution();
            }

            public override SumSolution getSolution()
            {
                return solution;
            }


            public override void checkSolvability(Node root)
            {

                if (root.getLeft() == null && root.getRight() == null)
                {
                    directlySolvable = false;
                    Console.WriteLine("It´s only one node in this tree!");
                    solution.setSum(root.getValue());
                }
            }

            public int getRecursion(Node node)
            {
                int sum = 0;
                if (node != null)
                {
                    sum += node.getValue() + getRecursion(node.getLeft()) + getRecursion(node.getRight());
                }
                return sum;
            }

            public override void highestNodeAndSumNodes(Node node) // summaryNodes
            {

                solution.setSum(this.getRecursion(node));

            }
       }

        class MaxSolution
        {
            private int max;

            public int getMax()
            {
                return max;
            }
            protected internal void setMax(int max)
            {
                this.max = max;
            }
        }


        class SumSolution
        {

            private int data;

            public int getSum()
            {
                return data;
            }
            protected internal void setSum(int element)
            {
               this.data = element;
            }
        }


    // ------ Programm


        class Programm
        {
            static void Main(string[] args)
            {


                //MaxProblem max = new MaxProblem();
                //max.setBinaryTree(20);
                //max.getBinaryTree().einfügen(233);
                //max.computeSolution();
                //Console.WriteLine("Höchster Knoten: " + max.getSolution().getMax());


                SumProblem sum = new SumProblem();

                sum.setBinaryTree(30);

                sum.getBinaryTree().insertion(33);

                sum.getBinaryTree().insertion(3);
                sum.getBinaryTree().insertion(5);
                sum.getBinaryTree().insertion(1);
                sum.getBinaryTree().insertion(20);

                sum.computeSolution();

                Console.WriteLine("Sum: " + sum.getSolution().getSum());

                Console.ReadKey();


            }
        }
}
