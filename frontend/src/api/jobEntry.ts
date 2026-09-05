import type {CreateJobEntry} from "../types/jobEntry.ts";

export const fetchJobEntries = async (authToken: string) => {
    const response = await fetch('/api/JobEntries', {
        method: "GET",
        headers: {Authorization: `Bearer ${authToken}`},
    });
    if (!response.ok) throw new Error("Could not fetch job entries");
    return response.json();
}

export const createJobEntry = async (createJobEntry: CreateJobEntry, authToken: string) => {
    const response = await fetch('/api/JobEntries', {
        method: "POST",
        headers: {"Content-Type": "application/json", Authorization: `Bearer ${authToken}`},
        body: JSON.stringify(createJobEntry)
    });
    if (!response.ok) throw new Error("Could not create job entries");

    return response.json();
}