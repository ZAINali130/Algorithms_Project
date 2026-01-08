using System;

namespace AlgorithmsAssignment
{
    // تعريف التقديرات الدراسية 
    enum StudentGrade { Excellent, VeryGood, Good, Fail }

    // كلاس العقدة لتمثيل كل طالب في اللائحة المزدوجة
    class Node
    {
        public Student Info; // بيانات الطالب
        public Node Next, Back; // Next للعقدة التالية و Back للعقدة السابقة

        public Node(Student data)
        {
            Info = data;
            Next = Back = null;
        }
    }

    // كلاس  معلومات الطالب وطريقة حساب نتيجته
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
            CalculateResult(); // حساب النتيجة والتقدير تلقائياً عند الإنشاء
        }

        // دالة لحساب المعدل وتحديد التقدير (ممتاز، جيد جداً، جيد، راسب)
        private void CalculateResult()
        {
            FinalResult = (Score1 + Score2) / 2.0;

            if (FinalResult >= 90) Grade = StudentGrade.Excellent;
            else if (FinalResult >= 80) Grade = StudentGrade.VeryGood;
            else if (FinalResult >= 50) Grade = StudentGrade.Good;
            else Grade = StudentGrade.Fail;
        }

        // دالة لطباعة بيانات الطالب بشكل منظم
        public void PrintDetails()
        {
            Console.WriteLine($"ID: {Id} | الاسم: {Name} | المدينة: {City} | المعدل: {FinalResult} | التقدير: {Grade}");
        }
    }

    // كلاس إدارة لائحة الطلاب (اللائحة المزدوجة)
    class StudentList
    {
        private Node head; // رأس اللائحة
        private Node LastNode; // نهاية اللائحة

        // إضافة طالب في نهاية اللائحة
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

        // إضافة طالب في بداية اللائحة
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

        // عرض جميع الطلاب الموجودين في اللائحة
        public void ShowAll()
        {
            Node temp = head;
            while (temp != null)
            {
                temp.Info.PrintDetails();
                temp = temp.Next;
            }
        }

        // دالة البحث العودي (Recursive Search) -    
        public Node FindByScoreRecursive(Node current, double target)
        {
            if (current == null) return null; // إذا وصلنا للنهاية ولم نجد المعدل
            if (current.Info.FinalResult == target) return current; // إذا وجدنا المعدل المطلوب
            return FindByScoreRecursive(current.Next, target); // الانتقال للعقدة التالية بشكل عودي
        }

        public Node GetHead() => head;

        // خوارزمية ترتيب الطلاب (Bubble Sort) حسب الاسم أو المعدل
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
                        // تبديل البيانات فقط بين العقد
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

            // إدخال بيانات 5 طلاب عند تشغيل البرنامج
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"--- بيانات الطالب {i} ---");
                list.InsertBack(CreateStudent(i));
            }

            // قائمة الخيارات للمستخدم
            while (true)
            {
                Console.WriteLine("\n[1]عرض [2]ترتيب بالاسم [3]ترتيب بالمعدل [4]بحث [5]إضافة للبداية [6]إضافة للنها [7]خروج");
                string op = Console.ReadLine();
                if (op == "7") break;

                switch (op)
                {
                    case "1": list.ShowAll(); break;
                    case "2": list.Sort(true); list.ShowAll(); break;
                    case "3": list.Sort(false); list.ShowAll(); break;
                    case "4":
                        Console.Write("أدخل المعدل للبحث عنه: ");
                        double target = double.Parse(Console.ReadLine());
                        var res = list.FindByScoreRecursive(list.GetHead(), target);
                        if (res != null) res.Info.PrintDetails();
                        else Console.WriteLine("لم يتم العثور على النتيجة!");
                        break;
                    case "5": list.InsertFront(CreateStudent(0)); break;
                    case "6": list.InsertBack(CreateStudent(0)); break;
                }
            }
        }

        // دالة  لإنشاء كائن طالب من إدخال المستخدم
        static Student CreateStudent(int id)
        {
            Console.Write("الاسم: "); string n = Console.ReadLine();
            Console.Write("المدينة: "); string c = Console.ReadLine();
            Console.Write("درجة الامتحان 1: "); double s1 = double.Parse(Console.ReadLine());
            Console.Write("درجة الامتحان 2: "); double s2 = double.Parse(Console.ReadLine());
            return new Student(id, n, c, s1, s2);
        }
    }
}
