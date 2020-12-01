using System;
using System.Collections.Generic;
using System.Text;

namespace CSE_Courses_Viewer
{
    /**
     * The data for the courses in the application.
     * For now, this will contain just a few hard-coded courses. In the future,
     * this will be populated by scraping and parsing the COE's list of CSE
     * Semester Courses.
     * 
     * @author Zach Baruch
     */
    public static class CSECourseData
    {
        public static ICSECourseList CourseList { get; private set; }
        static CSECourseData()
        {
            CourseList = new CSECourseList();

            // Software 1
            CourseList.AddCourse(new CSECourse
            {
                Number = 2221,
                Name = "Software I: Software Components",
                Syllabus = "http://coe-portal.cse.ohio-state.edu/pdf-exports/CSE/CSE-2221.pdf",
                Successors = new List<int>(new int[] { 2231, 2321 })
            });

            // Software 2
            CourseList.AddCourse(new CSECourse
            {
                Number = 2231,
                Name = "Software II: Software Development and Design",
                Syllabus = "http://coe-portal.cse.ohio-state.edu/pdf-exports/CSE/CSE-2231.pdf",
                Prereqs = new List<int>(new int[] { 2221, 2321 }),
                Successors = new List<int>(new int[] { 2321, 4253 })
            });

            // Foundations 1
            CourseList.AddCourse(new CSECourse
            {
                Number = 2321,
                Name = "Foundations I: Discrete Structures",
                Syllabus = "http://coe-portal.cse.ohio-state.edu/pdf-exports/CSE/CSE-2321.pdf",
                Prereqs = new List<int>(new int[] { 2221, 2231 }),
                Successors = new List<int>(new int[] { 4253 })
            });

            // C#
            CourseList.AddCourse(new CSECourse
            {
                Number = 4253,
                Name = "Programming in C#",
                Syllabus = "http://coe-portal.cse.ohio-state.edu/pdf-exports/CSE/CSE-4253.pdf",
                Prereqs = new List<int>(new int[] { 2231, 2321 })
            });

        }
    }
}
