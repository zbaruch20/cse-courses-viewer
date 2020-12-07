using System.Collections.Generic;

namespace CSE_Courses_Viewer
{
    /**
     * Interface for a representation of an individual CSE Course.
     * 
     * @author Zach Baruch
     */
    public interface ICSECourse
    {
        #region Properties
        /**
         * Course number.
         */
        int Number { get; set; }

        /**
         * Course name.
         */
        string Name { get; set; }

        /**
         * Link to syllabus page.
         */
        string Syllabus { get; set; }

        /**
         * Course numbers of courses that are a prerequisite to this.
         */
        IList<int> Prereqs { get; set; }

        /**
         * String containing the course prerequisites.
         */
        string PrereqStr { get; }

        /**
         * Course numbers of courses that this is a prerequisite to.
         */
        IList<int> Successors { get; set; }

        /**
         * String containing the course successors.
         */
        string SuccessorStr { get; }
        #endregion

        /**
         * Adds prereqNumber to the list of prerequisites for this.
         * prereqNumber must not be currently in this.Prereqs,
         * otherwise an ArgumentException will be thrown.
         * 
         * @param prereqNumber the number of the prerequisite course to add
         * @throws ArgumentException if prereqNumber is already in this.Prereqs
         */
        void AddPrereq(int prereqNumber);

        /**
         * Removes prereqNumber from the list of prerequisites for
         * this, and returns whether the operation was successful.
         * 
         * @param prereqNumber the number of the prerequisite course to remove
         * @return true if the prerequisite was successfully removed, false otherwise
         */
        bool RemovePrereq(int prereqNumber);

        /**
         * Adds successorNumber to the list of prerequisites for this.
         * successorNumber must not be currently in this.Successors,
         * otherwise an ArgumentException will be thrown.
         * 
         * @param successorNumber the number of the prerequisite course to add
         * @throws ArgumentException if successorNumber is already in this.Prereqs
         */
        void AddSuccessor(int successorNumber);

        /**
         * Removes successorNumber from the list of prerequisites for
         * this, and returns whether the operation was successful.
         * 
         * @param successorNumber the number of the prerequisite course to remove
         * @return true if the successor was successfully removed, false otherwise
         */
        bool RemoveSuccessor(int successorNumber);
    }
}
