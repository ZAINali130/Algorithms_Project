using System;

namespace AlgorithmsAssignment
{
    class Program
    {
         تعريف التقديرات الدراسية
        enum StudentGrade { Excellent, VeryGood, Good, Fail }

        // 2. تعريف العقدة (Node) - تحتوي على بيانات الطالب وروابط الشجرة (يمين ويسار)
        class TreeNode
        {
            public int Id;
            public string Name;
            public double FinalResult;
            public StudentGrade Grade;
            
            // روابط الشجرة
            public TreeNode Left;  // للقيم الأصغر من الـ ID الحالي
            public TreeNode Right; // للقيم الأكبر من الـ ID الحالي

            public TreeNode(int id, string name, double s1, double s2)
            {
                Id = id;
                Name = name;
                FinalResult = (s1 + s2) / 2.0;
                
                // تحديد التقدير بناءً على المعدل
                if (FinalResult >= 90) Grade = StudentGrade.Excellent;
                else if (FinalResult >= 80) Grade = StudentGrade.VeryGood;
                else if (FinalResult >= 50) Grade = StudentGrade.Good;
                else Grade = StudentGrade.Fail;

                Left = Right = null;
            }
        }

        //  (نقطة الانطلاق)
        static TreeNode root = null;

        static void Main(string[] args)
        {
            // إدخال بيانات تجريبية لبناء الشجرة
            // شرح للآنسة: الترتيب يتم تلقائياً بناءً على رقم الـ ID
            Insert(10, "Ahmad", 90, 88);
            Insert(5, "Lina", 70, 65);
            Insert(15, "Samer", 95, 92);
            Insert(8, "Noor", 88, 84);

            Console.WriteLine("=== الطلاب الذين معدلهم فوق 85 ===");
            ShowHighAchievers(root);

            Console.WriteLine("\n=== حذف الطالب صاحب الرقم 5 ===");
            Delete(5);

            Console.WriteLine("\n=== عرض جميع الطلاب مرتبين حسب الرقم (In-Order) ===");
            PrintTree(root);

            Console.WriteLine("\nاضغط أي مفتاح للخروج...");
            Console.ReadKey();
        }

        // --- الدوال البرمجية (Functions) ---

        // دالة الإضافة العودية - شرح: تضع الصغير يسار والكبير يمين
        static void Insert(int id, string name, double s1, double s2)
        {
            root = InsertRecursive(root, id, name, s1, s2);
        }

        static TreeNode InsertRecursive(TreeNode node, int id, string name, double s1, double s2)
        {
            if (node == null) return new TreeNode(id, name, s1, s2);

            if (id < node.Id) node.Left = InsertRecursive(node.Left, id, name, s1, s2);
            else if (id > node.Id) node.Right = InsertRecursive(node.Right, id, name, s1, s2);
            
            return node;
        }

        //  دالة تمر على الشجرة وتعرض فقط من يحقق الشرط (> 85)
        static void ShowHighAchievers(TreeNode node)
        {
            if (node == null) return;
            ShowHighAchievers(node.Left);
            if (node.FinalResult > 85) 
                Console.WriteLine($"ID: {node.Id} | الاسم: {node.Name} | المعدل: {node.FinalResult}");
            ShowHighAchievers(node.Right);
        }

        //   أصعب دالة في الشجرة، تعالج 3 حالات 
        static void Delete(int id) { root = DeleteRecursive(root, id); }

        static TreeNode DeleteRecursive(TreeNode node, int id)
        {
            if (node == null) return null;

            if (id < node.Id) node.Left = DeleteRecursive(node.Left, id);
            else if (id > node.Id) node.Right = DeleteRecursive(node.Right, id);
            else
            {
                    
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;

                //   نبحث عن أصغر قيمة في الجهة اليمنى 
                node.Id = MinValue(node.Right);
                node.Right = DeleteRecursive(node.Right, node.Id);
            }
            return node;
        }

        static int MinValue(TreeNode node)
        {
            int min = node.Id;
            while (node.Left != null) { node = node.Left; min = node.Id; }
            return min;
        }

        // طباعة الشجرة بالكامل مرتبة
        static void PrintTree(TreeNode node)
        {
            if (node == null) return;
            PrintTree(node.Left);
            Console.WriteLine($"ID: {node.Id} | الاسم: {node.Name} | التقدير: {node.Grade}");
            PrintTree(node.Right);
        }
    }
}
