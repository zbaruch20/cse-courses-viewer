using System;
using System.Collections;
using System.Collections.Generic;

namespace CSE_Courses_Viewer
{
    /**
     * Implentation of ICSECourseList interface.
     * 
     * @author Zach Baruch
     */
    public class CSECourseList : ICSECourseList, IEnumerable<ICSECourse>
    {
        #region Public members

        /**
         * The number of courses in this.
         */
        public int Count { get { return CourseList.Count; } }

        #endregion

        #region Private members

        /**
         * The sorted set of courses.
         */
        private SortedSet<ICSECourse> CourseList;

        /**
         * IComparer to compare CSECourses by their number.
         * Note: This is NOT consistent with equals.
         */
        private class CompareCSECourseByNumber : IComparer<ICSECourse>
        {
            public int Compare(ICSECourse x, ICSECourse y)
            {
                return x.Number.CompareTo(y.Number);
            }
        }

        #endregion

        /**
         * Constructor.
         */
        public CSECourseList()
        {
            CourseList = new SortedSet<ICSECourse>(new CompareCSECourseByNumber());
        }

        #region ICSECourseList Implementation
        public void AddCourse(ICSECourse course)
        {
            if (ContainsNumber(course.Number))
            {
                throw new ArgumentException("Violation of: A course with the same number is not in this");
            }

            CourseList.Add(course);

        }

        public bool ContainsNumber(int number)
        {
            ICSECourse temp = new CSECourse(number, "", "");

            /*
             * Even though the possible course in the list will most likely
             * have different data than temp, the implementaion of Contains
             * uses the provided IComparer. Since it returns 'equal' if the
             * numbers are equal, even if other properties are not equal, this
             * method call works.
             */
            return CourseList.Contains(temp);
        }

        public ICSECourse GetCourseByNumber(int number)
        {
            if (!ContainsNumber(number))
            {
                throw new ArgumentException("Violation of: A course with the same number is in this");
            }

            // This is super inefficient but it's the best way I can think of
            var courses = new List<ICSECourse>(CourseList);
            ICSECourse temp = new CSECourse(number, "", "");

            // Again this works due to the IComparer design
            int idxToSearchFor = courses.BinarySearch(temp, new CompareCSECourseByNumber()); 
            return courses[idxToSearchFor];
        }

        public ICSECourse RemoveCourse(int number)
        {
            if (!ContainsNumber(number))
            {
                throw new ArgumentException("Violation of: A course with the same number is in this");
            }

            // This breaks the kernel purity rule but I'm too checked out to care
            ICSECourse removedCourse = GetCourseByNumber(number);
            CourseList.Remove(removedCourse);
            return removedCourse;
        }

        public ICSECourse At(int index)
        {
            if (index < 0 || index > CourseList.Count)
            {
                throw new ArgumentOutOfRangeException("Violation of: 0 <= index <= |this|");
            }

            var courses = new List<ICSECourse>(CourseList);
            return courses[index];
        }
        #endregion

        #region IEnumerable Implementation
        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<ICSECourse> IEnumerable<ICSECourse>.GetEnumerator()
        {
            return CourseList.GetEnumerator();
        }
        #endregion
    }
}
