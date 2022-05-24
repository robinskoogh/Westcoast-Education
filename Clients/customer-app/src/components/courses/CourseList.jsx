import { useEffect, useState } from "react";

import CourseCard from "./CourseItem";
import CategoryList from "./CategoryList";

import './course-list.css';

function CourseList() {
    const [courses, setCourses] = useState([]);
    const [filteredCourses, setFilteredCourses] = useState([]);

    useEffect(() => {
        loadCourses();
    }, []);

    async function loadCourses() {
        const url = `${process.env.REACT_APP_BASEURL}/courses/list`;
        const response = await fetch(url, {
            method: 'GET'
        });

        const filterButtons = document.querySelectorAll('.filter-checkbox:checked');
        filterButtons.forEach(btn => {
            btn.checked = false;
        });

        if (!response.ok) {
            console.log("Something went wrong while fetching the courses");
        } else {
            var courseData = await response.json();
            setCourses(courseData);
            setFilteredCourses(courseData);
        }
    }

    function filterCourses() {
        const selectedCategories = document.querySelectorAll('.filter-checkbox:checked');

        if (selectedCategories.length > 0) {
            let categoryNames = [];
            for (let i = 0; i < selectedCategories.length; i++) {
                categoryNames.push(selectedCategories[i].defaultValue);
            }
            console.log(categoryNames);
            let filter = courses.filter(course => {
                return categoryNames.includes(course.category);
            })
            setFilteredCourses(filter);

        } else {
            setFilteredCourses(courses);
        }
    }

    return (
        <>
            <div className="page-title">Our courses</div>
            <div className="list-container">
                <section>
                    <div className="list-header">All courses</div>
                    <div className="card-container">
                        <div className="card-header">
                            <div>No.</div>
                            <div>Course</div>
                            <div>Length</div>
                        </div>
                        {filteredCourses.map((course) => (
                            <CourseCard
                                course={course}
                                key={course.courseNo}
                            />
                        ))}
                    </div>
                </section>
                <section>
                    <div className="list-header">Filter categories</div>
                    <CategoryList filterCourses={filterCourses} loadCourses={loadCourses} />
                </section>
            </div >
        </>
    );
}

export default CourseList;