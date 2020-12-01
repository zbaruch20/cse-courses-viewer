using System;
using System.Collections.Generic;

namespace CSE_Courses_Viewer
{
    class CSECourseTest
    {
        private class CompareCSECourseByNumber : IComparer<ICSECourse>
        {
            public int Compare(ICSECourse x, ICSECourse y)
            {
                return x.Number.CompareTo(y.Number);
            }
        }

        public static void Main()
        {
            int number = 4253;
            string name = "Programming in C#";
            string syllabus = "http://coe-portal.cse.ohio-state.edu/pdf-exports/CSE/CSE-4253.pdf";
            int[] prereqs = { 2122, 2123, 2231, 2321 };
            int[] successors = { 5194 };

            ICSECourse c = new CSECourse(number, name, syllabus);
            foreach (int p in prereqs) c.AddPrereq(p);
            foreach (int s in successors) c.AddSuccessor(s);

            Console.WriteLine(c);

            Console.WriteLine("\nPrereqs:");
            foreach (int p in c.Prereqs) Console.WriteLine(p);

            Console.WriteLine("\nSuccessors:");
            foreach (int s in c.Successors) Console.WriteLine(s);


            var set = new SortedSet<ICSECourse>(new CompareCSECourseByNumber());
            set.Add(c);

            ICSECourse tempC = new CSECourse(4253, "", "");
            Console.WriteLine("\nset.Contains({0}) = {1}", tempC, set.Contains(tempC));
        }
    }
}
