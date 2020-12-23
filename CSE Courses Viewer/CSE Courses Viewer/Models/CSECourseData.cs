using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Linq;

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
         * Recursively descent parses tokens and adds the proper prerequisites to prereqs.
         * 
         * @param tokens the tokens from the loaded preprequisites string from the webpage
         * @param prereqs the parsed list of prerequisite courses
         */
        private static void ParsePrereqs(Queue<string> tokens, IList<int> prereqs)
        {
            if (tokens.Count > 0)
            {
                string frontToken = tokens.Dequeue();
                switch (frontToken)
                {
                    case "ece":
                    case "engr":
                    case "ise":
                    case "math":
                    case "phil":
                    case "stat":
                        // Keep dequeueing until we get 'CSE' or 'concur'
                        while (tokens.Count > 0 && !tokens.Peek().Equals("cse") && !tokens.Peek().Equals("concur"))
                        {
                            tokens.Dequeue();
                        }
                        break;
                    default:
                        // If we get a four digit number, add it to the prereq string
                        if (frontToken.Length == 4 && int.TryParse(frontToken, out int num))
                        {
                            prereqs.Add(num);
                        }
                        break;
                }

                // Continue to parse
                ParsePrereqs(tokens, prereqs);
            }
        }

        /**
         * Parses prereqStr and returns a list of the CSE prerequisite courses. The returned list
         * contains each 4-digit course number that isn't succeeded by 'Math', 'ECE', 'Engr', 'ISE', or 'Stat'.
         * 
         * @param prereqStr the string containing the detailed description of prerequisites to be parsed
         * @return a list of the parsed CSE prerequisite courses
         */
        private static IList<int> LoadPrereqs(string prereqStr)
        {
            IList<int> prereqs = new List<int>();

            Queue<string> tokens = new Queue<string>(
                prereqStr.ToLower().Split(" ,.:;\t\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            );

            ParsePrereqs(tokens, prereqs);

            return prereqs;
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
                        Prereqs = LoadPrereqs(row.ChildNodes[9].InnerText)
                    });
                }
            }
        }

        /**
         * Traverses the CourseList and adds any successor courses.
         */
        private static void LoadSuccessors()
        {

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
            LoadSuccessors();
        }
    }
}
