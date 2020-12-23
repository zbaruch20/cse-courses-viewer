using System.Collections.Generic;

namespace CSE_Courses_Viewer
{
    /**
     * Interface for a list of CSE Courses, sorted by course number.
     * 
     * @author Zach Baruch
     */
    public interface ICSECourseList : IEnumerable<ICSECourse>
    {
        /**
         * Returns the length of this.
         */
        int Count { get; }

        /**
         * Returns the CSECourse in this whose course number is
         * number. Note that this course is aliased, to allow for
         * editing. If such course is not present, an
         * ArgumentException will be thrown.
         * 
         * @param number the number of the course to retrieve
         * @return the CSECourse in this whose course number is number
         * @throws ArgumentException if a course with number is not in this
         */
        ICSECourse GetCourseByNumber(int number);

        /**
         * Adds course to this. If course.Number is already present in
         * this, an ArgumentException will be thrown.
         * 
         * @param course the course to add to this
         * @throws ArgumentException if course.Number is present in this
         */
        void AddCourse(ICSECourse course);

        /**
         * Removes from this the CSECourse whose course number is
         * number. If such couse is not present, an ArgumentException
         * will be thrown.
         * 
         * @param number the number of the course to remove
         * @return the CSECourse removed
         * @throws ArgumentException if a course with number is not in this
         */
        ICSECourse RemoveCourse(int number);

        /**
         * Returns whether there is a CSECourse whose number is number
         * in this.
         * 
         * @param number the course the number to search for
         * @return true if a couse with number is in this, false otherwise
         */
        bool ContainsNumber(int number);

        /**
         * Returns the course at position index.
         * 
         * @oaram index the index of the course to retrieve
         * @return the course at position index
         * @throws ArgumentOutOfRangeException if index is out of range
         */
        ICSECourse At(int index);
    }
}
