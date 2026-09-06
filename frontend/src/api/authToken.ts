let tokenPromise: Promise<string> | null = null;
const refreshToken = async () => {

    const response = await fetch("/api/Auth/refresh", {
        method: "POST",
        credentials: "include",
    });
    if (!response.ok) throw new Error(`Request failed: ${response.status}`);
    return response.json();
}

export const getFreshAccessToken = async () => {
    if (!tokenPromise) {
        tokenPromise = refreshToken()
            .then((data) => data.accessToken)
            .finally(() => tokenPromise = null);
    }
    return tokenPromise;
}