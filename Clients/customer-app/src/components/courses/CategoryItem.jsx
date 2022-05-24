function CategoryItem({ category, filterCourses }) {
    return (
        <>
            <input type="checkbox" className="filter-checkbox" id={category.id} value={category.name} hidden onChange={filterCourses} />
            <label className="category-item" htmlFor={category.id}>
                <span className="category-item-content">{category.name}</span>
            </label>
        </>
    );
}

export default CategoryItem;