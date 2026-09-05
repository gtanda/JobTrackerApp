export const request = async (path: string, options: RequestInit = {}) => {
    const response = await fetch(path, {
        ...options,
        credentials: "include",
        headers: {"Content-Type": "application/json", ...options.headers},
    });
    
    if (!response.ok) throw new Error(`Request failed: ${response.status}`);
    return response.json();
}