import {useContext} from "react";
import {AuthContext} from "./AuthContext.tsx";
import {refreshToken} from "../../api/authFetch.ts";

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
                const data = await refreshToken()
                setAccessToken(data.accessToken)
                return await fetch(path, {
                    ...options,
                    headers: {...options.headers, Authorization: `Bearer ${data.accessToken}`},
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

