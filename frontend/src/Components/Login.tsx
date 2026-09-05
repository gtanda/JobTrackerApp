import {useContext, useState} from "react";
import * as React from "react";
import {AuthContext} from "./AuthContext.tsx";

export default function Login() {
    const {accessToken, setAccessToken} = useContext(AuthContext)
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [errorState, setErrorState] = useState('');
    
    const handleSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault()
        const response = await fetch("/api/Auth/login", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            credentials: "include",
            body: JSON.stringify({email, password})
        });
        if (response.ok){
            const responseJson = await response.json()
            setAccessToken(responseJson.accessToken);
            console.log(responseJson.accessToken);
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