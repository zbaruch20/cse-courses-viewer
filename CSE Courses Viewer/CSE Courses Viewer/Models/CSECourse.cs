using System;
using System.Collections.Generic;

namespace CSE_Courses_Viewer
{
    /**
     * Implementation of ICSECourse interface.
     * 
     * @author Zach Baruch
     */
    [Serializable]
    public class CSECourse : ICSECourse
    {
        #region Public Members
        /**
         * Course number.
         */
        public int Number { get; set; }

        /**
         * Course name.
         */
        public string Name { get; set; }

        /**
         * Link to syllabus page.
         */
        public string Syllabus { get; set; }

        /**
         * Course numbers of courses that are a prerequisite to this.
         */
        public IList<int> Prereqs { get; set; }

        /**
         * Course number of courses that this is a prerequisite to.
         */
        public IList<int> Successors { get; set; }

        #endregion

        #region Constructors

        /**
         * Constructor from arguments. To add prerequisite and
         * successor courses to this, you must use the methods
         * AddPrereq() and AddSuccessor(), respectively.
         * 
         * @param number the course number
         * @param name the course name
         * @param syllabus the link to the syllabus page
         */
        public CSECourse(int number, string name, string syllabus)
        {
            // Initialize fields from arguments
            Number = number;
            Name = name;
            Syllabus = syllabus;

            // Construct empty lists
            Prereqs = new List<int>();
            Successors = new List<int>();
        }

        /**
         * No-argument constructor.
         */
        public CSECourse() : this(0, "New Course", "https://cse.osu.edu") { }

        #endregion

        #region Public method implementations

        public void AddPrereq(int prereqNumber)
        {
            if (Prereqs.Contains(prereqNumber)) throw new ArgumentException("Violation of: prereqNumber is not in this.Prereqs");

            Prereqs.Add(prereqNumber);
        }

        public bool RemovePrereq(int prereqNumber)
        {
            return Prereqs.Remove(prereqNumber);
        }

        public void AddSuccessor(int successorNumber)
        {
            if (Successors.Contains(successorNumber)) throw new ArgumentException("Violation of: successorNumber is not in this.Successors");

            Successors.Add(successorNumber);
        }

        public bool RemoveSuccessor(int successorNumber)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object other)
        {
            if (other == null) return false;
            if (other == this) return true;
            if (!(other is CSECourse)) return false;

            CSECourse otherCourse = other as CSECourse;
            return Number == otherCourse.Number &&
                   Name == otherCourse.Name &&
                   Syllabus == otherCourse.Syllabus &&
                   Prereqs.Equals(otherCourse.Prereqs) &&
                   Successors.Equals(otherCourse.Successors);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", Number, Name, Syllabus);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}
