using System;

namespace AlgorithmsAssignment
{
  
    class Program
    {
         تعريف التقديرات الدراسية
        enum StudentGrade { Excellent, VeryGood, Good, Fail }

        تعريف العقدة (Node) وهي تحتوي على بيانات الطالب وروابط اللائحة معاً
        class Node
        {
            // بيانات الطالب  
            public int Id;
            public string Name;
            public string City;
            public double Score1;
            public double Score2;
            public double FinalResult;
            public StudentGrade Grade;

            // روابط اللائحة المزدوجة (التالي والسابق)
            public Node Next;
            public Node Back;

            public Node(int id, string name, string city, double s1, double s2)
            {
                this.Id = id;
                this.Name = name;
                this.City = city;
                this.Score1 = s1;
                this.Score2 = s2;
                
                // حساب المعدل والتقدير فوراً عند إنشاء العقدة
                this.FinalResult = (s1 + s2) / 2.0;
                if (FinalResult >= 90) Grade = StudentGrade.Excellent;
                else if (FinalResult >= 80) Grade = StudentGrade.VeryGood;
                else if (FinalResult >= 50) Grade = StudentGrade.Good;
                else Grade = StudentGrade.Fail;

                this.Next = null;
                this.Back = null;
            }
        }

        //  التحكم باللائحة (الرأس والنهاية)
        static Node head = null;
        static Node lastNode = null;

     
        static void Main(string[] args)
        {
            // إدخال بيانات 5 طلاب تلقائياً عند بدء التشغيل لتعبئة القائمة
            Console.WriteLine("=== مرحباً بك في نظام إدارة الطلاب (Linked List) ===");
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"\n--- إدخال بيانات الطالب رقم {i} ---");
                InsertAtBack();
            }

    
            while (true)
            {
                Console.WriteLine("\n--------------------------------------------------");
                Console.WriteLine("[1] عرض الطلاب [2] ترتيب بالاسم [3] ترتيب بالمعدل [4] بحث عودي [5] إضافة للبداية [6] إضافة للنهاية [7] خروج");
                Console.Write("اختر العملية المطلوبة: ");
                string choice = Console.ReadLine();

                if (choice == "7") break;

                switch (choice)
                {
                    case "1": ShowStudents(); break;
                    case "2": SortBy(true); ShowStudents(); break;
                    case "3": SortBy(false); ShowStudents(); break;
                    case "4": SearchProcess(); break;
                    case "5": InsertAtFront(); break;
                    case "6": InsertAtBack(); break;
                    default: Console.WriteLine("اختيار غير صحيح!"); break;
                }
            }
        }

       

        // دالة الإضافة في نهاية اللائحة
        static void InsertAtBack()
        {
            Node newNode = InputStudentData();
            if (head == null) head = lastNode = newNode;
            else
            {
                lastNode.Next = newNode;
                newNode.Back = lastNode;
                lastNode = newNode;
            }
        }

        // دالة الإضافة في بداية اللائحة
        static void InsertAtFront()
        {
            Node newNode = InputStudentData();
            if (head == null) head = lastNode = newNode;
            else
            {
                newNode.Next = head;
                head.Back = newNode;
                head = newNode;
            }
        }

        // دالة عرض جميع العقد في اللائحة
        static void ShowStudents()
        {
            if (head == null) { Console.WriteLine("القائمة فارغة!"); return; }
            Node temp = head;
            Console.WriteLine("\nقائمة الطلاب الحالية:");
            while (temp != null)
            {
                Console.WriteLine($"ID: {temp.Id} | الاسم: {temp.Name} | المعدل: {temp.FinalResult} | التقدير: {temp.Grade}");
                temp = temp.Next;
            }
        }

        //  دالة البحث العودي 
        static Node RecursiveSearch(Node current, double target)
        {
            if (current == null) return null;
            if (current.FinalResult == target) return current;
            return RecursiveSearch(current.Next, target);
        }

        // دالة لتنظيم عملية البحث وطلب المعدل من المستخدم
        static void SearchProcess()
        {
            Console.Write("أدخل المعدل الدقيق للبحث عنه: ");
            if (double.TryParse(Console.ReadLine(), out double score))
            {
                Node result = RecursiveSearch(head, score);
                if (result != null)
                    Console.WriteLine($"=> تم العثور على الطالب: {result.Name} من مدينة {result.City}");
                else
                    Console.WriteLine("=> عذراً، لا يوجد طالب بهذا المعدل.");
            }
        }

        // د للترتيب حسب الاسم أو المعدل
        static void SortBy(bool nameOption)
        {
            if (head == null || head.Next == null) return;
            bool swapped;
            do
            {
                swapped = false;
                Node curr = head;
                while (curr.Next != null)
                {
                    bool shouldSwap = nameOption 
                        ? string.Compare(curr.Name, curr.Next.Name) > 0 
                        : curr.FinalResult > curr.Next.FinalResult;

                    if (shouldSwap)
                    {
                        // تبديل البيانات داخل العقد
                        string tName = curr.Name; curr.Name = curr.Next.Name; curr.Next.Name = tName;
                        double tRes = curr.FinalResult; curr.FinalResult = curr.Next.FinalResult; curr.Next.FinalResult = tRes;
                        string tCity = curr.City; curr.City = curr.Next.City; curr.Next.City = tCity;
                        swapped = true;
                    }
                    curr = curr.Next;
                }
            } while (swapped);
            Console.WriteLine("تمت عملية الترتيب.");
        }

        // دالة مساعدة لطلب البيانات 
        static Node InputStudentData()
        {
            Console.Write("الاسم: "); string n = Console.ReadLine();
            Console.Write("المدينة: "); string c = Console.ReadLine();
            Console.Write("درجة الاختبار 1: "); double s1 = double.Parse(Console.ReadLine());
            Console.Write("درجة الاختبار 2: "); double s2 = double.Parse(Console.ReadLine());
            return new Node(0, n, c, s1, s2);
        }
    }
}
