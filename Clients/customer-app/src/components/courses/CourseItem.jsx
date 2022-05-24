import { useNavigate } from "react-router-dom";

function CourseCard({ course }) {
    const navigate = useNavigate();

    const onClickHandler = () => {
        navigate(`/courses/${course.courseNo}`);
    }

    return (
        <div className="card" onClick={onClickHandler}>
            <div className="course-number">{course.courseNo}</div>
            <div className="card-title">{course.name}</div>
            <div className="course-length">{course.length}</div>
        </div>
    );
}

export default CourseCard;