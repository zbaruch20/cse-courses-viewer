using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
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
        private static void loadHardCodedCourses(ICSECourseList courseList)
        {
            // Software 1
            courseList.AddCourse(new CSECourse
            {
                Number = 2221,
                Name = "Software I: Software Components",
                Syllabus = "http://coe-portal.cse.ohio-state.edu/pdf-exports/CSE/CSE-2221.pdf",
                Successors = new List<int>(new int[] { 2231, 2321 })
            });

            // Software 2
            courseList.AddCourse(new CSECourse
            {
                Number = 2231,
                Name = "Software II: Software Development and Design",
                Syllabus = "http://coe-portal.cse.ohio-state.edu/pdf-exports/CSE/CSE-2231.pdf",
                Prereqs = new List<int>(new int[] { 2221, 2321 }),
                Successors = new List<int>(new int[] { 2321, 4253 })
            });

            // Foundations 1
            courseList.AddCourse(new CSECourse
            {
                Number = 2321,
                Name = "Foundations I: Discrete Structures",
                Syllabus = "http://coe-portal.cse.ohio-state.edu/pdf-exports/CSE/CSE-2321.pdf",
                Prereqs = new List<int>(new int[] { 2221, 2231 }),
                Successors = new List<int>(new int[] { 4253 })
            });

            // C#
            courseList.AddCourse(new CSECourse
            {
                Number = 4253,
                Name = "Programming in C#",
                Syllabus = "http://coe-portal.cse.ohio-state.edu/pdf-exports/CSE/CSE-4253.pdf",
                Prereqs = new List<int>(new int[] { 2231, 2321 })
            });
        }

        private static void TestCSECourse()
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

        private static void TestScraping()
        {
            HtmlDocument doc = (new HtmlWeb()).Load(@"http://coe-portal.cse.ohio-state.edu/pdf-exports/CSE/");
            HtmlNode table = doc.DocumentNode.QuerySelector(".data_table");

            var rows = table.QuerySelectorAll("tr[class^=\"tr_data\"]");
            foreach (HtmlNode row in rows)
            {
                // Console.WriteLine(row.InnerHtml);
                Console.WriteLine("CSE {0} - {1}", row.ChildNodes[1].InnerText, row.ChildNodes[5].InnerText);
                // Console.WriteLine("\tSyllabus: {0}", row.ChildNodes[1].FirstChild.GetAttributeValue("href", ""));
            }
        }

        public static void Main()
        {
            // TestCSECourse();
            TestScraping();
        }
    }
}
