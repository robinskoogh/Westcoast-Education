import { useState } from "react";
import { useNavigate } from 'react-router-dom'

import './auth.css'

function Register() {

    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [streetAddress, setStreetAddress] = useState('');
    const [zipCode, setZipCode] = useState('');
    const [city, setCity] = useState('');

    function handleEmailTextChanged(e) {
        setEmail(e.target.value);
    }
    function handlePasswordTextChanged(e) {
        setPassword(e.target.value);
    }
    function handleFirstNameTextChanged(e) {
        setFirstName(e.target.value);
    }
    function handleLastNameTextChanged(e) {
        setLastName(e.target.value);
    }
    function handlePhoneNumberTextChanged(e) {
        setPhoneNumber(e.target.value);
    }
    function handleStreetAddressTextChanged(e) {
        setStreetAddress(e.target.value);
    }
    function handleZipCodeTextChanged(e) {
        setZipCode(e.target.value);
    }
    function handleCityTextChanged(e) {
        setCity(e.target.value);
    }

    async function handleRegister(e) {
        e.preventDefault();

        const url = `${process.env.REACT_APP_BASEURL}/auth/register`;

        const newUser = {
            email,
            password,
            firstName,
            lastName,
            phoneNumber,
            streetAddress,
            zipCode,
            city
        };

        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)
        });

        if (response.status >= 200 && response.status <= 299) {
            alert('You have successfully registered!');
            navigate('/courses');
        }
        else {
            alert("Something went terribly wrong! What did you just do?!");
            console.log(await response.json());
        }
    }

    return (
        <>
            <div className="page-title">Register</div>
            <section className="form-container">
                <form className="form" onSubmit={handleRegister}>
                    <div className="form-control">
                        <label htmlFor="email">Email</label>
                        <input onChange={handleEmailTextChanged} />
                    </div>
                    <div className="form-control">
                        <label htmlFor="password">Password</label>
                        <input type="password" onChange={handlePasswordTextChanged} />
                    </div>
                    <div className="form-control">
                        <label htmlFor="firstName">First name</label>
                        <input onChange={handleFirstNameTextChanged} />
                    </div>
                    <div className="form-control">
                        <label htmlFor="lastName">Last name</label>
                        <input onChange={handleLastNameTextChanged} />
                    </div>
                    <div className="form-control">
                        <label htmlFor="phoneNumber">Phone number</label>
                        <input onChange={handlePhoneNumberTextChanged} />
                    </div>
                    <div className="form-control">
                        <label htmlFor="streetAddress">Street address</label>
                        <input onChange={handleStreetAddressTextChanged} />
                    </div>
                    <div className="form-control">
                        <label htmlFor="zipCode">Zip code</label>
                        <input type="number" onChange={handleZipCodeTextChanged} />
                    </div>
                    <div className="form-control">
                        <label htmlFor="city">City</label>
                        <input onChange={handleCityTextChanged} />
                    </div>
                    <button type="submit" className="submit-btn">Register</button>
                </form>
            </section>
        </>
    );
}

export default Register;