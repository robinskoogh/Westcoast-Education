import { useEffect, useState } from "react";

import CategoryItem from "./CategoryItem";

import './category-list.css'

function CategoryList({ filterCourses, loadCourses }) {
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        loadCategories();
    }, []);

    async function loadCategories() {
        const url = `${process.env.REACT_APP_BASEURL}/categories/list`;
        const response = await fetch(url);

        if (!response.ok) {
            console.log("Something went wrong while fetching the categories");
        } else {
            setCategories(await response.json());
        }
    }

    return (
        <>
            <section className="category-container">
                <div className="selection-reset" onClick={loadCourses}>Reset filter</div>
                {categories.map((category) => (
                    <CategoryItem
                        filterCourses={filterCourses}
                        category={category}
                        key={category.id}
                    />
                ))}
            </section>
        </>
    );
}

export default CategoryList;