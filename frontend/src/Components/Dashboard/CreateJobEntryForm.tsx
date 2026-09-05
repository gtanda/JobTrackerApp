import * as React from "react";
import {useContext, useState} from "react";
import {AuthContext} from "../Auth/AuthContext.tsx";
import {createJobEntry} from "../../api/jobEntry.ts";
import type {CreateJobEntry} from "../../types/jobEntry.ts";

interface CreateJobEntryFormProps {
    onCreated: () => void;
}

export default function CreateJobEntryForm({onCreated}: CreateJobEntryFormProps) {
    const {accessToken} = useContext(AuthContext);
    const [error, setErrorState] = useState<string>('');
    const [form, setForm] = useState({
        companyName: "",
        jobTitle: "",
    });
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setForm(prev => ({...prev, [e.target.name]: e.target.value}));
    }

    const handleSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault();
        setErrorState('');
        const newJobEntry: CreateJobEntry = {companyName: form.companyName, jobTitle: form.jobTitle}
        try {
            await createJobEntry(newJobEntry, accessToken);
            onCreated();
            setForm({companyName: "", jobTitle: ""});
        } catch (error) {
            setErrorState(error instanceof Error ? error.message : "Could not create job entry");
        }

    }

    return (
        <>
            {error && <p>{error}</p>}
            <form onSubmit={handleSubmit}>
                <input onChange={handleChange} value={form.companyName} name="companyName" type="text"/>
                <input onChange={handleChange} value={form.jobTitle} name="jobTitle" type="text"/>
                <button type={"submit"}>Create Job Entry</button>
            </form>
        </>

    )
}