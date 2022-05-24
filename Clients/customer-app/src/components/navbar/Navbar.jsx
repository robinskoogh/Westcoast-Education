import { NavLink } from 'react-router-dom';

import logo from '../../imgs/logo.png';

function Navbar() {

    function offClick() {
        let navItems = document.querySelector('.nav-items');

        navItems.classList.remove('show');
        window.removeEventListener('click', offClick);
    }

    function handleClick(e) {
        let navItems = document.querySelector('.nav-items');

        e.stopPropagation();
        navItems.classList.add('show')
        if (navItems.classList.contains('show')) {
            window.addEventListener('click', offClick);
        }
    }

    return (
        <nav id='navbar'>
            <NavLink to="/"><img className='logo' src={logo} alt="logo" /></NavLink>
            <i className="fa-solid fa-bars nav-icon" onClick={handleClick}></i>
            <ul className='nav-items'>
                <li><NavLink className={({ isActive }) => (isActive ? "navlink-active" : "")} to="/">Home</NavLink></li>
                <li><NavLink className={({ isActive }) => (isActive ? "navlink-active" : "")} to="/courses">Courses</NavLink></li>
                <li><NavLink className={({ isActive }) => (isActive ? "navlink-active" : "")} to="/register">Register</NavLink></li>
            </ul>
        </nav >
    )
}

export default Navbar;