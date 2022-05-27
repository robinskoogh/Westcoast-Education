import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import './course-details.css';

function CourseDetails() {
    const params = useParams();

    const [course, setCourse] = useState({})

    useEffect(() => {
        fetchCourse(params.courseNo);
    }, [params.courseNo])

    async function fetchCourse(courseNo) {
        const url = `${process.env.REACT_APP_BASEURL}/courses/${courseNo}`;
        const response = await fetch(url);

        if (!response.ok) {
            console.log("There was an error fetching the course");
        }
        setCourse(await response.json());
    }

    return (
        <section>
            <h1 className="page-title">{course.name}</h1>
            <div className="details-container">
                <h3 className="course-description">{course.description}</h3>
                <div className="details-info">
                    <p>CourseNo <strong>{course.courseNo}</strong></p>
                    <p>Length <strong>{course.length}</strong></p>
                    <p>Category <strong>{course.category}</strong></p>
                </div>
                <div className="details-curriculum">Course curriculum
                    <p>{course.details}</p>
                </div>
            </div>
        </section>
    );
}

export default CourseDetails;