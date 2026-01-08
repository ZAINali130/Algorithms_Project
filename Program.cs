using System;

namespace AlgorithmsAssignment
{
    //  تعريف التقديرات 
    enum StudentGrade { Excellent, VeryGood, Good, Fail }

    //  كلاس الطالب
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
            // حساب المحصلة (مجموع الاختبارين على 2)
            FinalResult = (Score1 + Score2) / 2.0;

            if (FinalResult >= 90) Grade = StudentGrade.Excellent;
            else if (FinalResult >= 80) Grade = StudentGrade.VeryGood;
            else if (FinalResult >= 50) Grade = StudentGrade.Good;
            else Grade = StudentGrade.Fail;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"ID: {Id,-5} | Name: {Name,-10} | Result: {FinalResult,-5} | Grade: {Grade}");
        }
    }

    //  عقدة الشجرة )
    class TreeNode
    {
        public Student Info;
        public TreeNode Left, Right;

        public TreeNode(Student data)
        {
            Info = data;
            Left = Right = null;
        }
    }

    //  كلاس الشجرة (Binary Search Tree)
    class StudentTree
    {
        private TreeNode root;

        // إضافة طالب - ترتيب الشجرة يعتمد على ID الطالب
        public void Insert(Student s) => root = InsertRecursive(root, s);

        private TreeNode InsertRecursive(TreeNode root, Student s)
        {
            if (root == null) return new TreeNode(s);
            if (s.Id < root.Info.Id) root.Left = InsertRecursive(root.Left, s);
            else if (s.Id > root.Info.Id) root.Right = InsertRecursive(root.Right, s);
            return root;
        }

        //  عرض الطلاب الذين محصلتهم أعلى من 85
        public void ShowHighAchievers(TreeNode node)
        {
            if (node == null) return;
            ShowHighAchievers(node.Left);
            if (node.Info.FinalResult > 85) node.Info.PrintDetails();
            ShowHighAchievers(node.Right);
        }

        //  حذف طالب برقم محدد
        public void Delete(int id) => root = DeleteRecursive(root, id);

        private TreeNode DeleteRecursive(TreeNode root, int id)
        {
            if (root == null) return null;

            if (id < root.Info.Id) root.Left = DeleteRecursive(root.Left, id);
            else if (id > root.Info.Id) root.Right = DeleteRecursive(root.Right, id);
            else
            {
                // حالة العقدة بطفل واحد أو بدون أطفال
                if (root.Left == null) return root.Right;
                if (root.Right == null) return root.Left;

                // حالة العقدة بطفلين: نأتي بأصغر قيمة من الطرف الأيمن
                root.Info = FindMin(root.Right);
                root.Right = DeleteRecursive(root.Right, root.Info.Id);
            }
            return root;
        }

        private Student FindMin(TreeNode node)
        {
            Student min = node.Info;
            while (node.Left != null) { node = node.Left; min = node.Info; }
            return min;
        }

        public TreeNode GetRoot() => root;
    }

    
    class Program
    {
        static void Main()
        {
            StudentTree tree = new StudentTree();

            // إدخال بيانات تجريبية (يمكنك جعلها Input من المستخدم)
            tree.Insert(new Student(10, "Ahmad", "Damascus", 90, 88));
            tree.Insert(new Student(5, "Lina", "Homs", 70, 65));
            tree.Insert(new Student(15, "Samer", "Aleppo", 95, 92));
            tree.Insert(new Student(8, "Noor", "Hama", 88, 84));

            Console.WriteLine("=== Students with Average > 85 ===");
            tree.ShowHighAchievers(tree.GetRoot());

            Console.WriteLine("\n=== Deleting Student with ID 5 ===");
            tree.Delete(5);

            Console.WriteLine("\n=== All Students in Tree (Sorted by ID) ===");
            PrintInOrder(tree.GetRoot());

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void PrintInOrder(TreeNode node)
        {
            if (node == null) return;
            PrintInOrder(node.Left);
            node.Info.PrintDetails();
            PrintInOrder(node.Right);
        }
    }
}