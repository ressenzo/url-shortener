import { useState } from "react";
import './Input.css';

interface InputProps {
    setResult: React.Dispatch<React.SetStateAction<string | undefined>>;
}

const Input = ({ setResult }: InputProps) => {
    const [url, setUrl] = useState<string>('');

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.value === "") {
            setResult(undefined);
        }
        setUrl(e.target.value)
    }

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        setResult(url);
    }

    return (
        <div className="input">
            <form onSubmit={e => handleSubmit(e)}>
                <div className="input-group">
                    <input
                        className="form-control"
                        type="text"
                        placeholder="Input your URL here"
                        onChange={handleChange}
                        id="url-value"
                        autoComplete="off"
                    />
                    <button
                        className="btn btn-info"
                        disabled={url === ""}
                        type="submit">
                        Generate
                    </button>
                </div>
            </form>
        </div>
    )
}

export default Input;
