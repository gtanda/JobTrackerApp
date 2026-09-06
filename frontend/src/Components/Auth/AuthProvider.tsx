import * as React from "react";
import {useCallback, useEffect, useRef, useState} from "react";
import {AuthContext} from "./AuthContext";

export default function AuthProvider({children}: { children: React.ReactNode }) {
    const [accessToken, setAccessToken] = useState('');
    const accessTokenRef = useRef('');

    const updateAndSetAccessToken = useCallback((accessToken: string) => {
        accessTokenRef.current = accessToken;
        setAccessToken(accessTokenRef.current);
    }, []);

    const getAccessToken = useCallback(() => {
        return accessTokenRef.current
    }, []);

    useEffect(() => {
        const refreshToken = async () => {
            const response = await fetch('/api/Auth/refresh', {
                method: "POST",
                credentials: "include",
            });
            if (response.ok) {
                const data = await response.json();
                updateAndSetAccessToken(data.accessToken);
            }
        }
        refreshToken()
    }, [updateAndSetAccessToken]);


    return (
        <AuthContext.Provider value={{accessToken, updateAndSetAccessToken, getAccessToken}}>
            {children}
        </AuthContext.Provider>
    )
}