import type {JobEntry} from "../../types/jobEntry.ts";

export default function JobEntryCard({jobEntry}: { jobEntry: JobEntry }) {
    return (
        <div>
            <p>{jobEntry.companyName}</p>
            <p>{jobEntry.jobTitle}</p>
            <p>{jobEntry.applicationStatus}</p>
        </div>
    )
}