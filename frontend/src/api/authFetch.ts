export const refreshToken = async () => {
    const response = await fetch("/api/Auth/refresh", {
        method: "POST",
        credentials: "include",
    });
    if (!response.ok) throw new Error(`Request failed: ${response.status}`);
    return response.json();
}