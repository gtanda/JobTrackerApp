import {useState} from "react";
import * as React from "react";

export default function Login() {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [errorState, setErrorState] = useState('');
    
    const handleSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault()
        const response = await fetch("/api/Auth/login", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify({email, password})
        });
        if (response.ok){
            const responseJson = await response.json()
            console.log(responseJson); 
        } else {
            setErrorState("Login failed.");
        }
    }
    
    return (
    <>
    {errorState && <p>{errorState}</p>}
        <form onSubmit={handleSubmit}>
            <input value={email} onChange={(e) => setEmail(e.target.value)} type="email" />
            <input value={password} onChange={(e) => setPassword(e.target.value)} type="password" />
            <button type={"submit"}>Login</button>
        </form>
    </>
)

}