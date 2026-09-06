import type {CreateJobEntry} from "../types/jobEntry.ts";
import type {AuthFetchType} from "../Components/Auth/useAuthFetch.tsx";

export const fetchJobEntries = async (authFetch: AuthFetchType) => {
    const response = await authFetch('/api/JobEntries', {method: "GET",});
    if (!response.ok) throw new Error("Could not fetch job entries");
    return response.json();
}

export const createJobEntry = async (createJobEntry: CreateJobEntry, authFetch: AuthFetchType) => {
    const response = await authFetch('/api/JobEntries', {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(createJobEntry)
    });
    if (!response.ok) throw new Error("Could not create job entries");

    return response.json();
}

export const deleteJobEntry = async (jobId: string, authFetch: AuthFetchType) => {
    const response = await authFetch(`/api/JobEntries/${jobId}`, {method: "DELETE",});
    if (!response.ok) throw new Error("Could not delete job entries");
}