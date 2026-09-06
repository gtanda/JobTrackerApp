import * as React from "react";
import {useState} from "react";
import {createJobEntry} from "../../api/jobEntry.ts";
import type {ApplicationStatus, CreateJobEntry, JobEntryFormState, JobSource} from "../../types/jobEntry.ts";
import useAuthFetch from "../Auth/useAuthFetch.ts";

interface CreateJobEntryFormProps {
    onCreated: () => void;
}

export default function CreateJobEntryForm({onCreated}: CreateJobEntryFormProps) {
    const {authFetch} = useAuthFetch();
    const [error, setErrorState] = useState<string>('');
    const [form, setForm] = useState<JobEntryFormState>({
        companyName: "",
        jobTitle: "",
        applicationStatus: "",
        jobSource: "",
        notes: "",
        dateApplied: "",
        postingUrl: "",
        salaryMin: "",
        salaryMax: "",
        recruiterName: "",
        recruiterEmail: "",
        interviewDate: ""
    });

    const applicationStatus: ApplicationStatus[] = [
        "NotApplied",
        "Applied",
        "InterviewRequested",
        "Interviewing",
        "Offer",
        "Rejected",
        "AutoRejected",
        "NoResponse",
    ]

    const applicationStatusLabels: Record<ApplicationStatus, string> = {
        "NotApplied": "Not Applied",
        "Applied": "Applied",
        "InterviewRequested": "Interview Requested",
        "Interviewing": "Interviewing",
        "Offer": "Offer",
        "Rejected": "Rejected",
        "AutoRejected": "Auto Rejected",
        "NoResponse": "No Response",
    }

    const jobSources: JobSource[] = [
        "CompanyWebsite",
        "LinkedIn",
        "Indeed",
        "Referral",
        "Recruiter",
        "JobBoard",
        "Other"
    ]

    const jobSourcesLabels: Record<JobSource, string> = {
        "CompanyWebsite": "Company Web Site",
        "LinkedIn": "LinkedIn",
        "Indeed": "Indeed",
        "Referral": "Referral",
        "Recruiter": "Recruiter",
        "JobBoard": "Job Board",
        "Other": "Other",
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        setForm(prev => ({...prev, [e.target.name]: e.target.value}));
    }

    const handleSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault();
        setErrorState('');
        const newJobEntry: CreateJobEntry = {
            ...form,
            dateApplied: form.dateApplied === "" ? null : form.dateApplied,
            interviewDate: form.interviewDate === "" ? null : form.interviewDate,
            recruiterEmail: form.recruiterEmail === "" ? null : form.recruiterEmail,
            applicationStatus: form.applicationStatus === "" ? undefined : form.applicationStatus,
            jobSource: form.jobSource === "" ? null : form.jobSource,
            salaryMin: form.salaryMin === "" ? null : Number(form.salaryMin),
            salaryMax: form.salaryMax === "" ? null : Number(form.salaryMax)
        }
        try {
            await createJobEntry(newJobEntry, authFetch);
            onCreated();
            setForm({
                companyName: "",
                jobTitle: "",
                applicationStatus: "",
                jobSource: "",
                notes: "",
                dateApplied: "",
                postingUrl: "",
                salaryMin: "",
                salaryMax: "",
                recruiterName: "",
                recruiterEmail: "",
                interviewDate: ""
            });
        } catch (err) {
            setErrorState(err instanceof Error ? err.message : "Could not create job entry");
        }
    }

    return (
        <>
            {error && <p>{error}</p>}
            <form onSubmit={handleSubmit}>
                <div>
                    Company Name
                    <input onChange={handleChange} value={form.companyName} name="companyName" type="text"
                           aria-label={"Company Name"}/>
                </div>
                <div>
                    Job Title
                    <input onChange={handleChange} value={form.jobTitle} name="jobTitle" type="text"/>
                </div>
                <div>
                    <select onChange={handleChange} value={form.applicationStatus} name="applicationStatus">
                        <option value={""} disabled={true}>Select an application status</option>
                        {applicationStatus.map((status) => <option key={status}
                                                                   value={status}>{applicationStatusLabels[status]}</option>)}
                    </select>
                </div>
                <div>
                    <select onChange={handleChange} value={form.jobSource} name={"jobSource"}>
                        <option value={""} disabled>Select a source</option>
                        {jobSources.map((jobSource) => <option key={jobSource}
                                                               value={jobSource}>{jobSourcesLabels[jobSource]}</option>)}
                    </select>
                </div>
                <div>
                    Notes
                    <textarea onChange={handleChange} value={form.notes} name="notes"/>
                </div>
                <div>
                    Date Applied
                    <input onChange={handleChange} value={form.dateApplied} name={"dateApplied"} type={"date"}/>
                </div>
                <div>
                    Posting Url
                    <input onChange={handleChange} value={form.postingUrl} name="postingUrl" type={"url"}/>
                </div>
                <div>
                    SalaryMin
                    <input onChange={handleChange} value={form.salaryMin} name="salaryMin" type="number"/>
                    SalaryMax
                    <input onChange={handleChange} value={form.salaryMax} name="salaryMax" type="number"/>
                </div>
                <div>
                    Recruiter Name
                    <input onChange={handleChange} value={form.recruiterName} name={"recruiterName"} type="text"/>
                    Recruiter Email
                    <input onChange={handleChange} value={form.recruiterEmail} name={"recruiterEmail"} type="email"/>
                </div>
                <div>
                    Interview Date
                    <input onChange={handleChange} value={form.interviewDate} name="interviewDate" type="date"/>
                </div>
                <button type={"submit"}>Create Job Entry</button>
            </form>
        </>

    )
}