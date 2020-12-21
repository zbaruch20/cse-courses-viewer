using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

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
        /**
         * The CourseList containing the main course data, and will be data bound to the ViewModel.
         */
        public static ICSECourseList CourseList { get; private set; }

        /**
         * The base URI of the courses page. Each syllabus link should be an extension to this URI.
         */
        private static string BaseUri = @"http://coe-portal.cse.ohio-state.edu/pdf-exports/CSE/";

        /**
         * Parses prereqStr and returns a list of the CSE prerequisite courses. The returned list
         * contains each 4-digit course number that isn't succeeded by 'Math', 'ECE', 'Engr', or 'Stat'.
         * 
         * @param prereqStr the string containing the detailed description of prerequisites to be parsed
         * @return a list of the parsed CSE prerequisite courses
         */
        private static IList<int> ParsePrereqs(string prereqStr)
        {
            // TODO - Implement this
            return new List<int>();
        }

        /**
         * Parses courseTags and loads CourseList with each CSE course in courseTags.
         * For each course tag, the first child node should contain the course number with a
         * link to the syllabus, the third child node should contain the name of the course,
         * and the fifth child node should contain course prerequisites.
         * Any course whose number is not 'standard' (i.e. honors courses or courses with an extension)
         * will not be added to the CourseList.
         * 
         * @param courseTags tr tags containing information for each CSE course
         */
        private static void LoadCourses(IEnumerable<HtmlNode> courseTags)
        {
            CourseList = new CSECourseList();
            foreach (HtmlNode row in courseTags)
            {
                // Check that course number is valid
                if (int.TryParse(row.ChildNodes[1].InnerText, out int number))
                {
                    string name = row.ChildNodes[5].InnerText;
                    string syllabus = BaseUri + row.ChildNodes[1].FirstChild.GetAttributeValue("href", "");

                    CourseList.AddCourse(new CSECourse()
                    {
                        Number = number,
                        Name = name,
                        Syllabus = syllabus,
                        Prereqs = ParsePrereqs(row.ChildNodes[9].InnerText)
                    });
                }
            }
        }

        /**
         * Scrapes data from http://coe-portal.cse.ohio-state.edu/pdf-exports/CSE/ and
         * returns the tr tags containing the information for each CSE course.
         * 
         * @return IEnumerable of HTML tr tags containing the information for each CSE course
         */
        private static IEnumerable<HtmlNode> ScrapeCourses()
        {
            HtmlDocument doc = (new HtmlWeb()).Load(BaseUri);
            return doc.DocumentNode.QuerySelectorAll("table.data_table tr[class^=\"tr_data\"]");
        }

        static CSECourseData()
        {
            IEnumerable<HtmlNode> courseTags = ScrapeCourses();
            LoadCourses(courseTags);
        }
    }
}
