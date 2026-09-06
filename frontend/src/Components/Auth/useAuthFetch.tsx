import {useContext} from "react";
import {AuthContext} from "./AuthContext.tsx";
import {getFreshAccessToken} from "../../api/authFetch.ts";

export type AuthFetchType = (path: string, options: RequestInit) => Promise<Response>;

export default function useAuthFetch() {
    const {accessToken, setAccessToken} = useContext(AuthContext);

    const authFetch = async (path: string, options: RequestInit) => {
        try {
            const response = await fetch(path, {
                ...options,
                headers: {...options.headers, Authorization: `Bearer ${accessToken}`},
            });

            if (response.status === 401) {
                const newToken = await getFreshAccessToken()
                setAccessToken(newToken)
                return await fetch(path, {
                    ...options,
                    headers: {...options.headers, Authorization: `Bearer ${newToken}`},
                });
            } else {
                return response;
            }
        } catch (error) {
            setAccessToken("");
            throw error;
        }
    };
    return {authFetch};
}

