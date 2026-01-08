using System;

namespace AlgorithmsAssignment
{
    // Grades as requested in the assignment
    enum StudentGrade { Excellent, VeryGood, Good, Fail }

    class Node
    {
        public Student Info;
        public Node Next, Back; // Back instead of Prev for simplicity

        public Node(Student data)
        {
            Info = data;
            Next = Back = null;
        }
    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Score1 { get; set; }
        public double Score2 { get; set; }
        public double FinalResult { get; set; }
        public StudentGrade Grade { get; set; }

        public Student(int id, string name, string city, double s1, double s2)
        {
            Id = id;
            Name = name;
            City = city;
            Score1 = s1;
            Score2 = s2;
            CalculateResult();
        }

        private void CalculateResult()
        {
            FinalResult = (Score1 + Score2) / 2.0;

            // Grading logic based on result
            if (FinalResult >= 90) Grade = StudentGrade.Excellent;
            else if (FinalResult >= 80) Grade = StudentGrade.VeryGood;
            else if (FinalResult >= 50) Grade = StudentGrade.Good;
            else Grade = StudentGrade.Fail;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"ID: {Id} | Name: {Name} | City: {City} | Result: {FinalResult} | Grade: {Grade}");
        }
    }

    class StudentList
    {
        private Node head;
        private Node LastNode; // Simple name for Tail

        public void InsertBack(Student s)
        {
            Node newNode = new Node(s);
            if (head == null) head = LastNode = newNode;
            else
            {
                LastNode.Next = newNode;
                newNode.Back = LastNode;
                LastNode = newNode;
            }
        }

        public void InsertFront(Student s)
        {
            Node newNode = new Node(s);
            if (head == null) head = LastNode = newNode;
            else
            {
                newNode.Next = head;
                head.Back = newNode;
                head = newNode;
            }
        }

        public void ShowAll()
        {
            Node temp = head;
            while (temp != null)
            {
                temp.Info.PrintDetails();
                temp = temp.Next;
            }
        }

        // Recursive Search - Core Requirement
        public Node FindByScoreRecursive(Node current, double target)
        {
            if (current == null) return null;
            if (current.Info.FinalResult == target) return current;
            return FindByScoreRecursive(current.Next, target);
        }

        public Node GetHead() => head;

        // Sorting using Bubble Sort
        public void Sort(bool byName)
        {
            if (head == null) return;
            bool swapped;
            do
            {
                swapped = false;
                Node curr = head;
                while (curr.Next != null)
                {
                    bool condition = byName
                        ? string.Compare(curr.Info.Name, curr.Next.Info.Name) > 0
                        : curr.Info.FinalResult > curr.Next.Info.FinalResult;

                    if (condition)
                    {
                        Student temp = curr.Info;
                        curr.Info = curr.Next.Info;
                        curr.Next.Info = temp;
                        swapped = true;
                    }
                    curr = curr.Next;
                }
            } while (swapped);
        }
    }

    class Program
    {
        static void Main()
        {
            StudentList list = new StudentList();

            // Initial 5 students
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"--- Student {i} Data ---");
                list.InsertBack(CreateStudent(i));
            }

            while (true)
            {
                Console.WriteLine("\n[1]Show [2]Sort Name [3]Sort Score [4]Search [5]Add Front [6]Add Back [7]Exit");
                string op = Console.ReadLine();
                if (op == "7") break;

                switch (op)
                {
                    case "1": list.ShowAll(); break;
                    case "2": list.Sort(true); list.ShowAll(); break;
                    case "3": list.Sort(false); list.ShowAll(); break;
                    case "4":
                        Console.Write("Enter Score to find: ");
                        double target = double.Parse(Console.ReadLine());
                        var res = list.FindByScoreRecursive(list.GetHead(), target);
                        if (res != null) res.Info.PrintDetails();
                        else Console.WriteLine("Not Found!");
                        break;
                    case "5": list.InsertFront(CreateStudent(0)); break;
                    case "6": list.InsertBack(CreateStudent(0)); break;
                }
            }
        }

        static Student CreateStudent(int id)
        {
            Console.Write("Name: "); string n = Console.ReadLine();
            Console.Write("City: "); string c = Console.ReadLine();
            Console.Write("Exam 1 Score: "); double s1 = double.Parse(Console.ReadLine());
            Console.Write("Exam 2 Score: "); double s2 = double.Parse(Console.ReadLine());
            return new Student(id, n, c, s1, s2);
        }
    }
}
