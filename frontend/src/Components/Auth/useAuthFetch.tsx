import {useContext} from "react";
import {AuthContext} from "./AuthContext.tsx";
import {getFreshAccessToken} from "../../api/authToken.ts";

export type AuthFetchType = (path: string, options: RequestInit) => Promise<Response>;

export default function useAuthFetch() {
    const {updateAndSetAccessToken, getAccessToken} = useContext(AuthContext);


    const authFetch = async (path: string, options: RequestInit) => {
        try {
            const currentToken = getAccessToken();
            const response = await fetch(path, {
                ...options,
                headers: {...options.headers, Authorization: `Bearer ${currentToken}`},
            });

            if (response.status === 401) {
                const newToken = await getFreshAccessToken()

                updateAndSetAccessToken(newToken)
                return await fetch(path, {
                    ...options,
                    headers: {...options.headers, Authorization: `Bearer ${newToken}`},
                });
            } else {
                return response;
            }
        } catch (error) {
            updateAndSetAccessToken("");
            throw error;
        }
    };
    return {authFetch};
}

