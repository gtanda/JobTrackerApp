import {useEffect, useState} from "react";
import { AuthContext } from "./AuthContext";
import * as React from "react";

export default function AuthProvider ({children} : {children: React.ReactNode}) {
    const [accessToken, setAccessToken] =useState('');

    useEffect(() => {
        const refreshToken = async () => {
            const response = await fetch('/api/Auth/refresh', {
                method: "POST",
                credentials: "include",
            });
            if (response.ok) {
                const data = await response.json();
                setAccessToken(data.accessToken);
            }
        }
        refreshToken()
    }, []);


    return (
        <AuthContext.Provider value={{accessToken, setAccessToken}}>
            {children}
        </AuthContext.Provider>
    )
}