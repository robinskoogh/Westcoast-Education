import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import Navbar from './components/navbar/Navbar';
import Home from './components/home/Home';
import CourseList from './components/courses/CourseList';
import Register from './components/auth/Register';
import CourseDetails from './components/courses/CourseDetails';

import './utilities.css';
import './styles.css';

function App() {
    return (
        <Router>
            <Navbar />
            <main>
                <Routes>
                    <Route path='/' element={<Home />} />
                    <Route path='/courses' element={<CourseList />} />
                    <Route path='/register' element={<Register />} />
                    <Route path='/courses/:courseNo' element={<CourseDetails />} />
                </Routes>
            </main>
        </Router>
    )
}

export default App;