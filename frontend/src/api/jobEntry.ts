export const fetchJobEntries = async (authToken : string) => {
    const response = await fetch('/api/JobEntries', {
        method: "GET",
        headers: {Authorization: `Bearer ${authToken}`},
    });
    if (!response.ok) throw new Error("Could not fetch job entries");
    return response.json();
}