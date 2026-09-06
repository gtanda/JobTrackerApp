import {useContext} from "react";
import {AuthContext} from "./AuthContext.tsx";
import {getFreshAccessToken} from "../../api/authToken.ts";

export type AuthFetchType = (path: string, options: RequestInit) => Promise<Response>;

export default function useAuthFetch() {
    const {updateAndSetAccessToken, getAccessToken} = useContext(AuthContext);


    const authFetch = async (path: string, options: RequestInit) => {
        const currentToken = getAccessToken();
        const response = await fetch(path, {
            ...options,
            headers: {...options.headers, Authorization: `Bearer ${currentToken}`},
        });

        if (response.status !== 401) return response;
        let newToken: string;

        try {
            newToken = await getFreshAccessToken()
        } catch (error) {
            updateAndSetAccessToken('');
            throw error;
        }
        updateAndSetAccessToken(newToken);
        return fetch(path, {
            ...options,
            headers: {...options.headers, Authorization: `Bearer ${newToken}`},
        });
    }
    return {authFetch};
}

